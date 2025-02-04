using Microsoft.Extensions.DependencyInjection;

namespace JobManagement.Core;

public class TaskInterceptorBuilder
{
    private readonly List<ITaskInterceptor> _interceptors = new();
    private IServiceProvider _serviceProvider;

    public TaskInterceptorBuilder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TaskInterceptorBuilder AddInterceptor<T>() where T : ITaskInterceptor
    {
        var interceptor = _serviceProvider.GetRequiredService<T>();

        _interceptors.Add(interceptor);
        return this;
    }

    public TaskInterceptorBuilder AddInterceptorIf<T>(Func<bool> condition) where T : ITaskInterceptor
    {
        var interceptor = _serviceProvider.GetRequiredService<T>();

        if (condition())
        {
            _interceptors.Add(interceptor);
        }

        return this;
    }

    public List<ITaskInterceptor> Build()
    {
        return _interceptors;
    }
}