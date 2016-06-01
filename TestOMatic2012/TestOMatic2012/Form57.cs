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
	public partial class Form57 : Form {
		public Form57() {
			InitializeComponent();

			Logger.LoggerWrite += form8_onLoggerWrite;

			Logger.StartLogSession();

		}
		//---------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			ProcessChangedFiles();


			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();


		}
		//---------------------------------------------------------------------------------------------------
		private List<string> GetChangedFileList() {

			List<string> changedFileList = new List<string>();

			//string filePath = "C:\\Temp\\ChangeMixFiles.txt";

			//using (StreamReader sr = new StreamReader(filePath)) {

			//	while (sr.Peek() != -1) {

			//		string line = sr.ReadLine();

			//		changedFileList.Add(line);
			//	}
			//}

			string dirPath = @"C:\MixDestPollFiles";
			DirectoryInfo di = new DirectoryInfo(dirPath);

			FileInfo[] files = di.GetFiles("X11*_MixDest.pol");

			foreach (FileInfo file in files) {

				changedFileList.Add(file.FullName);
			}

			return changedFileList;
		}
		//---------------------------------------------------------------------------------------------------
		private string GetArchiveFilePath(string nodeName, DateTime fileDate) {

			string filePath = "";
			string archiveDirectory = "";

			archiveDirectory = @"\\xdata1\remoteware\StoreArchive";
				
			//if (fileDate >= new DateTime(2016, 2, 29)) {
				//archiveDirectory = @"D:\RemoteWare\StoreArchive";
			//}
			//else {
			//	archiveDirectory = @"\\ckecldfnp02\Offline\CKE_HFS_SinceJan2016\2016_02";
			//}

			string fileName = fileDate.AddDays(1).ToString("MMdd") + ".zip";

			filePath = Path.Combine(archiveDirectory, nodeName, fileName);

			return filePath;
		}
		//---------------------------------------------------------------------------------------------------
		private void ParseFileName(string fileName, out string nodeName, out DateTime fileDate) {

			nodeName = fileName.Substring(0, 8);
			string temp = fileName.Substring(9, 8);

			fileDate = DateTime.ParseExact(temp, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessChangedFile(string filePath) {

			DateTime startTime = DateTime.Now;
			Logger.Write("Process changed files starting...");

			string nodeName;
			DateTime fileDate;

			string fileName = Path.GetFileName(filePath);
			ParseFileName(fileName, out nodeName, out fileDate);

			FileInfo changedFile = new FileInfo(filePath);

			//nodeName = changedFile.Directory.Name;

			if (changedFile.Exists) {

				string archiveFilePath = GetArchiveFilePath(nodeName, fileDate);

				Logger.Write("Looking for archive: " + archiveFilePath);

				if (File.Exists(archiveFilePath)) {

					Logger.Write("Processing archive:  " + archiveFilePath);

					using (ZipFile zippy = new ZipFile(archiveFilePath)) {

						bool updataArchive = true;

						foreach (ZipEntry entry in zippy.Entries) {

							if (entry.FileName == fileName) {

								string existingText = "";
								string changedText = "";

								using (MemoryStream ms = new MemoryStream()) {
									entry.Extract(ms);

									long foo = ms.Length;

									ms.Seek(0, SeekOrigin.Begin);

									using (StreamReader sr = new StreamReader(ms)) {
										existingText = sr.ReadToEnd();
									}
								}

								using (StreamReader sr = changedFile.OpenText()) {
									changedText = sr.ReadToEnd();
								}


								if (HashOMatic.ComputeHash(existingText) != HashOMatic.ComputeHash(changedText)) {
									Logger.Write("Removing file from atchive.  File name: " + fileName);
									zippy.RemoveEntry(entry.FileName);
								}
								else {
									Logger.Write("File has NOT changed. File name: " + changedFile.Name);
									updataArchive = false;
								}

								break;
							}
						}

						if (updataArchive) {
							Logger.Write("Updating archive: " + archiveFilePath + " with file: " + fileName);
							zippy.AddEntry(fileName, changedFile.OpenRead());
							zippy.Save();
						}
					}
				}
				else {
					Logger.Write("Archive NOT found:  " + archiveFilePath);
				}
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessChangedFiles() {

			List<string> changedFileList = GetChangedFileList();

			foreach (string filePath in changedFileList) {
				Logger.Write("Processing changed  file:  " + filePath);
				ProcessChangedFile(filePath);
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void textBox1_TextChanged(object sender, EventArgs e) {

			if (textBox1.Text.Length > 4024) {
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
