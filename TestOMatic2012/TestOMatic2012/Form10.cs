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
	public partial class Form10 : Form {
		public Form10() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			DirectoryInfo di = new DirectoryInfo(@"\\xdata1\remoteware\StoreArchive");

			//List<string> directoryList = GetDirectoryList();


			//foreach (string directoryName in directoryList) {

				//DirectoryInfo[] directories = di.GetDirectories(directoryName);


				DirectoryInfo[] directories = di.GetDirectories("X11*");


				foreach (DirectoryInfo directory in directories) {

					textBox1.Text += "Processing Directory:  " + directory.Name + "\r\n";
					Application.DoEvents();

					ProcessDirectory(directory);
				}
			//}

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			string rootDirectory = @"\\anadevbatch\Prod\Testing\ckenode";
			//string rootDirectory = @"D:\xdata1\cmsos2\ckenode";


			DirectoryInfo di = new DirectoryInfo(rootDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X11*");


			foreach (DirectoryInfo directory in directories) {

				textBox1.Text += "Processing directory:  " + directory.Name + "\r\n";
				Application.DoEvents();


				string mtierDirectory = Path.Combine(directory.FullName, "time");

				if (!Directory.Exists(mtierDirectory)) {
					Directory.CreateDirectory(mtierDirectory);
				}


				FileInfo[] files = directory.GetFiles("????WKTIME.pol");

				foreach (FileInfo file in files) {

					string targetPath = Path.Combine(mtierDirectory, file.Name);
					file.CopyTo(targetPath, true);
				}

				//files = directory.GetFiles("*mix.pol");

				//foreach (FileInfo file in files) {

				//	string targetPath = Path.Combine(mtierDirectory, file.Name);
				//	file.CopyTo(targetPath, true);
				//}
			}


			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;

			DirectoryInfo di = new DirectoryInfo(@"\\xdata1\cmsos2\ckenode");
			//DirectoryInfo di = new DirectoryInfo(@"\\anadevbatch\prod\testing\ckenode");


			//List<string> directoryList = GetDirectoryList();
			//List<string> directoryList = di.GetDirectories("X11*").Select(d => d.FullName).ToList();


			//foreach (string directoryName in directoryList) {


			//DirectoryInfo[] directories = di.GetDirectories(directoryName);
			DirectoryInfo[] directories = di.GetDirectories("X11*");

				//DirectoryInfo[] directories = di.GetDirectories("X1*");

				foreach (DirectoryInfo directory in directories) {

					if (directory.Name == "X1100000") {
						continue;
					}

					textBox1.Text += "Processing directory:  " + directory.Name + "\r\n";
					Application.DoEvents();

					string targetDirectory = Path.Combine("D:\\xdata1\\cmsos2\\ckenode", directory.Name);

					if (!Directory.Exists(targetDirectory)) {
						Directory.CreateDirectory(targetDirectory);
					}

					FileInfo[] files = directory.GetFiles("*.pol");

					foreach (FileInfo file in files) {

						string targetPath = Path.Combine(targetDirectory, file.Name);
						textBox1.Text += "Copying file:  " + targetPath + "\r\n";
						Application.DoEvents();

						file.CopyTo(targetPath, true);
					}
					
					files = directory.GetFiles("*.fin");

					foreach (FileInfo file in files) {

						string targetPath = Path.Combine(targetDirectory, file.Name);
						textBox1.Text += "Copying file:  " + targetPath + "\r\n";
						Application.DoEvents();

						file.CopyTo(targetPath, true);
					}
					//}
			}

			button3.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessCkeDirectory(DirectoryInfo directory) {

			textBox1.Text += "Processing directory:  " + directory.Name + "\r\n";
			Application.DoEvents();

			FileInfo file = directory.GetFiles("0908WKTIME.pol").SingleOrDefault();

			if (file != null) {

				//string targetDirectory = @"\\anadevbatch\Prod\Testing\ckenode\\" + file.Directory.Name;
				string targetDirectory = Path.Combine(@"\\anadevbatch\prod\testing\ckenode", file.Directory.Name);

				if (!Directory.Exists(targetDirectory)) {

					Directory.CreateDirectory(targetDirectory);
				}

				file.CopyTo(Path.Combine(targetDirectory, file.Name), true);

			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo directory) {

			textBox1.Text += "Processing directory:  " + directory.Name + "\r\n";
			Application.DoEvents();


			string targetDirectory = @"D:\xdata1\cmsos2\ckenode\" + directory.Name; // +"\\mtier\\SalesLabor";

			if (!Directory.Exists(targetDirectory)) {
				Directory.CreateDirectory(targetDirectory);
			}

			FileInfo[] files = directory.GetFiles("0901.zip");

			foreach (FileInfo file in files) {

				//int fileNum = Convert.ToInt32(Path.GetFileNameWithoutExtension(file.Name));

				//if (fileNum >= 623) {


				ProcessFile(file);
				//}
			}

			//DirectoryInfo di = new DirectoryInfo(targetDirectory);

			//targetDirectory = Path.Combine(targetDirectory, "Mtier");

			//if (!Directory.Exists(targetDirectory)) {
			//	Directory.CreateDirectory(targetDirectory);
			//}


			//FileInfo[] files = di.GetFiles("*.fcp");

			//foreach (FileInfo efile in files) {

			//	string targetPath = Path.Combine(targetDirectory, efile.Name);
			//	efile.CopyTo(targetPath, true);
			//}



			////targetDirectory = Path.Combine(targetDirectory, "Mtier\\SalesLabor");

			//targetDirectory = Path.Combine(targetDirectory, "SalesLabor");

			//if (!Directory.Exists(targetDirectory)) {
			//	Directory.CreateDirectory(targetDirectory);
			//}


			//if (!Directory.Exists(targetDirectory)) {
			//	Directory.CreateDirectory(targetDirectory);
			//}

			//files = di.GetFiles("*.fin");

			//foreach (FileInfo efile in files) {

			//	string targetPath = Path.Combine(targetDirectory, efile.Name);
			//	efile.CopyTo(targetPath, true);
			//}


			//files = di.GetFiles("*.pol");

			//foreach (FileInfo efile in files) {

			//	string targetPath = Path.Combine(targetDirectory, efile.Name);
			//	efile.CopyTo(targetPath, true);
			//}








			//FileInfo[] files = directory.GetFiles("0901.zip");

			//foreach (FileInfo file in files) {

			//	//int fileNum = Convert.ToInt32(Path.GetFileNameWithoutExtension(file.Name));

			//	//if (fileNum >= 623) {


			//		ProcessFile(file);
			//	//}
			//}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFile(FileInfo file) {


			using (ZipFile zippy = new ZipFile(file.FullName)) {

				//string targetDirectory = @"\\anadevbatch\Prod\Testing\ckenode\\" + file.Directory.Name;
				string targetDirectory = @"D:\xdata1\cmsos2\ckenode\" + file.Directory.Name; // +"\\mtier\\SalesLabor";

				if (!Directory.Exists(targetDirectory)) {
					Directory.CreateDirectory(targetDirectory);
				}


				zippy.ExtractAll(targetDirectory, ExtractExistingFileAction.OverwriteSilently);


				//DirectoryInfo di = new DirectoryInfo(targetDirectory);

				//targetDirectory = Path.Combine(targetDirectory, "Mtier");

				//if (!Directory.Exists(targetDirectory)) {
				//	Directory.CreateDirectory(targetDirectory);
				//}


				//FileInfo[] files = di.GetFiles("*.fcp");

				//foreach (FileInfo efile in files) {

				//	string targetPath = Path.Combine(targetDirectory, efile.Name);
				//	efile.CopyTo(targetPath, true);
				//}



				////targetDirectory = Path.Combine(targetDirectory, "Mtier\\SalesLabor");

				//targetDirectory = Path.Combine(targetDirectory, "SalesLabor");

				//if (!Directory.Exists(targetDirectory)) {
				//	Directory.CreateDirectory(targetDirectory);
				//}


				//if (!Directory.Exists(targetDirectory)) {
				//	Directory.CreateDirectory(targetDirectory);
				//}

				//files = di.GetFiles("*.fin");

				//foreach (FileInfo efile in files) {

				//	string targetPath = Path.Combine(targetDirectory, efile.Name);
				//	efile.CopyTo(targetPath, true);
				//}


				//files = di.GetFiles("*.pol");

				//foreach (FileInfo efile in files) {

				//	string targetPath = Path.Combine(targetDirectory, efile.Name);
				//	efile.CopyTo(targetPath, true);
				//}


			}
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
		//---------------------------------------------------------------------------------------------------
		private List<string>	GetDirectoryList() {

			string filePath = @"C:\TestData\CarlsJuly13DivestedUnits.txt";


			List<string> directoryList = new List<string>();

			using (StreamReader sr = new StreamReader(filePath)) {

				while (sr.Peek() != -1) {

					string nodeName = "X" + sr.ReadLine();
					directoryList.Add(nodeName);
				}
			}

			return directoryList;

		}

	}
}
