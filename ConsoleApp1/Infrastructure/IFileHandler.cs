using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Infrastructure
{
    public interface IFileHandler
    {
        void LoadShapes(string fileName, App app);
        void SaveShapes(string fileName, ShapeCollection shapeCollection);
    }
}
