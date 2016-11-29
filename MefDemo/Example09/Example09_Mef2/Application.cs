using System;
using System.ComponentModel.Composition;

namespace Example09_Mef2
{
    [Export]
    public class Application
    {
        [Import]
        private ExportFactory<ProcessA> ProcAFactory { get; set; }
        [Import]
        private ExportFactory<ProcessB> ProcBFactory { get; set; }

        public void WriteLayoutA()
        {
            using (ExportLifetimeContext<ProcessA> lifeOfA = ProcAFactory.CreateExport())
            {
                ProcessA a = lifeOfA.Value;
                Console.WriteLine("Proc A");
                Console.WriteLine("\tPlug A: {0}", a.PlugA.GetHashCode());
                Console.WriteLine("\t\tDal: {0}", a.PlugA.Dal.GetHashCode());
                Console.WriteLine("\tPlug B: {0}", a.PlugB.GetHashCode());
                Console.WriteLine("\t\tDal: {0}", a.PlugB.Dal.GetHashCode());
            }
        }

        public void WriteLayoutB()
        {
            using (ExportLifetimeContext<ProcessB> lifeOfB = ProcBFactory.CreateExport())
            {
                ProcessB b = lifeOfB.Value;
                Console.WriteLine("Proc B");
                Console.WriteLine("\tPlug A: {0}", b.PlugA.GetHashCode());
                Console.WriteLine("\t\tDal: {0}", b.PlugA.Dal.GetHashCode());
                Console.WriteLine("\tPlug B: {0}", b.PlugB.GetHashCode());
                Console.WriteLine("\t\tDal: {0}", b.PlugB.Dal.GetHashCode());
            }
        }
    }
}