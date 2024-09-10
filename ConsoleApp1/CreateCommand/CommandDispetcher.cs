using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConsoleApp1.CreateCommand
{
    public class CommandDispetcher
    {
        private readonly Dictionary<string, IApp> commands = new Dictionary<string, IApp>();

        public void RegisterCommand(IApp command)
        {
            commands[command.Name] = command;
        }

        public void Dispatch(string input, App app)
        {
            var parts = input.Split(new[] { ' ' }, 2);
            if (parts.Length < 2)
            {
                Console.WriteLine("Неверный формат команды.");
                return;
            }

            var name = parts[0];
            var parameters = parts[1];

            if (commands.TryGetValue(name, out var command))
            {
                command.Execute(app, parameters); // Передаем app и параметры
            }
            else
            {
                Console.WriteLine("Неизвестная команда.");
            }
        }

        
    }
}
