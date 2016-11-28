using InternalShared;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace PluginSecond
{
    /// <summary>
    /// Interaction logic for PluginSecondScreen.xaml
    /// </summary>
    [Export(typeof(IView)), PartCreationPolicy(CreationPolicy.Any)]
    [ExportMetadata("Name", "PluginSecond")]
    public partial class PluginSecondScreen : UserControl, IView
    {
        public PluginSecondScreen()
        {
            InitializeComponent();
        }
    }
}
