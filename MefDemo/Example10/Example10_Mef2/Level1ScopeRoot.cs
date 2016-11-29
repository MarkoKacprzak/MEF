using System.ComponentModel.Composition;

namespace Example10_Mef2
{
    public class Level1ScopeRoot : PluginBase
    {
        [Import]
        public Level1ItemA ItemA { get; private set; }
        [Import]
        public Level1ItemB ItemB { get; private set; }
    }
}