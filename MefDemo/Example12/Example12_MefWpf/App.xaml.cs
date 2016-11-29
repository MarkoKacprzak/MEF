using System.ComponentModel.Composition.Hosting;
using System.Composition.Hosting;
using System.Windows;

namespace Example12_MefWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var asmCatalog = new AssemblyCatalog(typeof(App).Assembly);
            var catalogs = new AggregateCatalog(asmCatalog);
            var container = new CompositionContainer(catalogs);
            container.Compose(new CompositionBatch());
            CompositionHost.Initialize(container);
            var root = new MainWindow();
            root.ShowDialog();

        }
    }
}
