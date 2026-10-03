using System;

public class Foul
{
    public string Description { get; set; }

    public string GetDescription()
    {
        return Description;
    }
}

public class Injury
{
    public string Description { get; set; }

    public string GetDescription()
    {
        return Description;
    }
}

public class Incident
{
    public string Description { get; set; }

    public string GetDescription()
    {
        return Description;
    }
}

public class Manager
{
    public string Name { get; set; }
    public string Club { get; set; }
}

public static class PlayAnalyzer
{
    public static string AnalyzeOnField(int shirtNum)
    {
        return shirtNum switch
        {
            1 => "goalie",
            2 => "left back",
            3 or 4 => "center back",
            5 => "right back",
            6 or 7 or 8 => "midfielder",
            9 => "left wing",
            10 => "striker",
            11 => "right wing",
            _ => "UNKNOWN"
        };
    }

    public static string AnalyzeOffField(object report)
    {
        return report switch
        {
            int supporters =>
                $"There are {supporters} supporters at the match.",

            string announcement =>
                announcement,

            Foul foul =>
                foul.GetDescription(),

            Injury injury =>
                $"Oh no! {injury.GetDescription()} Medics are on the field.",

            Incident incident =>
                incident.GetDescription(),

            Manager manager when !string.IsNullOrEmpty(manager.Club) =>
                $"{manager.Name} ({manager.Club})",

            Manager manager =>
                manager.Name,

            _ => ""
        };
    }
}

class Program
{
    static void Main()
    {
        // Jugadores en el campo
        Console.WriteLine(PlayAnalyzer.AnalyzeOnField(1));
        Console.WriteLine(PlayAnalyzer.AnalyzeOnField(3));
        Console.WriteLine(PlayAnalyzer.AnalyzeOnField(7));
        Console.WriteLine(PlayAnalyzer.AnalyzeOnField(10));
        Console.WriteLine(PlayAnalyzer.AnalyzeOnField(15));

        Console.WriteLine();

        // Número de espectadores
        Console.WriteLine(
            PlayAnalyzer.AnalyzeOffField(50000)
        );

        // Anuncio
        Console.WriteLine(
            PlayAnalyzer.AnalyzeOffField("The match has started!")
        );

        // Falta
        Foul foul = new Foul
        {
            Description = "The player committed a foul."
        };

        Console.WriteLine(
            PlayAnalyzer.AnalyzeOffField(foul)
        );

        // Lesión
        Injury injury = new Injury
        {
            Description = "The player has injured his leg."
        };

        Console.WriteLine(
            PlayAnalyzer.AnalyzeOffField(injury)
        );

        // Incidente
        Incident incident = new Incident
        {
            Description = "A problem occurred on the field."
        };

        Console.WriteLine(
            PlayAnalyzer.AnalyzeOffField(incident)
        );

        // Manager con club
        Manager manager = new Manager
        {
            Name = "John Smith",
            Club = "FC Barcelona"
        };

        Console.WriteLine(
            PlayAnalyzer.AnalyzeOffField(manager)
        );

        // Manager sin club
        Manager managerWithoutClub = new Manager
        {
            Name = "David Brown",
            Club = ""
        };

        Console.WriteLine(
            PlayAnalyzer.AnalyzeOffField(managerWithoutClub)
        );
    }
}
