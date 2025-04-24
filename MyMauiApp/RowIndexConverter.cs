using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MyMauiApp.Converters
{
    public class RowIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var collectionView = parameter as CollectionView;
            var item = value;

            if (collectionView != null && item != null)
            {
                int index = collectionView.ItemsSource.Cast<object>().ToList().IndexOf(item);
                return index % 2 == 0 ? Colors.LightGray : Colors.LightYellow;
            }

            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
