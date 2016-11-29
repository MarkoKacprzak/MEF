using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example08_Mef2
{
    public interface ILogger
    {
        
    }
    public class Logger : ILogger
    {
        public void Write(string message) { /* … */}
    }

    public class Worker
    {
        private ILogger _logger;

        public Worker()
        {
            /* … */
        }

        public Worker(ILogger logger)
        {
            _logger = logger;
        }
    }

    class Program: IPartImportsSatisfiedNotification
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

        [Import(typeof(Worker))]
        public Worker worker;
        private void Compose()
        {
            //work with the registration builder
            /*
            var picker = new RegistrationBuilder();
            picker.ForTypesDerivedFrom<ILogger>()
                .Export<ILogger>();
            picker.ForType<Worker>()
                .SelectConstructor(builder => new Worker(builder.Import<ILogger>()))
                .Export();
                */
            var picker = new RegistrationBuilder();
            picker.ForTypesDerivedFrom<ILogger>().Export<ILogger>();
            picker.ForType<Worker>()
                .Export()
                .SelectConstructor(ctors =>
                    ctors.First(info => info.GetParameters().Length == 1));

            var catalog = new AssemblyCatalog(typeof(Program).Assembly, picker);
            var container = new CompositionContainer(catalog);
            container.Compose(new CompositionBatch());
            container.ComposeParts(this);
            var w = container.GetExportedValue<Worker>();
        }

        public void OnImportsSatisfied()
        {
            Console.WriteLine("OnImportsSatisfied");
        }
    }
}
