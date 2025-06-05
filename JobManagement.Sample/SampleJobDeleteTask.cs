using JobManagement.Core;

namespace JobManagement.Sample;

public class SampleJobDeleteTask : IJobTask
{
    public string TaskName => "SampleDelete";

    public async Task ExecuteAsync(TaskContext context)
    {
        var jobData = context.Get<SampleJobData>("info");
        if (jobData == null)
        {
            throw new NullReferenceException();
        }

        // 삭제 작업

        await Task.CompletedTask;
    }
}