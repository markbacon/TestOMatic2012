using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form52 : Form {
		public Form52() {
			InitializeComponent();

			Logger.LoggerWrite += form8_onLoggerWrite;
		}
		//---------------------------------------------------------------------------------------------------------
		//-- 
		//---------------------------------------------------------------------------------------------------------
		private DataAnalysisDataContext _dataContext = new DataAnalysisDataContext();

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			string filePath = "C:\\xdata1\\cmsos2\\ckenode";

			DirectoryInfo di = new DirectoryInfo(filePath);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {
				Logger.Write("Begin processing directory: " + directory.Name);
				ProcessDirectory(directory);
			}

			button1.Enabled = true;
		}
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			string filePath = "C:\\Temp.44\\1100512_1.txt";


			using (StreamReader sr = new StreamReader(filePath)) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();
					
					string[] items = line.Split(new char[] {'\t'});

					CreditCardTran cct = new CreditCardTran();

					cct.Amount = Convert.ToDecimal(items[5]);
					cct.CardNumber = items[4];
					cct.CardType = items[3];
					cct.FundedDate = Convert.ToDateTime(items[2]);
					cct.PosEntryDescription = items[6];
					cct.SubmitDate = Convert.ToDateTime(items[0]);
					cct.TransDate = Convert.ToDateTime(items[1]);

					_dataContext.CreditCardTrans.InsertOnSubmit(cct);
					_dataContext.SubmitChanges();
				}
			}



			button2.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------
		private void button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;

			string filePath = "C:\\Temp.44\\HardeStarPos.dat";

			using (StreamReader sr = new StreamReader(filePath)) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					JournalAnalysi ja = new JournalAnalysi();

					ja.Account = line.Substring(26, 9);
					ja.Amount = Convert.ToDecimal(line.Substring(40, 13)) / 100;
					ja.Company = line.Substring(0, 3);
					ja.CreditAmount = Convert.ToDecimal(line.Substring(66, 13)) / 100;
					ja.DebitAmount = Convert.ToDecimal(line.Substring(53, 13)) / 100;
					ja.DebitCredit = line.Substring(92, 2);
					ja.LineNumber = line.Substring(11, 5);
					ja.StatementAmount = Convert.ToDecimal(line.Substring(79, 13)) / 100;
					ja.Unit = "11" +  line.Substring(35, 5);
					ja.WeekEndDate = Convert.ToDateTime(line.Substring(16, 10));

					_dataContext.JournalAnalysis.InsertOnSubmit(ja);
					_dataContext.SubmitChanges();
				}
			}


			button3.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void button4_Click(object sender, EventArgs e) {

			button4.Enabled = false;

			//string filePath = "C:\\T5\\SaveJournal\\CarlsStarPos.dat";
			string filePath = "C:\\T5\\SaveJournal\\HardeStarPos.dat";

			string dirName = "C:\\T5\\SaveJournal2";

			DirectoryInfo di = new DirectoryInfo(dirName);

			FileInfo[] files = di.GetFiles("*StarPos.dat*");


			foreach (FileInfo file in files) {

				Stopwatch sw1 = new Stopwatch();
				sw1.Start();
				Logger.Write("Begin processing file: " + file.Name);


				using (StreamReader sr = file.OpenText()) {

					while (sr.Peek() != -1) {

						string line = sr.ReadLine();

						StarPosJournalAnalysi spja = new StarPosJournalAnalysi();

						spja.Account = line.Substring(30, 12);
						spja.Amount = Convert.ToDecimal(line.Substring(49, 13)) / 100;
						spja.DebitCredit = line.Substring(62, 1);
						spja.Unit = "110" + line.Substring(42, 7).Trim();
						spja.WeekEndDate = DateTime.ParseExact(line.Substring(22, 8), "MMddyyyy", System.Globalization.CultureInfo.InvariantCulture);


						using (_dataContext = new DataAnalysisDataContext()) {
							_dataContext.StarPosJournalAnalysis.InsertOnSubmit(spja);
							_dataContext.SubmitChanges();
						}
						//JournalAnalysi ja = new JournalAnalysi();

						//ja.Account = line.Substring(26, 9);
						//ja.Amount = Convert.ToDecimal(line.Substring(40, 13)) / 100;
						//ja.Company = line.Substring(0, 3);
						//ja.CreditAmount = Convert.ToDecimal(line.Substring(66, 13)) / 100;
						//ja.DebitAmount = Convert.ToDecimal(line.Substring(53, 13)) / 100;
						//ja.DebitCredit = line.Substring(92, 2);
						//ja.LineNumber = line.Substring(11, 5);
						//ja.StatementAmount = Convert.ToDecimal(line.Substring(79, 13)) / 100;
						//ja.Unit = "11" + line.Substring(35, 5);
						//ja.WeekEndDate = Convert.ToDateTime(line.Substring(16, 10));

						//_dataContext.JournalAnalysis.InsertOnSubmit(ja);
						//_dataContext.SubmitChanges();
					}
					Logger.Write("Finished processing file: " + file.Name + ". Elapsed time: " + sw1.Elapsed.ToString());
				}
			}


			button4.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();


		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFile(FileInfo file) {

			DateTime? businessDate = null;
			decimal customerDiscount = 0;
			decimal employeeDiscount = 0;
			decimal managerDiscount = 0;
			string unitNumber = null;
			int restuarantNumber = 0;

			using (StreamReader sr = file.OpenText()) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = line.Split(new char[] { ',' });

					if (unitNumber == null) {
						unitNumber = items[3];
						restuarantNumber = Convert.ToInt32(unitNumber);

						if (restuarantNumber < 1100000) {
							restuarantNumber += 1100000;
						}

					}
					if (businessDate == null) {
						businessDate = DateTime.ParseExact(items[0], "MMddyy", System.Globalization.CultureInfo.InvariantCulture);
					}

					int index = Convert.ToInt32(items[4]);

					if (index == 38) {

						employeeDiscount = Convert.ToDecimal(items[11]) / 100;
					}

					if (index == 99) {

						customerDiscount = Convert.ToDecimal(items[11]) / 100;
					}

					if (index == 96) {

						managerDiscount = Convert.ToDecimal(items[11]) / 100;
					}
	
				
				}
			}

			DiscountAnalysi disc = new DiscountAnalysi();

			disc.BusinessDate = Convert.ToDateTime(businessDate);
			disc.CustomerDiscount = customerDiscount;
			disc.EmployeeDiscount = employeeDiscount;
			disc.ManagerDiscount = managerDiscount;
			disc.RestaurantNumber = restuarantNumber;

			_dataContext.DiscountAnalysis.InsertOnSubmit(disc);
			_dataContext.SubmitChanges();


		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo di) {

			FileInfo[] files = di.GetFiles("*.fin");

			foreach (FileInfo file in files) {

				Logger.Write("Begin processing file: " + file.Name);
				ProcessFile(file);
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
