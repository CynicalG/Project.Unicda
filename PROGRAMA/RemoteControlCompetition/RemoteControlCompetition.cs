using System;
using System.Collections.Generic;

public interface IRemoteControlCar
{
    int DistanceTravelled { get; }
    void Drive();
}

public class ProductionRemoteControlCar : IRemoteControlCar, IComparable<ProductionRemoteControlCar>
{
    public int DistanceTravelled { get; private set; }
    public int NumberOfVictories { get; set; }

    public void Drive()
    {
        DistanceTravelled += 10;
    }

    public int CompareTo(ProductionRemoteControlCar? other)
    {
        if (other == null)
            return 1;

        return NumberOfVictories.CompareTo(other.NumberOfVictories);
    }
}

public class ExperimentalRemoteControlCar : IRemoteControlCar
{
    public int DistanceTravelled { get; private set; }

    public void Drive()
    {
        DistanceTravelled += 20;
    }
}

public static class TestTrack
{
    public static void Race(IRemoteControlCar car)
    {
        car.Drive();
    }

    public static List<ProductionRemoteControlCar> GetRankedCars(
        ProductionRemoteControlCar prc1,
        ProductionRemoteControlCar prc2)
    {
        var cars = new List<ProductionRemoteControlCar>
        {
            prc1,
            prc2
        };

        cars.Sort();

        return cars;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Crear los carros
        ProductionRemoteControlCar productionCar =
            new ProductionRemoteControlCar();

        ExperimentalRemoteControlCar experimentalCar =
            new ExperimentalRemoteControlCar();

        // Hacer una carrera
        TestTrack.Race(productionCar);
        TestTrack.Race(experimentalCar);

        // Mostrar distancia recorrida
        Console.WriteLine(
            $"Production car distance: {productionCar.DistanceTravelled}"
        );

        Console.WriteLine(
            $"Experimental car distance: {experimentalCar.DistanceTravelled}"
        );

        // Asignar victorias
        productionCar.NumberOfVictories = 5;

        ProductionRemoteControlCar secondProductionCar =
            new ProductionRemoteControlCar();

        secondProductionCar.NumberOfVictories = 10;

        // Obtener ranking
        List<ProductionRemoteControlCar> rankedCars =
            TestTrack.GetRankedCars(
                productionCar,
                secondProductionCar
            );

        // Mostrar ranking
        Console.WriteLine("\nRanking:");

        foreach (ProductionRemoteControlCar car in rankedCars)
        {
            Console.WriteLine(
                $"Victories: {car.NumberOfVictories}"
            );
        }
    }
}