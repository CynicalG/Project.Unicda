using System;

class Program
{
    static void Main(string[] args)
    {
        float firstValue = 400.232f;
        float secondValue = 700.565f;

        float largestValue = Math.Max(firstValue, secondValue);

        Console.WriteLine($"The largest number is: {largestValue}");
    }
}
