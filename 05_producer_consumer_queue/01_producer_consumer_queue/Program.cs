
using _01_producer_consumer_queue.Jobs;
using QueueManager;

ProducerConsumerQueue queue = new ProducerConsumerQueue(1);
for (int i = 0; i < 10; ++i)
    queue.EnqueueJob(new SendEmailJob() { Email = $"user_{i}@mail.com" });

for (int i = 0; i < 10; ++i)
{
    Thread.Sleep(300);
    Console.WriteLine($"MAIN: {i}");
}