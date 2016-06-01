using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form12 : Form {
		public Form12() {
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			string filePath = "L:\\Ckenode";

			DirectoryInfo di = new DirectoryInfo(filePath);

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			StringBuilder sb = new StringBuilder();

			foreach (DirectoryInfo directory in directories) {

				textBox1.Text += "Processing Directory:  " + directory.Name + "\r\n";

				string mtierPath = Path.Combine(directory.FullName, "mtier\\SalesLabor");

				DirectoryInfo mtierDirectory = new DirectoryInfo(mtierPath);

				if (mtierDirectory.Exists) {

					FileInfo[] files = mtierDirectory.GetFiles("*.*");

					if (files.Count() > 0) {

						sb.Append("The following files were found for:  ");
						sb.Append(mtierDirectory.FullName);
						sb.Append("\r\n");

						foreach (FileInfo file in files) {

							sb.Append(file.Name);
							sb.Append("\r\n");
						}

						sb.Append("\r\n");
						sb.Append("\r\n");

					}
				}
			}

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;


			DirectoryInfo di = new DirectoryInfo("D:\\TLogsIII");

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {

				DirectoryInfo dirInfo2 = new DirectoryInfo(Path.Combine(directory.FullName, "archive"));


				if (dirInfo2.Exists) {


					FileInfo[] files = dirInfo2.GetFiles("????????.transhist.xml");

					string targetDirectoryName = Path.Combine("D:\\TLogsIII", directory.Name);

					//if (!Directory.Exists(targetDirectoryName)) {

					//	Directory.CreateDirectory(targetDirectoryName);
					//}

					foreach (FileInfo file in files) {

						string filePath = Path.Combine(targetDirectoryName, file.Name);

						//string temp = file.Name.Substring(0, 6);

						textBox1.Text += "Copying file:: " + filePath + "\r\n";
						Application.DoEvents();
						file.CopyTo(filePath, true);

						//if (Convert.ToInt32(temp) < 20130601) {
						////textBox1.Text += "Copy file: " + filePath + "\r\n";
						textBox1.Text += "Deleting file:: " + file.FullName + "\r\n";
						Application.DoEvents();
						file.Delete();
						//}

					}
				}
			}

			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;


			DirectoryInfo di = new DirectoryInfo(@"L:\ckenode\GiftCardTransactions\CkeArchive");


			int restaurantNo = 1100001;


			while (restaurantNo < 1107000) {

				string searchPattern = "CkeGiftCardTrans.X" + restaurantNo.ToString() + "*.xml";

				FileInfo[] files = di.GetFiles(searchPattern);

				string targetDirectoryName = "C:\\GiftCardData\\GiftCardTransactions";

				foreach (FileInfo file in files) {

					string filePath = Path.Combine(targetDirectoryName, file.Name);

					if (!File.Exists(filePath)) {
						textBox1.Text += "Copying file:: " + filePath + "\r\n";
						Application.DoEvents();
						file.CopyTo(filePath, true);
					}
				}

				restaurantNo++;

			}

			button3.Enabled = true;

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

		private void button4_Click(object sender, EventArgs e) {

			button4.Enabled = false;

			GiftCardDataContext dataContext = new GiftCardDataContext();


			int restaurantNo = 1100001;


			while (restaurantNo < 1109999) {

				List<SvsHostRecon> svsRecordList = dataContext.SvsHostRecons.Where(s => s.Unit == restaurantNo.ToString()).ToList();


				foreach (SvsHostRecon svsRecord in svsRecordList) {


					DateTime businessDate = svsRecord.HostDate.Date;

					textBox1.Text += "Processing record for:  " + svsRecord.Unit + " and host date: " + svsRecord.HostDate.ToString("yyyy-Mm-dd HH:mm:ss") + "\r\n";
					Application.DoEvents();

					if (svsRecord.HostDate.Hour < 3) {
						textBox1.Text += "Change business date to previous day" + "\r\n";
						businessDate = businessDate.AddDays(-1);
					}

					svsRecord.BusinessDate = businessDate;
					dataContext.SubmitChanges();
				}

				restaurantNo++;
			}

			button4.Enabled = true;
		}

		private void button5_Click(object sender, EventArgs e) {

			button5.Enabled = false;

			string filePath1 = "C:\\Temp357\\CJFDMS_20160305Old.dat";
			string filePath2 = "C:\\Temp357\\CJFDMS_20160305.dat";
			string filePath3 = "C:\\Temp357\\CJFDMS_20160305Test.dat";

			List<string> oldFileList = new List<string>();
			List<string> newFileList = new List<string>();


			using (StreamReader sr = new StreamReader(filePath1)) {

				while (sr.Peek() != -1) {
					oldFileList.Add(sr.ReadLine());
				}
			}

			using (StreamReader sr = new StreamReader(filePath2)) {

				while (sr.Peek() != -1) {
					newFileList.Add(sr.ReadLine());
				}
			}

			List<int> deleteList = new List<int>();

			for (int i = 0; i < newFileList.Count; i++) {

				string line = newFileList[i];

				if (line.StartsWith("16")) {

					if (oldFileList.Where(f => f == line).Count() > 0) {
						deleteList.Add(i);
					}
				}
			}

			for (int i = deleteList.Count - 1; i >= 0; i--) {
				newFileList.RemoveAt(deleteList[i]);
			}


			using (StreamWriter sw = new StreamWriter(filePath3)) {

				foreach (string line in newFileList) {
					sw.WriteLine(line);
				}
			}



			button5.Enabled = true;

		}

		private void button6_Click(object sender, EventArgs e) {

			Process[] ps = Process.GetProcesses();

			foreach (Process p in ps) {
				textBox1.Text += p.ProcessName + "\r\n";
			}


		}
	}
}
