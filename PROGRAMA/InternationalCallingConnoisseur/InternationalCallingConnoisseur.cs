using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Crear diccionario vacío
        Dictionary<int, string> emptyDictionary =
            new Dictionary<int, string>();

        Console.WriteLine("Diccionario vacío creado.");

        // Crear diccionario con países
        Dictionary<int, string> countries =
            new Dictionary<int, string>
            {
                { 1, "United States of America" },
                { 55, "Brazil" },
                { 91, "India" }
            };

        // Agregar un país
        countries.Add(34, "Spain");

        // Mostrar países
        Console.WriteLine("\nPaíses:");

        foreach (var country in countries)
        {
            Console.WriteLine(
                $"Código: {country.Key} - País: {country.Value}"
            );
        }

        // Buscar país por código
        int code = 55;

        if (countries.TryGetValue(code, out string countryName))
        {
            Console.WriteLine(
                $"\nEl código {code} corresponde a: {countryName}"
            );
        }
        else
        {
            Console.WriteLine("\nCódigo no encontrado.");
        }

        // Comprobar si existe un código
        bool exists = countries.ContainsKey(91);

        Console.WriteLine(
            $"¿Existe el código 91?: {exists}"
        );

        // Actualizar un país
        if (countries.ContainsKey(91))
        {
            countries[91] = "India Updated";
        }

        Console.WriteLine(
            $"\nCódigo 91 actualizado: {countries[91]}"
        );

        // Eliminar un país
        countries.Remove(34);

        Console.WriteLine("\nDespués de eliminar España:");

        foreach (var country in countries)
        {
            Console.WriteLine(
                $"Código: {country.Key} - País: {country.Value}"
            );
        }

        // Encontrar el nombre más largo
        string longestCountry = "";

        foreach (var country in countries)
        {
            if (country.Value.Length > longestCountry.Length)
            {
                longestCountry = country.Value;
            }
        }

        Console.WriteLine(
            $"\nNombre de país más largo: {longestCountry}"
        );
    }
}
