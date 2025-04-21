namespace QueueManager;

public interface IJob
{
    public void Execute();
    public string GetInfo();
}
