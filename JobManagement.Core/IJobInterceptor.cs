namespace JobManagement.Core;

public interface IJobInterceptor
{
    Task OnBeforeExecuteAsync(TaskContext context);
    Task OnAfterExecuteAsync(Exception? exception, TaskContext context);
}