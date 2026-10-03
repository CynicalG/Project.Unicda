using System;

class WeighingMachine
{
    private readonly int _precision;
    private double _weight;
    private double _tareAdjustment;

    public WeighingMachine(int precision)
    {
        if (precision < 0)
            throw new ArgumentOutOfRangeException(nameof(precision));

        _precision = precision;
        _tareAdjustment = 0;
    }

    public int Precision => _precision;

    public double Weight
    {
        get => _weight;
        set => _weight = Math.Round(value, _precision);
    }

    public double TareAdjustment
    {
        get => _tareAdjustment;
        set => _tareAdjustment = value;
    }

    public double DisplayWeight => Math.Round(_weight - _tareAdjustment, _precision);
}

class Program
{
    static void Main(string[] args)
    {
        // Crear una máquina de pesaje con 2 decimales de precisión
        WeighingMachine machine = new WeighingMachine(2);

        // Establecer el peso
        machine.Weight = 75.5;

        // Mostrar la precisión
        Console.WriteLine($"Precisión: {machine.Precision} decimales");

        // Mostrar el peso original
        Console.WriteLine($"Peso: {machine.Weight} kg");

        // Mostrar la tara
        Console.WriteLine($"Tara: {machine.TareAdjustment} kg");

        // Mostrar el peso después de descontar la tara
        Console.WriteLine($"Peso mostrado: {machine.DisplayWeight}");
    }
}
