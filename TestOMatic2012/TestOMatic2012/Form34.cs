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
	public partial class Form34 : Form {
		public Form34() {
			InitializeComponent();
		}
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;





			button1.Enabled = true;
		}
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			string dirPath = @"D:\xdata1\cmsos2\ckenode";

			DirectoryInfo di = new DirectoryInfo(dirPath);

			DirectoryInfo[] directories = di.GetDirectories();

			foreach (DirectoryInfo directory in directories) {

				ProcessDirectoryHardees(directory);
			}

			button2.Enabled = true;
		}
		private void ProcessDirectoryHardees(DirectoryInfo di) {

			textBox1.Text += "Processing Directory: " + di.FullName + "\r\n";
			Application.DoEvents();

			string copyDirectory = @"D:\xdata1\cmsos2\ckenode\" + di.Name;

			if (!Directory.Exists(copyDirectory)) {
				Directory.CreateDirectory(copyDirectory);
			}

			FileInfo[] files = di.GetFiles("*");

			foreach (FileInfo file in files) {

				string copyPath = copyDirectory + "\\" + file.Name;

				//if (!File.Exists(copyPath)) {
				textBox1.Text += "Copying file: " + copyPath + "\r\n";
				Application.DoEvents();

				if (file.Name.IndexOf(".fin", StringComparison.InvariantCultureIgnoreCase) > -1
					|| file.Name.IndexOf(".pol", StringComparison.InvariantCultureIgnoreCase) > -1) {

					string mtierDirectory = copyDirectory + "\\mtier\\SalesLabor";

					if (!Directory.Exists(mtierDirectory)) {
						Directory.CreateDirectory(mtierDirectory);
					}

					string mtierPath = mtierDirectory + "\\" + file.Name;
					file.CopyTo(mtierPath, true);
				}

				if (file.Name.IndexOf(".fcp", StringComparison.InvariantCultureIgnoreCase) > -1) {

					string mtierDirectory = copyDirectory + "\\mtier";

					if (!Directory.Exists(mtierDirectory)) {
						Directory.CreateDirectory(mtierDirectory);
					}

					string mtierPath = mtierDirectory + "\\" + file.Name;
					file.CopyTo(mtierPath, true);
				}
			}
			//}
		}

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
