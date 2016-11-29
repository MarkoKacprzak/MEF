using System;
using System.Composition;

namespace Example13_Mef2
{
    [Export(typeof(ILogger))]
    [Shared]
    public class Logger : ILogger
    {
        public void Log(string message) =>
            Console.WriteLine(message);
    }
}