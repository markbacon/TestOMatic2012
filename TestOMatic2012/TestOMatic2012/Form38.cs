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
	public partial class Form38 : Form {
		public Form38() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			DirectoryInfo di = new DirectoryInfo("D:\\NetSalesFiles");

			List<DirectoryInfo> directoryList = di.GetDirectories("X15*").ToList();

			foreach (DirectoryInfo dirInfo in directoryList) {

				FileInfo[] files = dirInfo.GetFiles("RO*.RPT");


				foreach (FileInfo file in files) {

					ProcessRingOutRptFile(file);
				}

			}





			//FileInfo file = new FileInfo("C:\\Temp\\RO140105.RPT");


			//ProcessRingOutRptFile(file);

			//foreach (HourlyNetSales hns in hourlyNetSalesList) {
			//	textBox1.Text += hns.SalesDateTime.ToString("yyyy-MM-dd HH:mm");
			//	textBox1.Text += "\t";
			//	textBox1.Text += hns.Amount.ToString("0.00");
			//	textBox1.Text += "\r\n";
			//}

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			INFO2000Data data = new INFO2000Data();
			string errMessage = "";

			List<int> posFactIdList = data.GetPosFactIdList();

			foreach (int posFzctId in posFactIdList) {

				textBox1.Text += "Processing pos_fact_id:  " + posFzctId.ToString() + "\r\n";
				Application.DoEvents();

				//errMessage = data.ExecuteCashOverShortUpdate(posFzctId);

				if (!string.IsNullOrEmpty(errMessage)) {
					textBox1.Text += "Error updating cash o/s for pos_fact_id:  " + posFzctId.ToString() + " Error message:  " + errMessage + "\r\n";
					Application.DoEvents();
				}
			}

			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessRingOutRptFile(FileInfo rptFile) {

			string previousLine = "";
			bool processNetSalesLines = false;
			bool skipFour = true;
			int skipCount = 0;
			DateTime businessDate = DateTime.MinValue; ;
			bool done = false;

			string unitNumber = "";

			List<HourlyNetSalesII> hourlyNetSalesList = new List<HourlyNetSalesII>();

			using (StreamReader sr = rptFile.OpenText()) {


				while (sr.Peek() != -1 && !done) {

					string line = sr.ReadLine();

					if (line.StartsWith("Unit") && unitNumber.Length == 0) {
						unitNumber = line.Substring(5, 7);
					}


					if (!processNetSalesLines) {

						if (line.IndexOf("**** Hourly Sales ****") > 1) {

							processNetSalesLines = true;

							previousLine = previousLine.Trim();

							int pos = previousLine.Length - 10;
							string temp = previousLine.Substring(pos);

							businessDate = Convert.ToDateTime(temp);
						}
						else {
							previousLine = line;
						}
					}
					else {
						if (skipFour) {
							skipCount++;

							if (skipCount == 4) {
								skipFour = false;
							}
						}
						else {
							line = line.Trim();

							string[] items = line.Split(new char[] { '|' });

							HourlyNetSalesII hns = new HourlyNetSalesII();

							hns.UnitNumber = unitNumber;

							int hour = Convert.ToInt32(items[1]) - 1;

							hns.SalesDateTime = businessDate.Date.AddHours(hour);
							hns.Amount = Convert.ToDecimal(items[2].Replace("$", "").Trim());

							hourlyNetSalesList.Add(hns);


							if (hourlyNetSalesList.Count == 24) {

								done = true;
							}
						}
					}

				}

				textBox1.Text += "Saving data for unit:  " + unitNumber + " and  date: " + businessDate.ToString("yyyy-Mm-dd") + "\r\n";
				Application.DoEvents();

				DataAnalysisDataContext dataContext = new DataAnalysisDataContext();
				dataContext.HourlyNetSalesIIs.InsertAllOnSubmit(hourlyNetSalesList);
				dataContext.SubmitChanges();

			}




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

	}
}
