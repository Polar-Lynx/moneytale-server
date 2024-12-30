/************************************************************************************
*   File:           Program.cs
*   Description:    The entry point of the application, it uses top-level
*                   statements, sets up the application host, and configures
*                   services and middleware.
************************************************************************************/


/************************ IMPORTS **************************************************/
using Microsoft.EntityFrameworkCore;
using moneytale_server;


/************************ INITIALIZATION *******************************************/
// initializes the application builder
var builder = WebApplication.CreateBuilder(args);

// gets the necessary environment variables
string allowedOrigin = Environment.GetEnvironmentVariable("ALLOWED_ORIGIN");
string connectionServer = Environment.GetEnvironmentVariable("DB_SERVER");
string connectionDatabase = Environment.GetEnvironmentVariable("DB_NAME");
string connectionUserId = Environment.GetEnvironmentVariable("DB_USER");
string connectionPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

// ensures variables are not empty
if (string.IsNullOrWhiteSpace(allowedOrigin) ||
    string.IsNullOrWhiteSpace(connectionServer) ||
    string.IsNullOrWhiteSpace(connectionDatabase) ||
    string.IsNullOrWhiteSpace(connectionUserId) ||
    string.IsNullOrWhiteSpace(connectionPassword))
{
    throw new InvalidOperationException("Invalid Environment Variable(s).");
}


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
// sample data provided to simulate a weather forecast
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// maps the GET HTTP endpoint for the weather forecast to the specified logic
app.MapGet("/weatherforecast", () =>
{
    // generates an array of WeatherForecast objects
    var forecast = Enumerable.Range(1, 5).Select(index =>
        // generates future dates, random temperatures in degrees Celcius, and random summaries
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    return forecast;
})
.WithName("GetWeatherForecast");


/************************ MAIN ****************************************************/
// starts the application and listens for incoming HTTP requests
try
{
    await app.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Application failed to start: {ex.Message}");
    throw;
}


// defines a record for representing weather forecasts
internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    // a derived property that calculates the temperature in Fahrenheit
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
