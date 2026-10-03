using System;

public static class SimpleCalculator
{
    public static string Calculate(int operand1, int operand2, string? operation)
    {
        return operation switch
        {
            "+" => $"{operand1} + {operand2} = {operand1 + operand2}",

            "*" => $"{operand1} * {operand2} = {operand1 * operand2}",

            "/" when operand2 == 0 =>
                "Division by zero is not allowed.",

            "/" => $"{operand1} / {operand2} = {operand1 / operand2}",

            "" => throw new ArgumentException(
                "Operation cannot be empty.",
                nameof(operation)),

            null => throw new ArgumentNullException(
                nameof(operation)),

            _ => throw new ArgumentOutOfRangeException(
                nameof(operation))
        };
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine(SimpleCalculator.Calculate(10, 5, "+"));

        Console.WriteLine(SimpleCalculator.Calculate(10, 5, "*"));

        Console.WriteLine(SimpleCalculator.Calculate(10, 5, "/"));

        Console.WriteLine(SimpleCalculator.Calculate(10, 0, "/"));
    }
}
