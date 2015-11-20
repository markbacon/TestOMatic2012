using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Ionic.Zip;

namespace TestOMatic2012 {
	public partial class Form42 : Form {
		public Form42() {
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			string filePath = "C:\\PollData\\install_qsrkds_onT1.exe";

			using (ZipFile zippy = new ZipFile(filePath)) {

				string extractDirectory = "C:\\PollData\\Work";

				zippy.ExtractAll(extractDirectory);
			}

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			DirectoryInfo di = new DirectoryInfo("C:\\Temp\\TLogs");

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {


				ProcessDirectory(directory);
			}


			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo di) {

			DirectoryInfo[] directories = di.GetDirectories();

			foreach (DirectoryInfo directory in directories) {

				ProcessTLogDirectory(directory);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessTLogDirectory(DirectoryInfo di) {

			FileInfo file = di.GetFiles().FirstOrDefault();

			if (file != null) {

				string targetDirectoryName = Path.Combine("\\\\ckecldfnp02\\HFSCO_RO\\", di.Parent.Name, di.Name);

				if (!Directory.Exists(targetDirectoryName)) {
					Directory.CreateDirectory(targetDirectoryName);
				}

				string targetPath = Path.Combine(targetDirectoryName, file.Name);

				textBox1.Text += "Copying file to:  " + targetPath + "\r\n";
				Application.DoEvents();

				file.CopyTo(targetPath, true);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void textBox1_TextChanged(object sender, EventArgs e) {

			if (textBox1.Text.Length > 2024) {
				textBox1.Text = "";
			}

			if (textBox1.Text.Length > 0) {
				textBox1.SelectionStart = textBox1.Text.Length - 1;
				textBox1.ScrollToCaret();
				Application.DoEvents();
			}
		}
	}
}
