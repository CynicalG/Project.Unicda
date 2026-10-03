using System;

public static class PhoneNumber
{
    public static (bool IsNewYork, bool IsFake, string LocalNumber) Analyze(string phoneNumber)
    {
        string[] partes = phoneNumber.Split('-');

        return (
            partes[0] == "212",
            partes[1] == "555",
            partes[2]
        );
    }

    public static bool IsFake(
        (bool IsNewYork, bool IsFake, string LocalNumber) phoneNumberInfo)
    {
        return phoneNumberInfo.IsFake;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string phoneNumber = "212-555-1234";

        var phoneInfo = PhoneNumber.Analyze(phoneNumber);

        Console.WriteLine("=== Phone Number ===");
        Console.WriteLine($"Número: {phoneNumber}");
        Console.WriteLine($"¿Es de New York?: {phoneInfo.IsNewYork}");
        Console.WriteLine($"¿Es falso?: {PhoneNumber.IsFake(phoneInfo)}");
        Console.WriteLine($"Número local: {phoneInfo.LocalNumber}");

        Console.WriteLine("\nPresiona Enter para salir...");
        Console.ReadLine();
    }
}
