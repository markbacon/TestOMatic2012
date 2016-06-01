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
	public partial class Form59 : Form {
		public Form59() {
			InitializeComponent();

			Logger.LoggerWrite += form8_onLoggerWrite;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;


			string nodeDirectoryName = @"\\xdata1\cmsos2\ckenode";

			DirectoryInfo di = new DirectoryInfo(nodeDirectoryName);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {

				Logger.Write("Processing directory: " + directory.Name);
				ProcessDirectory(directory);
			}

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;


			string nodeDirectoryName = @"\\xdata1\cmsos2\ckenode";

			DirectoryInfo di = new DirectoryInfo(nodeDirectoryName);

			List<string> unitList = new List<string>();

			using (StreamReader sr = new StreamReader("C:\\TestData\\MixDestTestUnits.txt")) {

				while (sr.Peek() != -1) {
					unitList.Add("X" + sr.ReadLine().Trim());

				}

			}


			foreach (string unit in unitList) {

				DirectoryInfo directory = di.GetDirectories(unit).First();

				Logger.Write("Processing directory: " + directory.Name);
				//ProcessSmartReportsDirectory(directory);

				FileInfo[] mixDestFiles = directory.GetFiles("*_MixDest.pol");

				foreach (FileInfo mixDestFile in mixDestFiles) {

					string targetPath = Path.Combine("C:\\MixDestTestFiles", directory.Name);

					if (!Directory.Exists(targetPath)) {
						Directory.CreateDirectory(targetPath);
					}

					targetPath = Path.Combine(targetPath, mixDestFile.Name);
					Logger.Write("Copying file: " + targetPath);

					mixDestFile.CopyTo(targetPath, true);


				}



			}

			//DirectoryInfo[] directories = di.GetDirectories("X1*");

			//foreach (DirectoryInfo directory in directories) {

			//}

			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;

			string sourceDirName = @"\\ckecldfnp02\CJRCO_RO";

			DirectoryInfo di = new DirectoryInfo(sourceDirName);

			DirectoryInfo[] directories = di.GetDirectories("X11*");


			foreach (DirectoryInfo directory in directories) {

				if (string.Compare(directory.Name, "X1100512") < 0) {
					continue;
				}

				Logger.Write("Processing directory: " + directory.FullName);
				ProcessArchiveDirectory(directory);

			}

			button3.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button4_Click(object sender, EventArgs e) {

			button4.Enabled = false;

			string sourceDirName = @"\\ckecldfnp02\HFSCO_RO";
			DirectoryInfo di = new DirectoryInfo(sourceDirName);


			List<string> nodeList = new List<string>(); // GetNodeList();
			nodeList.Add("X1505466");

			foreach (string node in nodeList) {

				try {

					DirectoryInfo directory = di.GetDirectories(node).Single();

					DirectoryInfo subDirectory = directory.GetDirectories("20160502").Single();

					FileInfo laborFile = subDirectory.GetFiles("weeklyRingOutLaborSummary20160502.xml").Single();

					string destPath = Path.Combine("C:\\SmartReportsII", node , laborFile.Name);

					Logger.Write("Coping file: " + destPath);
					laborFile.CopyTo(destPath, true);
				}

				catch { }

			}


			button4.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button5_Click(object sender, EventArgs e) {

			button5.Enabled = false;

			string archiveDirName = @"\\xdata1\remoteware\storearchive";

			DirectoryInfo di = new DirectoryInfo(archiveDirName);

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {

				if (directory.Name == "X1100000") {
					continue;
				}

				Logger.Write("Processing directory: " + directory.FullName);

				DateTime businessDate = new DateTime(2016, 5, 17);

				//while (businessDate < DateTime.Today) {

					FileInfo srcFile = directory.GetFiles(businessDate.AddDays(1).ToString("MMdd") + ".zip").SingleOrDefault();

					if (srcFile != null) {
						Logger.Write("Processing file: " + srcFile.FullName);

						//string targetDirectory = Path.Combine(@"\\ckecldfnp02\CJRCO_RO", directory.Name, businessDate.ToString("yyyyMMdd"));
						string targetDirectory = Path.Combine(@"C:\HFSCO_RO", directory.Name);



						using (ZipFile zippy = new ZipFile(srcFile.FullName)) {

							string mixDestSearchString = "transhist";

							foreach (ZipEntry entry in zippy) {

								if (entry.FileName.IndexOf(mixDestSearchString, StringComparison.InvariantCultureIgnoreCase) > -1) {

									Logger.Write("Extracting file: " + entry.FileName);


									if (!Directory.Exists(targetDirectory)) {
										Logger.Write("Creating directory: " + targetDirectory);
										Directory.CreateDirectory(targetDirectory);
									}

									string destFileName = Path.Combine(targetDirectory, entry.FileName);

									entry.Extract(targetDirectory, ExtractExistingFileAction.OverwriteSilently);
								}
							}
						}
					//}
					//businessDate = businessDate.AddDays(1);
				}
			}

			button5.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button6_Click(object sender, EventArgs e) {

			button6.Enabled = false;


			string sourceDirName = @"\\ckecldfnp02\HFSCO_RO";
			//string sourceDirName = @"\\ckecldfnp02\CJRCO_RO";
			DirectoryInfo di = new DirectoryInfo(sourceDirName);


			DirectoryInfo[] directories = di.GetDirectories("X15*");
			//DirectoryInfo[] directories = di.GetDirectories("X11*");


			foreach (DirectoryInfo directory in directories) {

				Logger.Write("Checking directory: " + directory.Name);
			
				DirectoryInfo subDirectory = directory.GetDirectories("20160517").SingleOrDefault();

				if (subDirectory != null) {

					FileInfo tlogFile = subDirectory.GetFiles("20160517.transhist.xml").SingleOrDefault();

					if (tlogFile != null) {

						string targetPath = Path.Combine("C:\\20160517", directory.Name, tlogFile.Name);

						if (!Directory.Exists(Path.GetDirectoryName(targetPath))) {
							Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
						}


						tlogFile.CopyTo(targetPath, true);

					}



				}

			}

			button6.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button7_Click(object sender, EventArgs e) {


			button7.Enabled = false;

			int fileCount = 0;
			int dirCount = 0;

			string sourceDirName = "D:\\xdata1\\ckenode2";
			//string sourceDirName = @"\\ckecldfnp02\CJRCO_RO";
			DirectoryInfo di = new DirectoryInfo(sourceDirName);


			DirectoryInfo[] directories = di.GetDirectories("X15*");
			//DirectoryInfo[] directories = di.GetDirectories("X11*");


			foreach (DirectoryInfo directory in directories) {

				Logger.Write("Checking directory: " + directory.Name);

				fileCount += directory.GetFiles("20160514.transhist.xml").Count();
				dirCount++;

			}

			textBox1.Text = "";

			Logger.Write("File count = " + fileCount.ToString() + "   Directory count: " + dirCount.ToString());

			button7.Enabled = true;


		}
		//---------------------------------------------------------------------------------------------------------
		private List<string> GetNodeList() {

			List<string> nodeList = new List<string>();


			using (StreamReader sr = new StreamReader("C:\\TestData\\PonderUnits.txt")) {

				while (sr.Peek() != -1) {

					nodeList.Add(sr.ReadLine().Trim());


				}
			}

			return nodeList;
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessArchiveDirectory(DirectoryInfo di) {

			string destRootDirName = "D:\\HFSCO_RO";

			DateTime businessDate = new DateTime(2016, 4, 19);

			while (businessDate < DateTime.Today) {

				Logger.Write("Processing sub-directory: " + businessDate.ToString("yyyyMMdd"));

				DirectoryInfo sourceDirectory = di.GetDirectories(businessDate.ToString("yyyyMMdd")).FirstOrDefault();

				if (sourceDirectory != null) {

					if (sourceDirectory.Exists) {


						string destDirName = Path.Combine(destRootDirName, di.Name, businessDate.ToString("yyyyMMdd"));

						if (!Directory.Exists(destDirName)) {
							Directory.CreateDirectory(destDirName);
						}


						FileInfo[] files = sourceDirectory.GetFiles();

						foreach (FileInfo file in files) {

							Logger.Write("Copying file: " + file.FullName);

							string destFilePath = Path.Combine(destDirName, file.Name);

							file.CopyTo(destFilePath, true);
						}
					}
				}

				businessDate = businessDate.AddDays(1);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo di) {

			string targetDirectory = "C:\\MixDestPollFiles";

			string searchPattern = @"inbox\inbox\polldata.zip";


			if (Directory.Exists(Path.Combine(di.FullName, "inbox", "inbox"))) {


				FileInfo file = di.GetFiles(searchPattern).FirstOrDefault();

				if (file != null) {

					if (file.Exists) {

						Logger.Write("Processing zip file: " + file.FullName);

						using (ZipFile zippy = new ZipFile(file.FullName)) {

							string mixDestSearchString = "_MixDest.pol";

							foreach (ZipEntry entry in zippy) {

								if (entry.FileName.IndexOf(mixDestSearchString, StringComparison.InvariantCultureIgnoreCase) > -1) {

									Logger.Write("Extracting file: " + entry.FileName);

									string destFileName = Path.Combine(targetDirectory, entry.FileName);

									entry.Extract(targetDirectory, ExtractExistingFileAction.OverwriteSilently);
								}
							}
						}
					}
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDirectory2(DirectoryInfo di) {

			string targetDirectory = "C:\\FinFiles";

			string searchPattern = @"inbox\inbox\polldata.zip";


			if (Directory.Exists(Path.Combine(di.FullName, "inbox", "inbox"))) {


				FileInfo file = di.GetFiles(searchPattern).FirstOrDefault();

				if (file != null) {

					if (file.Exists) {

						Logger.Write("Processing zip file: " + file.FullName);

						using (ZipFile zippy = new ZipFile(file.FullName)) {

							string mixDestSearchString = "_MixDest.pol";

							foreach (ZipEntry entry in zippy) {

								if (entry.FileName.IndexOf(mixDestSearchString, StringComparison.InvariantCultureIgnoreCase) > -1) {

									Logger.Write("Extracting file: " + entry.FileName);

									string destFileName = Path.Combine(targetDirectory, entry.FileName);

									entry.Extract(targetDirectory, ExtractExistingFileAction.OverwriteSilently);
								}
							}
						}
					}
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessSmartReportsDirectory(DirectoryInfo di) {

			string targetDirectory = Path.Combine("C:\\SmartReportsII", di.Name);

			
			string searchPattern = @"inbox\inbox\SmartRpts*.zip";


			if (Directory.Exists(Path.Combine(di.FullName, "inbox", "inbox"))) {


				FileInfo file = di.GetFiles(searchPattern).FirstOrDefault();

				if (file != null) {

					if (file.Exists) {

						if (!Directory.Exists(targetDirectory)) {
							Directory.CreateDirectory(targetDirectory);
						}


						Logger.Write("Processing zip file: " + file.FullName);

						using (ZipFile zippy = new ZipFile(file.FullName)) {
			
							foreach (ZipEntry entry in zippy) {

								Logger.Write("Extracting file: " + entry.FileName);

								string destFileName = Path.Combine(targetDirectory, entry.FileName);

								entry.Extract(targetDirectory, ExtractExistingFileAction.OverwriteSilently);
							}
						}
					}
				}
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();


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
