using System;
using System.Collections.Generic;

namespace Programitas
{
    class EjercicioDos
    {
        public static void Main(string[] args)
        {
            List<string> listaEstudiantes = new List<string>();

            int totalA = 0;
            int totalB = 0;
            int totalC = 0;
            int totalReprobado = 0;

            string continuar = "s";

            while (string.Equals(continuar, "s", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Ingrese el nombre del estudiante: ");
                string nombre = Console.ReadLine() ?? string.Empty;

                Console.Write("Ingrese el apellido del estudiante: ");
                string apellido = Console.ReadLine() ?? string.Empty;

                int[] notas = new int[4];
                int suma = 0;

                for (int i = 0; i < 4; i++)
                {
                    Console.Write($"Ingrese la nota {i + 1} (0-100): ");
                    notas[i] = int.Parse(Console.ReadLine() ?? string.Empty);

                    while (notas[i] < 0 || notas[i] > 100)
                    {
                        Console.WriteLine("Error: nota incorrecta. Intente nuevamente.");
                        Console.Write($"Ingrese la nota {i + 1}: ");
                        notas[i] = int.Parse(Console.ReadLine() ?? string.Empty);
                    }

                    suma += notas[i];
                }

                double promedio = (double)suma / 4;
                string literal = "";

                if (promedio >= 90)
                {
                    literal = "A";
                    totalA++;
                }
                else if (promedio >= 80)
                {
                    literal = "B";
                    totalB++;
                }
                else if (promedio >= 70)
                {
                    literal = "C";
                    totalC++;
                }
                else
                {
                    literal = "F";
                    totalReprobado++;
                }

                string filaAlumno = $"{nombre} | {apellido} | {notas[0]} | {notas[1]} | {notas[2]} | {notas[3]} | {promedio:F2} | {literal}";

                listaEstudiantes.Add(filaAlumno);

                Console.Write("Desea ingresar otro estudiante? (s/n): ");
                continuar = Console.ReadLine() ?? string.Empty;

                Console.WriteLine();
            }

            // Ordenar estudiantes por apellido
            listaEstudiantes.Sort((a, b) =>
            {
                string apellidoA = a.Split('|')[1].Trim();
                string apellidoB = b.Split('|')[1].Trim();

                return string.Compare(apellidoA, apellidoB,
                    StringComparison.CurrentCultureIgnoreCase);
            });

            // Mostrar reporte
            Console.WriteLine("\nColegio Dios es bueno.");
            Console.WriteLine("Calificaciones del cuatrimestre");
            Console.WriteLine("==========================================================================");
            Console.WriteLine("Nombre | Apellido | Nota1 | Nota2 | Nota3 | Nota4 | Promedio | Literal");
            Console.WriteLine("==========================================================================");

            for (int i = 0; i < listaEstudiantes.Count; i++)
            {
                Console.WriteLine(listaEstudiantes[i]);
            }

            // Mostrar totales
            Console.WriteLine("==========================================================================");
            Console.WriteLine("TOTALES:");
            Console.WriteLine("Estudiantes en A: " + totalA);
            Console.WriteLine("Estudiantes en B: " + totalB);
            Console.WriteLine("Estudiantes en C: " + totalC);
            Console.WriteLine("Estudiantes en Reprobado: " + totalReprobado);

            Console.WriteLine("==========================================================================");

            Console.ReadKey();
        }
    }
}