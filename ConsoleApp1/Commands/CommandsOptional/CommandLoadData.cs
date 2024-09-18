
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
    /// <summary>
    /// Команда для загрузки данных о фигурах из файла и добавления их в коллекцию.
    /// </summary>
    internal class CommandLoadData : ICommand
    {
        private readonly ShapeCollection _shapeCollection;
        private const string DefaultFileName = "ShapeData.txt"; // Имя файла по умолчанию
        
        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "загрузить_данные".</value>
        public string Name => "загрузить_данные";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CommandLoadData"/>.
        /// </summary>
        /// <param name="shapeCollection">Коллекция фигур, в которую будут добавлены загруженные данные.</param>
        public CommandLoadData(ShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection;
        }

        /// <summary>
        /// Выполняет команду, загружая данные о фигурах из указанного файла и добавляя их в коллекцию.
        /// Если имя файла не указано, используется значение по умолчанию "ShapeData.txt".
        /// </summary>
        /// <param name="parameters">Имя файла, из которого будут загружены данные. Если параметр пустой, используется значение по умолчанию.</param>
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
                // Чтение содержимого файла построчно
                var lines = File.ReadAllLines(fileName, Encoding.UTF8);
                var importer = new CommandDataImporter(_shapeCollection);

                lines.ToList().ForEach(line => importer.ParseAndAddShape(line));

                Console.WriteLine("Данные успешно загружены в коллекцию.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            }


            /* LoadJSON
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
            */
        }

        /// <summary>
        /// Получает описание команды и её использования.
        /// </summary>
        /// <returns>Описание команды.</returns>
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
