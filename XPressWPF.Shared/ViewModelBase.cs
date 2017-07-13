using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XPressWPF.Shared
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        #region Constructor 
        public ViewModelBase()
        {
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region IDisposabble
        public void Dispose()
        {
            this.OnDispose();
        }
        protected virtual void OnDispose()
        {
        }
        #endregion

        // TODO: Ad IsBussy spinner to main view or each individual view
        // While calling asyc command we can use this property to notiffy UI
        // that other commands are disabled while we wait for async command to finish
        private bool _isWorking;
        public bool IsWorking
        {
            get => _isWorking;
            set
            {
                _isWorking = value;
                OnPropertyChanged(nameof(IsWorking));
            }

        }

        public bool IsNew
        {
            get;
            set;
        }
    }
}