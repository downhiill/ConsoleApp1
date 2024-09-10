using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ShapeCollection 
    { 
        public List<Shape> shapes = new List<Shape>();

        public void Add(Shape item)
        {
            shapes.Add(item);
        }

        public double S<T>() where T : Shape
        {
            double totalArea = 0;
            foreach (var shape in shapes)
            {
                if (shape is T typedShape)
                {
                    totalArea += typedShape.GetPerimeter();
                }
            }
            return totalArea;
        }
        public double S()
        {
            double totalArea = 0;
            foreach (var shape in shapes)
            {
                totalArea += shape.GetArea();
            }
            return totalArea;
        }

        public double P<T>() where T : Shape
        {
            double totalPerimeter = 0;

            foreach (var shape in shapes)
            {
                if (shape is T typedShape)
                {
                    totalPerimeter += typedShape.GetPerimeter();
                }
            }

            return totalPerimeter;
        }

        public double P()
        {
            double totalPerimeter = 0;

            foreach (var shape in shapes)
            {
                totalPerimeter += shape.GetPerimeter();
            }

            return totalPerimeter;
        }

    }
}
