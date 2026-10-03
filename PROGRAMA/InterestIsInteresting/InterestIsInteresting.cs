using System;

class SavingsAccount
{
    public static decimal InterestRate(decimal balance)
    {
        if (balance < 1000m)
        {
            return 0.5m;
        }
        else if (balance < 5000m)
        {
            return 1.621m;
        }
        else
        {
            return 2.475m;
        }
    }

    public static decimal Interest(decimal balance)
    {
        return balance * InterestRate(balance) / 100m;
    }

    public static decimal AnnualBalanceUpdate(decimal balance)
    {
        return balance + Interest(balance);
    }

    public static int YearsBeforeDesiredBalance(
        decimal balance,
        decimal targetBalance)
    {
        int years = 0;

        while (balance < targetBalance)
        {
            balance = AnnualBalanceUpdate(balance);
            years++;
        }

        return years;
    }
}

class Program
{
    static void Main()
    {
        decimal balance = 2000m;

        Console.WriteLine("Balance: " + balance);

        Console.WriteLine("Tasa de interés: " +
                          SavingsAccount.InterestRate(balance) + "%");

        Console.WriteLine("Interés anual: " +
                          SavingsAccount.Interest(balance));

        Console.WriteLine("Balance después de un año: " +
                          SavingsAccount.AnnualBalanceUpdate(balance));

        decimal targetBalance = 3000m;

        Console.WriteLine("Años para llegar a " +
                          targetBalance + ": " +
                          SavingsAccount.YearsBeforeDesiredBalance(
                              balance,
                              targetBalance));

        Console.WriteLine("\nPresiona Enter para salir...");
        Console.ReadLine();
    }
}
