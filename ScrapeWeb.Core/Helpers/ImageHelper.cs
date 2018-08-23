using System.Diagnostics;
using System.Drawing.Imaging;

namespace ScrapeWeb.Core.Helpers
{
    public static class ImageHelper
    {
        public static ImageCodecInfo FindJpegEncoder()
        {
            // find jpeg encode text
            foreach (ImageCodecInfo info in ImageCodecInfo.GetImageEncoders())
            {
                if (info.FormatID.Equals(ImageFormat.Jpeg.Guid))
                {
                    return info;
                }
            }

            Debug.Fail("Fail to find jPeg Encoder!");
            return null;
        }
    }
}
