using System;
using System.Security.Cryptography;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Reflection;

namespace Example07_Mef2
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.Run();
        }

        void Run()
        {
            Compose();

        }
        //OldFasion
        private void Compose()
        {
            //work with the registration builder
            var picker = new RegistrationBuilder();

            //Export Specifi SymetricAlgoritm with fluen api
            picker.ForType<AesManaged>()
                .Export<SymmetricAlgorithm>()
                .SetCreationPolicy(CreationPolicy.NonShared);

            //Export inheritance
            //you can choose to export anything that derived from a Type or implement an Interface:
            /*
            picker.ForTypesDerivedFrom<SymmetricAlgorithm>()
                .Export<SymmetricAlgorithm>()
                .SetCreationPolicy(CreationPolicy.NonShared);
                */
            //MEF2.0
            //Export by conventions
            picker.ForTypesMatching(pl => pl.Name.EndsWith("Plugin"))
               .ExportInterfaces();

            //ExportInterfaces is having a few overloads which you can use to refine the interface exporting, 
            //instead of exporting all of the implemented interfaces, you can define a convention, for example:
            //picker.ForTypesMatching(pl => pl.Name.EndsWith("Plugin"))
            //    .ExportInterfaces(t => t.Name.EndsWith("Plugin"));


            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new TypeCatalog(
                new[] {typeof(AesManaged), typeof(LoggerPlugin), typeof(FilePlugin)}, picker));

            catalog.Catalogs.Add(new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly()));
            var container = new CompositionContainer(catalog);
            container.Compose(new CompositionBatch());
            var w = container.GetExportedValue<ILogPlugin>();

            var foo = new Foo();
            container.ComposeParts(foo);
            container.ComposeParts(this);
            foo.IntAggregator.Send(2);

        }

        [System.ComponentModel.Composition.Import]
        private SymmetricAlgorithm Crypto { get; set; }

        [System.ComponentModel.Composition.Import(AllowDefault = true)]
        private ILogPlugin logPlugin { get; set; }

        [System.ComponentModel.Composition.Import(AllowDefault = true)]
        private ISavePlugin savePlugin { get; set; }
    }

    public interface ILogPlugin
    {
        void Write(string message);
    }
    public class LoggerPlugin : ILogPlugin
    {
        public void Write(string message) { /* … */}
    }

    public interface ISavePlugin
    {
        void Save(string message);
    }
    public class FilePlugin : ISavePlugin
    {
        public void Save(string message) { /* … */}
    }
    
    public class Foo
    {
        [Import]
        public EventAggregator<int> IntAggregator { get; set; }
    }

    [Export]
    public class EventAggregator<T>
    {
        public event Action<T> Notify = (item) => { Console.WriteLine(item.ToString()); };
        public void Send(T item)
        {
            Notify(item);
        }
    }
}
