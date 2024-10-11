using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeometricShapeCalculator.Infrastructure
{
    /// <summary>
    /// Команда для выхода из приложения.
    /// </summary>
    internal class CommandExit : ICommand
    {
        private readonly ShapeCollection _shapeCollection;


        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "выход".</value>
        public string Name => "выход";

        /// <summary>
        /// Выполняет команду выхода из приложения.
        /// </summary>
        /// <param name="parameters">Параметры команды. Не используются в данной реализации. Значение по умолчанию — пустая строка.</param>
        public void Execute(string parameters, bool shouldDisplayInfo = true)
        {
            Environment.Exit(0);
        }


        /// <summary>
        /// Получает описание команды и её использования.
        /// </summary>
        /// <returns>Описание команды.</returns>
        public string Help()
        {
            return "Команда 'выход' завершает выполнение приложения.\n" +
                   "Параметры команды не требуются.\n" +
                   "Пример использования:\n" +
                   "выход\n";
        }
    }
}
