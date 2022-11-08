using System;
namespace si_automated_tests.Source.Core
{
    public class PageFactoryManager
    {
        public static T Get<T>() where T: new()
        {
            var page =(T)Activator.CreateInstance(typeof(T));
            return page;
        }
    }
}
