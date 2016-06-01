using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form60 : Form {
		public Form60() {
			InitializeComponent();
			
			Logger.LoggerWrite += form8_onLoggerWrite;


			Logger.StartLogSession();
		}
		//---------------------------------------------------------------------------------------------------
		//---------------------------------------------------------------------------------------------------
		private const string PATTERN = @",(?!(?<=(?:^|,)\s*\x22(?:[^\x22]|\x22\x22|\\\x22)*,)(?:[^\x22]|\x22\x22|\\\x22)*\x22\s*(?:,|$))";

		private enum FinFileLineCsvPosition {
			BusinessDate = 0,
			UnitNumber = 3,
			Index = 4,
			Count = 10,
			Amount = 11
		}

		private class FinFileData {

			public string UnitNumber = "";
			public DateTime BusinessDate;
			public decimal NetSales = 0;
			public decimal CashDeposit = 0;
			public decimal CreditCardDeposits = 0;
			public decimal ToySales = 0;
			public decimal GiftCardSales = 0;
			public decimal Donations = 0;
			public decimal Exempt = 0;

		}



		private Regex _regex = new Regex(PATTERN);

		//---------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			string dirName = "N:\\Websites\\intranetWebApp\\uma";

			DirectoryInfo di = new DirectoryInfo(dirName);

			FileInfo[] files = di.GetFiles("*.asp");

			StringBuilder sb = new StringBuilder();

			foreach (FileInfo file in files) {

				Logger.Write("Checking file: " + file.Name);

				using (StreamReader sr = file.OpenText()) {

					sb = new StringBuilder();
					sb.Append(sr.ReadToEnd());
				}

				if (sb.ToString().IndexOf("AS tMax",StringComparison.InvariantCultureIgnoreCase) > -1) {
					sb.Replace("As tMax", " tMax");

					Logger.Write("Updating file: " + file.Name);

					using (StreamWriter sw = new StreamWriter(file.OpenWrite())) {
						sw.Write(sb.ToString());
					}
				}

			}

			button1.Enabled = true;


		}
		//---------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;


			string sourceDirName = "\\\\ckecldfnp02\\CJRCO_RO";

			DirectoryInfo di = new DirectoryInfo(sourceDirName);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {

				if (directory.Name == "X1100000") {
					continue;
				}


				DateTime businessDate = new DateTime(2016, 4, 25);
	
				Logger.Write("Processing directory: " + directory.Name);

				while (businessDate < new DateTime(2016, 5, 1)) {

					if (directory.GetDirectories(businessDate.ToString("yyyyMMdd")).Count() > 0) {

						string searchString = businessDate.ToString("yyyMMdd") + "\\" + businessDate.ToString("yyyMMdd") + ".Transhist.xml";

						FileInfo file = directory.GetFiles(searchString).SingleOrDefault();

						if (file != null) {

							string copyPath = Path.Combine("C:\\RmsFiles", businessDate.ToString("yyyyMMdd"), directory.Name);

							if (!Directory.Exists(copyPath)) {
								Directory.CreateDirectory(copyPath);
							}

							copyPath = Path.Combine(copyPath, file.Name);
							Logger.Write("Copying file: " + copyPath);
							file.CopyTo(copyPath, true);

						}
					}

					businessDate = businessDate.AddDays(1);
				}


			}

			button2.Enabled = true;
			
		}
		//---------------------------------------------------------------------------------------------------
		private void button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;

			string sourceDirName = "D:\\xdata1\\ckenodex16";

			DirectoryInfo di = new DirectoryInfo(sourceDirName);

			List<string> unitList = GetUnitList();

			foreach (String unit in unitList) {

				DirectoryInfo unitDirectory = di.GetDirectories(unit).SingleOrDefault();
				
				if (unitDirectory != null) {
					Logger.Write("Processing directory: " + unitDirectory.Name);
					ProcessUnitDirectory(unitDirectory);
				}
				else {
					Logger.Write("Missing directory for unit: " + unit);
				}
			}


			button3.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------
		private void button5_Click(object sender, EventArgs e) {

			button5.Enabled = false;


			string sourceDirName = "C:\\Store Data\\X1100207";

			DirectoryInfo di = new DirectoryInfo(sourceDirName);


			FileInfo[] files = di.GetFiles("*.fin");

			foreach (FileInfo finFile in files) {

				ProcessFinFile(finFile);
			}



			button5.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();


		}
		//---------------------------------------------------------------------------------------------------
		private List<string> GetUnitList() {

			List<string> unitList = new List<string>();

			string filePath = "D:\\xdata1\\ckenodeX16\\unitlist.txt";

			using (StreamReader sr = new StreamReader(filePath)) {

				while (sr.Peek() != -1) {

					unitList.Add("X" + sr.ReadLine());

				}
			}

			return unitList;

		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessFinFile(FileInfo finFile) {

			FinFileData fd = new FinFileData();

			using (StreamReader sr = finFile.OpenText()) {

				bool done = false;

				while (sr.Peek() != -1 && !done) {

					string line = sr.ReadLine();

					string[] items = _regex.Split(line);


					for (int i = 0; i < items.Length; i++) {

						items[i] = items[i].Replace("\"", "").Trim();
					}


					switch (items[(int)FinFileLineCsvPosition.Index]) {

						case "3":
							fd.BusinessDate = DateTime.ParseExact(items[(int)FinFileLineCsvPosition.BusinessDate], "MMddyy", System.Globalization.CultureInfo.CurrentCulture);
							fd.UnitNumber = items[(int)FinFileLineCsvPosition.UnitNumber];
							fd.NetSales = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							break;

						case "11":
							fd.Exempt = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							break;

						case "24":
						case "106":
						case "107":
						case "108":
						case "109":
						case "110":
						case "111":
						case "112":
						case "113":
						case "114":
							decimal amount = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;

							if (items[(int)FinFileLineCsvPosition.Count] == "1" || items[(int)FinFileLineCsvPosition.Count] == "2") {
								fd.CashDeposit += amount;
							}
							else if (items[(int)FinFileLineCsvPosition.Count] != "0") {
								fd.CreditCardDeposits += amount;
							}
							break;

					    
						case "37":
							fd.ToySales  = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							break;

						case "128":
							fd.GiftCardSales = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							break;

						case "129":
							fd.Donations = Convert.ToDecimal(items[(int)FinFileLineCsvPosition.Amount]) / 100;
							break;



						case "130":
							done = true;
							break;
					}
				}
			}

			WriteFinData(fd);
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessUnitDirectory(DirectoryInfo unitDirectory) {

			FileInfo[] finFiles = unitDirectory.GetFiles("*.fin");

			foreach (FileInfo finFile in finFiles) {

				Logger.Write("Processing file: " + finFile.FullName);
				ProcessFinFile(finFile);
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
		//---------------------------------------------------------------------------------------------------
		private void WriteFinData(FinFileData fd) {

			string filePath = "C:\\temp\\FinFileTaxData.csv";

			StringBuilder sb = new StringBuilder();

			sb.Append(fd.BusinessDate.ToString("MM/dd/yyyy"));
			sb.Append(",");
			sb.Append(fd.UnitNumber);
			sb.Append(",");
			//sb.Append(fd.NetSales.ToString("0.00"));
			//sb.Append(",");
			//sb.Append(fd.CashDeposit.ToString("0.00"));
			//sb.Append(",");
			//sb.Append(fd.CreditCardDeposits.ToString("0.00"));
			sb.Append(fd.Exempt.ToString("0.00"));
			sb.Append(",");

			sb.Append(fd.GiftCardSales.ToString("0.00"));
			sb.Append(",");

			sb.Append(fd.Donations.ToString("0.00"));
			//sb.Append(",");



			using (StreamWriter sw = File.AppendText(filePath)) {
				sw.WriteLine(sb.ToString());
			}

			FinData fin = new FinData();

			fin.BusinessDate = fd.BusinessDate;
			fin.CashDeposits = fd.CashDeposit;
			fin.CreditCardDeposits = fd.CreditCardDeposits;
			fin.NetSales = fd.NetSales;
			fin.UnitNumber = fd.UnitNumber;


			DataAnalysisDataContext dataContext = new DataAnalysisDataContext();
			dataContext.FinDatas.InsertOnSubmit(fin);
			dataContext.SubmitChanges();

		}

		private void button4_Click(object sender, EventArgs e) {

			button4.Enabled = false;

			List<string> unitList = GetUnitList();

			DataAnalysisDataContext dataContext = new DataAnalysisDataContext();

			foreach (String unit in unitList) {

				Unit u = new Unit();
				u.UnitNumber = unit.Substring(1);

				dataContext.Units.InsertOnSubmit(u);
				dataContext.SubmitChanges();

			}


			


			button4.Enabled = true;

		}
	}
}
