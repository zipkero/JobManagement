namespace JobManagement.Core;

public class TaskContext(CancellationToken cancellationToken)
{
    private Dictionary<string, object> _data = new();
    
    private CancellationToken _cancellationToken = cancellationToken;

    public T? Get<T>(string key)
    {
        return _data.TryGetValue(key, out var value) ? (T) value : default;
    }
    
    public void Set<T>(string key, T value)
    {
        if (value == null) return;
        _data[key] = value;
    }
    
    public void Clear(string key) => _data.Remove(key);
}  