using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Rectangle: Shape
    {

        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override double CalculateArea() => Width * Height;

        public override double CalculatePerimetr() => 2 * (Width + Height);

        public override void Display()
        {
            Console.WriteLine($"Прямоугольник: Ширина = {Width}, Высота = {Height}");
            Console.WriteLine($"Площадь = {CalculateArea():F2}");
            Console.WriteLine($"Периметр = {CalculatePerimetr():F2}");
        }
    }
}

