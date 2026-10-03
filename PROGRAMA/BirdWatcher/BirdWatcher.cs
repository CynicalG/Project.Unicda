using System;

class BirdCount
{
    private readonly int[] birds;

    public BirdCount(int[] birdsPerDay)
    {
        birds = birdsPerDay ?? throw new ArgumentNullException(nameof(birdsPerDay));
    }

    public static int[] LastWeek()
    {
        return new[] { 0, 2, 5, 3, 7, 8, 4 };
    }

    public int Today()
    {
        return birds[birds.Length - 1];
    }

    public void IncrementTodaysCount()
    {
        birds[birds.Length - 1]++;
    }

    public bool HasDayWithoutBirds()
    {
        foreach (int count in birds)
        {
            if (count == 0)
            {
                return true;
            }
        }

        return false;
    }

    public int CountForFirstDays(int numberOfDays)
    {
        int total = 0;

        for (int i = 0; i < Math.Min(numberOfDays, birds.Length); i++)
        {
            total += birds[i];
        }

        return total;
    }

    public int BusyDays()
    {
        int busyDays = 0;

        foreach (int count in birds)
        {
            if (count >= 5)
            {
                busyDays++;
            }
        }

        return busyDays;
    }
}

class Program
{
    static void Main()
    {
        int[] birdsPerDay = { 2, 5, 0, 3, 7, 4, 6 };

        BirdCount birds = new BirdCount(birdsPerDay);

        // LastWeek
        Console.WriteLine("Last week:");
        int[] lastWeek = BirdCount.LastWeek();

        foreach (int count in lastWeek)
        {
            Console.Write(count + " ");
        }

        Console.WriteLine();

        // Today
        Console.WriteLine($"Today: {birds.Today()}");

        // IncrementTodaysCount
        birds.IncrementTodaysCount();
        Console.WriteLine($"Today after increment: {birds.Today()}");

        // HasDayWithoutBirds
        Console.WriteLine($"Has day without birds: {birds.HasDayWithoutBirds()}");

        // CountForFirstDays
        Console.WriteLine($"Count first 3 days: {birds.CountForFirstDays(3)}");

        // BusyDays
        Console.WriteLine($"Busy days: {birds.BusyDays()}");
    }
}
