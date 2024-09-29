using ConsoleApp1.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Commands.CommandSaveType
{
    internal class CommandTXTConverter: IFileHandler
    {
        public void LoadShapes(string fileName, App app)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"Файл {fileName} не найден.");
            }

            var lines = File.ReadAllLines(fileName, Encoding.UTF8);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(new[] { ' ' }, 2);
                var commandName = parts[0];
                var commandParams = parts.Length > 1 ? parts[1] : string.Empty;
                app.ExecuteCommand(commandName, commandParams);
            }
        }

        public void SaveShapes(string fileName, ShapeCollection shapeCollection)
        {
            using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                foreach (var shape in shapeCollection.GetAllShapes())
                {
                    writer.WriteLine(shape.GetCommand());
                }
            }
        }
    }
}
