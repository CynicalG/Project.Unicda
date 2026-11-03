using System;
using System.Collections.Generic;
using System.IO;

namespace Programitas
{
    class EjercicioCuatro
    {
        public static void Main(string[] args)
        {
            List<string> listaEstudiantes = new List<string>();

            string archivo = "estudiantes.txt";

            if (!File.Exists(archivo))
            {
                File.Create(archivo).Close();
            }

            string[] estudiantesGuardados = File.ReadAllLines(archivo);

            for (int i = 0; i < estudiantesGuardados.Length; i++)
            {
                if (estudiantesGuardados[i] != "")
                {
                    listaEstudiantes.Add(estudiantesGuardados[i]);
                }
            }

            int opcion = 0;

            while (opcion != 5)
            {
                Console.Clear();

                Console.WriteLine("==========================================");
                Console.WriteLine("       SISTEMA DE ESTUDIANTES");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. Agregar estudiante");
                Console.WriteLine("2. Buscar estudiante");
                Console.WriteLine("3. Eliminar estudiante");
                Console.WriteLine("4. Reporte");
                Console.WriteLine("5. Salir");
                Console.WriteLine("==========================================");
                Console.Write("Seleccione una opcion: ");

                int.TryParse(Console.ReadLine(), out opcion);

                Console.Clear();

                if (opcion == 1)
                {
                    Console.WriteLine("==========================================");
                    Console.WriteLine("          AGREGAR ESTUDIANTE");
                    Console.WriteLine("==========================================");

                    Console.Write("Ingrese el curso: ");
                    string curso = Console.ReadLine() ?? string.Empty;

                    Console.Write("Ingrese el nombre del estudiante: ");
                    string nombre = Console.ReadLine() ?? string.Empty;

                    Console.Write("Ingrese el apellido del estudiante: ");
                    string apellido = Console.ReadLine() ?? string.Empty;

                    int[] notas = new int[4];
                    int suma = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        Console.Write($"Ingrese la nota {i + 1} (0-100): ");

                        while (!int.TryParse(Console.ReadLine(), out notas[i]) ||
                            notas[i] < 0 || notas[i] > 100)
                        {
                            Console.WriteLine("Error: nota incorrecta. Intente nuevamente.");
                            Console.Write($"Ingrese la nota {i + 1} (0-100): ");
                        }

                        suma += notas[i];
                    }

                    double promedio = (double)suma / 4;
                    string literal = "";

                    if (promedio >= 90)
                    {
                        literal = "A";
                    }
                    else if (promedio >= 80)
                    {
                        literal = "B";
                    }
                    else if (promedio >= 70)
                    {
                        literal = "C";
                    }
                    else
                    {
                        literal = "F";
                    }

                    string filaAlumno =
                        $"{curso}|{nombre}|{apellido}|{notas[0]}|{notas[1]}|{notas[2]}|{notas[3]}|{promedio}|{literal}";

                    listaEstudiantes.Add(filaAlumno);

                    StreamWriter escritor = new StreamWriter(archivo);

                    for (int i = 0; i < listaEstudiantes.Count; i++)
                    {
                        escritor.WriteLine(listaEstudiantes[i]);
                    }

                    escritor.Close();

                    Console.WriteLine();
                    Console.WriteLine("Estudiante agregado correctamente.");
                }

                else if (opcion == 2)
                {
                    Console.WriteLine("==========================================");
                    Console.WriteLine("          BUSCAR ESTUDIANTE");
                    Console.WriteLine("==========================================");

                    Console.Write("Ingrese el curso: ");
                    string cursoBuscar = Console.ReadLine() ?? string.Empty;

                    Console.Write("Ingrese el apellido: ");
                    string apellidoBuscar = Console.ReadLine() ?? string.Empty;

                    bool encontrado = false;

                    for (int i = 0; i < listaEstudiantes.Count; i++)
                    {
                        string[] datos = listaEstudiantes[i].Split('|');

                        string curso = datos[0].Trim();
                        string apellido = datos[2].Trim();

                        if (curso.ToLower() == cursoBuscar.ToLower() &&
                            apellido.ToLower() == apellidoBuscar.ToLower())
                        {
                            Console.WriteLine();
                            Console.WriteLine("Estudiante encontrado:");
                            Console.WriteLine("------------------------------------------");
                            Console.WriteLine("Curso: " + datos[0]);
                            Console.WriteLine("Nombre: " + datos[1]);
                            Console.WriteLine("Apellido: " + datos[2]);
                            Console.WriteLine("Nota 1: " + datos[3]);
                            Console.WriteLine("Nota 2: " + datos[4]);
                            Console.WriteLine("Nota 3: " + datos[5]);
                            Console.WriteLine("Nota 4: " + datos[6]);
                            Console.WriteLine("Promedio: " + datos[7]);
                            Console.WriteLine("Literal: " + datos[8]);
                            Console.WriteLine("------------------------------------------");

                            encontrado = true;
                        }
                    }

                    if (!encontrado)
                    {
                        Console.WriteLine();
                        Console.WriteLine("No se encontro el estudiante.");
                    }
                }

                else if (opcion == 3)
                {
                    Console.WriteLine("==========================================");
                    Console.WriteLine("        ELIMINAR ESTUDIANTE");
                    Console.WriteLine("==========================================");

                    Console.Write("Ingrese el curso: ");
                    string cursoEliminar = Console.ReadLine() ?? string.Empty;

                    Console.Write("Ingrese el apellido: ");
                    string apellidoEliminar = Console.ReadLine() ?? string.Empty;

                    bool eliminado = false;

                    for (int i = 0; i < listaEstudiantes.Count; i++)
                    {
                        string[] datos = listaEstudiantes[i].Split('|');

                        string curso = datos[0].Trim();
                        string apellido = datos[2].Trim();

                        if (curso.ToLower() == cursoEliminar.ToLower() &&
                            apellido.ToLower() == apellidoEliminar.ToLower())
                        {
                            listaEstudiantes.RemoveAt(i);

                            StreamWriter escritor = new StreamWriter(archivo);

                            for (int j = 0; j < listaEstudiantes.Count; j++)
                            {
                                escritor.WriteLine(listaEstudiantes[j]);
                            }

                            escritor.Close();

                            Console.WriteLine();
                            Console.WriteLine("Estudiante eliminado correctamente.");

                            eliminado = true;
                            break;
                        }
                    }

                    if (!eliminado)
                    {
                        Console.WriteLine();
                        Console.WriteLine("No se encontro el estudiante.");
                    }
                }

                else if (opcion == 4)
                {
                    Console.WriteLine("==========================================");
                    Console.WriteLine("               REPORTE");
                    Console.WriteLine("==========================================");

                    Console.Write("Ingrese el curso: ");
                    string cursoReporte = Console.ReadLine() ?? string.Empty;

                    List<string> listaCurso = new List<string>();

                    for (int i = 0; i < listaEstudiantes.Count; i++)
                    {
                        string[] datos = listaEstudiantes[i].Split('|');

                        if (datos[0].Trim().ToLower() ==
                            cursoReporte.ToLower())
                        {
                            listaCurso.Add(listaEstudiantes[i]);
                        }
                    }

                    if (listaCurso.Count == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("No hay estudiantes en este curso.");
                    }
                    else
                    {
                        listaCurso.Sort((a, b) =>
                        {
                            string apellidoA = a.Split('|')[2].Trim();
                            string apellidoB = b.Split('|')[2].Trim();

                            return string.Compare(apellidoA, apellidoB,
                                StringComparison.CurrentCultureIgnoreCase);
                        });

                        int totalA = 0;
                        int totalB = 0;
                        int totalC = 0;
                        int totalReprobado = 0;

                        Console.WriteLine();
                        Console.WriteLine("Curso: " + cursoReporte);
                        Console.WriteLine();
                        Console.WriteLine("Nombre | Apellido | Nota1 | Nota2 | Nota3 | Nota4 | Promedio | Literal");
                        Console.WriteLine("===============================================================");

                        for (int i = 0; i < listaCurso.Count; i++)
                        {
                            string[] datos = listaCurso[i].Split('|');

                            Console.WriteLine(
                                datos[1] + " | " +
                                datos[2] + " | " +
                                datos[3] + " | " +
                                datos[4] + " | " +
                                datos[5] + " | " +
                                datos[6] + " | " +
                                datos[7] + " | " +
                                datos[8]
                            );

                            if (datos[8].Trim() == "A")
                            {
                                totalA++;
                            }
                            else if (datos[8].Trim() == "B")
                            {
                                totalB++;
                            }
                            else if (datos[8].Trim() == "C")
                            {
                                totalC++;
                            }
                            else
                            {
                                totalReprobado++;
                            }
                        }

                        Console.WriteLine("===============================================================");
                        Console.WriteLine("Totales:");
                        Console.WriteLine("Estudiantes en A: " + totalA);
                        Console.WriteLine("Estudiantes en B: " + totalB);
                        Console.WriteLine("Estudiantes en C: " + totalC);
                        Console.WriteLine("Estudiantes en Reprobado: " + totalReprobado);
                    }
                }

                else if (opcion == 5)
                {
                    Console.WriteLine("Programa finalizado.");
                }

                else
                {
                    Console.WriteLine("Opcion incorrecta.");
                }

                if (opcion != 5)
                {
                    Console.WriteLine();
                    Console.WriteLine("Presione ENTER para continuar...");
                    Console.ReadLine();
                }
            }
        }
    }
}