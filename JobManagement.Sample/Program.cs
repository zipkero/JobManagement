using JobManagement.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JobManagement.Sample;

class Program
{
    static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        
        // JobManagement DI
        builder.Services.AddSingleton<IJobFactory<SampleJobData>, JobFactory<SampleJobData>>();
        builder.Services.AddScoped<IJobExecutor<SampleJobData>, JobExecutor<SampleJobData>>();
        builder.Services.AddSingleton<IJobManager<SampleJobData>, JobManager<SampleJobData>>();
        builder.Services.AddScoped<TaskBuilder>();
        builder.Services.AddScoped<JobInterceptorBuilder>();
        builder.Services.AddScoped<TaskInterceptorBuilder>();
        
        builder.Build().Run();
    }
}