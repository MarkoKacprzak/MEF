using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using PluginContracts;

namespace MEFPlugin
{
	public class GenericMefPluginLoader<T>
	{
		private DirectoryCatalog _catalog;
	    [ImportMany(AllowRecomposition = true)]
	    public ObservableCollection<T> Plugins { get; }

	    public GenericMefPluginLoader()
	    {
	        Plugins = new ObservableCollection<T>();
	    }
	    public void Initialize(string path)
	    {
            _catalog = new DirectoryCatalog(path);
	        //An aggregate catalog that combines multiple catalogs
	        var catalog = new AggregateCatalog(_catalog);
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(GenericMefPluginLoader<IPlugin>).Assembly));
	        // Create the CompositionContainer with all parts in the catalog (links Exports and Imports)
	        var container = new CompositionContainer(catalog);
	        var debug = new MefDebugger(container);
	        //Fill the imports of this object
	        container.ComposeParts(this);
	       // debug.Close();
	    }

	    public void Reload()
	    {
	        _catalog.Refresh();
	    }
	}
}
