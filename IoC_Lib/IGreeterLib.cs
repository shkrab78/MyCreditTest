using System;

namespace IoC_Lib
{
    /// <summary>
    /// во всех библиотеках GreeterCollection должен быть :
    /// 1. создан класс реализующий этот интерфейс с именем GreeterCollection
    /// 2. этот класс должен находится в корневом namespace
    /// 3. корневой namespace должен совпадать с именем библиотеки
    ///  </summary>
    public interface IGreeterLib
    {
        Type GetGreeterType(Type forType);
    }
}
