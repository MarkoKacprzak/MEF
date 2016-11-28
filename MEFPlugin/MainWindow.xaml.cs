using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PluginContracts;

namespace MEFPlugin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
	{
		string destination = Path.Combine( System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) , @"Plugins\SecondPlugin.dll");
		string source = Path.Combine( System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), @"ToLoad\SecondPlugin.dll");
	    private readonly GenericMefPluginLoader<IPlugin> _loader;
        public UniformGrid GridForPlugin => PluginGrid;
		public MainWindow()
		{
			InitializeComponent();
			Delete();
            _loader = new GenericMefPluginLoader<IPlugin>();
            new MEFObserverPlugin<IPlugin>().Subscribe(_loader.Plugins, new PluginContainer(this));
            _loader.Initialize("Plugins");			
			LoadDependency();
        }
		private void LoadDependency()
		{
			var load = new Button {Content = "Załaduj"};
            load.Click += (object sender, RoutedEventArgs e) => CopyAndReload();
			PluginGrid.Children.Add(load);			
		}
		private void CopyAndReload()
	    {
            File.Copy(source, destination);
			_loader.Reload();
        }
		private void Delete()
	    {			
			if (File.Exists(destination))
            	File.Delete(destination);
        }
	}
}
