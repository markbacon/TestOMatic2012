using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form53 : Form {

		public Form53() {
			InitializeComponent();
			Logger.LoggerWrite += form8_onLoggerWrite;
		}
		//---------------------------------------------------------------------------------------------------
		//---------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			RunTest();

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();


		}
		//---------------------------------------------------------------------------------------------------
		private void RunTest() {

			Logger.Write("CheckIfTimeToRun starting...");

			double timerInterval = ServiceSettings.DefaultTimerInterval;

			//-- What was the last business date that a weekly time file was created?
			DateTime lastRunBusinessDate = FileCreationLog.GetLastRunBusinessDate(FileType.WeeklyTimeFile);

			DateTime currentDateTime = DateTime.Now;

			//-- Get the last week end date. Poll file start time could be several days after week end date
			DateTime lastWeekEndDate = BusinessDateUtility.GetLastWeekEndDate(currentDateTime);

			//-- Has the weekly time file has already been created for this week end date?
			if (lastWeekEndDate.Date == lastRunBusinessDate.Date) {
				Logger.Write("Weekly time file has already been created for week end date: " + lastRunBusinessDate.ToString("MM/dd/yyyy"));
				timerInterval = ServiceSettings.DefaultTimerInterval;
			}
			else {
				//-- Based on the current date/time, when is the next run time for the weekly time file?
				DateTime pollFileStartTime = BusinessDateUtility.GetWeeklyPollFileStartTime(currentDateTime);

				if (pollFileStartTime <= currentDateTime) {
					//-- Run file generator
					//string commandArgs = ServiceSettings.WeeklyTimeFileCommandLineArguments.Replace("!BUSINESS_DATE", lastWeekEndDate.ToString("MM/dd/yyyy"));

					//TaskProcessor tp = new TaskProcessor();
					//tp.RunProgram(ServiceSettings.FileGeneratorFilePath, commandArgs);
					FileCreationLog.Write(FileType.WeeklyTimeFile, lastWeekEndDate);
				}
				else {

					//-- If the time difference is greater than the default timer interval then set this to the default time inverval
					if ((pollFileStartTime - currentDateTime).TotalHours > ServiceSettings.DefaultTimerIntervalHours) {
						timerInterval = ServiceSettings.DefaultTimerInterval;
					}

					else {
						//-- If the timer interval is less than the default then set this to the next run time
						timerInterval = (pollFileStartTime - currentDateTime).TotalSeconds * Constants.MILLISECONDS_PER_SECOND;
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
