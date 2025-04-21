namespace QueueManager;

public class ProducerConsumerQueue
{
    private Queue<IJob> jobs = new Queue<IJob>();
    private int workersCount;
    private List<Thread> threads = new List<Thread>();
    private EventWaitHandle wh = new AutoResetEvent(false);

    public ProducerConsumerQueue(int workersCount)
    {
        this.workersCount = workersCount;

        Init();
    }

    private void Init()
    {
        for (int i = 0; i < workersCount; i++)
        {
            Thread t = new Thread(Handle)
            {
                Name = $"thread_{i}",
            };

            threads.Add(t);

            t.Start();
        }
    }

    public void EnqueueJob(IJob job)
    {
        jobs.Enqueue(job);

        wh.Set();
    }

    private void Handle()
    {
        while(true)
        {
            IJob? job = null;

            lock(jobs)
            {
                if (jobs.Count > 0)
                    job = jobs.Dequeue();
            }

            if (job is not null)
            {
                job.Execute();
                Console.WriteLine($"{Thread.CurrentThread.Name} HANDLES {job.GetInfo()}");
            }
            else
            {
                wh.WaitOne();
            }
        }
    }






}
