using System;

public static class QuestLogic
{
    public static bool CanFastAttack(bool knightIsAwake)
    {
        return !knightIsAwake;
    }

    public static bool CanSpy(
        bool knightIsAwake,
        bool archerIsAwake,
        bool prisonerIsAwake)
    {
        return knightIsAwake || archerIsAwake || prisonerIsAwake;
    }

    public static bool CanSignalPrisoner(
        bool archerIsAwake,
        bool prisonerIsAwake)
    {
        return prisonerIsAwake && !archerIsAwake;
    }

    public static bool CanFreePrisoner(
        bool knightIsAwake,
        bool archerIsAwake,
        bool prisonerIsAwake,
        bool petDogIsPresent)
    {
        if (petDogIsPresent)
        {
            return !knightIsAwake && !archerIsAwake;
        }

        return prisonerIsAwake
            && !knightIsAwake
            && !archerIsAwake;
    }
}

class Program
{
    static void Main()
    {
        bool puedoAtacar = QuestLogic.CanFastAttack(
            knightIsAwake: true
        );

        Console.WriteLine(
            $"Ataque rápido: {puedoAtacar}"
        );

        bool puedoEspiar = QuestLogic.CanSpy(
            knightIsAwake: false,
            archerIsAwake: true,
            prisonerIsAwake: false
        );

        Console.WriteLine(
            $"Puedo espiar: {puedoEspiar}"
        );

        bool puedoSenalar = QuestLogic.CanSignalPrisoner(
            archerIsAwake: false,
            prisonerIsAwake: true
        );

        Console.WriteLine(
            $"Puedo señalar: {puedoSenalar}"
        );

        bool puedoLiberar = QuestLogic.CanFreePrisoner(
            knightIsAwake: false,
            archerIsAwake: true,
            prisonerIsAwake: false,
            petDogIsPresent: false
        );

        Console.WriteLine(
            $"Puedo liberar: {puedoLiberar}"
        );
    }
}