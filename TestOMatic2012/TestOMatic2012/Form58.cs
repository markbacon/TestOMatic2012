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
	public partial class Form58 : Form {
		public Form58() {
			InitializeComponent();

			Logger.LoggerWrite += form8_onLoggerWrite;

		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			DataAnalysis2DataContext _dataContext = new DataAnalysis2DataContext();

			using (StreamReader sr = new StreamReader("C:\\Temp\\FourCount.txt")) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					Logger.Write("Processing line: " + line);

					string[] items = line.Split(new char[] { ',' });

					FourCount fc = new FourCount();

					fc.FileDate = DateTime.ParseExact(items[0].Substring(9, 8), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
					fc.FourCount1 = Convert.ToInt32(items[1]);
					fc.Unit = items[0].Substring(1, 7);

					_dataContext.FourCounts.InsertOnSubmit(fc);
					_dataContext.SubmitChanges();


				}



			}


			button1.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;


			DirectoryInfo di = new DirectoryInfo("C:\\MixDestPollFiles");

			FileInfo[] files = di.GetFiles("*MixDest.pol");

			foreach (FileInfo file in files) {

				Logger.Write("Processing file: " + file.Name);



				StringBuilder sb = new StringBuilder();
				int fourCount = 0;

				using (StreamReader sr = file.OpenText()) {

					while (sr.Peek() != -1) {

						string line = sr.ReadLine();
						string[] items = line.Split(new char[] { ',' });

						if (items.Length > 2) {

							if (items[2].Replace("\"", "").Trim() == "4") {
							fourCount++;
							}

						}
					}

					sb.Append(file.Name);
					sb.Append(",");
					sb.Append(fourCount);
				}

				using (StreamWriter sw = File.AppendText("C:\\Temp\\FourCount.txt")) {
					sw.WriteLine(sb.ToString());
				}
			}

			button2.Enabled = true;

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
