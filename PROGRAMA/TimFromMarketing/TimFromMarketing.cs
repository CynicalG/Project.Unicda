using System;

static class Badge
{
    public static string Print(int? id, string name, string? department)
    {
        string formattedDepart = (department ?? "OWNER").ToUpper();

        string idPrefix = id != null ? $"[{id}] - " : "";

        return $"{idPrefix}{name} - {formattedDepart}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(Badge.Print(123, "John", "Marketing"));
        Console.WriteLine(Badge.Print(456, "Maria", "Sales"));
        Console.WriteLine(Badge.Print(null, "Carlos", null));

        Console.WriteLine("\nPresiona Enter para salir...");
        Console.ReadLine();
    }
}

