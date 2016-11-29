using System;
using System.ComponentModel.Composition.Hosting;

namespace Example09_Mef2
{
    class Program
    {
        static void Main(string[] args)
        {
            var scopeDependentCatalog = new TypeCatalog(
                typeof(ProcessA),
                typeof(ProcessB),
                typeof(PluginA),
                typeof(PluginB),
                typeof(Dal));
            var scopeDefDependent = new CompositionScopeDefinition(scopeDependentCatalog, null);

            var appCatalog = new TypeCatalog(typeof(Application));
            var scopeDefRoot = new CompositionScopeDefinition(appCatalog, new[] { scopeDefDependent });


            var container = new CompositionContainer(scopeDefRoot);

            var app = container.GetExportedValue<Application>();

            app.WriteLayoutA();
            Console.WriteLine("————————————");
            app.WriteLayoutB();
            Console.ReadKey();
        }
    }
}
