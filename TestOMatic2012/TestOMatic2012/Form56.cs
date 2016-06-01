using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form56 : Form {
		public Form56() {
			InitializeComponent();
			Logger.LoggerWrite += form8_onLoggerWrite;
		}
		//---------------------------------------------------------------------------------------------------------
		//---------------------------------------------------------------------------------------------------------
		private const string PATTERN = @",(?!(?<=(?:^|,)\s*\x22(?:[^\x22]|\x22\x22|\\\x22)*,)(?:[^\x22]|\x22\x22|\\\x22)*\x22\s*(?:,|$))";
		private Regex _regX = new Regex(PATTERN);
		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			ProcessDirectories();

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();
		}
		//---------------------------------------------------------------------------------------------------------
		private void LogChange(string fileName) {

			string filePath = "C:\\temp\\ChangeMixFiles.txt";

			using (StreamWriter sw = File.AppendText(filePath)) {
				sw.WriteLine(fileName);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFile(FileInfo fi) {

			bool fileChanged = false;

			StringBuilder sb = new StringBuilder();

			using (StreamReader sr = fi.OpenText()) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = _regX.Split(line);

					if (items.Length == 10) {
						if (items[1].Contains("Combos")) {

							items[9] = "0";
							line = "";

							for (int i = 0; i < items.Length; i++) {

								if (i > 0) {
									line += ",";
								}

								line += items[i];
							}

							fileChanged = true;
						}
					}

					sb.Append(line);
					sb.Append("\r\n");
				}
			}

			if (fileChanged) {

				Logger.Write("File: " + fi.Name + " has changed.  Updating...");

				fi.Delete();

				using (StreamWriter sw = new StreamWriter(fi.Open(FileMode.CreateNew))) {

					sw.Write(sb.ToString());
				}

				LogChange(fi.FullName);

				string copyPath = Path.Combine("C:\\MixFiles", fi.Name);
				fi.CopyTo(copyPath, true);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo di) {

			FileInfo[] files = di.GetFiles("X110*mix.pol");

			foreach (FileInfo file in files) {
				Logger.Write("Processing file: " + file.Name);
				ProcessFile(file);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDirectories() {

			string directoryPath = "D:\\xdata1\\cmsos2\\ckenode";

			DirectoryInfo di = new DirectoryInfo(directoryPath);

			DirectoryInfo[] directories = di.GetDirectories();

			foreach (DirectoryInfo directory in directories) {

				Logger.Write("Processing directory: " + directory.Name);
				ProcessDirectory(directory);
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
