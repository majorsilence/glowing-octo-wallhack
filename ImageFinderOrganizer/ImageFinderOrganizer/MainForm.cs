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
            var result = folderBrowserDialog1.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                TextBoxBaseSearch.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void ButtonChangeBaseOutput_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                TextBoxBaseOutput.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = true;
            ToolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            ToolStripStatusLabel1.Text = "Working ...";
            try
            {
                var iFinder = new ImageFinder();
                ToolStripStatusLabel1.Text = "Copying Images ...";
                await iFinder.Execute(new System.IO.DirectoryInfo(TextBoxBaseSearch.Text),
                    new System.IO.DirectoryInfo(TextBoxBaseOutput.Text));

                var vFinder = new VideoFinder();

                ToolStripStatusLabel1.Text = "Copying Videos ...";
                await vFinder.Execute(new System.IO.DirectoryInfo(TextBoxBaseSearch.Text),
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
