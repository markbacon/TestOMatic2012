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

			//string dirPath = @"C:\Store Data\x1503130";
			string dirPath = @"\\xdata1\remoteware\storearchive\x1503537";

			DirectoryInfo directory = new DirectoryInfo(dirPath);

			//DirectoryInfo[] directories = di.GetDirectories("X15*");

			//foreach (DirectoryInfo directory in directories) {

				DateTime startTime = DateTime.Now;

				Logger.Write("Begin processing directory:  " + directory.FullName);

				ProcessDirectory(directory);

				Logger.Write("Directory processing completed.  Elapsed time: " + (DateTime.Now - startTime).ToString());
			//}

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			List<UnitSalesDetail> salesDetailList = new List<UnitSalesDetail>();

			string dirName = "D:\\temp.308";

			DirectoryInfo di = new DirectoryInfo(dirName);

			FileInfo[] files = di.GetFiles("*.fin");

			foreach (FileInfo file in files) {

				Logger.Write("Processing file:  " + file.Name);

				UnitSalesDetail usd = new UnitSalesDetail();
				salesDetailList.Add(usd);

				using (StreamReader sr = file.OpenText()) {

					while (sr.Peek() != -1) {

						string line = sr.ReadLine();
						string[] items = line.Split(new char[] { ',' });

						if (items[4] == "3") {
							usd.UnitNumber = items[3];
							usd.BusinessDate = DateTime.ParseExact(items[0], "MMddyy", System.Globalization.CultureInfo.InvariantCulture);
							usd.NetSales = Convert.ToDecimal(items[11]) / 100;
						}

						else if (items[4] == "128") {
							usd.GiftCardSales = Convert.ToDecimal(items[11]) / 100;
						}
					}
				}
			}

			StringBuilder sb = new StringBuilder();
			DateTime startDate = new DateTime(2015, 10, 1);
			DateTime endDate = new DateTime(2015, 10, 31);


			foreach (UnitSalesDetail usd in salesDetailList) {

				if (usd.BusinessDate >= startDate && usd.BusinessDate <= endDate) {
					sb.Append(usd.UnitNumber);
					sb.Append('\t');
					sb.Append(usd.BusinessDate.ToString("MM/dd/yyyy"));
					sb.Append('\t');
					sb.Append(usd.NetSales.ToString("0.00"));
					sb.Append('\t');
					sb.Append(usd.GiftCardSales.ToString("0.00"));
					sb.Append("\r\n");
				}
			}

			textBox1.Text = sb.ToString();



			button2.Enabled = true;
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
		private void ProcessFileII(FileInfo fi) {


			using (ZipFile zippy = new ZipFile(fi.FullName)) {

				string targetDirectory = @"D:\Temp.308";

				if (!Directory.Exists(targetDirectory)) {
					Directory.CreateDirectory(targetDirectory);
				}

				string finFileSearchString = "_PD.FIN";

				bool fileFound = false;

				foreach (ZipEntry entry in zippy) {

					if (entry.FileName.IndexOf(finFileSearchString, StringComparison.InvariantCultureIgnoreCase) > -1) {

						string destFileName = Path.Combine(targetDirectory, entry.FileName);

						entry.Extract(targetDirectory, ExtractExistingFileAction.OverwriteSilently);

						fileFound = true;
					}
				}


				if (fileFound) {
					Logger.Write("Fin file found in zip archive:  " + fi.FullName);
				}
				else {
					Logger.Write("Fin file NOT found in zip archive:  " + fi.FullName);
				}
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo di) {

			FileInfo[] files = di.GetFiles("*.zip");

			foreach (FileInfo file in files) {

				ProcessFileII(file);

			}
		}
		//---------------------------------------------------------------------------------------------------
		private void textBox1_TextChanged(object sender, EventArgs e) {

			//if (textBox1.Text.Length > 2048) {
			//	textBox1.Text = "";
			//}



			if (textBox1.Text.Length > 0) {
				textBox1.SelectionStart = textBox1.Text.Length - 1;
				textBox1.ScrollToCaret();
				Application.DoEvents();
			}
		}

	}
}
