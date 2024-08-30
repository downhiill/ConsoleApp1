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

        public override double GetArea() => Width * Height;

        public override double GetPerimeter() => 2 * (Width + Height);

        public override void Display()
        {
            Console.WriteLine($"Прямоугольник: Ширина = {Width}, Высота = {Height}");
            Console.WriteLine($"Площадь = {GetArea():F2}");
            Console.WriteLine($"Периметр = {GetPerimeter():F2}");
        }

        public override string GetInputData()
        {
            return $"Прямоугольник: Ширина = {Width}, Высота = {Height}";
        }
    }
}

