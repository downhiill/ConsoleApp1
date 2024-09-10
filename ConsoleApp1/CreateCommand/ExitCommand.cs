using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeometricShapeCalculator.Infrastructure
{
    internal class ExitCommand : IApp
    {
        public string Name => "Выход";

        public void Execute(App app, string parameters = "")
        {
            Environment.Exit(0);
        }
    }
}
