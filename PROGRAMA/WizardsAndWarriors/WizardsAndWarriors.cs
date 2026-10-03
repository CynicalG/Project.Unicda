using System;

class Warrior
{
    public int Damage => 25;

    public override string ToString()
    {
        return "Warrior";
    }
}

class Wizard
{
    public int Damage => 15;

    public override string ToString()
    {
        return "Wizard";
    }
}

class Program
{
    static void Main()
    {
        var warrior = new Warrior();
        var wizard = new Wizard();

        Console.WriteLine("Warrior: " + warrior.ToString() + ", damage: " + warrior.Damage);
        Console.WriteLine("Wizard: " + wizard.ToString() + ", damage: " + wizard.Damage);
    }
}
