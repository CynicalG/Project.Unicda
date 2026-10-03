using System;

class RemoteControlCar
{
    private int distance = 0;
    private int battery = 100;

    public static RemoteControlCar Buy()
    {
        return new RemoteControlCar();
    }

    public void Drive()
    {
        if (battery >= 2)
        {
            distance += 20;
            battery -= 2;
        }
    }

    public string DistanceDisplay()
    {
        return $"Distance: {distance} meters";
    }

    public string BatteryDisplay()
    {
        return $"Battery: {battery}%";
    }
}

class Program
{
    static void Main(string[] args)
    {
        RemoteControlCar car = RemoteControlCar.Buy();

        Console.WriteLine(car.DistanceDisplay());
        Console.WriteLine(car.BatteryDisplay());

        car.Drive();
        car.Drive();
        car.Drive();

        Console.WriteLine(car.DistanceDisplay());
        Console.WriteLine(car.BatteryDisplay());

        Console.WriteLine("\nPresiona Enter para salir...");
        Console.ReadLine();
    }
}
