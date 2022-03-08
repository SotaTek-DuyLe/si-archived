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
            Console.WriteLine("crated");
            Logger loggerInstance = new Logger();
            Console.WriteLine("crated");
            LoggerThread.Value = loggerInstance;

            return LoggerThread.Value;
        }
        public void Info(string text)
        {
            Console.WriteLine("====[INFO]" + String.Format("<div style=\"color: green\">%s</div>", text) + "====");
        }
    }
}
