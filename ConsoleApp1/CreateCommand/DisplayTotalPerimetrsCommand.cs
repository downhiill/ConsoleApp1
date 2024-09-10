using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeometricShapeCalculator.Infrastructure
{
    internal class DisplayTotalPerimetrsCommand : IApp
    {
        public string Name => "Показать_все_периметры";

        public void Execute(App app, string parameters = "")
        {
            Console.Clear();
            foreach (var shape in app.shapes)
            {
                Console.WriteLine($"Фигура: {shape.GetType().Name}, Периметр = {app.P()}");
            }
        }
    }
}
