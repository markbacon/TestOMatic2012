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
	public partial class Form37 : Form {
		public Form37() {
			InitializeComponent();
			Logger.LoggerWrite += form8_onLoggerWrite;

			Logger.StartLogSession();
		}
		//---------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;



			ProcessNetSales();

			button1.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo di) {

			DateTime startTime = DateTime.Now;
			Logger.Write("Begin processing directory:  " + di.Name);

			FileInfo[] files = di.GetFiles();

			foreach (FileInfo file in files) {

				ProcessFile(file);
			}

			Logger.Write("Finshed processing directory:  " + di.Name + ".  Elapsed time:  " + (DateTime.Now - startTime).ToString());

		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessFile(FileInfo fi) {

			DateTime startTime = DateTime.Now;
			Logger.Write("Begin processing file:  " + fi.FullName);

			DataAnalysisDataContext dataContext = new DataAnalysisDataContext();

			int numLinesRead = 0;

			using (StreamReader sr = fi.OpenText()) {

				while (sr.Peek() != -1) {
					
					string line = sr.ReadLine();

					string[] items = line.Split(new char[] { ',' });

					if (items.Length == 3) {

						DateTime salesDateTime = Convert.ToDateTime(items[0]);

						salesDateTime = salesDateTime.AddHours(Convert.ToDouble(items[1]));

						decimal amount = Convert.ToDecimal(items[2]);

						string unitNumber = fi.Directory.Name.Substring(1);



						HourlyNetSale hns = dataContext.HourlyNetSales.Where(h => h.UnitNumber == unitNumber && h.SalesDateTime == salesDateTime).FirstOrDefault();

						if (hns == null) {

							Logger.Write("Saving new net sales value for unit: " + unitNumber + " and data\time:  " + salesDateTime.ToString("yyyy-MM-dd HH:mm"));

							hns = new HourlyNetSale();

							hns.Amount = amount;
							hns.SalesDateTime = salesDateTime;
							hns.UnitNumber = unitNumber;

							dataContext.HourlyNetSales.InsertOnSubmit(hns);
							dataContext.SubmitChanges();

						}
						else {
							if (amount != hns.Amount) {
								Logger.Write("Updating new net sales value for unit: " + unitNumber + " and data\time:  " + salesDateTime.ToString("yyyy-MM-dd HH:mm"));
								hns.Amount = amount;
								dataContext.SubmitChanges();
							}
						}
					}

					numLinesRead++;

					if (numLinesRead % 100 == 0) {
						Logger.Write(numLinesRead.ToString() + " lines read.");
					}
				}
			}

			Logger.Write("Finshed processing file:  " + fi.Name + ".  Elapsed time:  " + (DateTime.Now - startTime).ToString());
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessNetSales() {

			DirectoryInfo di = new DirectoryInfo("D:\\NetSalesFiles");

			DirectoryInfo[] directories = di.GetDirectories();

			foreach (DirectoryInfo directory in directories) {

				if (string.Compare(directory.Name, "X1500254", true) > 0) {
					ProcessDirectory(directory);
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
