using Microsoft.Extensions.DependencyInjection;

namespace JobManagement.Core;

public class TaskBuilder
{
    private readonly List<IJobTask> _tasks = new();
    private IServiceProvider _serviceProvider;

    public TaskBuilder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TaskBuilder AddTask<T>() where T : IJobTask
    {
        var task  = ActivatorUtilities.CreateInstance<T>(_serviceProvider);
        _tasks.Add(task);
        return this;
    }

    public TaskBuilder AddTask<T, TParam>(TParam arg) where T : IJobTask where TParam : notnull
    {
        var task  = ActivatorUtilities.CreateInstance<T>(_serviceProvider, arg);
        _tasks.Add(task);
        return this;
    }
    
    public TaskBuilder AddTask(string taskName, Func<TaskContext, Task> func)
    {
        _tasks.Add(new LamdaTask(taskName, func));
        return this;
    }

    public TaskBuilder AddTaskIf<T>(Func<bool> condition) where T : IJobTask
    {
        if (condition())
        {
            var task  = _serviceProvider.GetRequiredService<T>();
            _tasks.Add(task);
        }
        return this;
    }
    
    public List<IJobTask> Build()
    {
        return _tasks;
    }
    
    private class LamdaTask(string taskName, Func<TaskContext, Task> func) : IJobTask
    {
        public string TaskName { get; } = taskName;

        public async Task ExecuteAsync(TaskContext context)
        {
            await func(context);
        }
    }
}