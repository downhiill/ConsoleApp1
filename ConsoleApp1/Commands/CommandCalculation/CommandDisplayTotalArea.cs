using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Infrastructure;

namespace ConsoleApp1.GeometricShapeCalculator.Infrastructure
{
    /// <summary>
    /// Команда для отображения общей площади всех фигур в коллекции.
    /// </summary>
    internal class CommandDisplayTotalArea : ICommand
    {
        private readonly IShapeCollection _shapeCollection;

        public CommandDisplayTotalArea(IShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection;
        }
        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "показать_сумму_площади".</value>
        public string Name => "показать_сумму_площади";

        /// <summary>
        /// Выполняет команду, отображая общую площадь всех фигур в коллекции приложения.
        /// </summary>
        /// <param name="parameters">Параметры команды. Не используются в данной реализации. Значение по умолчанию — пустая строка.</param>
        public void Execute(string parameters, bool shouldDisplayInfo = true)
        {
            Console.Clear();
            Console.WriteLine($"Сумма площадей всех фигур: {_shapeCollection.S()}");
        }

        /// <summary>
        /// Получает описание команды и её использования.
        /// </summary>
        /// <returns>Описание команды.</returns>
        public string Help()
        {
            return "Команда 'показать_сумму_площади' отображает общую площадь всех фигур, которые добавлены в коллекцию.\n" +
                   "Параметры команды не требуются.\n" +
                   "Пример использования:\n" +
                   "показать_сумму_площади\n";
        }
    }
}
