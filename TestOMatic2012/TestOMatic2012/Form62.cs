using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Ionic.Zip;

namespace TestOMatic2012 {
	public partial class Form62 : Form {
		public Form62() {
			InitializeComponent();


			Logger.LoggerWrite += form8_onLoggerWrite;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			TaskProcessor tp = new TaskProcessor();

			string dirName = "C:\\MixPollFiles";

			DirectoryInfo di = new DirectoryInfo(dirName);

			DirectoryInfo[] directories = di.GetDirectories();

			foreach (DirectoryInfo directory in directories) {

				Logger.Write("Checking directory: " + directory.Name);

				if (directory.GetFiles("*MixDest.pol").Count() > 0) {

					//Use DOS command to rename file
					string commandName = "Cmd.exe";
					string commandArgs = "/C ren " + directory.FullName + "\\*MixDest.pol *Mix.pol ";


					DateTime startTime = DateTime.Now;
					Logger.Write("Renaming files!");
					tp.RunProgram(commandName, commandArgs);
					Logger.Write("Finished renaming files. Elapsed time: " + (DateTime.Now - startTime).ToString());

				}
				else {
					Logger.Write("No mix dest files found for: " + directory.FullName);

				}




				//FileInfo[] files = directory.GetFiles("*MixDest.pol");

				//foreach (FileInfo file in files) {

				//	string fileName = file.Name.ToLower().Replace("mixdest.pol", "Mix.pol");

				//	string filePath = Path.Combine(directory.FullName, fileName);
				//	Logger.Write("Copying file: " + file.FullName);

				//	if (!File.Exists(filePath)) {
				//		file.CopyTo(filePath);
				//	}
				//	else {
				//		Logger.Write("File already exists! Name: " + filePath);

				//	}


				//		file.Delete();


				//}

			}





			button1.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			string dirName = "C:\\MixPollFiles";

			DirectoryInfo di = new DirectoryInfo(dirName);

			DateTime businessDate = Convert.ToDateTime("1/1/2015");

			while (businessDate < DateTime.Today) {
				Logger.Write("Beging processing business date: " + businessDate.ToString("MM/dd/yyyy"));

				ProcessFilesForDate(di, businessDate);
				businessDate = businessDate.AddDays(1);
			}




			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessFilesForDate(DirectoryInfo di, DateTime businessDate) {

			string businessDateString = businessDate.ToString("yyyyMMdd");


			string zipFilePath = Path.Combine("C:\\MixZipFiles", "MixFiles_" + businessDateString + ".zip");

			using (ZipFile zippy = new ZipFile(zipFilePath)) {

				DirectoryInfo[] directories = di.GetDirectories();

				foreach (DirectoryInfo directory in directories) {

					Logger.Write("Checking directory: " + directory.FullName);

					string searchPattern = directory.Name + "_" + businessDateString + "_Mix.pol";
					FileInfo file = directory.GetFiles(searchPattern).SingleOrDefault();

					if (file != null) {
						Logger.Write("Adding file to archive: " + file.FullName);
						zippy.AddEntry(file.Name, file.OpenRead());
					}

					else {
						Logger.Write("File not found for unit " + directory.Name + " and business date " + businessDate.ToString("MM/dd/yyyy"));
					}
				}

				zippy.Save();
			}
		}
		//---------------------------------------------------------------------------------------------------
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
