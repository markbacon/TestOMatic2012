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
	public partial class Form27 : Form {
		public Form27() {
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			DateTime startTime = DateTime.Now;
			string filePath = "C:\\Temp556\\HourlyGMTimeCardData4.txt";

			ProcessFile(filePath);



			textBox1.Text += "Elapsed time: " + (DateTime.Now - startTime).ToString();

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			DateTime weekEndDate = new DateTime(2009, 6, 22);
			DateTime maxEndDate = new DateTime(2015, 3, 30);

			TimeFileAnalysisDataContext dataContext = new TimeFileAnalysisDataContext();

			while (weekEndDate <= maxEndDate) {

				TimeCardWeekEndDate tcd = new TimeCardWeekEndDate();
				tcd.WeekEndDate = weekEndDate;

				dataContext.TimeCardWeekEndDates.InsertOnSubmit(tcd);
				dataContext.SubmitChanges();

				weekEndDate = weekEndDate.AddDays(7);

			}

			button2.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------
		private void button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;
			
			DirectoryInfo srcDi = new DirectoryInfo("D:\\CkeTimeFiles");

			DirectoryInfo[] srcDirectories = srcDi.GetDirectories();

			foreach (DirectoryInfo srcDirectory in srcDirectories) {

				ProcessSourceDirectory(srcDirectory);

			}

			button3.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void DispayText(string text) {

			textBox1.Text += text + "\r\n";
			Application.DoEvents();
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessFile(string filePath) {

			TimeFileAnalysisDataContext dataContext = new TimeFileAnalysisDataContext();

			using (StreamReader sr = new StreamReader(filePath)) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					if (line.IndexOf("TIMECARD") > -1) {

						string[] items = line.Split(new char[] { '\t' });

						textBox1.Text += "Writing Time Card Record for: " + items[3] + "\r\n";
						Application.DoEvents();

						TimeCardRecord tcr = new TimeCardRecord();
						tcr.EmployeeJobCode = items[7].Replace("\"", "");
						tcr.EmployeeName = items[3].Replace("\"", ""); ;
						tcr.SSN = items[4].Replace("\"", ""); ;
						tcr.UnitNumber = items[0].Replace("\"", ""); ;
						tcr.WeekEndDate = Convert.ToDateTime(items[1]);

						dataContext.TimeCardRecords.InsertOnSubmit(tcr);
						dataContext.SubmitChanges();
					}
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessSourceDirectory(DirectoryInfo srcDirectory) {

			DispayText("Processing directory:  " + srcDirectory.Name);

			string destDirPath = Path.Combine("D:\\WkTimeFiles", srcDirectory.Name);

			if (!Directory.Exists(destDirPath)) {
				DispayText("Creting directory:  " + destDirPath);
				Directory.CreateDirectory(destDirPath);
			}

			FileInfo[] files = srcDirectory.GetFiles("*WKTime.pol");

			foreach (FileInfo file in files) {

				DispayText("Copying file:  " + file.FullName);
				string destPath = Path.Combine(destDirPath, file.Name);

				file.CopyTo(destPath, true);

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
