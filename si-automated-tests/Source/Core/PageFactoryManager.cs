using System;
namespace si_automated_tests.Source.Core
{
    public class PageFactoryManager
    {
        public static T Get<T>() where T: new()
        {
            return new T();
        }
    }
}
