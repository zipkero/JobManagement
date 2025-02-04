using Microsoft.Extensions.DependencyInjection;

namespace JobManagement.Core;

public class JobFactory<TInfo>(IServiceProvider serviceProvider) : IJobFactory<TInfo>
    where TInfo : class
{
    public TJob Create<TJob>(TInfo info) where TJob : IJob<TInfo>
    {
        var taskBuilder = serviceProvider.GetRequiredService<TaskBuilder>();
        var jobInterceptorBuilder = serviceProvider.GetRequiredService<JobInterceptorBuilder>();
        var taskInterceptorBuilder = serviceProvider.GetRequiredService<TaskInterceptorBuilder>();
        
        var job = ActivatorUtilities.CreateInstance<TJob>(serviceProvider);
        job.Build(taskBuilder, jobInterceptorBuilder, taskInterceptorBuilder, info);
        return job;
    }
}