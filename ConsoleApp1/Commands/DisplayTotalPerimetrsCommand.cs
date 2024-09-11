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
    internal class DisplayTotalPerimetrsCommand : ICommand
    {
        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "показать_все_периметры".</value>
        public string Name => "показать_все_периметры";

        /// <summary>
        /// Выполняет команду, отображая периметры всех фигур в коллекции приложения.
        /// </summary>
        /// <param name="app">Экземпляр приложения, содержащий коллекцию фигур.</param>
        /// <param name="parameters">Параметры команды. Не используются в данной реализации. Значение по умолчанию — пустая строка.</param>
        public void Execute(App app, string parameters = "")
        {
            Console.Clear();
            Console.WriteLine("Периметры всех фигур:");

            // Перебираем все фигуры в коллекции
            foreach (var shape in app.ShapeCollection.shapes) 
            {
                double perimeter = app.ShapeCollection.P();
                string shapeName = shape.GetType().Name;

                
                Console.WriteLine($"Фигура: {shapeName}, Периметр = {perimeter}");
            }
        }
    }
}
