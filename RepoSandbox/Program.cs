using System;
using RepoSandbox.Common;
using RepoSandbox.Logging;
using RepoSandbox.Mysql;

namespace RepoSandbox
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var arguments = ConsoleArgumentParser.Parse(args);

      TestDbConnection(arguments);
    }

    private static void TestDbConnection(Arguments arguments)
    {
      Console.WriteLine("Preparing logger and connection manager.");
      var logger = new StringBuilderLogger();
      var connectionManager = new MySqlConnectionManager(logger);

      Console.WriteLine("Setting up the database configuration.");
      var dbConnectionConfig = GetConnectionConfig(arguments);

      Console.WriteLine("Connecting to the database.");
      var mySqlConnection = connectionManager.GetConnection(dbConnectionConfig);
      if (mySqlConnection != null)
      {
        Console.WriteLine("Connection was successful.");
        Console.WriteLine("Disconnecting from the database.");
        connectionManager.Disconnect();
        Console.WriteLine("Disconnected from database.");
      }
      else
      {
        Console.WriteLine("Connection failed");
      }

      Console.WriteLine("Log Dump: ");
      Console.WriteLine(logger.Dump());
      Console.ReadLine();
    }

    private static MySqlConnectionConfig GetConnectionConfig(Arguments arguments)
    {
      return ConnectionStringParser.Load(arguments.DbConnectionConfigUrl);
    }
  }
}
