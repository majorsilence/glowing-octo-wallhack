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
using System.Windows.Media.Imaging;
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
        private DateTime GetDateTakenFromImage(string inFullPath)
        {
            DateTime returnDateTime = DateTime.MinValue;
            try
            {
                FileStream picStream = new FileStream(inFullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                BitmapSource bitSource = BitmapFrame.Create(picStream);
                picStream.Close();
                BitmapMetadata metaData = (BitmapMetadata)bitSource.Metadata;
                returnDateTime = DateTime.Parse(metaData.DateTaken);
            }
            catch
            {
                //do nothing  
            }
            return returnDateTime;
        }

        // *********************

        private void FindAndCopyFiles(DirectoryInfo source, DirectoryInfo target)
        {

            var files = SafeWalk.EnumerateFiles(source.FullName, "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".JPG") || s.EndsWith(".jpg") || s.EndsWith(".JPEG") || s.EndsWith(".jpeg"));

            foreach (string file in files)
            {
                if (file.Contains(target.FullName))
                {
                    // ignore the target folder
                    return;
                }

                try
                {
                    DateTime takenDate = GetDateTakenFromImage(file);
                    CopyFile(file, target.FullName, takenDate.Year);
                }
                catch (System.ArgumentException ae)
                {
                    System.Console.WriteLine(ae.Message);
                }
            }

        }

        private void CopyFile(string srcFile, string targetFolder, int yearTaken)
        {

            string filename = System.IO.Path.GetFileNameWithoutExtension(srcFile) + ".jpg";
            string destFile = System.IO.Path.Combine(targetFolder, yearTaken.ToString(), filename);

            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(destFile)))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destFile));
            }

            int count = 1;
            while (System.IO.File.Exists(destFile))
            {
                filename = System.IO.Path.GetFileNameWithoutExtension(srcFile)
                    + string.Format("_{0}.jpg", count);
                destFile = System.IO.Path.Combine(targetFolder, yearTaken.ToString(), filename);
                count++;
            }
            System.IO.File.Copy(srcFile, destFile, false);

        }

    }
}
