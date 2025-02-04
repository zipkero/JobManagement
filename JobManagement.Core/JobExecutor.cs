using JobManagement.Core.Exceptions;

namespace JobManagement.Core;

public class JobExecutor<T> : IJobExecutor<T>
{
    public async Task ExecuteAsync(IJob<T> job, CancellationToken cancellationToken)
    {
        var taskContext = new TaskContext(cancellationToken);

        taskContext.Set<T>(typeof(T).Name, job.JobRequest);
        
        var lastException = default(Exception?);

        try
        {
            foreach (var jobInterceptor in job.JobInterceptors)
            {
                await jobInterceptor.OnBeforeExecuteAsync(taskContext);
            }

            await ExecuteJobTasksAsync(job, taskContext);
        }
        catch (Exception e)
        {
            lastException = e;
                
            throw;
        }
        finally
        {
            foreach (var jobInterceptor in job.JobInterceptors.Reverse())
            {
                await jobInterceptor.OnAfterExecuteAsync(lastException, taskContext);
            }
        }
    }

    private async Task ExecuteJobTasksAsync(IJob<T> job, TaskContext taskContext)
    {
        var lastException = default(Exception?);
        
        // 태스크 실행
        foreach (var task in job.Tasks)
        {
            try
            {
                await InvokeBeforeExecuteInterceptorsAsync(job, task, taskContext);

                try
                {
                    await task.ExecuteAsync(taskContext);
                }
                catch (Exception e)
                {
                    throw new TaskExecutionException(e);
                }
            }
            catch (Exception exception)
            {
                lastException = exception;
                
                throw;
            }
            finally
            {   
                await InvokeAfterExecuteInterceptorsAsync(lastException, job, task, taskContext);
            }
        }
    }

    private async Task InvokeBeforeExecuteInterceptorsAsync(IJob<T> job, IJobTask task, TaskContext taskContext)
    {
        try
        {
            foreach (var interceptor in job.TaskInterceptors)
            {
                await interceptor.OnBeforeExecuteAsync(task, taskContext);
            }
        }
        catch (Exception exception)
        {
            // 
        }
    }

    private async Task InvokeAfterExecuteInterceptorsAsync(Exception? exception, IJob<T> job, IJobTask task, TaskContext taskContext)
    {
        try
        {
            foreach (var interceptor in job.TaskInterceptors.Reverse())
            {
                await interceptor.OnAfterExecuteAsync(exception, task, taskContext);
            }
        }
        catch (Exception)
        {
            // 
        }
    }
}