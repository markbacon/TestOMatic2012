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
	public partial class Form44 : Form {

		public Form44() {
			InitializeComponent();

			Logger.LoggerWrite += form8_onLoggerWrite;
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			StringBuilder sb = new StringBuilder();

			INFO2000Data data = new INFO2000Data();

			List<StarPosUnit> starPosUnitList = data.GetStarPosUnitList();

			foreach (StarPosUnit spu in starPosUnitList) {

				string directoryName = @"\\ckecldfnp02\CJRCO_RO";

				DateTime calDate = spu.EffectiveDate;

				while (calDate < DateTime.Today) {

					string calDateDirectoryName = Path.Combine(directoryName, spu.UnitNumber, calDate.ToString("yyyyMMdd"));

					DirectoryInfo di = new DirectoryInfo(calDateDirectoryName);

					if (di.Exists) {
						Logger.Write("Directory found:  " + calDateDirectoryName);

						if (di.GetFiles("SmartRpts.zip").Count() == 0) {

							Logger.Write("SmartRpts Zip File NOT found for:  " + calDate.ToString("MM/dd/yyyy"));
							sb.Append("SmartRpts Zip File NOT found for:  ");
							sb.Append(calDate.ToString("MM/dd/yyyy"));
							sb.Append("\t");
							sb.Append(spu.UnitNumber);
							sb.Append("\t");
							sb.Append(spu.EffectiveDate.ToString("MM/dd/yyyy"));
							sb.Append("\r\n");
						}
					}
					else {
						Logger.Write("Directory NOT found:  " + calDateDirectoryName);
					}

					calDate = calDate.AddDays(1);
				}
			}
			
			using (StreamWriter sw = new StreamWriter("C:\\temp\\Fooby.txt")) {
				sw.Write(sb.ToString());
			}

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			string srcDirName = @"D:\xdata1\ckenode";
			string destRootDirName = @"\\ckecldfnp02\cjrco_ro";

			DirectoryInfo di = new DirectoryInfo(srcDirName);

			DirectoryInfo[] directories = di.GetDirectories();

			foreach (DirectoryInfo srcDirInfo in directories) {

				FileInfo[] files = srcDirInfo.GetFiles("*.xml");

				foreach (FileInfo file in files) {

					int startPos = file.Name.IndexOf(".") - 8;

					string destSubDirName = file.Name.Substring(startPos, 8);

					string destPath = Path.Combine(destRootDirName, file.Directory.Name, destSubDirName);

					if (!Directory.Exists(destPath)) {
						Directory.CreateDirectory(destPath);
					}


					destPath = Path.Combine(destPath, file.Name);


					if (!File.Exists(destPath)) {
						Logger.Write("Copying file:  " + destPath);
						file.CopyTo(destPath, true);
					}
					else {
						Logger.Write("File already exists:  " + destPath);
					}
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
