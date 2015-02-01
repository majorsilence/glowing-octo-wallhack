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
using System.Threading;
namespace ImageFinderOrganizer
{
    public partial class MainForm
    {

        CancellationTokenSource videoSource;
        CancellationTokenSource imageSource;

        private void ButtonChangeBaseSearch_Click(object sender, EventArgs e)
        {

            var result = folderBrowserDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
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

        private int processingCount = 0;
        private async void buttonStart_Click(object sender, EventArgs e)
        {
            imageSource = new CancellationTokenSource();
            videoSource = new CancellationTokenSource();

            buttonCancel.Enabled = true;
            buttonStart.Enabled = false;
            ToolStripStatusLabel1.Text = "Working ...";
            try
            {

                await ProcessImages();


                await ProcessVideos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Some bad has happened.  " + ex.Message, "Something bad has happend",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ToolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            ToolStripStatusLabel1.Text = "All Done";
            buttonStart.Enabled = true;
            buttonCancel.Enabled = false;

        }

        private async Task ProcessImages()
        {
            var iFinder = new ImageFinder(imageSource.Token);
            ToolStripStatusLabel1.Text = "Copying Images ...";
            iFinder.FileCount += (s1, e1) =>
            {
                processingCount = e1.Value;
            };
            iFinder.CurrentItem += (s1, e1) =>
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    ToolStripStatusLabel1.Text = string.Format("Image {0} of {1}", e1.Value, processingCount);
                });


                int percent = (processingCount / e1.Value) * 100;
                if (percent > 100)
                {
                    percent = 100;
                }
                if (percent < 0)
                {
                    percent = 0;
                }
                this.BeginInvoke((MethodInvoker)delegate
                {
                    ToolStripProgressBar1.Value = percent;
                });

            };
            await iFinder.Execute(new System.IO.DirectoryInfo(TextBoxBaseSearch.Text),
                new System.IO.DirectoryInfo(TextBoxBaseOutput.Text));
        }

        private async Task ProcessVideos()
        {
            if (!imageSource.IsCancellationRequested)
            {
                var vFinder = new VideoFinder(videoSource.Token);

                ToolStripStatusLabel1.Text = "Copying Videos ...";

                vFinder.FileCount += (s1, e1) =>
                {
                    processingCount = e1.Value;
                };
                vFinder.CurrentItem += (s1, e1) =>
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        ToolStripStatusLabel1.Text = string.Format("Video {0} of {1}", e1.Value, processingCount);
                    });
                    int percent = (processingCount / e1.Value) * 100;
                    if (percent > 100)
                    {
                        percent = 100;
                    }
                    if (percent < 0)
                    {
                        percent = 0;
                    }
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        ToolStripProgressBar1.Value = percent;
                    });
                };

                await vFinder.Execute(new System.IO.DirectoryInfo(TextBoxBaseSearch.Text),
                    new System.IO.DirectoryInfo(TextBoxBaseOutput.Text));
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            imageSource.Cancel();
            videoSource.Cancel();
        }
    }
}
