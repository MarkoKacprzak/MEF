using System.ComponentModel.Composition;

namespace Example09_Mef2
{
    [Export]
    public class ProcessB
    {
        [Import]
        public PluginA PlugA { get; private set; }
        [Import]
        public PluginB PlugB { get; private set; }
    }
}