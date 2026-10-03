using System;
using System.Collections.Generic;
using System.Linq;

public static class Languages
{
    public static List<string> NewList()
    {
        return new List<string>();
    }

    public static List<string> GetExistingLanguages()
    {
        return new List<string>
        {
            "C#",
            "Clojure",
            "Elm"
        };
    }

    public static List<string> AddLanguage(
        List<string> languages,
        string language)
    {
        languages.Add(language);
        return languages;
    }

    public static int CountLanguages(List<string> languages)
    {
        return languages.Count;
    }

    public static bool HasLanguage(
        List<string> languages,
        string language)
    {
        return languages.Contains(language);
    }

    public static List<string> ReverseList(
        List<string> languages)
    {
        languages.Reverse();
        return languages;
    }

    public static bool IsExciting(
        List<string> languages)
    {
        if (languages.Count > 0 && languages[0] == "C#")
        {
            return true;
        }

        if ((languages.Count == 2 || languages.Count == 3)
            && languages[1] == "C#")
        {
            return true;
        }

        return false;
    }

    public static List<string> RemoveLanguage(
        List<string> languages,
        string language)
    {
        languages.Remove(language);
        return languages;
    }

    public static bool IsUnique(
        List<string> languages)
    {
        return languages.Distinct().Count() == languages.Count;
    }
}

class Program
{
    static void Main()
    {
        // Crear una lista nueva
        List<string> languages = Languages.NewList();

        // Agregar lenguajes
        Languages.AddLanguage(languages, "C#");
        Languages.AddLanguage(languages, "Clojure");
        Languages.AddLanguage(languages, "Elm");

        // Mostrar la lista
        Console.WriteLine("Lenguajes:");

        foreach (string language in languages)
        {
            Console.WriteLine(language);
        }

        // Contar lenguajes
        Console.WriteLine(
            $"\nCantidad: {Languages.CountLanguages(languages)}"
        );

        // Comprobar si existe C#
        Console.WriteLine(
            $"¿Tiene C#?: {Languages.HasLanguage(languages, "C#")}"
        );

        // Comprobar si es emocionante
        Console.WriteLine(
            $"¿Es emocionante?: {Languages.IsExciting(languages)}"
        );

        // Comprobar si los lenguajes son únicos
        Console.WriteLine(
            $"¿Son únicos?: {Languages.IsUnique(languages)}"
        );

        // Invertir la lista
        Languages.ReverseList(languages);

        Console.WriteLine("\nLista invertida:");

        foreach (string language in languages)
        {
            Console.WriteLine(language);
        }

        // Eliminar un lenguaje
        Languages.RemoveLanguage(languages, "Elm");

        Console.WriteLine("\nDespués de eliminar Elm:");

        foreach (string language in languages)
        {
            Console.WriteLine(language);
        }
    }
}
