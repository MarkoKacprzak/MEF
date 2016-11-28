using System.ComponentModel.Composition;

namespace Common
{
    [InheritedExport(typeof(IStringWriter))]
    public interface IStringWriter
    {
        string WriteMessage();
    }
}
