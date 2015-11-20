using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form39 : Form {
		public Form39() {
			InitializeComponent();

			dateTimePicker1.ShowUpDown = true;
			dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker1.CustomFormat = "hh:mm tt";
			dateTimePicker1.Value = DateTime.Today;
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			INFO2000Data data = new INFO2000Data();

			List<ExistingDeposit> existingDepositList = data.GetExistingDeposits();

			List<deposit_dtl_fact> productionDepositList = data.GetProductionDeposits();

			foreach (deposit_dtl_fact depositDetail in productionDepositList) {

				int count = existingDepositList.Where(d => d.RestaurantNumber == depositDetail.Restaurant_no && d.CalDate == depositDetail.Cal_Date 
													  && d.DepositId == depositDetail.deposit_id && d.DepositAmount == depositDetail.Deposit_amt ).Count();

				if (count == 0) {

					data.SaveDepositDetail(depositDetail);
				}



			}





			button1.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			ProcessEodIni("C:\\Temp\\Eod.ini");


			button2.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------------
		private void button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;

			try {

				FileInfo iniFile = new FileInfo("C:\\Temp\\eod.ini");


				string networkPath = @"\\10.38.32.91\C$";
				//string networkPath = @"\\FT1CKV1-E6420\C$";
				NetworkCredential credentials = new NetworkCredential();
				credentials.Domain = "ckrcorp";
				credentials.Password = "John10:10";
				credentials.UserName = "mbacon";

				using (new NetworkConnection(networkPath, credentials)) {

					string destpath = Path.Combine(networkPath, "temp", "EodIni.ini");
					iniFile.CopyTo(destpath);
				}


			}

			catch (Exception ex) {
				Logger.WriteError(ex);
			}

            
			button3.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------------
		private void button4_Click(object sender, EventArgs e) {

			//MessageBox.Show(dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));
			MessageBox.Show(dateTimePicker1.Text);

		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessEodIni(string iniFilePath) {

			try {
				string[] sectionNames = { "[FailEOD]", "[ExitEOD]" };

				Logger.Write("ProcessEodIni starting...");

				//-- First create a backup of EOD.ini
				FileInfo iniFile = new FileInfo(iniFilePath);

				string backupPath = iniFile.FullName + ".back";

				if (!File.Exists(backupPath)) {
					iniFile.CopyTo(backupPath);
				}

				//-- Next remove unwanted sections
				StringBuilder sb = new StringBuilder();

				bool sectionFound = false;

				using (StreamReader sr = iniFile.OpenText()) {

					while (sr.Peek() != -1) {

						string line = sr.ReadLine().Trim();

						if (!sectionFound) {
							if (line.StartsWith(sectionNames[0], StringComparison.InvariantCultureIgnoreCase) ||
								line.StartsWith(sectionNames[1], StringComparison.InvariantCultureIgnoreCase)) {

								sectionFound = true;
							}

							sb.Append(line);
							sb.Append("\r\n");
						}

						else {
							if (line.StartsWith("[")) {
								sectionFound = false;

								sb.Append(line);
								sb.Append("\r\n");

							}
							else if (!line.StartsWith("Cmd1", StringComparison.InvariantCultureIgnoreCase)) {

								sb.Append(line);
								sb.Append("\r\n");
							}
						}
					}
				}

				iniFile.Delete();

				using (StreamWriter sw = new StreamWriter(iniFilePath)) {
					sw.Write(sb.ToString());
				}
			}

			catch (Exception ex) {
				Logger.Write("an exception occurred in ProcessEodIni. Please see error log for details.");
				Logger.WriteError(ex);
			}
		}
	}
}
