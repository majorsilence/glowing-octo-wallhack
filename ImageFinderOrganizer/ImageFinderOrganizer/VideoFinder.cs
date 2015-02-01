using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageFinderOrganizer
{
    internal class VideoFinder : BaseFinder
    {

        public override event FileOrangizerEventHandler CurrentItem;
        public override event FileOrangizerEventHandler FileCount;

        internal VideoFinder(CancellationToken cancellationToken) 
            : base(cancellationToken)
        {
        }

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
            .Where(s => s.EndsWith(".webm") || s.EndsWith(".WEBM") 
                || s.EndsWith(".MOV") || s.EndsWith(".mov")
                || s.EndsWith(".mpg") || s.EndsWith(".MPG") 
                || s.EndsWith(".MP4") || s.EndsWith(".mp4")
                || s.EndsWith(".dv") || s.EndsWith(".DV")
                || s.EndsWith(".mkv") || s.EndsWith(".MKV")
                || s.EndsWith(".flv") || s.EndsWith(".flv")
                || s.EndsWith(".ogv") || s.EndsWith(".OGV")
                || s.EndsWith(".ogg") || s.EndsWith(".OGG")
                || s.EndsWith(".drc") || s.EndsWith(".DRC")
                || s.EndsWith(".mng") || s.EndsWith(".MNG")
                || s.EndsWith(".avi") || s.EndsWith(".AVI")
                || s.EndsWith(".qt") || s.EndsWith(".QT")
                || s.EndsWith(".wmv") || s.EndsWith(".WMV")
                || s.EndsWith(".yuv") || s.EndsWith(".YUV")
                || s.EndsWith(".rmvb") || s.EndsWith(".RMVB")
                || s.EndsWith(".asf") || s.EndsWith(".ASF")
                || s.EndsWith(".m4p") || s.EndsWith(".M4P")
                || s.EndsWith(".m4v") || s.EndsWith(".M4V")
                || s.EndsWith(".mp2") || s.EndsWith(".MP2")
                || s.EndsWith(".mpeg") || s.EndsWith(".MPEG")
                || s.EndsWith(".mpe") || s.EndsWith(".MPE")
                || s.EndsWith(".mpv") || s.EndsWith(".MPV")
                || s.EndsWith(".m2v") || s.EndsWith(".M2V")
                || s.EndsWith(".svi") || s.EndsWith(".SVI")
                || s.EndsWith(".3gp") || s.EndsWith(".3GP")
                || s.EndsWith(".3g2") || s.EndsWith(".3G2")
                || s.EndsWith(".mxf") || s.EndsWith(".MXF")
                || s.EndsWith(".roq") || s.EndsWith(".ROQ")
                || s.EndsWith(".nsv") || s.EndsWith(".NSV")
                );

            var videoTarget = System.IO.Path.Combine(target.FullName, "Videos");


            int count = files.Count();

            if (FileCount != null)
            {
                this.FileCount(this, new FileOrangizerArgs(count));
            }

            int currentItem = 0;
            foreach (string file in files)
            {
                currentItem++;

                if (CurrentItem != null)
                {
                    this.CurrentItem(this, new FileOrangizerArgs(currentItem));
                }

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
