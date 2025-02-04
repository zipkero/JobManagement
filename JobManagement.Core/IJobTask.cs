namespace JobManagement.Core;

public interface IJobTask
{
    /// <summary>
    /// 태스트 이름
    /// </summary>
    public string TaskName { get; }
    /// <summary>
    /// 태스크 실행
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task ExecuteAsync(TaskContext context);
}