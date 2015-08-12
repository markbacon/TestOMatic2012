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
	public partial class Form16 : Form {
		public Form16() {
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------------------------------
		//---------------------------------------------------------------------------------------------------------
		private enum FinFileLineCsvPosition {
			BusinessDate = 0,
			UnitNumber = 3,
			Index = 4,
			Count = 10,
			Amount = 11
		}

		private enum SettleCardFileCsvPosition {
			RefNo, 
			Terminal, 
			Account, 
			UserID, 
			Mkey, 
			Processor, 
			Amount, 
			AuthID, 
			TransCode, 
			OrigAuthDate, 
			OrigAuthTime, 
			TransDate, 
			Transtime, 
			CardType, 
			UniqueID

		}


		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;


			string rootDirectoryName = "D:\\FinFile";

			DirectoryInfo di = new DirectoryInfo(rootDirectoryName);

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {

				FileInfo file = directory.GetFiles("X15*20141216_PD.fin").FirstOrDefault();

				if (file != null) {

					ProcessFile(file);

				}
			}

			button1.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessCreditCardDataDump(FileInfo file) {


			DataAnalysisDataContext dataContext = new DataAnalysisDataContext();

			using (StreamReader sr = file.OpenText()) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = line.Split(new char[] { '\'' });

					CreditCardDetail ccd = new CreditCardDetail();

					ccd.Amount = Convert.ToDecimal(items[5]) / 100;
					ccd.CardType = items[4].Trim();
					ccd.ReferenceNo = items[2].Trim();
					ccd.RestaurantNo = Convert.ToInt32(items[0].Substring(1));
					ccd.TransDataTime = Convert.ToDateTime(items[1].Trim());
					ccd.TransType = items[3].Trim();
					dataContext.CreditCardDetails.InsertOnSubmit(ccd);
					dataContext.SubmitChanges();

				}
			}

		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFile(FileInfo file) {

			decimal amount = 0;


			using (StreamReader sr = file.OpenText()) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = line.Split(new char[] { ',' });

					int index = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Index]);

					if (index >= 106 && index <= 114) {

						int count = Convert.ToInt32(items[(int)FinFileLineCsvPosition.Count]);

						if (count == 4) {

							amount += Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]);
						}
					}
				}
			}

			if (amount > 0) {

				CreditCardSummary ccs = new CreditCardSummary();

				ccs.Amount = amount / 100;
				ccs.CalDate = new DateTime(2014, 12, 16);
				ccs.RestaurantNo = Convert.ToInt32(file.Directory.Name.Substring(1));

				DataAnalysisDataContext dataContext = new DataAnalysisDataContext();
				dataContext.CreditCardSummaries.InsertOnSubmit(ccs);
				dataContext.SubmitChanges();
			}
		}

		private void button2_Click(object sender, EventArgs e) {


			//FileInfo file = new FileInfo("C:\\Temp223\\cctrans.txt");
			FileInfo file = new FileInfo("C:\\Temp223\\1100140dump.txt");

			ProcessCreditCardDataDump(file);



		}

		private void button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;


			FileInfo file = new FileInfo("C:\\Temp223\\SettleTrans_121614A.txt");
			ProcessSettledCreditCardFile(file);


			button3.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessSettledCreditCardFile(FileInfo file) {


			DataAnalysisDataContext dataContext = new DataAnalysisDataContext();


			using (StreamReader sr = file.OpenText()) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = line.Split(new char[] { ',' });

					SettledCreditCard scc = new SettledCreditCard();

					scc.Account = items[(int)SettleCardFileCsvPosition.Account].Trim();
					scc.Amount = Convert.ToDecimal(items[(int)SettleCardFileCsvPosition.Amount]);
					scc.AuthId = items[(int)SettleCardFileCsvPosition.AuthID].Trim();

					if (items[(int)SettleCardFileCsvPosition.OrigAuthDate].Trim().Length > 0) {
						scc.OriginalAuthDateTime = BuildDateTime(items[(int)SettleCardFileCsvPosition.OrigAuthDate].Trim(), items[(int)SettleCardFileCsvPosition.OrigAuthTime].Trim());
					}
					else {
						scc.OriginalAuthDateTime = new DateTime(1900, 1, 1);
					}

					scc.ReferenceNo = items[(int)SettleCardFileCsvPosition.RefNo].Trim();
					scc.RestaurantNo = Convert.ToInt32(items[(int)SettleCardFileCsvPosition.Terminal].Trim().Substring(0, 7));
					scc.Terminal = items[(int)SettleCardFileCsvPosition.Account].Trim();
					scc.TransCode = items[(int)SettleCardFileCsvPosition.TransCode].Trim();

					scc.TransDateTime = BuildDateTime(items[(int)SettleCardFileCsvPosition.TransDate].Trim(), items[(int)SettleCardFileCsvPosition.Transtime].Trim());
					scc.UserId = items[(int)SettleCardFileCsvPosition.UserID].Trim();

					dataContext.SettledCreditCards.InsertOnSubmit(scc);
					dataContext.SubmitChanges();
				}
			}
		}
		//---------------------------------------------------------------------------------------------------
		private DateTime BuildDateTime(string dateString, string timeString) {

			StringBuilder sb = new StringBuilder();

			sb.Append(dateString.Substring(0, 2));
			sb.Append("/");
			sb.Append(dateString.Substring(2, 2));
			sb.Append("/20");
			sb.Append(dateString.Substring(4, 2));

			if (timeString.Length > 0) {

				sb.Append(" ");
				sb.Append(timeString.Substring(0, 2));
				sb.Append(":");
				sb.Append(timeString.Substring(2, 2));
				sb.Append(":");
				sb.Append(timeString.Substring(4, 2));

			}

			return Convert.ToDateTime(sb.ToString());
		}

	}
}
