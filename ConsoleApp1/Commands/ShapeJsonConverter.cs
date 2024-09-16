using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace ConsoleApp1.Commands
{
    internal class ShapeJsonConverter : JsonConverter<Shape>
    {

        public override Shape ReadJson(JsonReader reader, Type objectType, Shape existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var shapeType = (string)jsonObject["Фигура"];

            switch (shapeType)
            {
                case "Circle":
                    return new Circle
                    {
                        Radius = (double)jsonObject["Радиус"]
                    };
                case "Rectangle":
                    return new Rectangle(
                        (double)jsonObject["Ширина"],
                        (double)jsonObject["Высота"]
                    );
                case "Triangle":
                    return new Triangle(
                        (double)jsonObject["A"],
                        (double)jsonObject["B"],
                        (double)jsonObject["C"]
                    );
                case "Square":
                    return new Square
                    {
                        A = (double)jsonObject["Сторона"]
                    };
                case "Polygon":
                    var points = jsonObject["Точки"]
                        .Children<JObject>()
                        .Select(p => new Point((double)p["X"], (double)p["Y"]))
                        .ToList();
                    return new Polygon(points);
                default:
                    throw new JsonSerializationException($"Unknown shape type: {shapeType}");
            }
        }


        public override void WriteJson(JsonWriter writer, Shape value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Фигура");
            writer.WriteValue(value.GetType().Name);
            switch (value)
            {
                case Circle circle:
                    writer.WritePropertyName("Радиус");
                    writer.WriteValue(circle.Radius);
                    writer.WritePropertyName("Периметр");
                    writer.WriteValue(circle.P());
                    writer.WritePropertyName("Площадь");
                    writer.WriteValue(circle.S());
                    break;
                case Rectangle rectangle:
                    writer.WritePropertyName("Ширина");
                    writer.WriteValue(rectangle.Width);
                    writer.WritePropertyName("Высота");
                    writer.WriteValue(rectangle.Height);
                    writer.WritePropertyName("Периметр");
                    writer.WriteValue(rectangle.P());
                    writer.WritePropertyName("Площадь");
                    writer.WriteValue(rectangle.S());
                    break;
                case Triangle triangle:
                    writer.WritePropertyName("A");
                    writer.WriteValue(triangle.A);
                    writer.WritePropertyName("B");
                    writer.WriteValue(triangle.B);
                    writer.WritePropertyName("C");
                    writer.WriteValue(triangle.C);
                    writer.WritePropertyName("Периметр");
                    writer.WriteValue(triangle.P());
                    writer.WritePropertyName("Площадь");
                    writer.WriteValue(triangle.S());
                    break;
                case Square square:
                    writer.WritePropertyName("Сторона");
                    writer.WriteValue(square.A);
                    writer.WritePropertyName("Периметр");
                    writer.WriteValue(square.P());
                    writer.WritePropertyName("Площадь");
                    writer.WriteValue(square.S());
                    break;
                case Polygon polygon:
                    writer.WritePropertyName("Точки");
                    writer.WriteStartArray();
                    foreach (var point in polygon.Points)
                    {
                        writer.WriteStartObject();
                        writer.WritePropertyName("X");
                        writer.WriteValue(point.X);
                        writer.WritePropertyName("Y");
                        writer.WriteValue(point.Y);
                        writer.WriteEndObject();
                    }
                    writer.WriteEndArray();
                    writer.WritePropertyName("Периметр");
                    writer.WriteValue(polygon.P());
                    writer.WritePropertyName("Площадь");
                    writer.WriteValue(polygon.S());
                    break;
            }

            writer.WriteEndObject();
        }
    }
}
