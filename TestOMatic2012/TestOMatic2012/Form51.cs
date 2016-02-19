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
	public partial class Form51 : Form {
		public Form51() {
			InitializeComponent();

			Logger.LoggerWrite += form8_onLoggerWrite;
		}
		//---------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;



			DateTime startDate = new DateTime(2013, 1, 1);
			DateTime endDate = new DateTime(2016, 1, 1);

			while (startDate < endDate) {
				DateTime startTime = DateTime.Now;
				Logger.Write("Begin processing files for " + startDate.ToString("MMMM") + "-" + startDate.Year.ToString());
				ProcessGiftCardFiles(startDate);
				Logger.Write("Processing files for " + startDate.ToString("MMMM") + "-" + startDate.Year.ToString() + " has completed. Elapsed time: " + (DateTime.Now - startTime).ToString());
	
				startDate = startDate.AddMonths(1);
			}

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			MenuConfigProcessor processor = new MenuConfigProcessor();
			processor.Load("C:\\StarPos\\StarPosConfig\\MenuConfig.xml");

			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();


		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessGiftCardFiles(DateTime monthStartDate) {

			try {

				DirectoryInfo di = new DirectoryInfo(@"\\xdata1\cmsos2\ckenode\GiftCardTransactions\CkeArchive");

				string fileSearchPattern = "CkeGiftCardTrans.X11*." + monthStartDate.Year.ToString() + monthStartDate.Month.ToString("00") + "*.xml";

				FileInfo[] files = di.GetFiles(fileSearchPattern);

				if (files.Length > 0) {

					string zipFileName = "CkeGiftCardTrans." + monthStartDate.Year.ToString() + monthStartDate.Month.ToString("00") + ".zip";
					ZipFile zippy = new ZipFile(zipFileName);

					foreach (FileInfo file in files) {
						Logger.Write("Adding file to zip:  " + file.Name);
						zippy.AddFile(file.FullName, "");
					}

					string targetPath = Path.Combine("C:\\Temp\\GiftCardArchive", zippy.Name);
					Logger.Write("Saving zip file: " + targetPath);
					zippy.Save(targetPath);

					foreach (FileInfo file in files) {
						Logger.Write("Deleting file:  " + file.Name);
						file.Delete();
					}



				}
			}
			catch (Exception ex) {
				Logger.Write("An exception occurred in ProcessGiftCardFiles. Please see error log for details.");
				Logger.WriteError(ex);
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
