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
namespace ImageFinderOrganizer
{
	public partial class MainForm
	{

        private void ButtonChangeBaseSearch_Click(object sender, EventArgs e)
        {

        }

        private void ButtonChangeBaseOutput_Click(object sender, EventArgs e)
        {

        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = true;
            ToolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            ToolStripStatusLabel1.Text = "Working ...";
            try
            {
                var finder = new ImageFinder();
                await finder.Execute(new System.IO.DirectoryInfo(TextBoxBaseSearch.Text),
                    new System.IO.DirectoryInfo(TextBoxBaseOutput.Text));
            }
            catch(Exception ex)
            {
                MessageBox.Show("Some bad has happened.  " + ex.Message, "Something bad has happend",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ToolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            ToolStripStatusLabel1.Text = "All Done";
            buttonStart.Enabled = true;

        }
    }
}
