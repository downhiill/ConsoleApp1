using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Square : Shape
    {
        public double A {  get; set; }

        public Square (double a)
        {
            A = a;
        }

        public override double CalculateArea() => A * A;

        public override double CalculatePerimetr() => 4 * A;

        public override void Display()
        {
            Console.WriteLine($"Квадрат: Ширина = {A}, Высота = {A}");
            Console.WriteLine($"Площадь = {CalculateArea():F2}");
            Console.WriteLine($"Периметр = {CalculatePerimetr():F2}");

        }



        public override string GetInputData()
        {
            return $"Квадрат: Ширина = {A}, Высота = {A}";
        }
    }
}
