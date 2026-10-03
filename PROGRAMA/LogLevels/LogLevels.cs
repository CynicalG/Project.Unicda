using System;
class Program
{
    public static string Message(string logLine)
    {
        int start = logLine.IndexOf("]") + 2;
        return logLine.Substring(start);
    }

    public static string LogLevel(string logLine)
    {
        int start = logLine.IndexOf("[") + 1;
        int end = logLine.IndexOf("]");

        return logLine.Substring(start, end - start).ToLower();
    }

    public static string Reformat(string logLine)
    {
        return $"{Message(logLine)} ({LogLevel(logLine)})";
    }
}

class LogLine
{
    static void Main()
    {
        string log = "[WARNING]: Disk almost full";

        Console.WriteLine(Program.Message(log));
        Console.WriteLine(Program.LogLevel(log));
        Console.WriteLine(Program.Reformat(log));
    }
}
