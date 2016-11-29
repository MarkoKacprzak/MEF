using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Example11_Mef2
{
    class Program
    {
        static void Main(string[] args)
        {
            //DisableSilentRejection 
            var p = new Program();

            var cat = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(cat, CompositionOptions.DisableSilentRejection);

            try
            {
                container.ComposeParts(p);
                Console.WriteLine("{0} Plug-ins loaded", p.Plugins.Length);
            }
            catch (CompositionException e)
            {
                Console.WriteLine("Composition error");
            }
        }
        [ImportMany]
        public Iplugin[] Plugins { get; set; }
    }
    [InheritedExport]
    public interface Iplugin { }

    public class BadPlugin : Iplugin
    {
        [Import(typeof(IIdentity))]
        public IIdentity MissingDependency { get; set; }
    }

    public class FinePlugin : Iplugin { }
}
