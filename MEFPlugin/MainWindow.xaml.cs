using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using PluginContracts;

namespace MEFPlugin
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, IView
	{
	    private readonly GenericMefPluginLoader<IPlugin> _loader;
        public MainWindow()
		{
			InitializeComponent();

            _loader = new GenericMefPluginLoader<IPlugin>();
            new MEFObserverPlugin<IPlugin>().Subscribe(_loader.Plugins, new PluginContainer(this));
            _loader.Initialize("Plugins");
        }

        public UniformGrid GridForPlugin => PluginGrid;
	    public void Reload()
	    {
            _loader.Reload();
        }
	}
}
