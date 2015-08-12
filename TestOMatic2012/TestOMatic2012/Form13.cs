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
using System.Xml.Linq;

namespace TestOMatic2012 {
	public partial class Form13 : Form {
		public Form13() {
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------------------------
		TransHistDataContext _dataContext = new TransHistDataContext();

		//---------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = true;

			string rootDirectory = "D:\\TLogsIII";

			DirectoryInfo di = new DirectoryInfo(rootDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {

				ProcessDirectory(directory);

			}


			button1.Enabled = false;

		}
		//---------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {



			button2.Enabled = false;

			string importFilePath = @"\\ckeanafnp01\HFSCO_RO";
			string exportFileDirectory = @"D:\HFSCO_RO";

			DirectoryInfo di = new DirectoryInfo(importFilePath);

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {

				int directoryNum = 20140919;

				while (directoryNum < 20140927) {

					string filePath = Path.Combine(directory.FullName, directoryNum.ToString(), "SmartRpts.zip");

					FileInfo file = new FileInfo(filePath);

					if (file.Exists) {

						string exportFilePath = Path.Combine(exportFileDirectory, directory.Name, directoryNum.ToString());

						if (!Directory.Exists(exportFilePath)) {
							Directory.CreateDirectory(exportFilePath);
						}

						textBox1.Text += "Copying file: " + file.FullName + "\r\n";
						Application.DoEvents();

						file.CopyTo(Path.Combine(exportFilePath, "SmartRpts.zip"), true);

					}

					directoryNum++;
				}
			}


			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo di) {

			string unit = di.Name.Substring(1);

			DirectoryInfo directory = new DirectoryInfo(Path.Combine(di.FullName, "archive"));

			FileInfo[] files = directory.GetFiles("????????.TransHist.xml");

			foreach (FileInfo file in files) {
				ProcessFile(unit, file);
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessFile(string unit, FileInfo file) {

			DateTime startTime = DateTime.Now;
			textBox1.Text += "Begin processing file:  " + file.Name + " for unit: " + unit + "\r\n";
			Application.DoEvents();

			TransHistDataContext dataContext = new TransHistDataContext();


			TransHistArchive tha = new TransHistArchive();

			tha.CreateDate = DateTime.Now;
			tha.FileDate = file.LastWriteTime;
			tha.FileName = file.Name;

			using (StreamReader sr = new StreamReader(file.OpenRead())) {
				tha.FileXml = XElement.Parse(sr.ReadToEnd());
			}

			tha.Unit = unit;

			if (dataContext.TransHistArchives.Where(t => t.Unit == unit && t.FileName == tha.FileName).Count() == 0) {
				textBox1.Text += "Saving file" + "\r\n";
				Application.DoEvents();

				dataContext.TransHistArchives.InsertOnSubmit(tha);
				dataContext.SubmitChanges();
			}
			else {
				textBox1.Text += "File already exists." + "\r\n";
				Application.DoEvents();
			}

			textBox1.Text += "Processing completed file:  " + file.Name + " for unit: " + unit + " Elapsed time: " + (DateTime.Now - startTime).ToString() + "\r\n";
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
