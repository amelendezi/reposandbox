namespace RepoSandbox.Model
{
  public class ColumnType : ISqlStatementSerializable
  {
    private readonly string _typeName;

    private readonly int? _size;

    public ColumnType(string typeName, int? size = null)
    {
      _typeName = typeName;
      _size = size;
    }

    public string GetSize() => _size.HasValue ? $"({_size.Value.ToString()})" : string.Empty;

    public string Serialize()
    {
      return $"{_typeName}{GetSize()}";
    }
  }
}
