namespace RepoSandbox.Model
{
  public class TableColumnCollection : ISqlStatementSerializable
  {
    private readonly TableColumn[] _columns;

    public TableColumnCollection(TableColumn[] columns)
    {
      _columns = columns;
    }

    public string Serialize()
    {
      var columns = "";
      for(var i = 0; i < _columns.Length; i++)
      {
        if(i < _columns.Length - 1)
        {
          columns += $"{_columns[i].Serialize()}, ";
          continue;
        }
        columns += _columns[i].Serialize();
      }
      return $"({columns})";
    }
  }
}
