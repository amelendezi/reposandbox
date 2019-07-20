using System;

namespace RepoSandbox
{
  public static class ConsoleArgumentParser
  {
    public static Arguments Parse(string[] args)
    {
      var arguments = new Arguments();
      foreach(var argument in args)
      {
        var argumentBreakpoint = argument.IndexOf(':');
        var id = argument.Substring(0, argumentBreakpoint);
        var value = argument.Substring(argumentBreakpoint + 1);

        switch(id)
        {
          case "db":
            arguments.DbConnectionConfigUrl = value;
            break;
          default:
            throw new Exception("Invalid argument");
        }
      }
      return arguments;
    }
  }
}
