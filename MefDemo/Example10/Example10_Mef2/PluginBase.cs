namespace Example10_Mef2
{
    public abstract class PluginBase
    {
        private static int _globalId = 0;
        protected int _id;

        public PluginBase()
        {
            _id = _globalId++; // not thread-safe
        }
    }
}