namespace RepoSandbox.Logging
{
  public interface ILogger
  {
    ILogger Log(string message);

    string Dump();

    void Clear();
  }
}
