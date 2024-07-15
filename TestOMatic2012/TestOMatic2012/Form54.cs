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

namespace TestOMatic2012 {
	public partial class Form54 : Form {
		public Form54() {
			InitializeComponent();

			Logger.LoggerWrite += form8_onLoggerWrite;
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			List<string> unitList = GetUnitList();

			DirectoryInfo di = new DirectoryInfo("C:\\CkeMixTest");

			DirectoryInfo[] directories = di.GetDirectories();

			foreach (DirectoryInfo directory in directories) {

				ProcessDirectory(directory);


			}

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private List<string> GetUnitList() {

			List<string> unitList = new List<string>();

			unitList.Add("X1100471");

			//string filePath = "C:\\Temp.44\\FreeStyleUnits.txt";

			//using (StreamReader sr = new StreamReader(filePath)) {

			//	while (sr.Peek() != -1) {

			//		string line = sr.ReadLine();
			//		string[] items = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

			//		foreach (string item in items) {
			//			unitList.Add("X" + item.Trim());
			//		}
			//	}
			//}

			return unitList;
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDirectoryX(DirectoryInfo di) {

			DateTime startDate = Convert.ToDateTime("05/31/2016");

			while (startDate < DateTime.Today) {

				string fileName = di.Name + "_" + startDate.ToString("yyyyMMdd") + "_Mix.pol";

				FileInfo file = di.GetFiles(fileName).SingleOrDefault();

				if (file != null) {
					Logger.Write("Processing file: " + fileName);
					ProcessFile(file);
				}
				else {

					Logger.Write("File not found: ");
				}




				startDate = startDate.AddDays(1);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo di) {


			FileInfo[] files = di.GetFiles("*Mix.pol");

			foreach (FileInfo file in files) {

				Logger.Write("Processing file: " + file.Name);
				ProcessMixFile(file);

			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessMixFile(FileInfo file) {


			try {

				DataAnalysisDataContext dataContext = new DataAnalysisDataContext();

				MixPollFile mpf = dataContext.MixPollFiles.Where(m => m.FileName == file.Name).FirstOrDefault();

				if (mpf == null) {
					
					mpf = new MixPollFile();
					mpf.FileName = file.Name;
					mpf.BusinessDate = DateTime.ParseExact(file.Name.Substring(9, 8), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
					mpf.UnitNumber = file.Name.Substring(1, 7);
					mpf.DirectoryName = file.DirectoryName;

					dataContext.MixPollFiles.InsertOnSubmit(mpf);
					dataContext.SubmitChanges();
				}
				else {
					Logger.Write("File already exists: " + file.FullName);
				}

			}
			catch (Exception ex) {
				Logger.Write("An exception occurred processing file: " + file.Name + ". Please see error log for details.");
				Logger.WriteError(ex);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFile(FileInfo file) {

			int mixPollFileId = 0;

			MixPollFile mpf = new MixPollFile();
			mpf.FileName = file.Name;

			DataAnalysisDataContext dataContext = new DataAnalysisDataContext();


			using (StreamReader sr = file.OpenText()) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine().Replace("\"", "");

					string[] items = line.Split(new char[] { ',' });


					if (items.Count() < 5) {

						if (items[0].Trim() == "DATE") {

							mpf.BusinessDate = DateTime.ParseExact(items[1], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
						}

						else if (items[0].Trim() == "STORE") {

							mpf.UnitNumber = items[1].Trim();

							dataContext.MixPollFiles.InsertOnSubmit(mpf);
							dataContext.SubmitChanges();

							mixPollFileId = mpf.MixPollFileId;

						}
					}

					else {

						MixPollItem mpi = new MixPollItem();

						mpi.Department = items[1].Trim();
						mpi.ItemDescription = items[2].Trim();
						mpi.MenuItemId = items[9].Trim();
						mpi.MenuPollFileId = mixPollFileId;
						mpi.Price = Convert.ToDecimal(items[3]);
						mpi.QuantitySold = Convert.ToInt32(items[4]);

						dataContext.MixPollItems.InsertOnSubmit(mpi);
						dataContext.SubmitChanges();

					}

				}
			}
		}

		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();
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
