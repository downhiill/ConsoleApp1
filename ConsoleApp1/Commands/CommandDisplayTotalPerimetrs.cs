using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeometricShapeCalculator.Infrastructure
{
    /// <summary>
    /// Команда для отображения периметров всех фигур в коллекции.
    /// </summary>
    internal class CommandDisplayTotalPerimetrs : ICommand
    {
        private readonly ShapeCollection _shapeCollection;

        public CommandDisplayTotalPerimetrs(ShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection;
        }
        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "показать_все_периметры".</value>
        public string Name => "показать_все_периметры";

        /// <summary>
        /// Выполняет команду, отображая периметры всех фигур в коллекции приложения.
        /// </summary>
        /// <param name="parameters">Параметры команды. Не используются в данной реализации. Значение по умолчанию — пустая строка.</param>
        public void Execute( string parameters)
        {
            Console.Clear();
            Console.WriteLine("Периметры всех фигур:");

            // Перебираем все фигуры в коллекции
            foreach (var shape in _shapeCollection.shapes) 
            {
                double perimeter = _shapeCollection.P();
                string shapeName = shape.GetType().Name;

                
                Console.WriteLine($"Фигура: {shapeName}, Периметр = {perimeter}");
            }
        }
    }
}
