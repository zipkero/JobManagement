using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;

namespace JobManagement.Core;

public class JobManager<T> : IJobManager<T> where T : class
{
    private readonly Channel<T> _jobChannel = Channel.CreateUnbounded<T>();
    private readonly IServiceProvider _serviceProvider;
    private readonly IJobFactory<T> _jobFactory;

    public JobManager(IServiceProvider serviceProvider, IJobFactory<T> jobFactory)
    {
        _serviceProvider = serviceProvider;
        _jobFactory = jobFactory;
    }

    public IJob<T> CreateJob<TJob>(T info) where TJob : IJob<T>
    {
        return _jobFactory.Create<TJob>(info);
    }

    public async Task AddJobAsync(T job, CancellationToken cancellationToken)
    {
        await _jobChannel.Writer.WriteAsync(job, cancellationToken);
    }

    public IAsyncEnumerable<T?> GetJobAsync(CancellationToken cancellationToken)
    {
        return _jobChannel.Reader.ReadAllAsync(cancellationToken);
    }

    public async Task ExecuteJobAsync(IJob<T> job, CancellationToken cancellationToken)
    {
        var jobExecutor = _serviceProvider.GetRequiredService<IJobExecutor<T>>();
        await jobExecutor.ExecuteAsync(job, cancellationToken);
    }
}