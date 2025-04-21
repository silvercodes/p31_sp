using QueueManager;

namespace _01_producer_consumer_queue.Jobs;

internal class SendEmailJob : IJob
{
    public Random Random {  get; set; }
    public required string Email { get; set; }
    public SendEmailJob()
    {
        Random = new Random();
    }

    public void Execute()
    {
        Thread.Sleep(Random.Next(50, 200));
        Console.WriteLine($"Email to {Email} was sended...");
    }

    public string GetInfo()
    {
        return $"Email = {Email}";
    }
}
