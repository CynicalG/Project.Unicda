using System;

class Lasagna
{
    public int ExpectedMinutesInOven()
    {
        return 40;
    }

    public int RemainingMinutesInOven(int minutesInOven)
    {
        return ExpectedMinutesInOven() - minutesInOven;
    }

    public int PreparationTimeInMinutes(int layers)
    {
        return layers * 2;
    }

    public int ElapsedTimeInMinutes(int layers, int minutesInOven)
    {
        return PreparationTimeInMinutes(layers) + minutesInOven;
    }
}

class Program
{
    static void Main()
    {
        Lasagna lasagna = new Lasagna();

        Console.WriteLine(lasagna.ExpectedMinutesInOven());

        Console.WriteLine(
            lasagna.RemainingMinutesInOven(30));

        Console.WriteLine(
            lasagna.PreparationTimeInMinutes(3));

        Console.WriteLine(
            lasagna.ElapsedTimeInMinutes(3, 20));
    }
}

