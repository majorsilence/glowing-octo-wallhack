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
namespace ImageFinderOrganizer
{
	public class ImageFinder
	{

        
        internal Task Execute(DirectoryInfo searchBase, DirectoryInfo destBase)
        {

            return Task.Run(() => CopyFilesRecursively(searchBase, destBase));

        }


        private void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            }
            foreach (FileInfo file in source.GetFiles())
            {
                
                file.CopyTo(Path.Combine(target.FullName, file.Name), true); //overwrite
            }

          
        }

	}
}
