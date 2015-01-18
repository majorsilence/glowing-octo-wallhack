using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.Text;
namespace ImageFinderOrganizer
{
    public class ImageFinder
    {


        internal Task Execute(DirectoryInfo searchBase, DirectoryInfo destBase)
        {

            return Task.Run(() => FindAndCopyFiles(searchBase, destBase));

        }

        // *********************
        // See http://stackoverflow.com/questions/180030/how-can-i-find-out-when-a-picture-was-actually-taken-in-c-sharp-running-on-vista 
        //we init this once so that if the function is repeatedly called
        //it isn't stressing the garbage man
        private static Regex r = new Regex(":");

        //retrieves the datetime WITHOUT loading the whole image
        private static DateTime GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
        }

        // *********************

        private void FindAndCopyFiles(DirectoryInfo source, DirectoryInfo target)
        {

            var files = Directory.EnumerateFiles(source.FullName, "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".JPG") || s.EndsWith(".jpg") || s.EndsWith(".JPEG") || s.EndsWith(".jpeg"));

            foreach (string file in files)
            {
                DateTime takenDate = GetDateTakenFromImage(file);
                CopyFile(file, target.FullName, takenDate.Year);
            }

        }

        private void CopyFile(string srcFile, string targetFolder, int yearTaken)
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension(srcFile) + ".jpg";
            string destFile = System.IO.Path.Combine(targetFolder, yearTaken.ToString(), filename);

            int count = 1;
            while (System.IO.File.Exists(destFile))
            {
                filename = System.IO.Path.GetFileNameWithoutExtension(srcFile) 
                    + string.Format("_{1}.jpg", count);
                destFile = System.IO.Path.Combine(targetFolder, yearTaken.ToString(), filename);
                count++;
            }
            System.IO.File.Copy(srcFile, destFile, false);

        }

    }
}
