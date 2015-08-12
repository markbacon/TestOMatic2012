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
	public partial class Form20 : Form {
		public Form20() {

			InitializeComponent();

			Logger.LoggerWrite += form20_onLoggerWrite;

		}
		//---------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			string dirPath = @"d:\xdata1\remoteware\storeArchive";

			DirectoryInfo di = new DirectoryInfo(dirPath);

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {

				DateTime startTime = DateTime.Now;

				Logger.Write("Begin processing directory:  " + directory.FullName);

				ProcessDirectory(directory);

				Logger.Write("Directory processing completed.  Elapsed time: " + (DateTime.Now - startTime).ToString());
			}

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void form20_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessFile(FileInfo fi) {


			using (ZipFile zippy = new ZipFile(fi.FullName)) {

				//string targetDirectory = @"\\anadevbatch\Prod\Testing\ckenode\\" + file.Directory.Name;
				string targetDirectory = @"D:\xdata1\cmsos2\ckenode\" + fi.Directory.Name; // +"\\mtier\\SalesLabor";

				if (!Directory.Exists(targetDirectory)) {
					Directory.CreateDirectory(targetDirectory);
				}

				string laborFileSearchString = "_LaborHfs.csv";

				bool fileFound = false;

				foreach (ZipEntry entry in zippy) {

					if (entry.FileName.IndexOf(laborFileSearchString) > -1) {

						string destFileName = Path.Combine(targetDirectory, entry.FileName);

						entry.Extract(targetDirectory, ExtractExistingFileAction.OverwriteSilently);

						fileFound = true;
					}
				}


				if (fileFound) {
					Logger.Write("Labor file found in zip archive:  " + fi.FullName);
				}
				else {
					Logger.Write("Labor NOT file found in zip archive:  " + fi.FullName);
				}
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo di) {

			FileInfo[] files = di.GetFiles("*.zip");

			foreach (FileInfo file in files) {

				ProcessFile(file);

			}
		}
		//---------------------------------------------------------------------------------------------------
		private void textBox1_TextChanged(object sender, EventArgs e) {

			if (textBox1.Text.Length > 2048) {
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
