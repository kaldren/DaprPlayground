using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using Dapr.Client;

namespace FrontendApp.Pages;

public class WeatherModel : PageModel
{
    private readonly ILogger<WeatherModel> _logger;
    public List<WeatherForecast> WeatherForecasts { get; private set; } = new();

    public WeatherModel(ILogger<WeatherModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGet()
    {

        try
        {
            var httpClient = DaprClient.CreateInvokeHttpClient();
            WeatherForecasts = await httpClient.GetFromJsonAsync<List<WeatherForecast>>("http://weather-api/weatherforecast") ?? new();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching weather forecast");
        }
    }

    public record WeatherForecast(DateOnly Date, int TemperatureC, int TemperatureF, string? Summary);
}

