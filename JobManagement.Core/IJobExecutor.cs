namespace JobManagement.Core;

public interface IJobExecutor<T>
{
    public Task ExecuteAsync(IJob<T> job, CancellationToken cancellationToken);
}