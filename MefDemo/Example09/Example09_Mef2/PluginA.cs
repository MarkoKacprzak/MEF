using System.ComponentModel.Composition;

namespace Example09_Mef2
{
    [Export]
    public class PluginA
    {
        [Import]
        public Dal Dal { get; private set; }
    }
}