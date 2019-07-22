namespace RepoSandbox.Model
{
  public class CreateTableIfNotExistsStatement : ISqlStatementSerializable
  {
    private readonly string _tableName;

    private readonly TableColumnCollection _columns;

    public CreateTableIfNotExistsStatement(string tableName, TableColumnCollection columns)
    {
      _tableName = tableName;
      _columns = columns;
    }

    public string Serialize()
    {
      return $"CREATE TABLE IF NOT EXISTS `{_tableName}` {_columns.Serialize()} ENGINE=InnoDB DEFAULT CHARSET=latin1;";
    }
  }
}
