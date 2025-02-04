namespace JobManagement.Core;

public interface ITaskInterceptor
{
    Task OnBeforeExecuteAsync(IJobTask task, TaskContext context);
    Task OnAfterExecuteAsync(Exception? exception, IJobTask task, TaskContext context);
}