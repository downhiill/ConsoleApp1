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
    internal class ExitCommand : ICommand
    {
        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "выход".</value>
        public string Name => "выход";

        /// <summary>
        /// Выполняет команду выхода из приложения.
        /// </summary>
        /// <param name="app">Экземпляр приложения. Не используется в данной реализации.</param>
        /// <param name="parameters">Параметры команды. Не используются в данной реализации. Значение по умолчанию — пустая строка.</param>
        public void Execute(App app, string parameters = "")
        {
            Environment.Exit(0);
        }
    }
}
