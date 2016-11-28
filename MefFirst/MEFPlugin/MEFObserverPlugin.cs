using System;
using PluginContracts;

namespace MEFPlugin
{
    public class MEFObserverPlugin<T> : IObserver<T>
        where T : IPlugin
    {
        private IDisposable _unsubscriber;
        private PluginContainer _container;
        public virtual void Subscribe(IObservable<T> provider, PluginContainer container)
        {
            _unsubscriber = provider.Subscribe(this);
            _container = container;
        }

        public virtual void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }

        public void OnNext(T value)
        {
            Console.WriteLine($"Plugin type {value.GetType()} Name: {value.Name}");
            _container.Add(value);
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"Some Error {error.Message}");
        }

        public void OnCompleted()
        {
            Console.WriteLine($"Additional IPlugin data will not be transmitted.");
        }
    }
}