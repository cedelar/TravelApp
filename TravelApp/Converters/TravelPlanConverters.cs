using System;
using Windows.UI.Xaml.Data;

namespace TravelApp.Converters
{
    public class TitleFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var name = value as string;
            return name + "'s travelplans";
         
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class StartdateFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime date = (DateTime) value;
            if (date.Equals(new DateTime()))
            {
                return "Startdate: Unknown";
            }
            return "Startdate: " + date.ToShortDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class EnddateFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime date = (DateTime)value;
            if(date.Equals(new DateTime()))
            {
                return "Enddate: Unknown";
            }
            return "Enddate: " + date.ToShortDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class DestinationFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var destination = value as string;
            if (destination == "-")
            {
                return "Destination: Unknown";
            }
            return "Destination: " + destination;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

}
