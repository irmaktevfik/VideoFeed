using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace VideoApp.Business.Helpers
{
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this List<T> col)
        {
            return new ObservableCollection<T>(col);
        }
    }
}
