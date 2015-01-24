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
using System.Threading;
namespace ImageFinderOrganizer
{
    internal class ImageFinder : BaseFinder
    {

        internal ImageFinder(CancellationToken cancellationToken)
            : base(cancellationToken)
        {
        }

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

                FileInfo fileInfo = new FileInfo(inFullPath);
                DateTime modifiedTime = fileInfo.LastWriteTime;
                DateTime creationTime =  (metaData.DateTaken != null) ? DateTime.Parse(metaData.DateTaken) : DateTime.Now;

                returnDateTime = (creationTime < modifiedTime) ? creationTime : modifiedTime;
                               
            }
            catch
            {
                //do nothing  
            }
            return returnDateTime;
        }

        // *********************

        protected override void FindAndCopyFiles(DirectoryInfo source, DirectoryInfo target)
        {

            var files = SafeWalk.EnumerateFiles(source.FullName, "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".JPG") || s.EndsWith(".jpg") || s.EndsWith(".JPEG") || s.EndsWith(".jpeg"));

            var imageTarget = System.IO.Path.Combine(target.FullName, "Photos");

            foreach (string file in files)
            {
                if (file.Contains(target.FullName))
                {
                    // ignore the target folder with image portion
                    return;
                }

                try
                {
                    DateTime takenDate = GetDateTakenFromImage(file);
                    CopyFile(file, imageTarget, takenDate.Year);
                }
                catch (System.ArgumentException ae)
                {
                    System.Console.WriteLine(ae.Message);
                }
            }

        }

        

    }
}
