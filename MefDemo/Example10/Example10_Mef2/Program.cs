using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example10_Mef2
{
    class Program
    {
        static void Main(string[] args)
        {
            var picker = new RegistrationBuilder();
            picker.ForTypesDerivedFrom<PluginBase>()
                // add ScopeLevel metadata
                .AddMetadata("ScopeLevel", t => t.Name.StartsWith("Level1") ? 1 : 2)
                .Export<PluginBase>();

            var catalog = new AssemblyCatalog(typeof(Program).Assembly, picker);

            var catalogL0 = catalog.Filter(p => p.ContainsPartMetadata("ScopeLevel", 1));
            var catalogL1 = catalog.Filter(p => p.ContainsPartMetadata("ScopeLevel", 2));

            var scopeL1 = new CompositionScopeDefinition(catalogL1, null);
            var scopeL0 = new CompositionScopeDefinition(catalogL0, new[] { scopeL1 });

            var container = new CompositionContainer(scopeL0);
        }
    }
}
