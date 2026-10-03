using System;

public static class LogExtensions
{
    public static string Message(this string log)
    {
        int separatorIndex = log.IndexOf(":");

        if (separatorIndex == -1)
            return log;

        return log.Substring(separatorIndex + 1).Trim();
    }

    public static string LogLevel(this string log)
    {
        int startIndex = log.IndexOf("[") + 1;
        int endIndex = log.IndexOf("]");

        if (startIndex == 0 || endIndex == -1)
            return "";

        return log.Substring(startIndex, endIndex - startIndex);
    }

    public static string Reformat(this string log)
    {
        return $"{log.LogLevel()}: {log.Message()}";
    }
}

class Program
{
    static void Main()
    {
        string log = "[INFO]: Hello world";

        Console.WriteLine("Message: " + log.Message());
        Console.WriteLine("Log level: " + log.LogLevel());
        Console.WriteLine("Reformatted: " + log.Reformat());
    }
}
