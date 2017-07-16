using System;
using System.ComponentModel;

namespace XPressWPF.Modules.Tests.Helpers
{
    public static class NotifyPropertyChangedHelper
    {
        public static bool IsPropertyChangedRaised(this INotifyPropertyChanged notifyPropertyChanged,
            Action action,
            string propertyName)
        {
            bool isFired = false;
            notifyPropertyChanged.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == propertyName)
                {
                    isFired = true;
                }
            };

            action();

            return isFired;
        }
    }
}
