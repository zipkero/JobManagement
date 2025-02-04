namespace JobManagement.Core;

public interface ITaskFactory
{
    public T CreateTask<T>() where T : IJobTask;
}