using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// Представляет команду, которую можно выполнить в приложении.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, которое используется для её идентификации.</value>
        string Name { get; }

        /// <summary>
        /// Выполняет команду с заданными параметрами.
        /// </summary>
        /// <param name="app">Экземпляр приложения, в котором выполняется команда.</param>
        /// <param name="parameters">Параметры, необходимые для выполнения команды. Значение по умолчанию — пустая строка.</param>
        void Execute(App app, string parameters = "");
    }
}
