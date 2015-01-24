using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFinderOrganizer
{
    internal class VideoFinder : BaseFinder
    {

        private DateTime GetDateTakenFromVideo(string inFullPath)
        {
            DateTime returnDateTime = DateTime.MinValue;
            try
            {
                FileInfo fileInfo = new FileInfo(inFullPath);
                DateTime creationTime = fileInfo.CreationTime;
                DateTime modifiedTime = fileInfo.LastWriteTime;

                returnDateTime = (creationTime < modifiedTime) ? creationTime : modifiedTime;
            }
            catch
            {
                //do nothing  
            }
            return returnDateTime;
        }



        protected override void FindAndCopyFiles(DirectoryInfo source, DirectoryInfo target)
        {

            var files = SafeWalk.EnumerateFiles(source.FullName, "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".webm") || s.EndsWith(".WEBM") || s.EndsWith(".MOV") || s.EndsWith(".mov")
             || s.EndsWith(".mpg") || s.EndsWith(".MPG") || s.EndsWith(".MP4") || s.EndsWith(".mp4")
              || s.EndsWith(".dv") || s.EndsWith(".DV"));

            var videoTarget = System.IO.Path.Combine(target.FullName, "Videos");

            foreach (string file in files)
            {
                if (file.Contains(target.FullName))
                {
                    // ignore the target folder without the video portion
                    return;
                }

                try
                {
                    DateTime takenDate = GetDateTakenFromVideo(file);
                    CopyFile(file, videoTarget, takenDate.Year);
                }
                catch (System.ArgumentException ae)
                {
                    System.Console.WriteLine(ae.Message);
                }
            }

        }



    }
}
