using System.ComponentModel.Composition;

namespace Example09_Mef2
{
    [Export]
    public class PluginB
    {
        [Import]
        public Dal Dal { get; private set; }
    }
}