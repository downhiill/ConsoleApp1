using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class ShapeController
    {
        static List<Shape> shapes = new List<Shape>();
        private void addShape()
        {
            Console.Clear();
            Console.WriteLine("Выберите фигуру для расчета:");
            Console.WriteLine("1. Круг");
            Console.WriteLine("2. Прямоугольник");
            Console.WriteLine("3. Треугольник");
            Console.WriteLine("4. Квадрат");
            Console.WriteLine("5. Показать сумму площади фигур и все периметры");
            Console.WriteLine("6. Выход");


            var choice = Console.ReadLine();
            Shape shape = null;

            switch (choice)
            {
                case "1":
                    shape = CreateCircle();
                    break;
                case "2":
                    shape = CreateRectangle();
                    break;
                case "3":
                    shape = CreateTriangle();
                    break;
                case "4":
                    shape = CreateSquare();
                    break;
                case "5":
                    DisplayTotalAreaAndPerimeters();
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    return;
            }
            if (shape != null)
            {
                shapes.Add(shape);
                shape.Display(); // Вызов метода Display() для отображения информации о фигуре
            }
            else
            {
                Console.WriteLine("Не удалось создать фигуру.");
            }
        }
        private void DisplayAllInputData()
        {
            Console.Clear();
            Console.WriteLine("Введеные данные о фигурах:");
            foreach (var shape in shapes)
            {
                Console.WriteLine(shape.GetInputData());
            }
        }

        private static Circle CreateCircle()
        {
            Console.Write("Введите радиус круга: ");
            if (double.TryParse(Console.ReadLine(), out double radius))
            {
                return new Circle(radius);
            }
            else
            {
                Console.WriteLine("Неверное значение радиуса.");
                return null;
            }
        }
        private static Rectangle CreateRectangle()
        {
            Console.Write("Введите ширину прямоугольника: ");
            if (double.TryParse(Console.ReadLine(), out double width))
            {
                Console.Write("Введите высоту прямоугольника: ");
                if (double.TryParse(Console.ReadLine(), out double height))
                {
                    return new Rectangle(width, height);
                }
                else
                {
                    Console.WriteLine("Неверное значение высоты.");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Неверное значение ширины.");
                return null;
            }
        }
        private static Triangle CreateTriangle()
        {
            Console.Write("Введите длину стороны A треугольника: ");
            if (double.TryParse(Console.ReadLine(), out double a))
            {
                Console.Write("Введите длину стороны B треугольника: ");
                if (double.TryParse(Console.ReadLine(), out double b))
                {
                    Console.Write("Введите длину стороны C треугольника: ");
                    if (double.TryParse(Console.ReadLine(), out double c))
                    {
                        return new Triangle(a, b, c);
                    }
                    else
                    {
                        Console.WriteLine("Неверное значение стороны C.");
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine("Неверное значение стороны B.");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Неверное значение стороны A.");
                return null;
            }
        }

        private static Square CreateSquare()
        {

            Console.Write("Введите ширину и высоту квадрата: ");
            if (double.TryParse(Console.ReadLine(), out double a))
            {
                return new Square(a);
            }
            else
            {
                Console.WriteLine("Неверное значение ширины и высоты. (Для расчета введите одно значение)");
                return null;
            }
        }

        private void DisplayTotalAreaAndPerimeters()
        {
            Console.Clear();
            double totalArea = 0;

            foreach (var shape in shapes)
            {
                totalArea += shape.GetArea();
            }
            Console.WriteLine($"Сумма всех S = {totalArea}");

            Console.WriteLine("\nПериметры всех фигур:");
            foreach (var shape in shapes)
            {
                Console.WriteLine($"Фигура: {shape.GetType().Name}, P = {shape.GetPerimeter()}");
            }
        }

        public void Run()
        {
            while (true)
            {
                addShape();
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }
}
