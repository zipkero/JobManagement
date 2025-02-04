namespace JobManagement.Core;

public interface IJobFactory<TInfo> where TInfo : class
{
    public TJob Create<TJob>(TInfo info) where TJob : IJob<TInfo>;
}