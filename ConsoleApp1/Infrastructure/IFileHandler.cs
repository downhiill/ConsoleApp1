using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Infrastructure
{
    /// <summary>
    /// Интерфейс для обработки файлов сохранения и загрузки фигур.
    /// </summary>
    public interface IFileHandler
    {
        /// <summary>
        /// Загружает фигуры из указанного файла в приложение.
        /// </summary>
        /// <param name="fileName">Имя файла, из которого будут загружены фигуры.</param>
        /// <param name="app">Экземпляр приложения, куда загружаются фигуры.</param>
        void LoadShapes(string fileName, App app);

        /// <summary>
        /// Сохраняет коллекцию фигур в указанный файл.
        /// </summary>
        /// <param name="fileName">Имя файла, в который будут сохранены фигуры.</param>
        /// <param name="shapeCollection">Коллекция фигур для сохранения.</param>
        void SaveShapes(string fileName, ShapeCollection shapeCollection);
    }
}