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

                fileName = fileName.Replace("%ext%", "png");

                int c = 1;
                while (File.Exists(fileName.Replace("%id%", c.ToString().Equals("1") ? "" : "-" + c.ToString()))) c++;

                i.Save(fileName.Replace("%id%", c.ToString().Equals("1") ? "" : "-" + c.ToString()), ImageFormat.Png);
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
    }
}
