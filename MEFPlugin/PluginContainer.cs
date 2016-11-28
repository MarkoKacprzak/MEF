using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PluginContracts;

namespace MEFPlugin
{
    public class PluginContainer
    {
        private readonly Dictionary<string, IPlugin> _plugins;
        private readonly IView _view;

        private PluginContainer()
        {
            _plugins = new Dictionary<string, IPlugin>();
        }

        public PluginContainer(IView view) : this()
        {
            _view = view;
        }

        public void Add(IPlugin plugin)
        {
            if (!_plugins.ContainsKey(plugin.Name))
            {
                _plugins.Add(plugin.Name, plugin);
                var b = new Button {Content = plugin.Name};
                b.Click += b_Click;
                _view.GridForPlugin.Children.Add(b);
            }
        }

        private void b_Click(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            if (b != null)
            {
                var key = b.Content.ToString();
                if (_plugins.ContainsKey(key))
                {
                    var plugin = _plugins[key];
                    plugin.Do();
                }
            }
        }
    }
}