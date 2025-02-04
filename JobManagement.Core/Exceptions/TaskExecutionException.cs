namespace JobManagement.Core.Exceptions;

public class TaskExecutionException : Exception
{
    public TaskExecutionException(string message) : base(message)
    {
    }

    public TaskExecutionException(string message, Exception e) : base(message, e)
    {
    }

    public TaskExecutionException(Exception e) : this(e.Message, e)
    {

    }
}