using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// Представляет круг с его радиусом 
    /// </summary>
    internal class Circle : Shape
    {
        /// <summary>
        /// Радиус круга
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// Инициализация нового экземпляра круга с указанным радиусом 
        /// </summary>
        /// <param name="radius">Радиус круга</param>
        public Circle(double radius)
        {
            Radius = radius;
        }

        /// <summary>
        /// Переопределяет метод для вычисления площади круга.
        /// </summary>
        /// <returns>Площадь круга</returns>
        public override double GetArea() => Math.PI * Radius * Radius;

        /// <summary>
        /// Переопределяет метод для вычисления периметра круга.
        /// </summary>
        /// <returns>Периметр круга</returns>
        public override double GetPerimeter() => 2 * Math.PI * Radius;

        /// <summary>
        /// Переопределение метода для отображения информации о круге.
        /// </summary>
        public override void Display()
        {
            Console.WriteLine($"Круг: Радиус = {Radius}");
            Console.WriteLine($"Площадь = {GetArea():F2}");
            Console.WriteLine($"Длина окружности = {GetPerimeter():F2}");

        }


        /// <summary>
        /// Создает новый экземпляр круга на основе ввода пользователя.
        /// </summary>
        /// <returns>Новый экземпляр круга или <c>null</c>, если ввод неверен.</returns>
        public static Circle CreateCircle()
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
    }
}
