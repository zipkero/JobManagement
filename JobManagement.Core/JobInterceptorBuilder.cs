using Microsoft.Extensions.DependencyInjection;

namespace JobManagement.Core;

public class JobInterceptorBuilder(IServiceProvider serviceProvider)
{
    private readonly List<IJobInterceptor> _interceptors = new();

    public JobInterceptorBuilder AddInterceptor<T>() where T : IJobInterceptor
    {
        var interceptor = serviceProvider.GetRequiredService<T>();

        _interceptors.Add(interceptor);
        return this;
    }

    public JobInterceptorBuilder AddInterceptorIf<T>(Func<bool> condition) where T : IJobInterceptor
    {
        var interceptor = serviceProvider.GetRequiredService<T>();

        if (condition())
        {
            _interceptors.Add(interceptor);
        }

        return this;
    }

    public List<IJobInterceptor> Build()
    {
        return _interceptors;
    }
}