using System.ComponentModel.Composition;

namespace Example10_Mef2
{
    public class Level2SubScopeRoot : PluginBase
    {
        [Import]
        public Level2SubItem1 SubItem1 { get; private set; }

        [Import]
        public Level2SubItem2 SubItem2 { get; private set; }
    }
}