using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example13_Mef2
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ContainerConfiguration()
                .WithAssembly(typeof(Program).Assembly);

            using (var container = configuration.CreateContainer())
            {

                var logic = new MyLogic();
                container.SatisfyImports(logic);
                logic.Execute();

            }
        }
    }
}