using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MyList<T> where T : Shape
    {
        public List<T> shapes = new List<T>();

        public void Add(T item)
        {
            shapes.Add(item);
        }

        public T Get(int index)
        {
            if (index < 0 || index >= shapes.Count)
            {
                throw new IndexOutOfRangeException("Index out of range.");
            }
            return shapes[index];
        }

        public void PrintSquare()
        {
            double totalArea = 0;

            foreach (var shape in shapes)
            {
                totalArea += shape.GetArea();
            }
            Console.WriteLine($"Сумма всех S = {totalArea}");
        }
        public void PrintPerimeter()
        {
            Console.WriteLine("\nПериметры всех фигур:");
            foreach (var shape in shapes)
            {
                Console.WriteLine($"Фигура: {shape.GetType().Name}, P = {shape.GetPerimeter()}");
            }
            
        }

        public double S<U>() where U : T
        {
            // Подсчёт площади всех фигур типа U
            return shapes.OfType<U>().Sum(shape => shape.GetArea());
        }

        public double P<U>() where U : T
        {
            // Подсчёт периметра всех фигур типа U
            return shapes.OfType<U>().Sum(shape => shape.GetPerimeter());
        }

    }
}
