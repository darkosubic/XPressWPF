using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XPressWPF.Shared.Helpers
{
    public static class ObservableExtensions
    {
        public static ObservableCollection<T> AddToNewObservableCollection<T>(this IEnumerable<T> source)
        {
            ObservableCollection<T> collection = new ObservableCollection<T>();

            foreach (T item in source)
            {
                collection.Add(item);
            }

            return collection;
        }
    }
}
