using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LagosArch.Extensions
{
    public static class ObservableExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> models)
        {
            ObservableCollection<T> collection = new ObservableCollection<T>();
            foreach (T model in models)
            {
                collection.Add(model);
            }
            return collection;
        }
    }
}
