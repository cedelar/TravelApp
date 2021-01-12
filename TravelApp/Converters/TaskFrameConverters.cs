using System;
using TravelApp.Models;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace TravelApp.Converters
{
    class PriorityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var priority = (Priority) value;
            switch (priority)
            {
                case Priority.Low:
                    return Colors.Green;
                case Priority.Normal:
                    return Colors.Yellow;
                case Priority.High:
                    return Colors.Orange;
                case Priority.VeryHigh:
                    return Colors.Red;
            }
            return Colors.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return Priority.Normal;
        }
    }
}
