using System;
using System.IO;
using RepoSandbox.Mysql;

namespace RepoSandbox.Common
{
  public static class ConnectionStringParser
  {
    public static MySqlConnectionConfig Load(string path)
    {
      MySqlConnectionConfig config = null;
      try
      {
        StreamReader reader = new StreamReader(path);
        string connectionStringLine = reader.ReadLine();

        if(connectionStringLine != null)
        {
          config = Parse(connectionStringLine);
        }
        reader.Close();
      }
      catch
      {
        throw new Exception("Loading ConnectionConfig Fail");
      }
      return config;
    }

    public static MySqlConnectionConfig Parse(string connectionString)
    {
      var config = new MySqlConnectionConfig();
      string[] pairValues = connectionString.Split(';');

      foreach (var pairValue in pairValues)
      {
        string[] item = pairValue.Split('=');
        switch (item[0])
        {
          case "Server":
            config.Server = item[1];
            break;
          case "Database":
            config.Database = item[1];
            break;
          case "Uid":
            config.User = item[1];
            break;
          case "Pwd":
            config.Password = item[1];
            break;
        }
      }
      return config;
    }

    public static string SerializeToConnectionString(MySqlConnectionConfig config)
    {
      return $"Uid={config.User};Pwd={config.Password};Server={config.Server};Database={config.Database}";
    }
  }
}
