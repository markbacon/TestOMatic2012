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
	public partial class Form32 : Form {
		public Form32() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			StringBuilder sb = new StringBuilder();

			string dirPath = "D:\\PaidIOTest";

			DirectoryInfo di = new DirectoryInfo(dirPath);

			DirectoryInfo[] directories = di.GetDirectories();

			foreach (DirectoryInfo directory in directories) {

				FileInfo[] files = directory.GetFiles("*pd.fin");

				foreach (FileInfo file in files) {
					ProcessFinFile(file, sb);
				}
			}

			textBox1.Text = sb.ToString();

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFinFile(FileInfo file, StringBuilder sb) {


			string dateString = "";
			string unitNumber = "";
			string piCnt = "";
			decimal piAmt = 0;
			string poCnt = "";
			decimal poAmt = 0;

			using (StreamReader sr = file.OpenText()) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine().Replace("\"", "");
					string[] items = line.Split(new char[] { ',' });

					switch (items[4]) {

						case "87":
							dateString = items[0];
							unitNumber = items[3];
							piAmt = Convert.ToDecimal(items[11]) / 100;
							piCnt = items[10];
							break;

						case "92":
							poAmt = Convert.ToDecimal(items[11]) / 100;
							poCnt = items[10];
							break;

					}
				}

				sb.Append(unitNumber);
				sb.Append("\t");
				sb.Append(dateString);
				sb.Append("\t");
				sb.Append(piCnt);
				sb.Append("\t");
				sb.Append(piAmt.ToString("0.00"));
				sb.Append("\t");
				sb.Append(poCnt);
				sb.Append("\t");
				sb.Append(poAmt.ToString("0.00"));
				sb.Append("\r\n");


			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessPaidPol(FileInfo file, StringBuilder sb) {


			string dateString = "";
			string unitNumber = "";
			string piCnt = "";
			string piAmt = "";
			string poCnt = "";
			string poAmt = "";

			using (StreamReader sr = file.OpenText()) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine().Replace("\"", "");
					string[] items = line.Split(new char[] { ',' });

					switch (items[0]) {

						case "DATE":
							dateString = items[1];
							break;

						case "STORE":
							unitNumber = items[1];
							break;


						case "TOTAL PAIDOUTS":
							piAmt = items[2];
							piCnt = items[1];
							break;


						case "TOTAL PAIDINS":
							poAmt = items[2];
							poCnt = items[1];
							break;

					}
				}

				sb.Append(unitNumber);
				sb.Append("\t");
				sb.Append(dateString);
				sb.Append("\t");
				sb.Append(piCnt);
				sb.Append("\t");
				sb.Append(piAmt);
				sb.Append("\t");
				sb.Append(poCnt);
				sb.Append("\t");
				sb.Append(poAmt);
				sb.Append("\r\n");


			}
		}
	}
}
