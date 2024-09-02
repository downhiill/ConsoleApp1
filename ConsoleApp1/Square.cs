using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// Представляет квадрат с заданной шириной и высотой.
    /// </summary>
    internal class Square : Shape
    {
        /// <summary>
        /// Ширина и высота квадрата.
        /// </summary>
        public double A {  get; set; }

        /// <summary>
        /// Инициализация нового экземпляра квадрата с указаной шириной и высотой.
        /// </summary>
        /// <param name="a">Ширина и высота квадрата</param>
        public Square (double a)
        {
            A = a;
        }

        /// <summary>
        /// Переопределение метода для вычисления площади.
        /// </summary>
        /// <returns>Площадь квадрата</returns>
        public override double GetArea() => A * A;

        /// <summary>
        /// Переопределение метода для вычисления периметра.
        /// </summary>
        /// <returns> Периметр квадрата</returns>
        public override double GetPerimeter() => 4 * A;

        /// <summary>
        /// Переопределение метода для отображения информации о квадрате.
        /// </summary>
        public override void Display()
        {
            Console.WriteLine($"Квадрат: Ширина = {A}, Высота = {A}");
            Console.WriteLine($"Площадь = {GetArea():F2}");
            Console.WriteLine($"Периметр = {GetPerimeter():F2}");

        }

        /// <summary>
        /// Переопределяет метод для получения строки с данными о квадрате.
        /// </summary>
        /// <returns>Данные о квадрате.</returns>
        public override string GetInputData()
        {
            return $"Квадрат: Ширина = {A}, Высота = {A}";
        }

        /// <summary>
        /// Создает новый экземпляр квадрата на основе ввода пользователя.
        /// </summary>
        /// <returns>Новый экземпляр квадрата или <c>null</c>, если ввод неверен.</returns>
        public static Square CreateSquare()
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
