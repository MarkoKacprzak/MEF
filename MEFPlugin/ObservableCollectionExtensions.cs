using System;
using System.Collections.Generic;
using System.Reactive.Disposables;

namespace MEFPlugin
{
    public class ObservableCollection<T> : IList<T>, IObservable<T>
    {
        private readonly IList<T> _list;
        private readonly IList<IObserver<T>> _observers;

        public ObservableCollection()
        {
            this._list = new List<T>();
            this._observers = new List<IObserver<T>>();
        }

        #region IObservable implementation

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observers.Add(observer);
            return Disposable.Create(() => this._observers.Remove(observer));
        }

        private void NotifyNext(T item)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(item);
            }
        }

        #endregion

        #region IList implementation

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
            NotifyNext(item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(0);
        }

        public T this[int index]
        {
            get
            {
                return _list[index];
            }
            set
            {
                _list[index] = value;
            }
        }

        #endregion

        #region ICollection implementation

        public void Add(T item)
        {
            _list.Add(item);
            NotifyNext(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        public int Count => this._list.Count;

        public bool IsReadOnly => this._list.IsReadOnly;

        #endregion

        #region IEnumerable implementation

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion

        #region IEnumerable implementation

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion
    }
}