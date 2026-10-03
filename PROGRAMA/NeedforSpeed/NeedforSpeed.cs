using System;

class RemoteControlCar
{
    private int speed;
    private int batteryDrain;
    private int battery = 100;
    private int distance = 0;

    public RemoteControlCar(int speed, int batteryDrain)
    {
        this.speed = speed;
        this.batteryDrain = batteryDrain;
    }

    public static RemoteControlCar Nitro()
    {
        return new RemoteControlCar(50, 4);
    }

    public bool BatteryDrained()
    {
        return battery < batteryDrain;
    }

    public void Drive()
    {
        if (!BatteryDrained())
        {
            distance += speed;
            battery -= batteryDrain;
        }
    }

    public int DistanceDriven()
    {
        return distance;
    }
}

class RaceTrack
{
    private int distance;

    public RaceTrack(int distance)
    {
        this.distance = distance;
    }

    public bool TryFinishTrack(RemoteControlCar car)
    {
        while (!car.BatteryDrained())
        {
            car.Drive();
        }

        return car.DistanceDriven() >= distance;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Remote Control Car ===");

        RemoteControlCar car = new RemoteControlCar(20, 2);

        Console.WriteLine($"Distancia inicial: {car.DistanceDriven()}");
        Console.WriteLine($"¿Batería agotada?: {car.BatteryDrained()}");

        car.Drive();
        car.Drive();
        car.Drive();

        Console.WriteLine($"Distancia después de conducir: {car.DistanceDriven()}");
        Console.WriteLine($"¿Batería agotada?: {car.BatteryDrained()}");

        RaceTrack track = new RaceTrack(100);

        bool canFinish = track.TryFinishTrack(car);

        Console.WriteLine($"¿Puede terminar la pista?: {canFinish}");

        Console.WriteLine("\n=== Nitro Car ===");

        RemoteControlCar nitro = RemoteControlCar.Nitro();

        Console.WriteLine($"Distancia inicial: {nitro.DistanceDriven()}");

        nitro.Drive();

        Console.WriteLine($"Distancia después de conducir: {nitro.DistanceDriven()}");

        Console.WriteLine("\nPresiona Enter para salir...");
        Console.ReadLine();
    }
}
