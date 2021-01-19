using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace MobileSoft.Common
{
    public static class ImageExtension
    {
        /// <summary>
        /// 转换为渐进式JPEG图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="quality">图片质量，限制100</param>
        public static Image ToProgressive(this Image image, long quality = 100L)
        {
            if (image == null)
            {
                return null;
            }

            ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders().First(c => c.MimeType == "image/jpeg");

            EncoderParameters parameters = new EncoderParameters(3);
            parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (quality > 100 ? 100 : quality));
            parameters.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.ScanMethod, (int)EncoderValue.ScanMethodInterlaced);
            parameters.Param[2] = new EncoderParameter(System.Drawing.Imaging.Encoder.RenderMethod, (int)EncoderValue.RenderProgressive);

            using (Stream stream = new MemoryStream())
            {
                image.Save(stream, codec, parameters);

                return Image.FromStream(stream);
            }
        }
    }
}
