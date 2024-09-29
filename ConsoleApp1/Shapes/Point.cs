using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [Serializable]
    /// <summary>
    /// Представляет точку в двумерной системе координат.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Получает или устанавливает значение координаты X точки.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Получает или устанавливает значение координаты Y точки.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Point"/> с указанными координатами.
        /// </summary>
        /// <param name="x">Координата X точки.</param>
        /// <param name="y">Координата Y точки.</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
