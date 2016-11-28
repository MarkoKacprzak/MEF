using InternalShared;

namespace PluginCalculator
{
    public class CalculatorScreenModel : NotifyModelBase
    {
        private double _numberOneVar;
        public double NumberOne
        {
            get { return _numberOneVar; }
            set
            {
                _numberOneVar = value;
                Sum = NumberOne + NumberTwo;
                NotifyChangedThis();
            }
        }
		
        private double _numberTwoVar;
        public double NumberTwo
        {
            get { return _numberTwoVar; }
            set
            {
                _numberTwoVar = value;
                Sum = NumberOne + NumberTwo;			
                NotifyChangedThis();
            }
        }
		
        private double _sumVar;
        public double Sum
        {
            get { return _sumVar; }
            set
            {
                _sumVar = value;				
                NotifyChangedThis();
            }
        }
    }
}
