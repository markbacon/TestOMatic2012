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

			DirectoryInfo di = new DirectoryInfo("D:\\xdata1\\ckenode2");
			//DirectoryInfo di = new DirectoryInfo("C:\\ckenode2");

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {

				ProcessTLogDirectory(directory);
				//ProcessDirectory(directory);
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

			FileInfo[] files = di.GetFiles();

			int existingFileCount = 0;
			int copyCount = 0;


			foreach (FileInfo file in files) {

				if (file != null) {

					string subDirName = file.Name.Substring(0, 8);

					//string targetDirectoryName = Path.Combine("\\\\ckecldfnp02\\HFSCO_RO\\", di.Parent.Name, di.Name);
					string targetDirectoryName = Path.Combine("\\\\ckecldfnp02\\HFSCO_RO\\", di.Name, subDirName);

					if (!Directory.Exists(targetDirectoryName)) {
						Directory.CreateDirectory(targetDirectoryName);
					}

					string targetPath = Path.Combine(targetDirectoryName, file.Name);


					if (!File.Exists(targetPath)) {

						textBox1.Text += "Copying file to:  " + targetPath + "\r\n";
						Application.DoEvents();
						Logger.Write("Copying file to:  " + targetPath);
						//file.CopyTo(targetPath, true);

						copyCount++;

					}
					else {
						textBox1.Text += "File already exists: " + targetPath + "\r\n";
						Application.DoEvents();
						Logger.Write("File already exists: " + targetPath);

						existingFileCount++;
					}


				}

			}

			Logger.Write("Existing file count: " + existingFileCount.ToString());
			Logger.Write("Copied file count: " + copyCount.ToString());
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
