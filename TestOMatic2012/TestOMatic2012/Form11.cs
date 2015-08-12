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
using Ionic.Zip;

namespace TestOMatic2012 {
	public partial class Form11 : Form {
		public Form11() {
			InitializeComponent();
		}


		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			ProcessCkeNodeirectoryX();

			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private List<string> GetStarPosUnitList() {

			List<string> starPosUnitList = new List<string>();

			starPosUnitList.Add("X1100104");
			starPosUnitList.Add("X1100106");
			starPosUnitList.Add("X1100132");
			starPosUnitList.Add("X1100194");
			starPosUnitList.Add("X1100140");
			starPosUnitList.Add("X1100280");


			return starPosUnitList;
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessCkeNodeirectory() {

			List<string> starPosUnitList = GetStarPosUnitList();

			string filePath = @"\\xdata1\cmsos2\ckenode\";




			DirectoryInfo di = new DirectoryInfo(filePath);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {

				textBox1.Text += "Processing Directory:  " + directory.Name + "\r\n";

				////if (String.Compare(directory.Name, "X1501610") > 0) {
				if (starPosUnitList.Contains(directory.Name)) {

					ProcessDirectoryHardees(directory);
				}
				else {
					ProcessDirectory(directory);
				}


				//}
			}

		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessCkeNodeirectoryX() {

			List<string> starPosUnitList = GetStarPosUnitList();

			string filePath = @"\\xdata1\cmsos2\ckenode\";




			DirectoryInfo di = new DirectoryInfo(filePath);


			foreach (string starPosUnit in starPosUnitList) {

				DirectoryInfo[] directories = di.GetDirectories(starPosUnit);

				foreach (DirectoryInfo directory in directories) {

					textBox1.Text += "Processing Directory:  " + directory.Name + "\r\n";

					ProcessDirectoryHardees(directory);
				}
			}

		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo di) {

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

					file.CopyTo(copyPath, true);

					if (file.Name.IndexOf(".fin", StringComparison.InvariantCultureIgnoreCase) > -1
						|| file.Name.IndexOf(".pol", StringComparison.InvariantCultureIgnoreCase) > -1
						|| file.Name.IndexOf(".fcp", StringComparison.InvariantCultureIgnoreCase) > -1) {

						//string mtierDirectory = copyDirectory + "\\mtier\\SalesLabor";
						string mtierDirectory = copyDirectory + "\\mtier";

						if (!Directory.Exists(mtierDirectory)) {
							Directory.CreateDirectory(mtierDirectory);
						}

						string mtierPath = mtierDirectory + "\\" + file.Name;
						file.CopyTo(mtierPath, true);

						if (file.Name.IndexOf("wktime.pol", StringComparison.InvariantCultureIgnoreCase) > -1) {

							string timeDirectory = copyDirectory + "\\Time";

							if (!Directory.Exists(timeDirectory)) {
								Directory.CreateDirectory(timeDirectory);
							}



							file.CopyTo(Path.Combine(timeDirectory, file.Name), true);

						}



					}
				}
			//}
		}
		//---------------------------------------------------------------------------------------------------------
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

				file.CopyTo(copyPath, true);

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
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDirectoryII(DirectoryInfo di) {

			textBox1.Text += "Processing Directory: " + di.FullName + "\r\n";
			Application.DoEvents();

			string copyDirectory = @"D:\xdata1\cmsos2\ckenode\" + di.Name;

			if (!Directory.Exists(copyDirectory)) {
				Directory.CreateDirectory(copyDirectory);
			}



			FileInfo[] files = di.GetFiles("coupon_orders.csv");

			foreach (FileInfo file in files) {

				string copyPath = copyDirectory + "\\" + file.Name;

				if (!File.Exists(copyPath)) {
					textBox1.Text += "Copying file: " + copyPath + "\r\n";
					Application.DoEvents();

					file.CopyTo(copyPath, true);
				}
			}
			files = di.GetFiles("*pd*.fin");

			foreach (FileInfo file in files) {

				string copyPath = copyDirectory + "\\" + file.Name;

				if (!File.Exists(copyPath)) {
					textBox1.Text += "Copying file: " + copyPath + "\r\n";
					Application.DoEvents();

					file.CopyTo(copyPath, true);
				}
			}

			files = di.GetFiles("*MixDest.pol");

			foreach (FileInfo file in files) {

				string copyPath = copyDirectory + "\\" + file.Name;

				if (!File.Exists(copyPath)) {
					textBox1.Text += "Copying file: " + copyPath + "\r\n";
					Application.DoEvents();

					file.CopyTo(copyPath, true);
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDirectoryIII(DirectoryInfo di) {

			textBox1.Text += "Processing Directory: " + di.FullName + "\r\n";
			Application.DoEvents();

			string copyDirectory = @"D:\xdata1\cmsos2\ckenode\" + di.Name;

			if (!Directory.Exists(copyDirectory)) {
				Directory.CreateDirectory(copyDirectory);
			}



			FileInfo[] files = di.GetFiles("coupon_orders.csv");

			foreach (FileInfo file in files) {

				string copyPath = copyDirectory + "\\" + file.Name;

				if (!File.Exists(copyPath)) {
					textBox1.Text += "Copying file: " + copyPath + "\r\n";
					Application.DoEvents();

					file.CopyTo(copyPath, true);
				}
			}
			files = di.GetFiles("*pd*.fin");

			foreach (FileInfo file in files) {

				string copyPath = copyDirectory + "\\" + file.Name;

				if (!File.Exists(copyPath)) {
					textBox1.Text += "Copying file: " + copyPath + "\r\n";
					Application.DoEvents();

					file.CopyTo(copyPath, true);
				}
			}

			files = di.GetFiles("*MixDest.pol");

			foreach (FileInfo file in files) {

				string copyPath = copyDirectory + "\\" + file.Name;

				if (!File.Exists(copyPath)) {
					textBox1.Text += "Copying file: " + copyPath + "\r\n";
					Application.DoEvents();

					file.CopyTo(copyPath, true);
				}
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

		private void button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;

			string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

			List<string> units = new List<string>();

			foreach (string line in lines) {
				units.Add("X" + line.Trim());
			}


			string filePath = @"\\xdata1\cmsos2\ckenode";

			StringBuilder sb = new StringBuilder();

			DirectoryInfo di = new DirectoryInfo(filePath);

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {

				if (units.Where(u => u == directory.Name).Count() == 0) {
					continue;
				}



				FileInfo file = directory.GetFiles("Coupon_orders.csv").FirstOrDefault();

				if (file != null) {
					if (file.Exists) {

						using (StreamReader sr = file.OpenText()) {

							sb.Append(directory.Name);
							sb.Append(',');
							sb.Append(sr.ReadLine());
							sb.Append("\r\n");
						}
					}
				}
			}

			textBox1.Text = sb.ToString();

			button3.Enabled = true;
		}

		private void button4_Click(object sender, EventArgs e) {

			button4.Enabled = false;

			string filePath = @"D:\xdata1\cmsos2\ckenode";

			DirectoryInfo di = new DirectoryInfo(filePath);

			DirectoryInfo[] directories = di.GetDirectories("X1*");

			foreach (DirectoryInfo directory in directories) {

				textBox1.Text += "Processing Directory:  " + directory.Name + "\r\n";
				Application.DoEvents();

				string subDirName = "";

				//if (directory.Name.StartsWith("X15")) {
				//	subDirName = "mtier\\saleslabor";
				//}
				//else {
					subDirName = "mtier";
				//}


				foreach (string searchPattern in _searchPatterns) {

					FileInfo[] files = directory.GetFiles(searchPattern);

					foreach (FileInfo file in files) {

						string fileCopyPath = Path.Combine(directory.FullName, subDirName);

						if (!Directory.Exists(fileCopyPath)) {
							Directory.CreateDirectory(fileCopyPath);
						}

						fileCopyPath = Path.Combine(fileCopyPath, file.Name);
						file.CopyTo(fileCopyPath, true);
					}
				}
			}
			button4.Enabled = true;
		}
		private void button4_Clicko(object sender, EventArgs e) {

			button4.Enabled = false;



			string filePath = @"D:\xdata1\cmsos2\ckenode";

			DirectoryInfo di = new DirectoryInfo(filePath);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {

				textBox1.Text += "Processing Directory:  " + directory.Name + "\r\n";
				Application.DoEvents();

				FileInfo[] files = directory.GetFiles("*mix.pol");
				foreach (FileInfo file in files) {

					string fileCopyPath = Path.Combine(directory.FullName, "mtier");

					if (!Directory.Exists(fileCopyPath)) {
						Directory.CreateDirectory(fileCopyPath);
					}

					fileCopyPath = Path.Combine(fileCopyPath, file.Name);
					file.CopyTo(fileCopyPath, true);
				}
			}

			button4.Enabled = true;

		}

		private void button5_Click(object sender, EventArgs e) {

			button5.Enabled = false;

			INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString);

			string filePath = @"D:\xdata1\cmsos2\ckenode";

			StringBuilder sb = new StringBuilder();

			DirectoryInfo di = new DirectoryInfo(filePath);

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {

				textBox1.Text += "Processing Directory:  " + directory.Name + "\r\n";
				Application.DoEvents();

				string unitNumber = directory.Name.Substring(1);

				FileInfo file = directory.GetFiles("Coupon_orders.csv").FirstOrDefault();

				if (file != null) {
					if (file.Exists) {

						using (StreamReader sr = file.OpenText()) {

							string line = sr.ReadLine();

							string[] items = line.Split(new char[] {','});

							DateTime businessDate = Convert.ToDateTime(items[0]);
							int quantity = Convert.ToInt32(items[1]);
							decimal amount = Convert.ToDecimal(items[2]);

							IR_Total_Net_Sale netSalesRecord = 
								(from n in dataContext.IR_Total_Net_Sales
								 where n.IR_Unit_Number == unitNumber
									&& n.IR_Sales_Date == businessDate
								 select n).SingleOrDefault();


							if (netSalesRecord != null) {
								netSalesRecord.Total_Scanned_Cpn_Amt = amount;
								netSalesRecord.Total_Scanned_Cpn_Cnt = quantity;

								dataContext.SubmitChanges();
							}
						}
					}
				}
			}



			button5.Enabled = true;
		}


		private string[] _searchPatterns = {	"*.fcp"};
		//private string[] _searchPatterns = {	"*.FIN",
		//										"*.Pol"};
		//private string[] _searchPatterns = {	"X15*_LaborAdj.pol",
		//										"X15*_LaborHours.pol",
		//										"X15*_HourlySales.pol",
		//										"X15*_MixDest.pol",
		//										"X15*_PD.FIN",
		//										"X15*_Paid.Pol"};

		private void button6_Click(object sender, EventArgs e) {

			button6.Enabled = false;


			//DirectoryInfo di = new DirectoryInfo(@"\\xdata1\remoteware\StoreArchive");
			DirectoryInfo di = new DirectoryInfo(@"C:\Store Data");


			DirectoryInfo[] directories = di.GetDirectories("X1500025");

	
			StringBuilder sb = new StringBuilder();


			foreach (DirectoryInfo directory in directories) {

				textBox1.Text = "Processing Directory:  " + directory.Name + "\r\n";
				Application.DoEvents();

				ProcessDirectory(directory, sb);
			}

			textBox1.Text = sb.ToString();

			button6.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo directory, StringBuilder sb) {

			textBox1.Text += "Processing directory:  " + directory.Name + "\r\n";
			Application.DoEvents();

			FileInfo[] files = directory.GetFiles("09*.zip");

			foreach (FileInfo file in files) {

				ProcessFile(file, sb);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFile(FileInfo file, StringBuilder sb) {


				string targetDirectory = @"C:\temp223";

				if (!Directory.Exists(targetDirectory)) {
					Directory.CreateDirectory(targetDirectory);
				}

			using (ZipFile zippy = new ZipFile(file.FullName)) {

				zippy.ExtractAll(targetDirectory, ExtractExistingFileAction.OverwriteSilently);
			}

			FileInfo couponFile = new FileInfo(Path.Combine(targetDirectory, "coupon_orders.csv"));

			if (couponFile.Exists) {

				using (StreamReader sr = couponFile.OpenText()) {
					string line = sr.ReadLine();

					sb.Append(line);
					sb.Append("\r\n");
				}
			}
		}

		private void btnCopy_Click(object sender, EventArgs e) {

			btnCopy.Enabled = false;

			string rootDirectory = "D:\\TLogsIII";

			DirectoryInfo di = new DirectoryInfo(rootDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {

				textBox1.Text += "Processing directory:  " + directory.Name + "\r\n";
				Application.DoEvents();

				string targetDirectoryName = Path.Combine("D:\\TlogData", directory.Name);

				DirectoryInfo targetDirectory = new DirectoryInfo(targetDirectoryName);

				if (!targetDirectory.Exists) {
					targetDirectory.Create();
				}
	
				int dateNum = Convert.ToInt32(DateTime.Today.AddDays(-35).ToString("yyyyMMdd"));
				int todaysDateNum = Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd"));

				while (dateNum < todaysDateNum) {

					string searchPattern = dateNum.ToString() + "*.xml";

					//FileInfo[] files = directory.GetDirectories("archive").Single().GetFiles(searchPattern);
					FileInfo[] files = directory.GetFiles(searchPattern);

					foreach (FileInfo file in files) {

						string targetPath = Path.Combine(targetDirectory.FullName, file.Name);

						if (!File.Exists(targetPath)) {

							textBox1.Text += "Copying file: " + targetPath + "\r\n";
							Application.DoEvents();

							file.CopyTo(targetPath, true);
						}
						else {
							textBox1.Text += "File: " + targetPath + " already exists\r\n";
							Application.DoEvents();

						}
					}

					dateNum++;
				}
			}

			btnCopy.Enabled = true;
		}
	}
}
