namespace JobManagement.Core;

public interface IJobManager<T>
{
    public IJob<T> CreateJob<TJob>(T info) where TJob : IJob<T>;
    
    public Task AddJobAsync(T job, CancellationToken cancellationToken);

    public IAsyncEnumerable<T?> GetJobAsync(CancellationToken cancellationToken);
    
    public Task ExecuteJobAsync(IJob<T> job, CancellationToken cancellationToken);
}