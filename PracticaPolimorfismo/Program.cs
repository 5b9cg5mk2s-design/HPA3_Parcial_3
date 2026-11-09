using System;

namespace PracticaPolimorfismo
{

    // 1. CLASE ABSTRACTA

    public abstract class Figura
    {
        // Métodos abstractos que actúan como contrato obligatorio
        public abstract double CalcularArea();
        public abstract double CalcularPerimetro();
    }

    // 2. CLASES DERIVADAS

    public class Rectangulo : Figura
    {
        // Propiedades autoimplementadas encapsuladas
        public double Base { get; set; }
        public double Altura { get; set; }

        // Constructor parametrizado
        public Rectangulo(double baseRect, double alturaRect)
        {
            Base = baseRect;
            Altura = alturaRect;
        }

        // Implementación obligatoria de los métodos abstractos
        public override double CalcularArea()
        {
            return Base * Altura;
        }

        public override double CalcularPerimetro()
        {
            return 2 * (Base + Altura);
        }
    }

    public class Cuadrado : Figura
    {
        public double Lado { get; set; }

        public Cuadrado(double lado)
        {
            Lado = lado;
        }

        public override double CalcularArea()
        {
            return Lado * Lado;
        }

        public override double CalcularPerimetro()
        {
            return 4 * Lado;
        }
    }

    public class Triangulo : Figura
    {
        public double Base { get; set; }
        public double Altura { get; set; }
        public double LadoA { get; set; }
        public double LadoB { get; set; }
        public double LadoC { get; set; }

        // Constructor para área (base y altura) y perímetro (tres lados)
        public Triangulo(double baseTri, double alturaTri, double ladoA, double ladoB, double ladoC)
        {
            Base = baseTri;
            Altura = alturaTri;
            LadoA = ladoA;
            LadoB = ladoB;
            LadoC = ladoC;
        }

        public override double CalcularArea()
        {
            return (Base * Altura) / 2;
        }

        public override double CalcularPerimetro()
        {
            return LadoA + LadoB + LadoC;
        }
    }

    // CLASE PRINCIPAL

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("REPORTE DE GEOMETRÍA POLIMÓRFICA\n");

            // 3. ARREGLO UNIDIMENSIONAL DE TIPO PADRE (Figura[])
            Figura[] figuras = new Figura[3];

            // Instanciación de objetos hijos dentro del vector del padre
            figuras[0] = new Rectangulo(5.0, 3.0);
            figuras[1] = new Cuadrado(4.0);
            figuras[2] = new Triangulo(4.0, 3.0, 4.0, 5.0, 3.0);

            // 4. RECORRIDO POLIMÓRFICO
            foreach (Figura fig in figuras)
            {
                // GetType().Name extrae el nombre de la clase derivada en tiempo de ejecución
                string nombreClase = fig.GetType().Name;

                double area = fig.CalcularArea();
                double perimetro = fig.CalcularPerimetro();

                Console.WriteLine($"Clase: {nombreClase,-12} | Área: {area,6:F2} | Perímetro: {perimetro,6:F2}");
            }

            Console.ReadLine();
        }
    }
}
