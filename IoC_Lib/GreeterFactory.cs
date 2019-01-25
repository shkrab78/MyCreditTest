using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;

namespace IoC_Lib
{
    public static class GreeterFactory
    {
        /// <summary>
        /// Создаёт экземпляр класса Greeter для объекта заданного типа
        /// </summary>
        /// <param name="forType">тип для которого создаётся Greeter</param>
        /// <param name="createArgs">параметры передаваемые при создании объекта</param>
        /// <param name="libName">имя библиотеки из которой формировать объект, если null, то из библиотеки "GreeterCollection"</param>
        /// <returns></returns>
        public static IGreeter Create(Type forType, object[] createArgs = null, string libName = null)
        {
            var lName = libName ?? _defaultLibName;
            var lib = GetLibrary(lName);
            try
            {
                //экспортируем класс содержащийся в ClassName 
                //корневое пространство имен должно быть "GreeterCollection" 
                var libGreeterType = lib?.BaseObject.GetGreeterType(forType);
                //создаем Greeter 
                return (IGreeter) libGreeterType?.InvokeMember(null, BindingFlags.CreateInstance, null, null, createArgs);
            }
            catch (Exception e)
            {
                throw new IoCException("Ошибка при создании Greeter. \n" +
                                       $"Библиотека - \"{lName}\" \n" +
                                       $"Имя класса - \"{forType.Name}\"", e);
            }
        }

        public static IGreeter Create(Type forType, string libName)
        {
            return Create(forType, null, libName);
        }

        public static IGreeter Create<T>(object[] createArgs = null, string libName = null)
        {
            return Create(typeof(T), createArgs, libName);
        }

        public static IGreeter Create<T>(string libName)
        {
            return Create<T>(null, libName);
        }

        private static Library GetLibrary(string libName)
        {
            var libFName = Path.ChangeExtension(libName, ".dll");
            var nameSp = Path.GetFileNameWithoutExtension(libName);
            var baseClassName = $"{nameSp}.{GREETER_COLLECTION_NAME_SPACE}";

            if (libFName == null || nameSp == null) return null;

            if (Libraries.ContainsKey(nameSp))
                return Libraries[nameSp];

            var rootDir = AppDomain.CurrentDomain.RelativeSearchPath;
            var libPath = Path.Combine(rootDir, libFName);

            if (!File.Exists(libPath)) return null;
            //загружаем библиотеку
            try
            {
                var assembly = Assembly.LoadFile(libPath);

                var t = assembly.GetType(baseClassName);
                var baseObject = (IGreeterLib)t.InvokeMember(null, BindingFlags.CreateInstance, null, null, null);
                var lib = new Library(baseObject);
                Libraries.Add(nameSp, lib);
                return lib;
            }
            catch (Exception e)
            {
                throw new IOException("Ошибка при открытии библиотеки репозиториев. \n" +
                                      $"Библиотека - \"{libPath}\" \n" +
                                      $"Имя класса - \"{baseClassName}\"", e);
            }
        }

        private const string GREETER_COLLECTION_NAME_SPACE = "GreeterCollection";

        private static readonly Dictionary<string, Library> Libraries = new Dictionary<string, Library>();

        private static string _defaultLibName = "DefaultGreeterCollection";

        private class Library
        {
            public readonly IGreeterLib BaseObject;

            public Library(IGreeterLib baseObject)
            {
                BaseObject = baseObject;
            }
        }
    }
}
