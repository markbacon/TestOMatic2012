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
	public partial class Form17 : Form {
		public Form17() {
			InitializeComponent();

			Logger.LoggerWrite += form8_onLoggerWrite;

		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();
		}
		//---------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			try {


				DirectoryInfo di = new DirectoryInfo(AppSettings.LocalLaborHfsDirectory);

				FileInfo[] files = di.GetFiles("*MixDest.pol");

				foreach (FileInfo file in files) {

					Logger.Write("Begin processing file:  " + file.Name);
					ProcessMixDestPollFile(file);


				}
			}

			catch (Exception ex) {
				Logger.Write("An exception occured while updting zip file archive.  Please see error log for details.");
				Logger.WriteError(ex);

			}


			button1.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------
		private DateTime GetDateFromFileName(string fileName) {

			string temp = fileName.Substring(9, 8);

			DateTime fileDate  = DateTime.ParseExact(temp, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
			
			return fileDate;
		}
		//---------------------------------------------------------------------------------------------------
		private string GetUnitFromFileName(string fileName) {

			string unitNumber = fileName.Substring(0, 8);

			return unitNumber;
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessLaborHfsFile(FileInfo laborHfsFile) {

			string unitNumber = GetUnitFromFileName(laborHfsFile.Name);
			DateTime fileDate = GetDateFromFileName(laborHfsFile.Name).AddDays(1);

			string directoryName = Path.Combine(AppSettings.StoreArchiveDirectory, unitNumber);

			DirectoryInfo di = new DirectoryInfo(directoryName);

			if (di.Exists) {

				string zipFileName = fileDate.Month.ToString("00") + fileDate.Day.ToString("00") + ".zip";

				FileInfo zipArchive = di.GetFiles(zipFileName).FirstOrDefault();

				if (zipArchive != null) {

					using (ZipFile zippy = ZipFile.Read(zipArchive.FullName)) {

						if (!zippy.ContainsEntry(laborHfsFile.Name)) {
							Logger.Write("Adding file to zip archive.");
							zippy.AddFile(laborHfsFile.FullName, "");
							zippy.Save();
						}
						else {
							Logger.Write("File already exists zip archive.");
						}

					}
				}
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessMixDestPollFile(FileInfo mixDestPollFile) {

			string unitNumber = GetUnitFromFileName(mixDestPollFile.Name);
			DateTime fileDate = GetDateFromFileName(mixDestPollFile.Name).AddDays(1);

			string directoryName = Path.Combine(AppSettings.StoreArchiveDirectory, unitNumber);

			DirectoryInfo di = new DirectoryInfo(directoryName);

			if (di.Exists) {

				string zipFileName = fileDate.Month.ToString("00") + fileDate.Day.ToString("00") + ".zip";

				FileInfo zipArchive = di.GetFiles(zipFileName).FirstOrDefault();

				if (zipArchive != null) {

					using (ZipFile zippy = ZipFile.Read(zipArchive.FullName)) {

						if (zippy.ContainsEntry(mixDestPollFile.Name)) {
							Logger.Write("Updating file in zip archive.");
							zippy.UpdateFile(mixDestPollFile.FullName, "");
						}
						else {
							Logger.Write("Adding file to zip archive.");
							zippy.AddFile(mixDestPollFile.FullName, "");
						}
						zippy.Save();
					}
				}
			}
		}
	}
}
