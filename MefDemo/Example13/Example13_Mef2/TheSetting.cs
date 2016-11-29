using System;
using System.Composition;

namespace Example13_Mef2
{
    [Export(typeof(ISetting))]
    [Shared]
    public class TheSetting : ISetting
    {
        public ConsoleColor GetColor() =>
            ConsoleColor.Yellow;
    }
}