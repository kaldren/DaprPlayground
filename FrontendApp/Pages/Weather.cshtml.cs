using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace FrontendApp.Pages;

public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;
    public List<WeatherForecast> WeatherForecasts { get; private set; } = new();

    public PrivacyModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGet()
    {

        try
        {
            using var httpClient = new HttpClient();
            WeatherForecasts = await httpClient.GetFromJsonAsync<List<WeatherForecast>>("http://localhost:5250/weatherforecast") ?? new();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching weather forecast");
        }
    }

    public record WeatherForecast(DateOnly Date, int TemperatureC, int TemperatureF, string? Summary);
}

