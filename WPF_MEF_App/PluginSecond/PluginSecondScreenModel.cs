using InternalShared;

namespace PluginSecond
{
    public class PluginSecondScreenModel : NotifyModelBase
    {
        private double _valueVar;
        public double Value
        {
            get { return _valueVar; }
            set
            {
                _valueVar = value;				
                NotifyChangedThis();
            }
        }
    }
}
