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
	public partial class Form49 : Form {
		public Form49() {
			InitializeComponent();

			Logger.LoggerWrite += form8_onLoggerWrite;

		}
		//---------------------------------------------------------------------------------------------------------
		private const string PATTERN = @",(?!(?<=(?:^|,)\s*\x22(?:[^\x22]|\x22\x22|\\\x22)*,)(?:[^\x22]|\x22\x22|\\\x22)*\x22\s*(?:,|$))";

		private Regex _regX = new Regex(PATTERN);

		private DataAnalysisDataContext _dataContext = new DataAnalysisDataContext();
		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			string dirPath = @"D:\xdata1\cmsos2\Franchise\Cjb\ckenode";
			//string dirPath = @"C:\Pollfile\Pollfiles";

			DirectoryInfo di = new DirectoryInfo(dirPath);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

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



			FileInfo[] files = di.GetFiles("X11*_MixDest.pol");
			foreach (FileInfo file in files) {

				Logger.Write("Processing file:  " + file.Name);
				ProcessFile(file);
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessFile(FileInfo file) {

			using (StreamReader sr = file.OpenText()) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = _regX.Split(line);

					for (int i = 0; i < items.Length; i++) {

						items[i] = items[i].Replace("\"", "");
					}


					if (items.Length > 4) {

						if (items[2] == "2") {

							if (items[7].ToLower().Contains("kids")) {

								ItemSold itm = new ItemSold();
								itm.Franchisee = file.Directory.Parent.Parent.Name;

								itm.BusinessDate = DateTime.ParseExact(items[1], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
								itm.DayPartId = Convert.ToInt32(items[9]);
								itm.DestinationId = Convert.ToInt32(items[8]);
								itm.ItemDescription = items[7];
								itm.QuantitySold = Convert.ToInt32(items[11]);
								itm.UnitNumber = items[0];

								_dataContext.ItemSolds.InsertOnSubmit(itm);
								_dataContext.SubmitChanges();
							}
						}
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
