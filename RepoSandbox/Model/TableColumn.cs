namespace RepoSandbox.Model
{
  public class TableColumn : ISqlStatementSerializable
  {
    private readonly string _name;

    private readonly ColumnType _type;

    private readonly bool _notNull;

    private string GetNotNull() => _notNull ? "NOT NULL" : string.Empty;

    public TableColumn(string name, ColumnType type, bool notNull)
    {
      _name = name;
      _type = type;
      _notNull = notNull;
    }

    public string Serialize()
    {
      return $"`{_name}` {_type.Serialize()} {GetNotNull()}";
    }
  }
}
