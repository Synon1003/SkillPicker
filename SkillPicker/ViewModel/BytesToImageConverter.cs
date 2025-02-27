using System.Globalization;

namespace SkillPicker.ViewModel
{
    public class BytesToImageConverter: IValueConverter
    {
        public object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value == null)
                return Binding.DoNothing;

            var bytes = (byte[])value;

            try
            {
                var streamSource = ImageSource.FromStream(() => new MemoryStream(bytes));

                return streamSource;
            }
            catch
            {
                return Binding.DoNothing;
            }
        }

        public object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
