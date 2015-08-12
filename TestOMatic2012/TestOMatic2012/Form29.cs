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
	public partial class Form29 : Form {
		public Form29() {
			InitializeComponent();

			Logger.LoggerWrite += form29_onLoggerWrite;
		}

		private TimeFileAnalysisDataContext _dataContext = new TimeFileAnalysisDataContext();

		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			DateTime startTime = DateTime.Now;
			string filePath = "C:\\Temp556\\HourlyGMTimeCardData4.txt";

			Logger.Write("Begin processing file:  " + filePath);

			ProcessFile(filePath);

			Logger.Write("Elapsed time: " + (DateTime.Now - startTime).ToString());

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetEmployeeData);

			DataAccess dac = new DataAccess(AppSettings.DataAnalysisConnectionString);
			DataTable dt = dac.ExecuteQuery(sql);

			foreach (DataRow dr in dt.Rows) {

				string employeeName = dr["EmployeeName"].ToString();

				Logger.Write("Processing Employee:  " + employeeName);

				string ssn = dr["SSN"].ToString();

				string[] nameParts = employeeName.Split(new char[] { ',' });

				Employee emp = new Employee();
				emp.EmployeeName = employeeName;
				emp.FirstName = nameParts[1].Trim();
				emp.LastName = nameParts[0].Trim();
				emp.SSN = ssn;

				_dataContext.Employees.InsertOnSubmit(emp);
				_dataContext.SubmitChanges();
			}

			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void form29_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();
		}
		//---------------------------------------------------------------------------------------------------
		private int GetTimeCardRecordId(string SSN, string unitNumber, DateTime weekEndDate) {

			int timeCardRecordId = -1;

			TimeCardRecord tcr =
				(from t in _dataContext.TimeCardRecords
				 where t.SSN == SSN
					&& t.UnitNumber == unitNumber
					&& t.WeekEndDate == weekEndDate
				 select t).FirstOrDefault();

			if (tcr != null) {

				timeCardRecordId = tcr.TimeCardRecordId;
			}

			return timeCardRecordId;
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessFile(string filePath) {

			int lineCount = 0;
			int timeCardRecordId = -1;

			using (StreamReader sr = new StreamReader(filePath)) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = line.Split(new char[] { '\t' });

					if (line.IndexOf("TIMECARD") > -1) {


						string ssn = items[4].Replace("\"", "");
						string unitNumber = items[0].Replace("\"", "");
						DateTime weekEndDate = Convert.ToDateTime(items[1]);

						timeCardRecordId = GetTimeCardRecordId(ssn, unitNumber, weekEndDate);

						if (timeCardRecordId == -1) {
							Logger.Write("Time card record not found for SSN: " + ssn + ". Unit Number:  " + unitNumber + ". Week End Date:  " + weekEndDate.ToString("MM/dd/yyyy"));

							TimeCardRecord tcr = new TimeCardRecord();
							tcr.EmployeeJobCode = items[7].Replace("\"", "");
							tcr.EmployeeName = items[3].Replace("\"", ""); ;
							tcr.SSN = items[4].Replace("\"", ""); ;
							tcr.UnitNumber = items[0].Replace("\"", ""); ;
							tcr.WeekEndDate = Convert.ToDateTime(items[1]);


							_dataContext.TimeCardRecords.InsertOnSubmit(tcr);
							_dataContext.SubmitChanges();

							timeCardRecordId = tcr.TimeCardRecordId;
						}
					}
					else if (line.IndexOf("SHIFT") > -1) {
						TimeCardDetail tcd = new TimeCardDetail();

						tcd.BusinessDate = DateTime.ParseExact(items[4], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
						tcd.ClockIn = Convert.ToDecimal(items[6]);
						tcd.ClockOut = Convert.ToDecimal(items[7]);

						tcd.DayOfWeek = items[5].Replace("\"", ""); ;
						tcd.ElapsedTime = Convert.ToDecimal(items[8]);
						tcd.ShiftStatusCode = items[10].Replace("\"", "");
						tcd.TimeCardRecordId = timeCardRecordId;

						_dataContext.TimeCardDetails.InsertOnSubmit(tcd);
						_dataContext.SubmitChanges();
					}

					else if (line.IndexOf("TOTALS") > -1) {

						TimeCardTotal tct = new TimeCardTotal();

						tct.OvertimeHours = Convert.ToDecimal(items[7]);
						tct.RegularHours = Convert.ToDecimal(items[6]);
						tct.TimeCardRecordId = timeCardRecordId;
						tct.TotalHours = Convert.ToDecimal(items[9]);
						tct.VacationHours = Convert.ToDecimal(items[8]);

						_dataContext.TimeCardTotals.InsertOnSubmit(tct);
						_dataContext.SubmitChanges();

					}

					lineCount++;

					if (lineCount % 100 == 0) {
						Logger.Write(lineCount.ToString() + " lines processed.");
					}

					if (lineCount % 100 == 0) {
						_dataContext = new TimeFileAnalysisDataContext();
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
