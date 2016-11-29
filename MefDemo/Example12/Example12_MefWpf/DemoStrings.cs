

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Composition.Hosting;
using System.Windows.Markup;

namespace Example12_MefWpf
{
    class DemoStrings
    {
        [Export("MyTag")]
        public string Text1 => "Hello world";

        [Export("MyTag")]
        public string Text2 => "export using custom attribute";

        [Export("NotMyTag")]
        public string Text3 => "not included (Not My Tag contract)";

        [Export("MyTag")]
        public string Text4 => "Wpf markup extension";

        [Export]
        public string Text5 => "not included (have no contract)";
    }

    [MarkupExtensionReturnType(typeof(IEnumerable<string>))]
    public class ImportManyStringsExtension : MarkupExtension
    {
        private readonly IEnumerable<string> _dataSource;
        public ImportManyStringsExtension(string contract)
        {
            _dataSource = CompositionHost.CompositionInitializer.GetExportedValues<string>(contract);
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _dataSource;
        }
    }

    public class CompositionHost
    {

        internal static CompositionContainer _container = null;

        public static void Initialize(CompositionContainer container)
        {
            _container = container;
        }

        public static CompositionContainer CompositionInitializer => _container as CompositionContainer;
    }
}
