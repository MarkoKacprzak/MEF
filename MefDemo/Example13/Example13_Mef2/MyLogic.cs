using System;
using System.Composition;

namespace Example13_Mef2
{
    public class MyLogic
    {
        [ImportMany]
        private ILogger[] Loggers { get; set; }

        [Import]
        private ISetting Setting { get; set; }
        [Import]
        private ISetting Setting1 { get; set; }
        public void Execute()
        {
            Console.WriteLine(Setting.GetHashCode());
            Console.WriteLine(Setting1.GetHashCode());
            foreach (var logger in Loggers)
            {
                logger.Log("MEF");
            }
        }
    }
}