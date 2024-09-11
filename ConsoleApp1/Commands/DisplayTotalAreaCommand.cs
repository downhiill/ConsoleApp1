using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeometricShapeCalculator.Infrastructure
{
    /// <summary>
    /// Команда для отображения общей площади всех фигур в коллекции.
    /// </summary>
    internal class DisplayTotalAreaCommand : ICommand
    {
        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "показать_сумму_площади".</value>
        public string Name => "показать_сумму_площади";

        /// <summary>
        /// Выполняет команду, отображая общую площадь всех фигур в коллекции приложения.
        /// </summary>
        /// <param name="app">Экземпляр приложения, содержащий коллекцию фигур.</param>
        /// <param name="parameters">Параметры команды. Не используются в данной реализации. Значение по умолчанию — пустая строка.</param>
        public void Execute(App app, string parameters = "")
        {
            Console.Clear();
            double totalArea = app.ShapeCollection.S(); // Получаем общую площадь
            Console.WriteLine($"Сумма площадей всех фигур: {totalArea}");
        }
    }
}
