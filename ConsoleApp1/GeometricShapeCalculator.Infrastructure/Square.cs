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
        /// Создает новый экземпляр квадрата на основе ввода пользователя.
        /// </summary>
        /// <returns>Новый экземпляр квадрата или <c>null</c>, если ввод неверен.</returns>
        /*
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
        */
    }
}
