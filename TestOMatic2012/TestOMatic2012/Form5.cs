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
	public partial class Form5 : Form {
		public Form5() {
			InitializeComponent();
			Logger.LoggerWrite += form8_onLoggerWrite;
		}
		//---------------------------------------------------------------------------------------------------
		private void form8_onLoggerWrite(object sender, LoggerEventArgs e) {

			textBox1.Text += e.Message + "\r\n";
			Application.DoEvents();
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			DataAnalysisDataContext dataContext = new DataAnalysisDataContext();

			List<string> win7NodeList = Win7NodeUtility.GetWin7NodeList();

			foreach (string win7Node in win7NodeList) {

				Win7Unit win7Unit = new Win7Unit();

				win7Unit.RestaurantNo = Convert.ToInt32(win7Node.Substring(1));
				win7Unit.UnitNumber = win7Node.Trim();

				textBox1.Text = win7Unit.UnitNumber;
				Application.DoEvents();

				dataContext.Win7Units.InsertOnSubmit(win7Unit);
				dataContext.SubmitChanges();
			}

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;


			//SmartReportsProcessor srp = new SmartReportsProcessor();
			//srp.RunAgainstStoreArchive();

			//string sql = textBox1.Text;

			//DataAccess dac = new DataAccess(AppSettings.IRISConnectionString);

			//DataTable dt = dac.ExecuteQuery(sql);

			//dt.TableName = "DayPartSales";

			//dt.WriteXmlSchema("C:\\temp\\DayPartSales.xsd");

			string fileDirectory = "D:\\xdata1\\cmsos2\\ckenode";

			DirectoryInfo di = new DirectoryInfo(fileDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {

				FileInfo[] files = directory.GetFiles("*.fin");

				foreach (FileInfo file in files) {

					string targetPath = Path.Combine(file.DirectoryName, "mtier", file.Name);
					file.CopyTo(targetPath, true);
				}
			}



			button2.Enabled = true;
		}

		private void button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;

			string fileDirectory = "D:\\xdata1\\cmsos2\\ckenode";

			DirectoryInfo di = new DirectoryInfo(fileDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {

				FileInfo[] files = directory.GetFiles("*.fin");

				foreach (FileInfo file in files) {

					ZipFile zippy = new ZipFile(directory.Name + ".zip");
					zippy.AddFile(file.FullName, "");

					string targetPath = Path.Combine(file.DirectoryName, "mtier", zippy.Name);

					zippy.Save(targetPath);

				}
			}

			button3.Enabled = true;
		}

		private void button4_Click(object sender, EventArgs e) {

			button4.Enabled = false;

			string fileDirectory = "D:\\xdata1\\cmsos2\\ckenode";

			DirectoryInfo di = new DirectoryInfo(fileDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {

				string srcPath = Path.Combine(@"\\xdata1\cmsos2\ckenode" , directory.Name);

				DirectoryInfo srcDirectory = new DirectoryInfo(srcPath);

				FileInfo[] files = srcDirectory.GetFiles("*.pol");

				foreach (FileInfo file in files) {
					string destPath = Path.Combine(directory.FullName, file.Name);
					file.CopyTo(destPath, true);
				}

				files = srcDirectory.GetFiles("*.fin");

				foreach (FileInfo file in files) {
					string destPath = Path.Combine(directory.FullName, file.Name);
					file.CopyTo(destPath, true);
				}
	
			}

			button4.Enabled = false;
		}

		private void button5_Click(object sender, EventArgs e) {

			button5.Enabled = false;

			ShamrockXmlFileProcessor fileProcessor = new ShamrockXmlFileProcessor();

			string srcDirName = @"C:\CkeProcesses\DmaFileProcessor\Archive\Shamrock";

			DirectoryInfo di = new DirectoryInfo(srcDirName);

			DirectoryInfo[] dateDirectories = di.GetDirectories("2023*");

			foreach (DirectoryInfo dateDirectory in dateDirectories) {

				fileProcessor.ProcessFiles(dateDirectory.FullName);
			}





			button5.Enabled = true;
		}

		class Foo {
			public string UnitNumber;
			public string DateString;
			public string FilEname;


		}



		private void button6_Click(object sender, EventArgs e) {


			//List<Foo> fooList = new List<Foo>();
			StringBuilder sb = new StringBuilder();

			DateTime businessDate = new DateTime(2019, 12, 31);

			while (businessDate >= new DateTime(2019, 4, 25)) {

				sb.Append("BrinkAPiFileGenerator /Action=CreateFinFile /BusinessDate=");
				sb.Append(businessDate.ToString("yyyy-MM-dd"));
				sb.Append("\r\n");

				businessDate = businessDate.AddDays(-1);
			}

			textBox1.Text = sb.ToString();


			//string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

			//foreach (string line in lines) {

			//	string[] items = line.Split('\t');

			//	if (items[1].Trim() == "2023-07-17") {

			//		if (fooList.Where(f => f.UnitNumber == items[0] && f.DateString == items[1]).Count() == 0) {
			//			Foo foo = new Foo() {
			//				UnitNumber = items[0],
			//				DateString = items[1],
			//				FilEname = items[2]
			//			};


			//			fooList.Add(foo);

			//			sb.Append(line);
			//			sb.Append("\r\n");
			//		}
			//	}
			//}



		}

		private void button7_Click(object sender, EventArgs e) {

			button7.Enabled = false;

			string year = "2019";
			CopyFinFiles(year);

			button7.Enabled = true;
		}
		private void button8_Click(object sender, EventArgs e) {

			button8.Enabled = false;

			string year = "2020";
			CopyFinFiles(year);

			button8.Enabled = true;

		}

		private void button9_Click(object sender, EventArgs e) {

			button9.Enabled = false;

			string year = "2021";
			CopyFinFiles(year);

			button9.Enabled = true;

		}

		private void button10_Click(object sender, EventArgs e) {

			button10.Enabled = false;

			string year = "2022";
			CopyFinFiles(year);

			button10.Enabled = true;

		}

		private void button11_Click(object sender, EventArgs e) {

			button11.Enabled = false;

			string year = "2023";
			CopyFinFiles(year);

			button11.Enabled = true;

		}

		private void button12_Click(object sender, EventArgs e) {

			button12.Enabled = false;

			using (StreamReader sr = new StreamReader("C:\\Temp105.mm\\MissingShamrock.txt")) {

				using (INFO2000DataContext dataContext = new INFO2000DataContext()) {

					while (sr.Peek() != -1) {

						string[] items = sr.ReadLine().Split('\t');

						ShamrockInvoice shamrock = new ShamrockInvoice() {
							DueDate = Convert.ToDateTime(items[8]),
							InvoiceDate = Convert.ToDateTime(items[1]),
							InvoiceNumber = items[2].Trim(),
							Memo = items[4].Trim(),
							RefInvoiceNumber = items[3].Trim(),
							RemainingAmount = Convert.ToDecimal(items[7]),
							SettledAmount = Convert.ToDecimal(items[6]),
							Type = items[5].Trim(),
							UnitNumber = items[0].Trim()
						};

						Logger.Write("Processing Invoice: " + shamrock.InvoiceNumber);

						dataContext.ShamrockInvoices.InsertOnSubmit(shamrock);
						dataContext.SubmitChanges();
					}
				}
			}

			button12.Enabled = true;
		}

		private void button13_Click(object sender, EventArgs e) {

			button13.Enabled = false;

			string previousInvoiceNumber = "";

			VCHR3Header hdr = null;

			using (StreamReader sr = new StreamReader("C:\\Temp105.mm\\MissingInvoiceData.txt")) {



				using (INFO2000DataContext dataContext = new INFO2000DataContext()) {

					while (sr.Peek() != -1) {

						string[] items = sr.ReadLine().Split('\t');

						string invoiceNumber = items[1];

						if (invoiceNumber != previousInvoiceNumber) {
							hdr = new VCHR3Header() {
								BusinessUnit = "210",
								CreateDate = DateTime.Now,
								FileName = "Created From CT Invoice Data",
								InvoiceDate = DateTime.Today,
								InvoiceNumber = invoiceNumber,
								InvoiceTotal = Convert.ToDecimal(items[2]),
								LoadDate = DateTime.Today,
								ProcessFlag = "Ready To Process",
								UnitNumber = items[0].Substring(0, 7),
							};

							dataContext.VCHR3Headers.InsertOnSubmit(hdr);
							dataContext.SubmitChanges();

							previousInvoiceNumber = invoiceNumber;
						}

						VCHR3Detail dtl = new VCHR3Detail() {
							CreateDate = DateTime.Now,
							GlAccountNumber = items[5],
							ItemAmount = Convert.ToDecimal(items[8]),
							ItemNumber = items[3],
							LineDescription = "SHAMROCK INVOICE",
							VCHR3HeaderId = hdr.VCHR3HeaderId
						};

						dataContext.VCHR3Details.InsertOnSubmit(dtl);
						dataContext.SubmitChanges();
					}
				}
			}

			button13.Enabled = true;

		}


		private void CopyFinFiles(string year) {

			try {
				Stopwatch sw1 = new Stopwatch();
				sw1.Start();
				Logger.Write("Begin processing FIN files for " + year);


				string logPath = "C:\\AppTest\\FinFileLog_" + year + ".txt";
				List<string> fileNames = new List<string>();

				if (File.Exists(logPath)) {
					using (StreamReader sr = new StreamReader(logPath)) {
						while (sr.Peek() != -1) {
							fileNames.Add(sr.ReadLine());
						}
					}
				}

				string srcDirName = @"C:\Xdata1\cmsos2\NewFinFiles_" + year;
				string destDirName = "C:\\FinFiles_" + year;

				if (!Directory.Exists(destDirName)) {
					Logger.Write("Creating directory: " + destDirName);
					Directory.CreateDirectory(destDirName);
				}

				DirectoryInfo mainDirectory = new DirectoryInfo(srcDirName);

				DirectoryInfo[] unitDirectories = mainDirectory.GetDirectories();

				foreach (DirectoryInfo unitDirectory in unitDirectories) {
					Stopwatch sw2 = new Stopwatch();
					sw2.Start();
					Logger.Write("Begin processing unit directory: " + unitDirectory.Name);


					FileInfo[] finFiles = unitDirectory.GetFiles("*.fin");

					foreach (FileInfo finFile in finFiles) {

						if (fileNames.Where(f => f == finFile.Name).Count() == 0) {
							fileNames.Add(finFile.Name);

							string destPath = Path.Combine(destDirName, finFile.Name);
							Logger.Write("Copying file: " + destPath);
							finFile.CopyTo(destPath, true);
						}
						else {
							Logger.Write("FIN file: " + finFile.Name + " has already been copied.");
						}
					}

					Logger.Write("Finished processing unit directory: " + unitDirectory.Name + ". Elapsed time: " + sw2.Elapsed.ToString());

				}

				using (StreamWriter sw = new StreamWriter(logPath)) {
					foreach (string fileName in fileNames) {
						sw.WriteLine(fileName);
					}
				}

				Logger.Write("Finished processing FIN files for " + year + ".  Elpased time: " + sw1.Elapsed.ToString());



			}
			catch (Exception ex) {
				Logger.Write("An exception occurred in CopyFinFiles.  Please see error log for details.");
				Logger.WriteError(ex);
			}
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
