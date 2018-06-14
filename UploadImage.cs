using ERISCOTools.Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ERISCOTools
{
    public static class UploadImage
    {
        public static string saveImage(ImageCrop image_crop)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(getBase64(image_crop.image_file));
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    Image image = Image.FromStream(ms, true);
                    return uploadAndCrop(image, image_crop);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error pujant la imatge: " + ex.Message);
            }
        }

        private static string uploadAndCrop(Image img, ImageCrop crop)
        {

            int x = System.Math.Min(crop.x1, crop.x2);
            int y = System.Math.Min(crop.y1, crop.y2);

            if (crop.w == 0 || crop.h == 0)
            {
                crop.w = img.Width;
                crop.h = img.Height;
            }

            int final_height = Convert.ToInt32(System.Convert.ToDouble(crop.desired_width) / (System.Convert.ToDouble(crop.w) / System.Convert.ToDouble(crop.h)));

            using (System.Drawing.Bitmap _bitmap = new System.Drawing.Bitmap(crop.desired_width, final_height))
            {
                _bitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);
                using (Graphics _graphic = Graphics.FromImage(_bitmap))
                {
                    _graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    _graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    _graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    _graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    _graphic.DrawImage(img, 0, 0, crop.desired_width, final_height);
                    _graphic.DrawImage(img, new Rectangle(0, 0, crop.desired_width, final_height), x, y, crop.w, crop.h, GraphicsUnit.Pixel);

                    string extension = getExtension(crop.image_file); 
                    var fileName = Path.GetFileName(crop.file_name) + "." + extension;

                    string path = System.Web.Hosting.HostingEnvironment.MapPath(crop.server_directory);

                    string newFullPathName = string.Concat(path, fileName);
                    delete(newFullPathName);

                    using (EncoderParameters encoderParameters = new EncoderParameters(1))
                    {
                        encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                        _bitmap.Save(newFullPathName, GetImageCodec("." + extension), encoderParameters);
                    }
                    return fileName;
                }
            }
        }

        private static string getBase64(string base64)
        {
            var splitted = base64.Split(',');
            if (splitted.Count() <= 1)
            {
                throw new Exception("Format de la imatge incorrecte... ");
            }
            return splitted[splitted.Count() - 1];
        }

        private static string getExtension(string base64)
        {
            var splitted = base64.Split(',');
            if (splitted.Count() != 2)
            {
                throw new Exception("Format de la imatge incorrecte... ");
            }
            var splitted2 = splitted[0].Split('/');
            if (splitted2.Count() != 2)
            {
                throw new Exception("Format de la imatge incorrecte... ");
            }
            var splitted3 = splitted2[1].Split(';');
            if (splitted3.Count() != 2)
            {
                throw new Exception("Format de la imatge incorrecte... ");
            }
            return splitted3[0];
        }

        private static void delete(string url)
        {
            if (System.IO.File.Exists(url))
            {
                System.IO.File.Delete(url);
            }
        }

        private static ImageCodecInfo GetImageCodec(string extension)
        {
            extension = extension.ToUpperInvariant();
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FilenameExtension.Contains(extension))
                {
                    return codec;
                }
            }
            return codecs[1];
        }
    }
}
