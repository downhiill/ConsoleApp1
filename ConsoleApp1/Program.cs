using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите фигуру для расчета:");
                Console.WriteLine("1. Круг");
                Console.WriteLine("2. Прямоугольник");
                Console.WriteLine("3. Треугольник");
                Console.WriteLine("4. Квадрат");
                Console.WriteLine("5. Выход");

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
                        return;
                    default:
                        Console.WriteLine("Неверный выбо. Попробуйте снова.");
                        continue;
                }
                if (shape != null)
                {
                    shape.Display(); // Вызов метода Display() для отображения информации о фигуре
                }
                else
                {
                    Console.WriteLine("Не удалось создать фигуру.");
                }

                // Ожидание ввода пользователя перед продолжением
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        static Circle CreateCircle()
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
        static Rectangle CreateRectangle()
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
        static Triangle CreateTriangle()
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

        static Square CreateSquare()
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
    }
}
