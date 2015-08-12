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
	public partial class Form14 : Form {
		public Form14() {
			InitializeComponent();

			Logger.LoggerWrite += form8_onLoggerWrite;
		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			string sourceDirectory = @"\\ckeanafnp01\CJRCO_RO";
			string targetDirectory = @"D:\TLogsIII";

			if (!Directory.Exists(targetDirectory)) {
				Directory.CreateDirectory(targetDirectory);
			}

			DirectoryInfo di = new DirectoryInfo(sourceDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {

				//if (string.Compare(directory.Name, "X1506805") > 0) {

				DateTime startTime = DateTime.Now;
				Logger.Write("Begin copying files for:  " + directory.Name);

				//if (string.Compare(directory.Name, "X1100412", true) < 0) {
				//	continue;
				//}

				//DateTime startDate = DateTime.Today.AddYears(-4);
				//DateTime endDate = DateTime.Today;

				DateTime startDate = new DateTime(2015, 7, 9);
				DateTime endDate = new DateTime(2015, 7, 11);


				while (startDate <= endDate) {

					string searchPattern = startDate.ToString("yyyyMMdd");

					startDate = startDate.AddDays(1);

					DirectoryInfo[] subDirectories = directory.GetDirectories(searchPattern);

					foreach (DirectoryInfo subDirectory in subDirectories) {


						FileInfo[] files = subDirectory.GetFiles("????????.transhist.xml");

						foreach (FileInfo file in files) {

							if (Convert.ToInt32(file.Name.Substring(0, 8)) >= 20111001) {



								string targetPath = Path.Combine(targetDirectory, directory.Name);

								if (!Directory.Exists(targetPath)) {
									Directory.CreateDirectory(targetPath);
								}

								targetPath = Path.Combine(targetPath, file.Name);

								if (!File.Exists(targetPath)) {

									textBox1.Text += "Copying file: " + file.FullName + ".\r\n";
									Application.DoEvents();
									file.CopyTo(targetPath, true);

								}
								else {
									textBox1.Text += "File: " + targetPath + "already exists and will not be copied.\r\n";
									Application.DoEvents();
								}
							}
						}
					}
				}

				Logger.Write("Finished copying files for: " + directory.Name + ". Elapsed Time:  " + (DateTime.Now - startTime).ToString());

				//	startTime = DateTime.Now;
				//	Logger.Write("Begin creating zip file");

				//	DirectoryInfo localDI = new DirectoryInfo(Path.Combine(targetDirectory, directory.Name));

				//	if (localDI.Exists) {


				//		using (ZipFile zippy = new ZipFile(directory.Name + ".zip")) {

				//			FileInfo[] files = localDI.GetFiles("*.*");

				//			foreach (FileInfo file in files) {




				//				zippy.AddFile(file.FullName, "");
				//			}

				//			string targetPath = Path.Combine(targetDirectory, zippy.Name);
				//			zippy.Save(targetPath);
				//		}

				//		Logger.Write("Finished creating zip file for: " + directory.Name + ". Elapsed Time:  " + (DateTime.Now - startTime).ToString());


				//		FileInfo[] files2 = localDI.GetFiles("*.*");

				//		foreach (FileInfo file in files2) {

				//			file.Delete();
				//		}
				//	}
				//}
				//}

				//sourceDirectory = @"\\ckeanafnp01\HFSCO_RO";
				//targetDirectory = @"D:\TLogsIII";

				//if (!Directory.Exists(targetDirectory)) {
				//	Directory.CreateDirectory(targetDirectory);
				//}

				//di = new DirectoryInfo(sourceDirectory);

				//directories = di.GetDirectories("X15*");

				//foreach (DirectoryInfo directory in directories) {

				//	if (string.Compare(directory.Name, "X1500150") >= 0) {

				//		DateTime startTime = DateTime.Now;
				//		Logger.Write("Begin copying files for:  " + directory.Name);

				//		//if (string.Compare(directory.Name, "X1100412", true) < 0) {
				//		//	continue;
				//		//}

				//		DateTime startDate = DateTime.Today.AddYears(-4);
				//		DateTime endDate = DateTime.Today;


				//		while (startDate < endDate) {

				//			string searchPattern = startDate.ToString("yyyyMMdd");

				//			startDate = startDate.AddDays(1);

				//			DirectoryInfo[] subDirectories = directory.GetDirectories(searchPattern);

				//			foreach (DirectoryInfo subDirectory in subDirectories) {


				//				FileInfo[] files = subDirectory.GetFiles("????????.transhist.xml");

				//				foreach (FileInfo file in files) {

				//					string targetPath = Path.Combine(targetDirectory, directory.Name);

				//					if (!Directory.Exists(targetPath)) {
				//						Directory.CreateDirectory(targetPath);
				//					}

				//					targetPath = Path.Combine(targetPath, file.Name);

				//					if (!File.Exists(targetPath)) {

				//						textBox1.Text += "Copying file: " + file.FullName + ".\r\n";
				//						Application.DoEvents();
				//						file.CopyTo(targetPath, true);

				//					}
				//					else {
				//						textBox1.Text += "File: " + targetPath + "already exists and will not be copied.\r\n";
				//						Application.DoEvents();
				//					}
				//				}
				//			}
				//		}

				//		Logger.Write("Finished copying files for: " + directory.Name + ". Elapsed Time:  " + (DateTime.Now - startTime).ToString());

				//		startTime = DateTime.Now;
				//		Logger.Write("Begin creating zip file");

				//		DirectoryInfo localDI = new DirectoryInfo(Path.Combine(targetDirectory, directory.Name));

				//		if (localDI.Exists) {


				//			using (ZipFile zippy = new ZipFile(directory.Name + ".zip")) {

				//				FileInfo[] files = localDI.GetFiles("*.*");

				//				foreach (FileInfo file in files) {

				//					zippy.AddFile(file.FullName, "");
				//				}

				//				string targetPath = Path.Combine(targetDirectory, zippy.Name);
				//				zippy.Save(targetPath);
				//			}

				//			Logger.Write("Finished creating zip file for: " + directory.Name + ". Elapsed Time:  " + (DateTime.Now - startTime).ToString());


				//			FileInfo[] files2 = localDI.GetFiles("*.*");

				//			foreach (FileInfo file in files2) {

				//				file.Delete();
				//			}
				//		}
				//	}
				//}
			}
			button1.Enabled = true;
		}

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

		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			Logger.Write("Begin copying files...");

			string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string line in lines) {

				string[] dirNames = line.Split(new char[] { ',' });

				Logger.Write(dirNames.Length.ToString() +  " directory names found");

				foreach (string dirName in dirNames) {

					string dirPath = Path.Combine(@"\\xdata1\cmsos2\ckenode", dirName.Trim());

					DirectoryInfo di = new DirectoryInfo(dirPath);

					Logger.Write("Directory path = " + dirPath);

					if (di.Exists) {

						Logger.Write("Directory path exists: " + dirPath);


						FileInfo[] files = di.GetFiles("*HourlySales.pol");

						foreach (FileInfo file in files) {
							string destPath = Path.Combine(dirPath, "mtier\\saleslabor", file.Name);

							Logger.Write("Copying file: " + destPath);

							file.CopyTo(destPath, true);

						}


						//files = di.GetFiles("*.fin");

						//foreach (FileInfo file in files) {
						//	string destPath = Path.Combine(dirPath, "mtier\\saleslabor", file.Name);

						//	Logger.Write("Copying file: " + destPath);

						//	file.CopyTo(destPath, true);

						//}

					}
				}
			}

			button2.Enabled = true;
		}
	}
}
