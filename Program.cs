/************************************************************************************
*   File:           Program.cs
*   Description:    The entry point of the application, it uses top-level
*                   statements, sets up the application host, and configures
*                   services and middleware.
************************************************************************************/


/************************ INITIALIZATION *******************************************/
//  initializes the application builder
var builder = WebApplication.CreateBuilder(args);


/************************ SERVICES *************************************************/
// adds OpenAPI (Swagger) services to the dependency injection container
builder.Services.AddOpenApi();


/************************ MIDDLEWARE ***********************************************/
// builds the application pipeline using the configured builder
var app = builder.Build();

// configures the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // maps the OpenAPI(Swagger) UI to a default endpoint
    app.MapOpenApi();
}

// redirects all HTTP requests to HTTPS for security
app.UseHttpsRedirection();


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

// starts the application and listens for incoming HTTP requests
await app.RunAsync();


// defines a record for representing weather forecasts
internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    // a derived property that calculates the temperature in Fahrenheit
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
