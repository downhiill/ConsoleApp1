using System;
using System.Collections.Generic;

namespace ConsoleApp1.Infrastructure
{
    /// <summary>
    /// Контейнер зависимостей для регистрации и разрешения зависимостей.
    /// </summary>
    public class DependencyContainer
    {
        // Словарь для хранения зависимостей
        private readonly Dictionary<Type, object> _dependencies = new Dictionary<Type, object>();

        /// <summary>
        /// Регистрирует зависимость в контейнере.
        /// </summary>
        /// <typeparam name="TInterface">Тип интерфейса.</typeparam>
        /// <typeparam name="TImplementation">Тип реализации.</typeparam>
        public void Register<TInterface, TImplementation>() where TImplementation : TInterface, new()
        {
            // Создание экземпляра TImplementation и сохранение его в словаре
            _dependencies[typeof(TInterface)] = new TImplementation();
        }

        /// <summary>
        /// Разрешает зависимость и возвращает экземпляр интерфейса.
        /// </summary>
        /// <typeparam name="TInterface">Тип интерфейса для разрешения.</typeparam>
        /// <returns>Экземпляр реализации интерфейса.</returns>
        /// <exception cref="KeyNotFoundException">Выбрасывается, если зависимость не зарегистрирована.</exception>
        public TInterface Resolve<TInterface>()
        {
            // Получение экземпляра из словаря и приведение его к TInterface
            return (TInterface)_dependencies[typeof(TInterface)];
        }

        // Перегрузка метода Resolve для поддержки передачи аргумента типа Type
        public object Resolve(Type type)
        {
            if (!_dependencies.TryGetValue(type, out var instance))
            {
                throw new KeyNotFoundException($"Зависимость для типа {type} не зарегистрирована.");
            }

            return instance;
        }
    }
}