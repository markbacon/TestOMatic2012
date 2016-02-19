using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form50 : Form {
		public Form50() {
			InitializeComponent();

			Logger.LoggerWrite += form8_onLoggerWrite;
		}
		//---------------------------------------------------------------------------------------------------
		//-- Private
		//---------------------------------------------------------------------------------------------------
		DataAnalysisDataContext _dataContext = new DataAnalysisDataContext();

		//---------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			DirectoryInfo di = new DirectoryInfo(@"D:\xdata1\cmsos2\ckenode\X1100919");

			FileInfo[] files = di.GetFiles("PD*.fin");

			foreach (FileInfo file in files) {

				Logger.Write("Processing file:  " + file.Name);

				ProcessFile(file);
			}


			button1.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			Unzipit("C:\\Pollfile2\\");

			button2.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessFile(FileInfo file) {

			BasicStoreSale bss = new BasicStoreSale();

			using (StreamReader sr = file.OpenText()) {

				int counter = 1;

				while (sr.Peek() != -1) {

					string[] items = sr.ReadLine().Split(new char[] {','});

					if (counter++ == 1) {
						bss.BusinessDate = DateTime.ParseExact(items[0], "MMddyy", System.Globalization.CultureInfo.InvariantCulture);
						bss.RestaurantNumber = 1100000 + Convert.ToInt32(items[3]);
					}


					switch (items[4]) {

						case "2":
							bss.GrossSales = Convert.ToDecimal(items[11]) / 100M;
							break;

						case "3":
							bss.NetSales = Convert.ToDecimal(items[11]) / 100M;
							break;

						case "8":
							bss.TaxableSales = Convert.ToDecimal(items[11]) / 100M;
							break;

						case "9":
							bss.SalesTax = Convert.ToDecimal(items[11]) / 100M;
							break;

						case "49":
							bss.NonFoodSales = Convert.ToDecimal(items[11]) / 100M;
							break;

					}
				}

				_dataContext.BasicStoreSales.InsertOnSubmit(bss);
				_dataContext.SubmitChanges();
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
		//---------------------------------------------------------------------------------------------------
		private void Unzipit(string LocalFolder) {
			bool exists;

			try {
				//If "unzippedzips" subdirectory doesn't exist, create it
				string subPath = LocalFolder + "unzippedzips";
				exists = System.IO.Directory.Exists(subPath);
				if (!exists) System.IO.Directory.CreateDirectory(subPath);

				//If "pollfiles" subdirectory doesn't exist, create it
				string pollPath = LocalFolder + "Pollfiles";
				exists = System.IO.Directory.Exists(pollPath);
				if (!exists) System.IO.Directory.CreateDirectory(pollPath);

				//If "Archive" subdirectory doesn't exist, create it
				string archivePath = LocalFolder + "Archive";
				exists = System.IO.Directory.Exists(archivePath);
				if (!exists) System.IO.Directory.CreateDirectory(archivePath);

				//Traverse the inbox directory to find files that must be unzipped
				// Put all zip files in root directory into array.
				string[] array1 = Directory.GetFiles(@LocalFolder, "*.ZIP"); // <-- Case-insensitive

				// unzip all zipfiles found after emptying the unzippedzips folder of all files
				foreach (string ZIPname in array1) {
					string[] filePaths = Directory.GetFiles(subPath);
					foreach (string filePath in filePaths)
						File.Delete(filePath);
					System.IO.Compression.ZipFile.ExtractToDirectory(ZIPname, subPath);
					//Now go unzip the unzipped zipfiles before unzipping next master zipfile
					Unzipzips(ZIPname, subPath, pollPath);

					//Now that all content has been unzipped, archive the master zipfile
					string Archivename = Path.Combine(archivePath, Path.GetFileName(ZIPname));
					File.Delete(Archivename);
					File.Move(ZIPname, Archivename);
				}
			}
			catch (Exception ex) {

				Logger.Write("An exception occurred in Unzipit.  Please see error log for details.");
				Logger.WriteError(ex);
			}
		}
		private void Unzipzips(string ZIPname, string subPath, string pollPath) {
			bool exists;
			string Archivename, Store_name, Store_date, Prior_Store_date = "";

			try {
				//Refresh the path for Reports, TLogs and Pollfiles
				string ReportPath = "C:\\PollFile2\\Reports";
				string TLogPath = "C:\\PollFile2\\TLogs";
				string PollfilePath = "C:\\PollFile2\\Pollfiles";
				string ConsolidationfilePath = Path.Combine(PollfilePath, "Consolidations");
				//If "Reports", "TLog" or "Pollfile" paths don't exist, create them
				exists = System.IO.Directory.Exists(ReportPath);
				if (!exists) System.IO.Directory.CreateDirectory(ReportPath);
				exists = System.IO.Directory.Exists(TLogPath);
				if (!exists) System.IO.Directory.CreateDirectory(TLogPath);
				exists = System.IO.Directory.Exists(PollfilePath);
				if (!exists) System.IO.Directory.CreateDirectory(PollfilePath);

				//Traverse the unzippedzips subdirectory to unzip the poll files
				string[] array2 = Directory.GetFiles(subPath, "*.ZIP"); // <-- Case-insensitive
				// unzip all zipfiles found
				foreach (string ZIPzipname in array2) {
					Store_name = ZIPzipname.Substring(ZIPzipname.IndexOf("X1"), 8);

					using (ZipArchive archive = System.IO.Compression.ZipFile.OpenRead(ZIPzipname)) {
						foreach (ZipArchiveEntry entry in archive.Entries)
							entry.ExtractToFile(pollPath + "\\" + entry.FullName, true);

						// All files are unzipped.  Now traverse the entries and move them to their corresponding permanent storage folders
						// Clean up the pollPath after all files are moved to ready for the next unzip
						string[] array1 = Directory.GetFiles(pollPath, "*.*"); // <-- Case-insensitive
						foreach (string pollFilename in array1) {
							if (pollFilename.Contains("\\X1") || pollFilename.ToLower().Contains(".pol") || pollFilename.ToLower().Contains(".fin") ||
								pollFilename.ToLower().Contains(".fcp") || pollFilename.ToLower().Contains(".csv") ||
								pollFilename.ToLower().Contains("invsummary") || pollFilename.ToLower().Contains("hourlynetsales")) {
								string Pollfile_Store_Path = Path.Combine(PollfilePath, Store_name);
								exists = System.IO.Directory.Exists(Pollfile_Store_Path);
								if (!exists) System.IO.Directory.CreateDirectory(Pollfile_Store_Path);
								Archivename = Path.Combine(Pollfile_Store_Path, Path.GetFileName(pollFilename));
								File.Delete(Archivename);
								File.Move(pollFilename, Archivename);

								//// Consolidate Financials and Timekeeping data for subsequent above-store processin g
								//if (pollFilename.ToLower().Contains(".fin"))
								//	ConsolidateFin(pollFilename, Archivename, ConsolidationfilePath);
								//if (pollFilename.ToLower().Contains("laborhours.pol"))
								//	ConsolidateTime(pollFilename, Archivename, ConsolidationfilePath);
								//if (pollFilename.ToLower().Contains("wktime.pol"))
								//	ConsolidateWkTimePol(pollFilename, Archivename, ConsolidationfilePath);
								//else
								//	if (pollFilename.ToLower().Contains("time.pol"))
								//		ConsolidateTimePol(pollFilename, Archivename, ConsolidationfilePath);
								//if (pollFilename.ToLower().Contains("selfauthtracking"))
								//	ConsolidateSelfAuth(pollFilename, Archivename, ConsolidationfilePath);
								//if (pollFilename.ToLower().Contains("mix.pol"))
								//	SummarizeMenu(pollFilename, Archivename, ConsolidationfilePath);
							}
							else
								if (pollFilename.ToLower().Contains("transhist.xml")) {
									Store_date = pollFilename.Substring(pollFilename.Length - 22, 8);
									string TLog_Store_Path = Path.Combine(TLogPath, Store_name);
									exists = System.IO.Directory.Exists(TLog_Store_Path);
									if (!exists) System.IO.Directory.CreateDirectory(TLog_Store_Path);
									TLog_Store_Path = Path.Combine(TLog_Store_Path, Store_date);
									exists = System.IO.Directory.Exists(TLog_Store_Path);
									if (!exists) System.IO.Directory.CreateDirectory(TLog_Store_Path);
									Archivename = Path.Combine(TLog_Store_Path, Path.GetFileName(pollFilename));
									File.Delete(Archivename);
									File.Move(pollFilename, Archivename);
								}
								else
									if (pollFilename.ToLower().Contains("xml")) {
										Store_date = pollFilename.Substring(pollFilename.Length - 12, 8);
										string Reports_Store_Path = Path.Combine(ReportPath, Store_name);
										exists = System.IO.Directory.Exists(Reports_Store_Path);
										if (!exists) System.IO.Directory.CreateDirectory(Reports_Store_Path);
										if (1 == 0 && pollFilename.ToLower().Contains("weekly") && Prior_Store_date.Length > 0)
											Reports_Store_Path = Path.Combine(Reports_Store_Path, Prior_Store_date);
										else {
											Reports_Store_Path = Path.Combine(Reports_Store_Path, Store_date);
											Prior_Store_date = Store_date;
										}
										exists = System.IO.Directory.Exists(Reports_Store_Path);
										if (!exists) System.IO.Directory.CreateDirectory(Reports_Store_Path);
										Archivename = Path.Combine(Reports_Store_Path, Path.GetFileName(pollFilename));
										File.Delete(Archivename);
										File.Move(pollFilename, Archivename);
									}
							File.Delete(pollFilename);

						}

					}
				}
			}
			catch (Exception ex) {
			}

		}

	}
}
