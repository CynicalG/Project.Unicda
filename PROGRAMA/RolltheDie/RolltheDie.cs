using System;

public class Player
{
    Random random = new Random();

    public int RollDie() => random.Next(1, 19);

    public double GenerateSpellStrength() => random.NextDouble() * 100;
}

class Program
{
    static void Main(string[] args)
    {
        Player player = new Player();

        Console.WriteLine("=== Player ===");

        Console.WriteLine($"Resultado del dado: {player.RollDie()}");
        Console.WriteLine($"Fuerza del hechizo: {player.GenerateSpellStrength():F2}");

        Console.WriteLine("\nPresiona Enter para salir...");
        Console.ReadLine();
    }
}
