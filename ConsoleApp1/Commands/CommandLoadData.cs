
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1.Commands
{
    internal class CommandLoadData : ICommand
    {
        private readonly ShapeCollection _shapeCollection;
        private const string DefaultFileName = "Shape.json"; // Имя файла по умолчанию

        public string Name => "загрузить_данные";

        public CommandLoadData(ShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection;
        }

        public void Execute(string parameters)
        {
            // Используем имя файла по умолчанию, если параметр пустой
            var fileName = string.IsNullOrWhiteSpace(parameters) ? DefaultFileName : parameters;

            if (!File.Exists(fileName))
            {
                Console.WriteLine($"Файл {fileName} не найден.");
                return;
            }


            try
            {
                // Чтение содержимого файла
                var jsonData = File.ReadAllText(fileName);

                // Чтение массива объектов JSON
                var shapesArray = JArray.Parse(jsonData);

                // Для каждой фигуры в JSON создаём объект ShapeInfo и добавляем его в коллекцию _shapeCollection
                foreach (var shape in shapesArray)
                {
                    var shapeInfo = new ShapeInfo
                    {
                        Name = (string)shape["Фигура"],  // Название фигуры
                        Perimeter = (double)shape["Периметр"],  // Периметр
                        Area = (double)shape["Площадь"]  // Площадь
                    };

                    // Создаём фигуру на основе названия и добавляем её в коллекцию _shapeCollection
                    switch (shapeInfo.Name)
                    {
                        case "Circle":
                            var radius = (double)shape["Радиус"];
                            _shapeCollection.Add(new Circle(radius));
                            break;
                        case "Square":
                            var side = (double)shape["Сторона"];
                            _shapeCollection.Add(new Square(side));
                            break;
                        case "Rectangle":
                            var width = (double)shape["Ширина"];
                            var height = (double)shape["Высота"];
                            _shapeCollection.Add(new Rectangle(width, height));
                            break;
                        case "Triangle":
                            var a = (double)shape["A"];
                            var b = (double)shape["B"];
                            var c = (double)shape["C"];
                            _shapeCollection.Add(new Triangle(a,b,c));
                            break;
                        case "Polygon":
                            // Извлечение списка точек из JSON
                            var pointsArray = shape["Точки"].Children<JObject>()
                                .Select(p => new Point((double)p["X"], (double)p["Y"]))
                                .ToList();
                            _shapeCollection.Add(new Polygon(pointsArray));
                            break;
                        default:
                            Console.WriteLine($"Неизвестная фигура: {shapeInfo.Name}");
                            break;
                    }
                }

                Console.WriteLine("Данные успешно загружены в коллекцию.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        public string Help()
        {
            return "Команда 'загрузить_данные' загружает данные о фигурах из файла.\n" +
                   "Параметры команды: имя файла для загрузки. Если имя файла не указано, используется значение по умолчанию 'default_shapes_data.json'.\n" +
                   "Пример использования:\n" +
                   "загрузить_данные имя_файла.json\n" +
                   "или\n" +
                   "загрузить_данные\n";
        }
    }
}
