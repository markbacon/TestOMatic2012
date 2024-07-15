using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Ionic.Zip;

namespace TestOMatic2012 {
	public partial class frmSmartReportsFix : Form {
		public frmSmartReportsFix() {
			InitializeComponent();

			Logger.LoggerWrite += Form89_onLoggerWrite;
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			string hfscoRootDirectory = @"\\ckeanafnp01\HFSCO_RO";

			DirectoryInfo di = new DirectoryInfo(hfscoRootDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {

				ProcessDirectory(directory);
			}


			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void Button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			DirectoryInfo di = new DirectoryInfo("G:\\ckenode");

			DirectoryInfo[] unitDirectories = di.GetDirectories("X11*");

			foreach (DirectoryInfo unitDirectory in unitDirectories) {



				FileInfo smrtRptZipFile = unitDirectory.GetFiles("SmartRpts.zip").SingleOrDefault();

				if (smrtRptZipFile != null) {

					string destPath = Path.Combine("O:\\", unitDirectory.Name, "20190922", smrtRptZipFile.Name);

					Stopwatch sw1 = new Stopwatch();
					sw1.Start();

					Logger.Write("Begin copying file: " + destPath);
					smrtRptZipFile.CopyTo(destPath, true);
					Logger.Write("Finished copying file. Elapsed time: " + sw1.Elapsed.ToString());


				}





			}

			button2.Enabled = true;


		}
		//---------------------------------------------------------------------------------------------------

		private void Button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;

			DirectoryInfo di = new DirectoryInfo("O:\\");

			DirectoryInfo[] unitDirectories = di.GetDirectories("X11*");

			foreach (DirectoryInfo unitDirectory in unitDirectories) {

				DirectoryInfo dateDirInfo = unitDirectory.GetDirectories("20190922").SingleOrDefault();

				if (dateDirInfo != null) {

					FileInfo semFile = dateDirInfo.GetFiles("SmartReports.sem").SingleOrDefault();

					if (semFile != null) {

						Logger.Write("Deleting file: " + semFile.FullName);
						semFile.Delete();
					}


				}
			}

			button3.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void Form89_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo di) {

			DateTime startDate = DateTime.Today.AddDays(-10);


			while (startDate < DateTime.Today) {

				DirectoryInfo directory = new DirectoryInfo(Path.Combine(di.FullName, startDate.ToString("yyyyMMdd")));

				startDate = startDate.AddDays(1);

				if (directory.Exists) {

					FileInfo file = directory.GetFiles(".zip").FirstOrDefault();

					if (file != null) {

						if (file.Exists) {

							string targetPath = Path.Combine("D:\\HFSCO_RO", di.Name, directory.Name);

							if (!Directory.Exists(targetPath)) {
								Directory.CreateDirectory(targetPath);
							}

							targetPath = Path.Combine(targetPath, file.Name);

							file.CopyTo(targetPath);
						}
					}
				}
			}
		}

		private void TextBox1_TextChanged(object sender, EventArgs e) {

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
