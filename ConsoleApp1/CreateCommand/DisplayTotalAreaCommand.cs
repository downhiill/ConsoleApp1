using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeometricShapeCalculator.Infrastructure
{
    internal class DisplayTotalAreaCommand : IApp
    {
        public string Name => "Показать_сумму_площади";

        public void Execute(App app, string parameters = "")
        {
            Console.Clear();
            foreach(var shape in app.shapes)
            {
                Console.WriteLine($"Сумма площади\n S = {app.S()}");
            }
           
        }
    }
}
