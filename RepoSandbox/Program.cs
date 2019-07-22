using System;
using MySql.Data.MySqlClient;
using RepoSandbox.Common;
using RepoSandbox.Logging;
using RepoSandbox.Model;
using RepoSandbox.Mysql;

namespace RepoSandbox
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var arguments = ConsoleArgumentParser.Parse(args);
      TestNewOrCreateWithStatementModel(arguments);

      // TestNewOrCreate(arguments);
      //TestDbConnection(arguments);
      // Console.ReadKey();
    }

    private static void TestNewOrCreateWithStatementModel(Arguments arguments)
    {
      var logger = new StringBuilderLogger();
      var connectionManager = new MySqlConnectionManager(logger);
      var dbConnectionConfig = GetConnectionConfig(arguments);
      var mySqlConnection = connectionManager.GetConnection(dbConnectionConfig);
      if(mySqlConnection != null)
      {
        CreateTableIfNotExistsStatement statement = new CreateTableIfNotExistsStatement(
          "eror",
          new TableColumnCollection(
            new TableColumn[]
            {
              new TableColumn(
                "Id",
                new ColumnType("VARCHAR", 256),
                true),
              new TableColumn(
                "Description",
                new ColumnType("VARCHAR", 256),
                true),
              new TableColumn(
                "Rating",
                new ColumnType("INT", 11),
                true)
            }));

        MySqlCommand command = new MySqlCommand(statement.Serialize(), mySqlConnection);
        command.ExecuteNonQuery();
        connectionManager.Disconnect();
      }
      else
      {
        Console.WriteLine("Connection failed");
      }
    }

    private static void TestNewOrCreate(Arguments arguments)
    {
      var logger = new StringBuilderLogger();
      var connectionManager = new MySqlConnectionManager(logger);
      var dbConnectionConfig = GetConnectionConfig(arguments);
      var mySqlConnection = connectionManager.GetConnection(dbConnectionConfig);
      if(mySqlConnection != null)
      {
        string statement =
          "CREATE TABLE IF NOT EXISTS `product` ( " +
          "`Id` varchar(256) NOT NULL," +
          "`Description` varchar(256) NOT NULL," +
          "`Rating` int(11) NOT NULL) " +
          "ENGINE=InnoDB DEFAULT CHARSET=latin1;";

        MySqlCommand command = new MySqlCommand(statement, mySqlConnection);
        command.ExecuteNonQuery();
        connectionManager.Disconnect();
      }
      else
      {
        Console.WriteLine("Connection failed");
      }
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
