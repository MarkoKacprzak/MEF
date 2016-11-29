using System.ComponentModel.Composition;

namespace Example10_Mef2
{
    public class Level1ItemB : PluginBase
    {
        [Import]
        public Level1ItemShared SharedItem { get; private set; }

        [Import]
        public ExportFactory<Level2SubScopeRoot> SubScope { get; private set; }
    }
}