namespace JobManagement.Core;

public interface IJob<T>
{
    public T JobRequest { get; set; }
    public IList<IJobTask> Tasks { get; set; }
    public IList<IJobInterceptor> JobInterceptors { get; set; }
    public IList<ITaskInterceptor> TaskInterceptors { get; set; }

    public void Build(TaskBuilder taskBuilder, JobInterceptorBuilder jobInterceptorBuilder,
        TaskInterceptorBuilder taskInterceptorBuilder, T info);
}