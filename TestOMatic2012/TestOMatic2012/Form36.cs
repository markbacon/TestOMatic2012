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
	public partial class Form36 : Form {
		public Form36() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			DirectoryInfo di = new DirectoryInfo("D:\\xdata1\\cmsos2\\ckenode");

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			using (StreamWriter sw = new StreamWriter("C:\\Temp\\ConsolidatedPCN.txt")) {


				foreach (DirectoryInfo directory in directories) {


					FileInfo file = directory.GetFiles("Empl.sav").SingleOrDefault();

					if (file != null) {

						if (file.Exists) {

							textBox1.Text += "File found: " + file.FullName + "\r\n";
							Application.DoEvents();

							using (StreamReader sr = file.OpenText()) {

								while (sr.Peek() != -1) {

									string line = sr.ReadLine();
									sw.WriteLine(line);
								}
							}
						}
					}
				}

			}
			button1.Enabled = true;

		}
	}
}
