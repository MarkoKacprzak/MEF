using System.ComponentModel.Composition;

namespace Example10_Mef2
{
    public class Level1ItemA : PluginBase
    {
        [Import]
        public Level1ItemShared ItemZ { get; private set; }
    }
}