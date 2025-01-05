/************************************************************************************
*   File:           Program.cs
*   Description:    The entry point of the application, it uses top-level
*                   statements, sets up the application host, and configures
*                   services and middleware.
************************************************************************************/


/************************ IMPORTS **************************************************/
using Microsoft.EntityFrameworkCore;
using moneytale_server;
using moneytale_server.Repositories;


/************************ INITIALIZATION *******************************************/
// initializes the application builder
var builder = WebApplication.CreateBuilder(args);

// gets and validates the necessary environment variables
string allowedOrigin = GetRequiredEnvironmentVariable("ALLOWED_ORIGIN");
string connectionServer = GetRequiredEnvironmentVariable("DB_SERVER");
string connectionDatabase = GetRequiredEnvironmentVariable("DB_NAME");
string connectionUserId = GetRequiredEnvironmentVariable("DB_USER");
string connectionPassword = GetRequiredEnvironmentVariable("DB_PASSWORD");


/************************ SERVICES *************************************************/
// registers the CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("WebClientPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigin)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// adds OpenAPI (Swagger) services to the dependency injection container
builder.Services.AddOpenApi();

// adds Entity Framework Core with PostgreSQL
builder.Services.AddDbContext<ServerDbContext>(options =>
    options.UseNpgsql($"Host={connectionServer};Database={connectionDatabase};Username={connectionUserId};Password={connectionPassword}"));

// adds database operation repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


/************************ MIDDLEWARE ***********************************************/
// builds the application pipeline using the configured builder
var app = builder.Build();

// applies CORS globally
app.UseCors("WebClientPolicy");

// sets up HTTP request routing
app.UseRouting();

// redirects all HTTP requests to HTTPS for security
app.UseHttpsRedirection();

// configures the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // maps the OpenAPI(Swagger) UI to a default endpoint
    app.MapOpenApi();
}


/************************ REQUESTS *************************************************/
// maps the GET HTTP endpoint for the user's dashboard to the specified logic
app.MapGet("/dashboard", async (string email, IUserRepository userRepository) =>
{
    try
    {
        // checks if the email is valid
        if (string.IsNullOrWhiteSpace(email))
        {
            return Results.BadRequest("Invalid email address.");
        }
        else
        {
            try
            {
                // checks if it adheres to the basic format of an email address
                var address = new System.Net.Mail.MailAddress(email);

                if (address.Address != email) {
                    return Results.BadRequest("Invalid email address.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid email");
            }
        }

        // finds the user by email
        var user = await userRepository.GetUserByEmailAsync(email);

        // if found, returns the user's dashboard information; else, 404
        return user == null ? Results.NotFound("User not found") : Results.Ok(user.Username);
    }
    catch (Exception ex)
    {
        // handles the error
        Console.WriteLine($"Error: {ex.Message}");
        return Results.Problem("An unexpected error occurred while processing your request.");
    }
})
.WithName("GetDashboard");


/************************ MAIN ****************************************************/
try
{
    // asynchronously creates tables if needed
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ServerDbContext>();
    await dbContext.Database.EnsureCreatedAsync();

    // starts the application and listens for incoming HTTP requests
    await app.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Application failed to start: {ex.Message}");
    throw;
}


/************************ METHODS **************************************************/
/// <summary>
/// Retrieves an environment variable or throws an exception if it is missing or empty.
/// </summary>
/// <param name="key">The environment variable key.</param>
/// <returns>The value of the environment variable.</returns>
/// <exception cref="InvalidOperationException">Thrown if the variable is not set or empty.</exception>
static string GetRequiredEnvironmentVariable(string key)
{
    // loads the environment variable
    string? value = Environment.GetEnvironmentVariable(key);

    // checks if environment variable is valid
    if (string.IsNullOrWhiteSpace(value))
    {
        // handles invalid environment variable
        throw new InvalidOperationException($"{key} is not set or is empty.");
    }

    return value;
}
