using InternalShared;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace PluginCalculator
{
    /// <summary>
    /// Interaction logic for CalculatorScreen.xaml
    /// </summary>
    [Export(typeof(IView)), PartCreationPolicy(CreationPolicy.Any)]
    [ExportMetadata("Name", "CalculatorPlugin")]
    public partial class CalculatorScreen : UserControl, IView
    {
        public CalculatorScreen()
        {
            InitializeComponent();
        }
    }
}
