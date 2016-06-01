using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form48 : Form {
		public Form48() {
			InitializeComponent();
		}

		private System.Diagnostics.EventLog eventLog2 = new EventLog();

		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			Unzipit("C:\\Inbox");

			button2.Enabled = true;

		}

		//**********************************************************************************
		//MenuMix Summary of Red/Green Burrito and Drinks for Franchise royalty calculation
		//**********************************************************************************
		private void SummarizeMenu(string pollFilename, string Archivename, string ConsolidationfilePath) {
			bool exists;
			StreamWriter ConsolidateOutfile;

			try {
				string filePath = @Archivename;
				StreamReader sr = new StreamReader(filePath);
				List<string[]> data = new List<string[]>();
				string Header_row = "Unit,Date,RedGreen Burrito Sales,Drink Sales";
				string Outrow = "";
				int Row = 0;
				int x;
				double BurritoValue = 0, DrinkValue = 0;

				while (!sr.EndOfStream) {
					string[] Line = sr.ReadLine().Split(',');
					data.Add(Line);
					Row++;
				}
				sr.Close();

				//strip quotes off of store number
				string store_nbr = data[2][1];
				store_nbr = store_nbr.Replace("\"", "");

				//If "Consolidations" path doesn't exist, create them
				exists = System.IO.Directory.Exists(ConsolidationfilePath);
				if (!exists) System.IO.Directory.CreateDirectory(ConsolidationfilePath);

				//Now check to see if the consolidation file already exist for the business date.  If not, create it and write header records.  Otherwise, just open it.
				if (data.Count == 0)
					return;
				string ConsolidationFilename = data[1][1] + "MNU.csv";
				string ConsolidationFile = Path.Combine(ConsolidationfilePath, ConsolidationFilename);
				if (!File.Exists(ConsolidationFile)) {
					ConsolidateOutfile = File.AppendText(ConsolidationFile);
					ConsolidateOutfile.WriteLine(Header_row);
				}
				else {
					//File already exists
					//Before we proceed to write the store record, we will first make sure it doesn't already exist.
					//If the store already exists in the file, we will not add it again
					StreamReader mn = new StreamReader(ConsolidationFile);
					while (!mn.EndOfStream) {
						string[] mnLine = mn.ReadLine().Split(',');
						if (mnLine[0] == store_nbr) {
							mn.Close();
							return;
						}
					}
					mn.Close();
					ConsolidateOutfile = File.AppendText(ConsolidationFile);
				}

				//Reformat data as decimal and consolidate
				Outrow = store_nbr + "," + data[1][1];  //Restaurant_no and Date

				for (x = 4; x < Row; x++) {
					if (data[x][1].ToLower().Contains("beverages") || data[x][1].ToLower().Contains("drinks"))
						DrinkValue += System.Convert.ToDouble(data[x][5]);
					else
						if (data[x][1].ToLower().Contains("red burrito") || data[x][1].ToLower().Contains("green burrito"))
							BurritoValue += System.Convert.ToDouble(data[x][5]);
				}

				Outrow = Outrow + "," + BurritoValue.ToString("###0.00");
				Outrow = Outrow + "," + DrinkValue.ToString("###0.00");
				ConsolidateOutfile.WriteLine(Outrow);
				ConsolidateOutfile.Close();
			}
			catch (Exception ex) {
				eventLog2.Source = "FTPFranchise";
				eventLog2.WriteEntry("Error summarizing Menu for royalties " + Archivename + " - " + ex.Message);
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
				eventLog2.Source = "FTPFranchise";
				eventLog2.WriteEntry("Error unzipping from " + LocalFolder + " - " + ex.Message);
			}

		}
		//---------------------------------------------------------------------------------------------------
		private void Unzipzips(string ZIPname, string subPath, string pollPath) {
			bool exists;
			string Archivename, Store_name, Store_date;//, Prior_Store_date = "";

			try {
				//Refresh the path for Reports, TLogs and Pollfiles
				string ReportPath = "C:\\PollFile\\Reports";
				string TLogPath = "C:\\PollFile\\TLogs";
				string PollfilePath = "C:\\PollFile\\PollFiles";
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
								pollFilename.ToLower().Contains(".fcp") || pollFilename.ToLower().Contains(".csv") || pollFilename.ToLower().Contains("hourlynetsales")) {
								string Pollfile_Store_Path = Path.Combine(PollfilePath, Store_name);
								exists = System.IO.Directory.Exists(Pollfile_Store_Path);
								if (!exists) System.IO.Directory.CreateDirectory(Pollfile_Store_Path);
								Archivename = Path.Combine(Pollfile_Store_Path, Path.GetFileName(pollFilename));
								File.Delete(Archivename);
								File.Move(pollFilename, Archivename);

								// Consolidate Financials and Timekeeping data for subsequent above-store processin g
								//if (pollFilename.ToLower().Contains(".fin"))
									//ConsolidateFin(pollFilename, Archivename, ConsolidationfilePath);
								//if (pollFilename.ToLower().Contains("laborhours.pol"))
									//ConsolidateTime(pollFilename, Archivename, ConsolidationfilePath);
									//if (pollFilename.ToLower().Contains("wktime.pol")) {
										//ConsolidateWkTimePol(pollFilename, Archivename, ConsolidationfilePath);
									
									//else
										//if (pollFilename.ToLower().Contains("time.pol"))
											//ConsolidateTimePol(pollFilename, Archivename, ConsolidationfilePath);
											//if (pollFilename.ToLower().Contains("selfauthtracking"))
												//ConsolidateSelfAuth(pollFilename, Archivename, ConsolidationfilePath);
								if (pollFilename.ToLower().Contains("mix.pol")) {
									SummarizeMenu(pollFilename, Archivename, ConsolidationfilePath);
								}
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
										if (!exists) {
											System.IO.Directory.CreateDirectory(Reports_Store_Path);
										}

										//if (1 == 0 && pollFilename.ToLower().Contains("weekly") && Prior_Store_date.Length > 0)
										//	Reports_Store_Path = Path.Combine(Reports_Store_Path, Prior_Store_date);
										//else {
										//	Reports_Store_Path = Path.Combine(Reports_Store_Path, Store_date);
										//	Prior_Store_date = Store_date;
										//}
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
				eventLog2.Source = "FTPFranchise";
				eventLog2.WriteEntry("Error unzipping " + ZIPname + " - " + ex.Message);
			}

		}
	}
}
