using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Infrastructure;

namespace ConsoleApp1.GeometricShapeCalculator.Infrastructure
{
    /// <summary>
    /// Команда для отображения периметров всех фигур в коллекции.
    /// </summary>
    internal class CommandDisplayTotalPerimetrs : ICommand
    {
        private readonly IShapeCollection _shapeCollection;

        public CommandDisplayTotalPerimetrs(IShapeCollection shapeCollection)
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
        public void Execute(string parameters, bool shouldDisplayInfo = true)
        {
            Console.Clear();
            Console.WriteLine("Периметры всех фигур:");

            // Для каждой фигуры в коллекции выводим ее периметр
            foreach (var shape in _shapeCollection.ToList())
            {
                Console.WriteLine($"Фигура: {shape.GetType().Name}, Периметр = {shape.P()}");
            }
        }

        /// <summary>
        /// Получает описание команды и её использования.
        /// </summary>
        /// <returns>Описание команды.</returns>
        public string Help()
        {
            return "Команда 'показать_все_периметры' отображает периметры всех фигур, которые добавлены в коллекцию.\n" +
                   "Параметры команды не требуются.\n" +
                   "Пример использования:\n" +
                   "показать_все_периметры\n";
        }
    }
}
