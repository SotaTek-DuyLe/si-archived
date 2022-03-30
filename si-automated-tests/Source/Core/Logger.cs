using System;
using System.Threading;

namespace si_automated_tests.Source.Core
{
    public class Logger
    {
        public static ThreadLocal<Logger> loggerThread = new ThreadLocal<Logger>();
        public static ThreadLocal<Logger> LoggerThread { get => loggerThread; set => loggerThread = value; }

        public static Logger Get()
        {
            Logger loggerInstance = new Logger();
            LoggerThread.Value = loggerInstance;

            return LoggerThread.Value;
        }
        public void Info(string text)
        {
            Console.WriteLine("====[INFO]   " + text);
        }
    }
}
