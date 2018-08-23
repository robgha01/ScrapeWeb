using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using ScrapeWeb.Core.Helpers;
using ScrapeWeb.Helpers;

namespace ScrapeWeb.ValueConverters
{
    public class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] byteBlob = value as byte[];

            if (byteBlob == null)
            {
                return null;
            }

            ImageCodecInfo myImageCodecInfo = ImageHelper.FindJpegEncoder();
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 90L);

            using (MemoryStream ms = new MemoryStream(byteBlob))
            {
                var bitmap = (Bitmap)Image.FromStream(ms);

                BitmapHelper.LoadBitmap(bitmap);
                return bitmap;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}