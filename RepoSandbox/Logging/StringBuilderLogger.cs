using System.Text;

namespace RepoSandbox.Logging
{
  public class StringBuilderLogger : ILogger
  {
    private StringBuilder _buffer;

    public StringBuilderLogger()
    {
      _buffer = new StringBuilder();
    }

    public void Clear()
    {
      _buffer.Clear();
    }

    public string Dump()
    {
      return _buffer.ToString();
    }

    public ILogger Log(string message)
    {
      _buffer.AppendLine(message);
      return this;
    }
  }
}
