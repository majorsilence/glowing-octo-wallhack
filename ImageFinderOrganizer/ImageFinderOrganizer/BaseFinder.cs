using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageFinderOrganizer
{
    internal abstract class BaseFinder
    {
        private CancellationToken cancellationToken;

        private BaseFinder() { }

        internal BaseFinder(CancellationToken cancellationToken) 
        {
            this.cancellationToken = cancellationToken;
        }

        internal Task Execute(DirectoryInfo searchBase, DirectoryInfo destBase)
        {

            return Task.Run(() => FindAndCopyFiles(searchBase, destBase));

        }

        protected void CopyFile(string srcFile, string targetFolder, int yearTaken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string fileExtension = System.IO.Path.GetExtension(srcFile).ToLower();
            string filename = System.IO.Path.GetFileNameWithoutExtension(srcFile) + fileExtension;
            string destFile = System.IO.Path.Combine(targetFolder, yearTaken.ToString(), filename);

            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(destFile)))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destFile));
            }

            int count = 1;
            while (System.IO.File.Exists(destFile))
            {
                filename = System.IO.Path.GetFileNameWithoutExtension(srcFile)
                    + string.Format("_{0}{1}", count, fileExtension);
                destFile = System.IO.Path.Combine(targetFolder, yearTaken.ToString(), filename);
                count++;
            }
            System.IO.File.Copy(srcFile, destFile, false);

        }

        protected abstract void FindAndCopyFiles(DirectoryInfo source, DirectoryInfo target);

    }
}
