using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// Коллекция геометрических фигур, предоставляющая методы для работы с ними.
    /// </summary>
    public class ShapeCollection 
    {
        /// <summary>
        /// Список фигур, содержащихся в коллекции.
        /// </summary>
        public List<Shape> shapes = new List<Shape>();

        /// <summary>
        /// Добавляет фигуру в коллекцию.
        /// </summary>
        /// <param name="shape">Фигура, которую нужно добавить.</param>
        public void Add(Shape shape)
        {
            shapes.Add(shape);
        }

        /// <summary>
        /// Вычисляет общую площадь всех фигур указанного типа в коллекции.
        /// </summary>
        /// <typeparam name="T">Тип фигур для расчета площади. Тип должен наследовать от <see cref="Shape"/>.</typeparam>
        /// <returns>Общая площадь фигур указанного типа.</returns>
        public double S<T>() where T : Shape
        {
            double totalArea = shapes.OfType<T>().Sum(shape => shape.S());
            return totalArea;
        }
        /// <summary>
        /// Вычисляет общую площадь всех фигур в коллекции.
        /// </summary>
        /// <returns>Общая площадь всех фигур в коллекции.</returns>
        public double S()
        {
            double totalArea = shapes.Sum(shape => shape.S());
            return totalArea;
        }
        /// <summary>
        /// Выводит периметры всех фигур указанного типа в коллекции.
        /// </summary>
        /// <typeparam name="T">Тип фигур для расчета периметра. Тип должен наследовать от <see cref="Shape"/>.</typeparam>
        /// <returns>Периметры фигур указанного типа в виде строки, разделенной переводами строк.</returns>
        public double P<T>() where T : Shape
        {
            double totalPerimeter = shapes.OfType<T>().Sum(shape => shape.P());

            return totalPerimeter;
        }
        /// <summary>
        /// Выводит периметры всех фигур в коллекции.
        /// </summary>
        /// <returns>Периметры всех фигур в коллекции в виде строки, разделенной переводами строк.</returns>
        public double P()
        {
            double totalPerimeter = shapes.Sum(shape => shape.P());


            return totalPerimeter;
        }

    }
}
