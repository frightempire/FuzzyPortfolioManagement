﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FuzzyExpert.WpfClient.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool.TryParse(parameter?.ToString(), out var reversed);
            return reversed ? 
                value == null ? Visibility.Visible : Visibility.Hidden :
                value == null ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}