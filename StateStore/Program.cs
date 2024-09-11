using Dapr.Client;

const string DAPR_STORE_NAME = "statestore";
const string KEY = "last-check";

while (true)
{
    Thread.Sleep(3000);
    using var client = new DaprClientBuilder().Build();

    await client.SaveStateAsync(DAPR_STORE_NAME, KEY, DateTimeOffset.UtcNow);

    Console.WriteLine("State saved successfully");

    var getStateResponse = await client.GetStateAsync<string>(DAPR_STORE_NAME, KEY);
    Console.WriteLine($"State retrieved successfully. Retrieved value: {getStateResponse}");
}