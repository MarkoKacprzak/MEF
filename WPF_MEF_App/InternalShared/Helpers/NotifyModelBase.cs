using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InternalShared
{
    /// <summary>
    /// Base class to support property change notifications
    /// </summary>
    public abstract class NotifyModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void NotifyChangedThis([CallerMemberName] string propertyName = null)
        {
            if (propertyName != null)
                NotifyPropertyChanged(propertyName);
        }
    }
}
