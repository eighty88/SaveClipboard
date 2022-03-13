using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Specialized;
using System.Drawing.Imaging;

namespace SaveClipboard
{
    internal class SaveClipboard
    {
        string PathToSave;
        public SaveClipboard(string PathToSave)
        {
            this.PathToSave = PathToSave;
        }

        public void save()
        {
            if (!Directory.Exists(PathToSave))
            {
                Directory.CreateDirectory(PathToSave);
            }

            string fileName = PathToSave + DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss") + "%id%.%ext%";

            if (Clipboard.ContainsImage())
            {
                Image i = Clipboard.GetImage();

                fileName = fileName.Replace("%ext%", GetImageExtension(i));

                int c = 1;
                while (File.Exists(fileName.Replace("%id%", c.ToString().Equals("1") ? "" : "-" + c.ToString()))) c++;

                i.Save(fileName.Replace("%id%", c.ToString().Equals("1") ? "" : "-" + c.ToString()));
            }
            else if (Clipboard.ContainsText())
            {
                string s = Clipboard.GetText();

                fileName = fileName.Replace("%ext%", "txt");

                int c = 1;
                while (File.Exists(fileName.Replace("%id%", c.ToString().Equals("1") ? "" : "-" + c.ToString()))) c++;

                File.WriteAllText(fileName.Replace("%id%", c.ToString().Equals("1") ? "" : "-" + c.ToString()), s);
            }
        }

        private static string GetImageExtension(Image image)
        {
            if (image.RawFormat.Equals(ImageFormat.Jpeg))
                return "jpg";
            if (image.RawFormat.Equals(ImageFormat.Bmp))
                return "bmp";
            if (image.RawFormat.Equals(ImageFormat.Png))
                return "png";
            if (image.RawFormat.Equals(ImageFormat.Emf))
                return "emf";
            if (image.RawFormat.Equals(ImageFormat.Exif))
                return "exif";
            if (image.RawFormat.Equals(ImageFormat.Gif))
                return "gif";
            if (image.RawFormat.Equals(ImageFormat.Icon))
                return "ico";
            if (image.RawFormat.Equals(ImageFormat.MemoryBmp))
                return "bmp";
            if (image.RawFormat.Equals(ImageFormat.Tiff))
                return "tiff";
            else
                return "wmf";
        }
    }
}
