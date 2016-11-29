using System;
using System.Composition;

namespace Example13_Mef2
{
    [Export(typeof(ILogger))]
    [Shared]
    public class ColorLogger : ILogger
    {
        [Import]
        public ISetting Setting { get; set; }
        public void Log(string message)
        {
            Console.ForegroundColor = Setting.GetColor();
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}