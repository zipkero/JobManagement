using JobManagement.Core;

namespace JobManagement.Sample;

public class SampleJob :IJob<SampleJobData>
{
    public SampleJobData JobRequest { get; set; }
    public IList<IJobTask> Tasks { get; set; }
    public IList<IJobInterceptor> JobInterceptors { get; set; }
    public IList<ITaskInterceptor> TaskInterceptors { get; set; }

    public void Build(TaskBuilder taskBuilder, JobInterceptorBuilder jobInterceptorBuilder,
        TaskInterceptorBuilder taskInterceptorBuilder, SampleJobData info)
    {
        JobRequest = info;
        
        JobInterceptors = jobInterceptorBuilder.Build();
        
        Tasks = taskBuilder
            .AddTask("SampleCheck", (context) =>
            {
                var jobData = context.Get<SampleJobData>("info");

                if (jobData == null)
                {
                    throw new NullReferenceException();
                }
                
                // 조회 작업

                jobData.JobId = 1;
                jobData.JobName = "SampleJob";

                context.Set("info", jobData);

                return Task.CompletedTask;
            })
            .AddTask<SampleJobDeleteTask>()
            .Build();
        
        TaskInterceptors = taskInterceptorBuilder.Build();
    }
}