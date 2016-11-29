using System.ComponentModel.Composition;

namespace Example10_Mef2
{
    public class Level2SubItem2 : PluginBase
    {
        [Import]
        public Level2SubItemShared SubItem { get; private set; }
    }
}