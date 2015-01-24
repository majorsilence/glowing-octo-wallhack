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
	partial class MainForm : System.Windows.Forms.Form
	{

		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try {
				if (disposing && components != null) {
					components.Dispose();
				}
			} finally {
				base.Dispose(disposing);
			}
		}

		//Required by the Windows Form Designer

		private System.ComponentModel.IContainer components;
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.Label1 = new System.Windows.Forms.Label();
            this.TextBoxBaseSearch = new System.Windows.Forms.TextBox();
            this.ButtonChangeBaseSearch = new System.Windows.Forms.Button();
            this.ButtonChangeBaseOutput = new System.Windows.Forms.Button();
            this.TextBoxBaseOutput = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ToolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.ToolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonStart = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.StatusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 24);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(100, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Base Search Folder";
            // 
            // TextBoxBaseSearch
            // 
            this.TextBoxBaseSearch.Location = new System.Drawing.Point(15, 40);
            this.TextBoxBaseSearch.Name = "TextBoxBaseSearch";
            this.TextBoxBaseSearch.Size = new System.Drawing.Size(341, 20);
            this.TextBoxBaseSearch.TabIndex = 1;
            // 
            // ButtonChangeBaseSearch
            // 
            this.ButtonChangeBaseSearch.Location = new System.Drawing.Point(365, 40);
            this.ButtonChangeBaseSearch.Name = "ButtonChangeBaseSearch";
            this.ButtonChangeBaseSearch.Size = new System.Drawing.Size(150, 23);
            this.ButtonChangeBaseSearch.TabIndex = 2;
            this.ButtonChangeBaseSearch.Text = "Change Folder";
            this.ButtonChangeBaseSearch.UseVisualStyleBackColor = true;
            this.ButtonChangeBaseSearch.Click += new System.EventHandler(this.ButtonChangeBaseSearch_Click);
            // 
            // ButtonChangeBaseOutput
            // 
            this.ButtonChangeBaseOutput.Location = new System.Drawing.Point(368, 89);
            this.ButtonChangeBaseOutput.Name = "ButtonChangeBaseOutput";
            this.ButtonChangeBaseOutput.Size = new System.Drawing.Size(150, 23);
            this.ButtonChangeBaseOutput.TabIndex = 5;
            this.ButtonChangeBaseOutput.Text = "Change Folder";
            this.ButtonChangeBaseOutput.UseVisualStyleBackColor = true;
            this.ButtonChangeBaseOutput.Click += new System.EventHandler(this.ButtonChangeBaseOutput_Click);
            // 
            // TextBoxBaseOutput
            // 
            this.TextBoxBaseOutput.Location = new System.Drawing.Point(18, 89);
            this.TextBoxBaseOutput.Name = "TextBoxBaseOutput";
            this.TextBoxBaseOutput.Size = new System.Drawing.Size(341, 20);
            this.TextBoxBaseOutput.TabIndex = 4;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(15, 73);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(98, 13);
            this.Label2.TabIndex = 3;
            this.Label2.Text = "Base Output Folder";
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripProgressBar1,
            this.ToolStripStatusLabel1});
            this.StatusStrip1.Location = new System.Drawing.Point(0, 170);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new System.Drawing.Size(554, 22);
            this.StatusStrip1.TabIndex = 6;
            this.StatusStrip1.Text = "StatusStrip1";
            // 
            // ToolStripProgressBar1
            // 
            this.ToolStripProgressBar1.Name = "ToolStripProgressBar1";
            this.ToolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // ToolStripStatusLabel1
            // 
            this.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1";
            this.ToolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(99, 126);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 7;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Location = new System.Drawing.Point(18, 126);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 192);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.StatusStrip1);
            this.Controls.Add(this.ButtonChangeBaseOutput);
            this.Controls.Add(this.TextBoxBaseOutput);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.ButtonChangeBaseSearch);
            this.Controls.Add(this.TextBoxBaseSearch);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.Text = "Find and Organize Images";
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.TextBox TextBoxBaseSearch;
		internal System.Windows.Forms.Button ButtonChangeBaseSearch;
		internal System.Windows.Forms.Button ButtonChangeBaseOutput;
		internal System.Windows.Forms.TextBox TextBoxBaseOutput;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.StatusStrip StatusStrip1;
		internal System.Windows.Forms.ToolStripProgressBar ToolStripProgressBar1;

		internal System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel1;
		public MainForm()
		{
			InitializeComponent();
		}

        private Button buttonStart;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button buttonCancel;
	}
}
