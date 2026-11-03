using System;

namespace PrimerEjercicio
{
    class Calculadora
    {
        static void Main(string[] args)
        {
            Console.Write("Inserte el primer numero: ");
            double num1 = double.Parse(Console.ReadLine() ?? string.Empty);

            Console.Write("Inserte el segundo numero: ");
            double num2 = double.Parse(Console.ReadLine() ?? string.Empty);

            // Suma
            double suma = num1 + num2;
            Console.WriteLine($"La suma es: {suma}");

            // Resta
            double resta = num1 - num2;
            Console.WriteLine($"La resta es: {resta}");

            // Multiplicacion
            double multi = num1 * num2;
            Console.WriteLine($"La multiplicacion es: {multi}");

            // Division
            if (num2 != 0)
            {
                double division = num1 / num2;
                Console.WriteLine($"La division es: {division}");
            }
            else
            {
                Console.WriteLine("No es posible dividir entre 0.");
            }

            // Raices cuadradas
            if (num1 >= 0 && num2 >= 0)
            {
                double raiz1 = Math.Sqrt(num1);
                double raiz2 = Math.Sqrt(num2);

                Console.WriteLine($"La raiz cuadrada de {num1} es: {raiz1:F4}");
                Console.WriteLine($"La raiz cuadrada de {num2} es: {raiz2:F4}");
            }
            else
            {
                Console.WriteLine("No se puede calcular la raiz de un numero negativo.");
            }

            Console.ReadKey();
        }
    }
}