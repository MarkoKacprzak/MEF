using System.ComponentModel.Composition;

namespace Example10_Mef2
{
    public class Level2SubItem1 : PluginBase
    {
        [Import]
        public Level1ItemShared ItemShared { get; private set; }
        [Import]
        public Level2SubItemShared SubItemShared { get; private set; }
    }
}