using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TestOMatic2012 {

	public partial class Form45 : Form {

		public Form45() {

			InitializeComponent();

			Logger.LoggerWrite += form8_onLoggerWrite;
		}
		//---------------------------------------------------------------------------------------------------------
		private const string PATTERN = @",(?!(?<=(?:^|,)\s*\x22(?:[^\x22]|\x22\x22|\\\x22)*,)(?:[^\x22]|\x22\x22|\\\x22)*\x22\s*(?:,|$))";

		private Regex _regX = new Regex(PATTERN);
		//---------------------------------------------------------------------------------------------------------
		private string BuildTimeFileLine(string unitNumber, DateTime fileDate, string[] items) {

			StringBuilder sb = new StringBuilder();

			sb.Append(unitNumber);
			sb.Append("\t");
			sb.Append(fileDate.ToString("MM/dd/yyyy"));



			for (int i = 0; i < items.Length; i++) {

				sb.Append("\t");
				sb.Append(items[i]);
			}

			return sb.ToString();
		}
		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			ProcessTimeCardFiles();


			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();


		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessTimeCardFile(DataRow dr, CkeTimePollData data) {

			string timeFileLine;
	
			string unitNumber = dr["UnitNumber"].ToString();
			string ssn   = Convert.ToInt32(dr["SSN"]).ToString("000-00-0000");
			string fileName = dr["FileName"].ToString();

			DateTime fileDate = Convert.ToDateTime(dr["FileDate"]);

			DateTime startTime = DateTime.Now;
			Logger.Write("Begin processing file:  " + fileName + " for unit;  " + unitNumber + " and file date:  " + fileDate.ToString("MM/dd/yyyy"));

			string filePath = Path.Combine(@"D:\CkeTimeFilesXX", "X" + unitNumber, fileName);

			FileInfo timeFile = new FileInfo(filePath);

			bool isGmRecord = false;


			using (StreamReader sr = timeFile.OpenText()) {

				while (sr.Peek() != -1) {


					string line = sr.ReadLine();

					string[] items = _regX.Split(line);

					for (int i = 0; i < items.Length; i++) {

						items[i] = items[i].Replace("\"", "");
					}

					if (items[0] == "TIMECARD") {

						string timeFileSsn = items[2];

						if (timeFileSsn == ssn) {
							Logger.Write("GM record found!");

							isGmRecord = true;

							timeFileLine = BuildTimeFileLine(unitNumber, fileDate, items);
							data.InsertTimeFileData(timeFileLine);

						}
					}

					else if (isGmRecord == true) {

						if (items[0] == "SHIFT") {

							timeFileLine = BuildTimeFileLine(unitNumber, fileDate, items);
							data.InsertTimeFileData(timeFileLine);

						}

						else if (items[0] == "TOTALS") {
							timeFileLine = BuildTimeFileLine(unitNumber, fileDate, items);
							data.InsertTimeFileData(timeFileLine);

							break;
						}
					}
				}
			}

			Logger.Write("Finished processing file:  " + fileName + ". Elapsed time:  " + (DateTime.Now - startTime).ToString());
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessTimeCardFiles() {

			CkeTimePollData data = new CkeTimePollData();

			DataTable dt = data.GetGMTimeCardHeaderData();

			foreach (DataRow dr in dt.Rows) {


				ProcessTimeCardFile(dr, data);
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

		private void Form45_Load(object sender, EventArgs e) {

		}
	}
}
