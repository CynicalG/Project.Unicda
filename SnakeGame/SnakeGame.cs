using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace JuegoSnake
{
    // ==========================================
    // ENUMERACIÓN DE DIRECCIÓN
    // ==========================================
    public enum Direccion
    {
        Derecha,
        Izquierda,
        Arriba,
        Abajo
    }

    // ==========================================
    // CLASE VENTANA
    // ==========================================
    public class Ventana
    {
        public string Titulo { get; set; }
        public int Ancho { get; set; }
        public int Altura { get; set; }
        public ConsoleColor ColorFondo { get; set; }
        public ConsoleColor ColorLetra { get; set; }
        public Point LimiteSuperior { get; set; }
        public Point LimiteInferior { get; set; }
        public int Area { get; set; }
        public Snake SnakeC { get; set; } = null!;

        public Ventana(string titulo, int ancho, int altura, ConsoleColor colorFondo, ConsoleColor colorLetra, Point limiteSuperior, Point limiteInferior)
        {
            Titulo = titulo;
            Ancho = ancho;
            Altura = altura;
            ColorFondo = colorFondo;
            ColorLetra = colorLetra;
            LimiteSuperior = limiteSuperior;
            LimiteInferior = limiteInferior;

            Init();
        }

        private void Init()
        {
            if (OperatingSystem.IsWindows())
            {
                Console.SetWindowSize(Ancho, Altura);
            }
            Console.Title = Titulo;
            Console.CursorVisible = false;
            Console.BackgroundColor = ColorFondo;
            Console.Clear();

            // Área utilizable dentro del marco
            Area = (LimiteInferior.X - LimiteSuperior.X - 1) * (LimiteInferior.Y - LimiteSuperior.Y - 1);
        }

        public void DibujarMarco()
        {
            Console.ForegroundColor = ColorLetra;

            // Marco superior e inferior
            for (int i = LimiteSuperior.X; i <= LimiteInferior.X; i++)
            {
                Console.SetCursorPosition(i, LimiteSuperior.Y);
                Console.Write("═"); // alt 205
                Console.SetCursorPosition(i, LimiteInferior.Y);
                Console.Write("═"); // alt 205
            }

            // Marco izquierdo y derecho
            for (int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++)
            {
                Console.SetCursorPosition(LimiteSuperior.X, i);
                Console.Write("║"); // alt 186
                Console.SetCursorPosition(LimiteInferior.X, i);
                Console.Write("║"); // alt 186
            }

            // Esquinas
            Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
            Console.Write("╔"); // alt 201
            Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
            Console.Write("╚"); // alt 200
            Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
            Console.Write("╗"); // alt 187
            Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
            Console.Write("╝"); // alt 188
        }

        public void Menu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            int centroX = (LimiteSuperior.X + LimiteInferior.X) / 2;
            int centroY = (LimiteSuperior.Y + LimiteInferior.Y) / 2;

            Console.SetCursorPosition(centroX - 12, centroY - 4);
            Console.Write("J U E G O   S N A K E");

            Console.SetCursorPosition(centroX - 8, centroY - 2);
            Console.Write("ENTER - Jugar");

            Console.SetCursorPosition(centroX - 8, centroY - 1);
            Console.Write("ESCAPE - Salir");

            if (SnakeC != null)
            {
                SnakeC.MoverMenu();
            }
        }

        public void Teclado(ref bool ejecucion, ref bool jugar, Snake snake)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                if (tecla.Key == ConsoleKey.Enter)
                {
                    jugar = true;
                    Console.Clear();
                    DibujarMarco();
                    snake.Init();
                }
                else if (tecla.Key == ConsoleKey.Escape)
                {
                    ejecucion = false;
                }
            }
        }

        public void GameOver(string texto)
        {
            Console.Clear();
            DibujarMarco();
            Console.ForegroundColor = ConsoleColor.White;

            int centroX = (LimiteSuperior.X + LimiteInferior.X) / 2;
            int centroY = (LimiteSuperior.Y + LimiteInferior.Y) / 2;

            Console.SetCursorPosition(centroX - 10, centroY - 2);
            Console.Write(texto);

            Thread.Sleep(3000);
            Console.Clear();
            DibujarMarco();
        }
    }

    // ==========================================
    // CLASE SNAKE
    // ==========================================
    public class Snake
    {
        public bool Vivo { get; set; }
        public ConsoleColor ColorCabeza { get; set; }
        public ConsoleColor ColorCuerpo { get; set; }
        public Ventana VentanaC { get; set; }
        public List<Point> Cuerpo { get; set; }
        public Point Cabeza { get; set; }
        public Point PosicionInicial { get; set; }
        public Comida? ComidaC { get; set; }
        public Direccion DireccionActual { get; set; }
        public bool Comiendo { get; set; }
        public int Puntaje { get; set; }
        public int PuntajeMaximo { get; set; }

        public Snake(Point posicionInicial, ConsoleColor colorCabeza, ConsoleColor colorCuerpo, Ventana ventana)
        {
            PosicionInicial = posicionInicial;
            Cabeza = posicionInicial;
            ColorCabeza = colorCabeza;
            ColorCuerpo = colorCuerpo;
            VentanaC = ventana;
            Cuerpo = new List<Point>();
            DireccionActual = Direccion.Derecha;
            Vivo = true;
            Puntaje = 0;
            PuntajeMaximo = 0;
        }

        public void Init()
        {
            Console.Clear();
            Cabeza = PosicionInicial;
            Cuerpo.Clear();
            IniciarCuerpo(2);
            Vivo = true;
            DireccionActual = Direccion.Derecha;
            Puntaje = 0;
            if (ComidaC != null)
            {
                ComidaC.GenerarComida(this);
            }
        }

        public void IniciarCuerpo(int partes)
        {
            int x = Cabeza.X - 1;
            Console.ForegroundColor = ColorCuerpo;

            for (int i = 0; i < partes; i++)
            {
                Console.SetCursorPosition(x, Cabeza.Y);
                Console.WriteLine("▓"); // alt 178
                Cuerpo.Add(new Point(x, Cabeza.Y));
                x--;
            }
        }

        public void Mover()
        {
            Teclado();
            Point posCabezaAnterior = Cabeza;
            MoverCabeza();
            MoverCuerpo(posCabezaAnterior);
            ColisionesComida();

            if (ColisionesCuerpo())
            {
                Muerte();
                VentanaC.GameOver("G A M E   O V E R");
            }
        }

        private void MoverCabeza()
        {
            Console.ForegroundColor = ColorCabeza;
            Console.SetCursorPosition(Cabeza.X, Cabeza.Y);
            Console.WriteLine(" ");

            Point nuevaCabeza = Cabeza;

            switch (DireccionActual)
            {
                case Direccion.Derecha:
                    nuevaCabeza.X++;
                    break;
                case Direccion.Izquierda:
                    nuevaCabeza.X--;
                    break;
                case Direccion.Arriba:
                    nuevaCabeza.Y--;
                    break;
                case Direccion.Abajo:
                    nuevaCabeza.Y++;
                    break;
            }

            Cabeza = nuevaCabeza;
            ColisionesMarco();

            Console.SetCursorPosition(Cabeza.X, Cabeza.Y);
            Console.WriteLine("█"); // alt 219
        }

        private void MoverCuerpo(Point posCabezaAnterior)
        {
            Console.ForegroundColor = ColorCuerpo;
            Console.SetCursorPosition(posCabezaAnterior.X, posCabezaAnterior.Y);
            Console.WriteLine("▓"); // alt 178
            Cuerpo.Insert(0, posCabezaAnterior);

            if (Comiendo)
            {
                Comiendo = false;
            }
            else
            {
                Point ultimaParte = Cuerpo[Cuerpo.Count - 1];
                Console.SetCursorPosition(ultimaParte.X, ultimaParte.Y);
                Console.WriteLine(" ");
                Cuerpo.RemoveAt(Cuerpo.Count - 1);
            }
        }

        private void Teclado()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                if (tecla.Key == ConsoleKey.RightArrow && DireccionActual != Direccion.Izquierda)
                    DireccionActual = Direccion.Derecha;
                else if (tecla.Key == ConsoleKey.LeftArrow && DireccionActual != Direccion.Derecha)
                    DireccionActual = Direccion.Izquierda;
                else if (tecla.Key == ConsoleKey.UpArrow && DireccionActual != Direccion.Abajo)
                    DireccionActual = Direccion.Arriba;
                else if (tecla.Key == ConsoleKey.DownArrow && DireccionActual != Direccion.Arriba)
                    DireccionActual = Direccion.Abajo;
            }
        }

        private void ColisionesMarco()
        {
            Point p = Cabeza;

            if (p.X <= VentanaC.LimiteSuperior.X)
                p.X = VentanaC.LimiteInferior.X - 1;
            else if (p.X >= VentanaC.LimiteInferior.X)
                p.X = VentanaC.LimiteSuperior.X + 1;

            if (p.Y <= VentanaC.LimiteSuperior.Y)
                p.Y = VentanaC.LimiteInferior.Y - 1;
            else if (p.Y >= VentanaC.LimiteInferior.Y)
                p.Y = VentanaC.LimiteSuperior.Y + 1;

            Cabeza = p;
        }

        private void ColisionesComida()
        {
            if (ComidaC != null && Cabeza.X == ComidaC.Posicion.X && Cabeza.Y == ComidaC.Posicion.Y)
            {
                Comiendo = true;
                Puntaje++;
                if (Puntaje > PuntajeMaximo)
                {
                    PuntajeMaximo = Puntaje;
                }

                if (!ComidaC.GenerarComida(this))
                {
                    Vivo = false;
                    VentanaC.GameOver("J U E G O   C O M P L E T A D O");
                }
            }
        }

        private bool ColisionesCuerpo()
        {
            foreach (Point parte in Cuerpo)
            {
                if (Cabeza.X == parte.X && Cabeza.Y == parte.Y)
                {
                    Vivo = false;
                    return true;
                }
            }
            return false;
        }

        private void Muerte()
        {
            Console.ForegroundColor = ColorCuerpo;
            foreach (Point parte in Cuerpo)
            {
                if (parte.X == Cabeza.X && parte.Y == Cabeza.Y)
                    continue;

                Console.SetCursorPosition(parte.X, parte.Y);
                Console.WriteLine("░"); // alt 176
                Thread.Sleep(120);
            }
        }

        public void Informacion(int dist1, int dist2)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(VentanaC.LimiteSuperior.X + dist1, VentanaC.LimiteSuperior.Y - 1);
            Console.Write($"Puntaje: {Puntaje}  ");

            Console.SetCursorPosition(VentanaC.LimiteSuperior.X + dist2, VentanaC.LimiteSuperior.Y - 1);
            Console.Write($"Puntaje Máximo: {PuntajeMaximo}  ");
        }

        public void MoverMenu()
        {
            Point posCabezaAnterior = Cabeza;
            MoverCabeza();
            MoverCuerpo(posCabezaAnterior);
        }
    }

    // ==========================================
    // CLASE COMIDA
    // ==========================================
    public class Comida
    {
        public Point Posicion { get; set; }
        public ConsoleColor Color { get; set; }
        public Ventana VentanaC { get; set; }

        public Comida(ConsoleColor color, Ventana ventana)
        {
            Color = color;
            VentanaC = ventana;
        }

        public void Dibujar()
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(Posicion.X, Posicion.Y);
            Console.WriteLine("█"); // alt 219
        }

        public bool GenerarComida(Snake snake)
        {
            int longitudSnake = snake.Cuerpo.Count + 1;
            if (VentanaC.Area - longitudSnake <= 0)
            {
                return false;
            }

            Random random = new Random();
            int x = random.Next(VentanaC.LimiteSuperior.X + 1, VentanaC.LimiteInferior.X);
            int y = random.Next(VentanaC.LimiteSuperior.Y + 1, VentanaC.LimiteInferior.Y);

            Point nuevaPos = new Point(x, y);

            foreach (Point parte in snake.Cuerpo)
            {
                if ((nuevaPos.X == parte.X && nuevaPos.Y == parte.Y) || (nuevaPos.X == snake.Cabeza.X && nuevaPos.Y == snake.Cabeza.Y))
                {
                    return GenerarComida(snake);
                }
            }

            Posicion = nuevaPos;
            Dibujar();
            return true;
        }
    }

    // ==========================================
    // PROGRAMA PRINCIPAL (PROGRAM)
    // ==========================================
    class Program
    {
        static bool ejecucion = true;
        static bool jugar = false;

        static Ventana ventana = null!;
        static Snake? snake;
        static Comida comida = null!;

        static void Main(string[] args)
        {
            Iniciar();
            Game();
        }

        static void Iniciar()
        {
            Point limiteSuperior = new Point(5, 3);
            Point limiteInferior = new Point(59, 18);

            ventana = new Ventana("Snake", 65, 20, ConsoleColor.Black, ConsoleColor.White, limiteSuperior, limiteInferior);
            snake = new Snake(new Point(8, 4), ConsoleColor.Red, ConsoleColor.White, ventana);
            comida = new Comida(ConsoleColor.Green, ventana);

            snake.ComidaC = comida;

            // Serpiente decorativa para el menú
            Point posMenu = new Point(limiteInferior.X / 2, limiteInferior.Y - 3);
            Snake snakeMenu = new Snake(posMenu, ConsoleColor.Magenta, ConsoleColor.White, ventana);
            snakeMenu.IniciarCuerpo(4);
            ventana.SnakeC = snakeMenu;
        }

        static void Game()
        {
            while (ejecucion)
            {
                ventana.Menu();
                ventana.Teclado(ref ejecucion, ref jugar, snake!);

                while (jugar)
                {
                    snake!.Informacion(0, 34);
                    snake.Mover();

                    if (!snake.Vivo)
                    {
                        jugar = false;
                        snake.Puntaje = 0;
                    }

                    Thread.Sleep(100);
                }

                Thread.Sleep(100);
            }
        }
    }
}
