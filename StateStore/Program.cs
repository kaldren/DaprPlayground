using Dapr.Client;

const string DAPR_STORE_NAME = "statestore";
const string KEY = "last-check";

while (true)
{
    using var client = new DaprClientBuilder().Build();
    
    try
    {
        var currentTime = DateTime.UtcNow.ToString("o");
        await client.SaveStateAsync(DAPR_STORE_NAME, KEY, currentTime);
        Console.WriteLine($"Last check: {currentTime}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error saving state: {ex.Message}");
    }

    await Task.Delay(5000); // Wait for 5 seconds before the next check
}