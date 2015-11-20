using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form47 : Form {
		public Form47() {
			InitializeComponent();

			Logger.LoggerWrite += form8_onLoggerWrite;
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			Logger.StartLogSession();

			//string pollFilePath = "U:\\PollFile\\PollFiles";
			//string pollFilePath = "W:\\Ckenode";
			string pollFilePath = "C:\\Temp357";

			Logger.Write("Begin processing " + pollFilePath);

			DirectoryInfo di = new DirectoryInfo(pollFilePath);

			DirectoryInfo[] directories = di.GetDirectories("new*");

			foreach (DirectoryInfo directory in directories) {
				Logger.Write("Processing directory:  " + directory.Name);

				ProcessDirectory(directory);
			}

			button1.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();


		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo di) {


			FileInfo[] files = di.GetFiles("INVU*.fcp");

			foreach (FileInfo file in files) {

				Logger.Write("Processing file:  " + file.Name);

				ProcessFile(file);
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessFile(FileInfo file) {

			DataAnalysisDataContext dataContext = new DataAnalysisDataContext();

			using (StreamReader sr = file.OpenText()) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					if (line.StartsWith("D")) {

						DailyInvUsage usage = new DailyInvUsage();

						usage.ActualAmount = Convert.ToDecimal(line.Substring(144, 11));
						usage.ActualQuantity = Convert.ToDecimal(line.Substring(117, 9));

						usage.BusinessDate = DateTime.ParseExact(line.Substring(11, 8), "MMddyyyy", System.Globalization.CultureInfo.InvariantCulture);
						usage.CategoryCode = line.Substring(19, 4).Trim();
						usage.Cost = Convert.ToDecimal(line.Substring(92, 10));
						usage.IdealAmount = Convert.ToDecimal(line.Substring(155, 11));
						usage.IdealQuantity = Convert.ToDecimal(line.Substring(126, 9));

						if (line.Substring(135, 1) == "T") {
							usage.IsDailyCountItem = true;
						}
						else {
							usage.IsDailyCountItem = false;
						}

						usage.ItemCode = line.Substring(23, 10).Trim();
						usage.ItemDescription = line.Substring(52, 30).Trim();
						usage.OnHandAmount = Convert.ToDecimal(line.Substring(226, 11));
						usage.OnHandQuantity = Convert.ToDecimal(line.Substring(217, 9));

						usage.UnitNumber = line.Substring(1, 7);

						dataContext.DailyInvUsages.InsertOnSubmit(usage);
						dataContext.SubmitChanges();
					}
				}
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
