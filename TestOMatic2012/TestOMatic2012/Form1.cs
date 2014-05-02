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

namespace TestOMatic2012 {

	public enum EmployeeJobCode {
		Crewhourly = 1,
		Crewleader = 2,
		Crewsuper = 3,
		Mgrtrainee = 4,
		Mgr1 = 5,
		Mgr2 = 6,
		Genmgr1 = 7,
		Genmgr2 = 8,
		Student = 9
	};


	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

//			ProcessPromoItems();//

			//MenuPollData pollData = new MenuPollData();
			//pollData.Run();

			//LaborHoursProcessor processor = new LaborHoursProcessor();
			//processor.Run();
			//textBox1.Text = processor.BuildUpdateSQL();

			StringBuilder sb = new StringBuilder();

			string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);


			int counter = 1;

			foreach (string line in lines) {

				sb.Append("'");
				sb.Append(line.Trim());
				sb.Append("', ");

				if (++counter % 10 == 0) {

					sb.Append("\r\n");

				}

			}

			textBox1.Text = sb.ToString();

			//	//sb.Append(line.Replace("  ", " "));
			//	//sb.Replace("  ", " ");
			//	//sb.Replace("  ", " ");
			//	//sb.Replace("  ", " ");
			//	//sb.Replace("  ", " ");
			//	//sb.Replace("  ", " ");
			//	//sb.Replace(" ", "\t");

			//	if (line.Length > 0) {

			//		//sb.Append(line.Substring(16, 20).Trim());
			//		//sb.Append("\t");
			//		//sb.Append(line.Substring(0, 4).Trim());
			//		//sb.Append("\t");
			//		//sb.Append(line.Substring(37, 9).Trim());


			//		//sb.Append(line.Substring(0, 7).Trim());
			//		//sb.Append("\t");
			//		//sb.Append(line.Substring(8, 20).Trim());
			//		//sb.Append("\t");
			//		//sb.Append(line.Substring(32, 7).Trim());

			//		sb.Append(line.Substring(0, 18).Trim());
			//		sb.Append("\t");
			//		sb.Append(line.Substring(19, 20).Trim());
			//		sb.Append("\t");
			//		sb.Append(line.Substring(43, 7).Trim());


			//		sb.Append("\r\n");
			//	}

			//}

			//textBox1.Text = sb.ToString();

			button1.Enabled = true;

			//CopyLaborHoursFiles();

			//DateTime start = Convert.ToDateTime("2014-02-11 08:48:17");
			//DateTime end = Convert.ToDateTime("2014-02-11 08:49:45");

			//textBox1.Text = (end - start).ToString();

			//DateTime then = new DateTime(2013, 01, 01);
			//DateTime fred = new DateTime(2013, 12, 31);


			////textBox1.Text = (DateTime.Now - then).TotalMinutes.ToString();
			////textBox1.Text = (fred - then).TotalSeconds.ToString();

			////-- Get "Then". Then needs to be at least 1 year in past but not more than 21 months
			//DateTime utcNow = DateTime.UtcNow;

			//int subtrahend = 0;


			//if (utcNow.Month < 10 || (utcNow.Month == 9 && utcNow.Day == 30 && utcNow.Hour < 23)) {
			//	subtrahend = 1;
			//}
			
			//then = new DateTime(utcNow.Year - subtrahend, 2, 2, 2, 2, 2);

			//int numericValue = (int)(utcNow - then).TotalMinutes;

			//string numString = numericValue.ToString("000000");

			//textBox1.Text += numString + "\r\n";

			//numString = new string(numString.Reverse().ToArray());

			//List<char> theCharList = new List<char>();

			//for (int i = 0; i < numString.Length; i++) {

			//	char numChar = numString[i];

			//	if (numChar == '8') {
			//		numChar = '0';
			//	}

			//	else if (numChar == '9') {
			//		numChar = '1';
			//	}

			//	else {

			//		int charVal = (int)numChar;
			//		charVal += 2;

			//		numChar = (char)charVal;
			//	}

			//	theCharList.Add(numChar);
			//}

			//textBox1.Text += numString + "\r\n";

			//StringBuilder sb = new StringBuilder();

			//foreach (char theChar in theCharList) {
			//	sb.Append(theChar);
			//}

			//textBox1.Text += sb.ToString() + "\r\n";


			////DateTime synchDate = DateTime.UtcNow;

			////string dateString = synchDate.ToString("yyyy-MM-dd");

			////string hashedDate = HashOMatic.ComputeHash(dateString);

			////textBox1.Text = hashedDate + "\r\n";


			////synchDate = synchDate.AddMinutes(10);

			////textBox1.Text += synchDate.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n"; 

			////int totMins = (int)new TimeSpan(synchDate.Hour, synchDate.Minute, synchDate.Second).TotalMinutes;

			////textBox1.Text += totMins.ToString() + "\r\n"; ;

			////totMins *= 7;

			////totMins += synchDate.DayOfYear;

			//textBox1.Text += totMins.ToString() + "\r\n"; ;

			//DateTime unDate = DateTime.UtcNow.Date;

			//int hours = totMins / 60;
			//int mins = totMins % 60;


			//totMins /= 7;

			//totMins -= unDate.DayOfYear;

			//unDate = unDate.AddMinutes(totMins);

			//textBox1.Text += unDate.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";


			//string testString = synchDate.ToString("MMddHHmm");

			//textBox1.Text += testString + "\r\n";

			//textBox1.Text +=MapNumericString(testString) + "\r\n";


			//string encryptedText = TextEncryption.EncryptText(testString);


			//textBox1.Text += encryptedText + "\r\n";




		}
		//---------------------------------------------------------------------------------------------------------
		private void CopyLaborHoursFile(string node) {

			if (node.Trim().Length > 0) {

				string filePath = @"\\xdata1\cmsos2\ckenode\" + "X" + node.Trim();

				DirectoryInfo di = new DirectoryInfo(filePath);

				FileInfo[] files = di.GetFiles("X*LaborHours.pol");

				foreach (FileInfo file in files) {

					filePath = @"C:\Temp62\" + file.Name;

					file.CopyTo(filePath, true);
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void CopyLaborHoursFiles() {

			string filePath = "C:\\Temp\\Win7_2.txt";

			using (StreamReader sr = new StreamReader(filePath)) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] nodes = line.Split(new char[] { ',' });

					foreach (string node in nodes) {

						CopyLaborHoursFile(node);
					}

				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private string MapNumericString(string numString) {

			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < numString.Length; i++) {


				switch (numString[i]) {

					case '0':
						sb.Append('Z');
						break;

					case '1':
						sb.Append('A');
						break;

					case '2':
						sb.Append('X');
						break;

					case '3':
						sb.Append('F');
						break;

					case '4':
						sb.Append('C');
						break;

					case '5':
						sb.Append('Q');
						break;

					case '6':
						sb.Append('R');
						break;

					case '7':
						sb.Append('M');
						break;

					case '8':
						sb.Append('O');
						break;

					case '9':
						sb.Append('Y');
						break;
				}
			}

			return sb.ToString();
		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			StringBuilder sb = new StringBuilder();

			string[] items = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

			int counter = 0;

			foreach (string item in items) {

				sb.Append("'");
				sb.Append(item);
				sb.Append("',");

				if ((++counter) % 10 == 0) {
					sb.Append("\r\n");
				}


			}


			textBox1.Text = sb.ToString();
			//ProcessErrorEmail();

			//ProcessUnitList();

			//ReformatTest();

			Unzipper.UnzipDatabaseZipFile("C:\\Temp2.0\\HFSDB_Monday_20131223.zip", "C:\\Temp2.0");

			//ProcessFirstDataDeletions();


//			StringBuilder sb = new StringBuilder();

//			string template1 = 
//@"		(SELECT ISNULL(SUM(Quantity), 0) 
//  		 FROM @ItemsSold 
//		 WHERE MenuItemId = m.MenuItemId
//		 AND Convert(TIME, OrderDate) >= (SELECT StartTime FROM @DayPart96 WHERE DayPart96Id = !ORDINAL)
//		 AND Convert(TIME, OrderDate) <= (SELECT EndTime FROM @DayPart96 WHERE DayPart96Id = !ORDINAL)) AS Segment!ORDINAL";

//			string template2 =
//@"		(SELECT ISNULL(SUM(Quantity), 0) 
//  		 FROM @ItemsSold i
//  		 INNER JOIN @DayPart96 d ON Convert(TIME, OrderDate) BETWEEN d.StartTime AND d.EndTime
//		 WHERE MenuItemId = m.MenuItemId 
//		 AND DayPart96Id = !ORDINAL) AS Segment!ORDINAL";


//			string template =
//@"		ISNULL((SELECT QuantitySold 
//				FROM @DayPartSales
//				WHERE MenuItemId = m.MenuItemId 
//				AND DayPart96Id = !ORDINAL), 0) AS Segment!ORDINAL";


//			for (int i = 1; i <= 96; i++) {

//				sb.Append(template);
//				sb.Replace("!ORDINAL", i.ToString());
//				sb.Append(",\r\n\r\n");

//			}


//			textBox1.Text = sb.ToString();





			//BuildUnitNumberInsertSql();

			//ProcessMbmEmailList();

			//ProcessRODirectory();

			//ProcessRemovableDrive();

			//SalaryDataReader sdr = new SalaryDataReader();
			//textBox1.Text = sdr.GetGeneralManagerJobCode();


			//string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

			//StringBuilder sb = new StringBuilder();
			//int counter = 0;


			//foreach (string line in lines) {

			//	sb.Append("'");
			//	sb.Append(line);
			//	sb.Append("', ");

			//	counter++;

			//	if (counter % 10 == 0) {
			//		sb.Append("\r\n");
			//	}
			//}


			//textBox1.Text = sb.ToString();





//			MenuConfigProcessor processor = new MenuConfigProcessor();
	//		processor.Load("C:\\Temp\\MenuConfig.xml");

			
			//AccessCardNumber cardNum = new AccessCardNumber();

			//string accessCode = cardNum.CreateCode();

			//textBox1.Text += accessCode + "\r\n";

			//DateTime expirationTime = cardNum.Decode(accessCode);

			//textBox1.Text += expirationTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\r\n";

			//textBox1.Text += DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\r\n";

			//string items = textBox1.Text.Replace("\t", "\r\n");

			//textBox1.Text = Convert.ToString((int)EmployeeJobCode.Crewhourly);
			//textBox1.Text = (int)EmployeeJobCode.Crewhourly.ToString();

			//string[] jrNumbers = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

			//StringBuilder sb = new StringBuilder();

			//int counter = 0;

			//foreach (string jrNumber in jrNumbers) {
			//	sb.Append("'");
			//	sb.Append(jrNumber);
			//	sb.Append("', ");

			//	if (++counter % 10 == 0) {
			//		sb.Append("\r\n");
			//	}
			//}


			//textBox1.Text = sb.ToString();


			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void BuildUnitNumberInsertSql() {

			string template = "INSERT INTO [DataValidation].[dbo].[UnitMbmNumber]([UnitNumber])  VALUES( '!UNIT_NUMBER')";

			string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

			StringBuilder sb = new StringBuilder();

			foreach (string line in lines) {

				sb.Append(template);
				sb.Replace("!UNIT_NUMBER", line);

				sb.Append("\r\n\r\n");
			}


			textBox1.Text = sb.ToString();

		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo di) {

			textBox1.Text += "Processing Directory: " + di.FullName + "\r\n";
			Application.DoEvents();

			DirectoryInfo[] directories = di.GetDirectories();

			string copyDirectory = "D:\\TLogFiles\\" + di.Name;

			if (!Directory.Exists(copyDirectory)) {
				Directory.CreateDirectory(copyDirectory);
			}
			
			foreach (DirectoryInfo directory in directories) {

				int dirNum = Convert.ToInt32(directory.Name);

				if (dirNum > 20130831) {


					FileInfo[] files = directory.GetFiles("*transhist*");

					foreach (FileInfo file in files) {

						string copyPath = copyDirectory + "\\" + file.Name;

						if (!File.Exists(copyPath)) {

							textBox1.Text += "Copying file: " + copyPath + "\r\n";
							Application.DoEvents();

							file.CopyTo(copyPath, true);
						}
					}

				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessErrorEmail() {

			DataAnalysisDataContext dataContext = new DataAnalysisDataContext();

			string filePath = "C:\\Temp\\EmailError.txt";

			int counter = 1;

			using (StreamReader sr = new StreamReader(filePath)) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();



					if (line.StartsWith("X", StringComparison.InvariantCultureIgnoreCase)) {

						string[] items = line.Split(new char[] { '\t' });

						ErrorEmail errEmail = new ErrorEmail();

						errEmail.ErrorDate = Convert.ToDateTime(items[2]);
						errEmail.ErrorMessage = items[4].Trim();
						errEmail.ErrorType = "HFSDB";

						errEmail.UnitNumber = items[0].Substring(1, 7);

						dataContext.ErrorEmails.InsertOnSubmit(errEmail);
						dataContext.SubmitChanges();

					}

					textBox1.Text = counter.ToString() + " Lines Processed";
					Application.DoEvents();


				}
			}

		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFirstDataDeletions() {


			string connectionString = "DATABASE=FirstDataProcessing;SERVER=anatrx01;UID=fdpService;PWD=FdP53rvi(e!;";
			string sql = "SELECT FirstDataDetailId FROM FirstDataDetail WHERE FirstDataFileId = 1692";

			DataAccess dac = new DataAccess(connectionString);


			DataTable dt = dac.ExecuteQuery(sql);

			textBox1.Text = dt.Rows.Count.ToString() + " Records Found.\r\n";
			Application.DoEvents();

			int lineCount = 1;

			foreach (DataRow dr in dt.Rows) {

				int firstDateDetailId = Convert.ToInt32(dr["FirstDataDetailId"]);

				sql = "DELETE FROM FirstDataDetail WHERE FirstDataDetailId = " + firstDateDetailId.ToString();

				dac.ExecuteActionQuery(sql);

				lineCount++;

				if (lineCount % 1000 == 0) {

					textBox1.Text = lineCount.ToString() + " Records Deleted.\r\n";
					Application.DoEvents();
				}

			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessMbmEmailList() {

			List<string> unitList = new List<string>();

			string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

			foreach (string line in lines) {

				string unitNumber = line.Substring(1, 7);

				if (unitList.Where(u => u == unitNumber).Count() == 0) {

					unitList.Add(unitNumber);
				}
			}

			foreach (string unit in unitList) {

				textBox1.Text += unit + "\r\n";
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessRODirectory() {

			string filePath = @"\\ckeanafnp01\CJRCO_RO";


			DirectoryInfo di = new DirectoryInfo(filePath);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {


				ProcessDirectory(directory);
			}

		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessRemovableDrive() {


			DirectoryInfo di = new DirectoryInfo("F:\\");


			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {


				textBox1.Text += "Processing directory: " + directory.Name + "\r\n";
				Application.DoEvents();

				FileInfo[] files = directory.GetFiles("*.xml");

				foreach (FileInfo file in files) {


					if (file.Name.StartsWith("Original_")) {

						int value = Convert.ToInt32(file.Name.Substring("Original_".Length, 8));

						if (value < 20130915) {
							file.Delete();
						}
					}

					else {

						int value = Convert.ToInt32(file.Name.Substring(0, 8));

						if (value < 20130915) {
							file.Delete();
						}
					}
				}
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessUnitList() {

			string template = @"INSERT INTO [HardeesTransactionData].[dbo].[UnitInfo]
															   ([UnitNumber]
															   ,[Franchisee]
															   ,[DataSource])
														 VALUES
															   ('!UNIT_NUMBER'
															   ,'!FRANCHISEE'
															   ,'!DATA_SOURCE')";

			StringBuilder sb = new StringBuilder();

			string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

			foreach (string line in lines) {

				string[] items = line.Split(new char[] { '\t' });

				if (items.Length == 3) {
					sb.Append(template);

					sb.Replace("!UNIT_NUMBER", items[0]);
					sb.Replace("!FRANCHISEE", items[1]);
					sb.Replace("!DATA_SOURCE", items[2]);

					sb.Append("\r\n\r\n");
				}
			}

			textBox1.Text = sb.ToString();
		}
		//---------------------------------------------------------------------------------------------------------
		private void ReformatTest() {

			string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

			StringBuilder sb = new StringBuilder();

			foreach (string line in lines) {

				if (line.Length > 0) {

					int pos = line.IndexOf("@");
					int pos2 = line.IndexOf(' ', pos);

					int len = pos2 - pos;

					string temp = line.Substring(pos, len);

					sb.Append(line.Substring(0, pos2));

					sb.Append(" = ");

					StringBuilder sb2 = new StringBuilder();
					sb2.Append("!");

					for (int i = 1; i < temp.Length; i++) {

						if (i > 1) {

							if (char.IsUpper(temp[i])) {
								sb2.Append("_");
							}
						}

						sb2.Append(temp[i].ToString().ToUpper());
					}

					sb.Append(sb2.ToString());
					sb.Append("\r\n");
				}
			}

			textBox1.Text = sb.ToString();
		}
		//---------------------------------------------------------------------------------------------------------
		private void textBox1_TextChanged(object sender, EventArgs e) {

			//if (textBox1.Text.Length > 2024) {
			//	textBox1.Text = "";
			//}


			//if (textBox1.Text.Length > 0) {
			//	textBox1.SelectionStart = textBox1.Text.Length - 1;
			//	textBox1.ScrollToCaret();
			//	Application.DoEvents();
			//}

		}
		//---------------------------------------------------------------------------------------------------------
		private void button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;

			StringBuilder sb1223 = new StringBuilder();
			StringBuilder sb1224_25 = new StringBuilder();

			string filePath = "C:\\Temp2.1\\posdata_original.dat";


			using (StreamReader sr = new StreamReader(filePath)) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					if (string.Compare(line.Substring(50, 2),  "23") <= 0) {
						sb1223.Append(line);
						sb1223.Append("\r\n");
					}
					else {
						sb1224_25.Append(line);
						sb1224_25.Append("\r\n");
					}
				}
			}

			filePath = "C:\\Temp2.1\\posdata_20131223.dat";

			using (StreamWriter sw = new StreamWriter(filePath)) {
				sw.Write(sb1223.ToString());
			}

			filePath = "C:\\Temp2.1\\posdata_20131224&25.dat";

			using (StreamWriter sw = new StreamWriter(filePath)) {
				sw.Write(sb1224_25.ToString());
			}


			button3.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private void button4_Click(object sender, EventArgs e) {

			button4.Enabled = false;

			MenuConfigProcessor processor = new MenuConfigProcessor();
			processor.Load("C:\\StarPos\\StarPosConfig\\MenuConfig.xml");




			//string foo = @"C:\BackOffice\CKE\NextGenFileGenerator\NextGenFileGenerator.exe /BusinessDate=!BUSINESS_DATE /ReportType=MixDestPoll";

			//StringBuilder sb = new StringBuilder();

			//DateTime fromDate = new DateTime(2014, 2, 1);
			//DateTime toDate = new DateTime(2014, 4, 4);

			//while (fromDate < toDate) {

			//	sb.Append(foo.Replace("!BUSINESS_DATE",fromDate.ToString("MM/dd/yyyy")));
			//	sb.Append("\r\n");

			//	fromDate = fromDate.AddDays(1);

			//}


			//textBox1.Text = sb.ToString();


			//string temp = textBox1.Text;

			//textBox1.Text = TextEncryption.EncryptText("Wmc9AD00");

			//StringBuilder sb = new StringBuilder();

			//string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);


			//for (int i = 0; i < lines.Length; i++) {

			//	string line = lines[i].Trim();

			//	if (!string.IsNullOrEmpty(line)) {

			//		line = line.Replace("[", "");
			//		line = line.Replace("]", "");
			//		line = ",@" + line.Substring(1) + " = " + line.Substring(1);
			//		sb.Append(line);
			//		sb.Append("\r\n");

			//	}
			//}

			//textBox1.Text = sb.ToString();

			button4.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------------
		private class PromoItem {

			public string MenuItemId = "";
			public string MenuItemName = "";
			public int Quantity = 0;
			public decimal Price = 0;
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessPromoItems() {

			List<PromoItem> promoItemList = new List<PromoItem>();

			string[] lines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

			foreach (string line in lines) {

				string[] items = line.Split(new char[] { ',' });

				PromoItem pi = promoItemList.Where(p => p.MenuItemId == items[0].Trim()).Select(p => p).SingleOrDefault();

				if (pi != null) {
					pi.Quantity += Convert.ToInt32(items[3]);
				}

				else {

					pi = new PromoItem();

					pi.MenuItemId = items[0].Trim();
					pi.MenuItemName = items[1].Trim();

					pi.Price = Convert.ToDecimal(items[2]);
					pi.Quantity = Convert.ToInt32(items[3]);

					promoItemList.Add(pi);
				}
			}


			StringBuilder sb = new StringBuilder();

			foreach (PromoItem pi in promoItemList) {

				sb.Append(pi.MenuItemId);
				sb.Append('\t');

				sb.Append(pi.MenuItemName);
				sb.Append('\t');

				sb.Append(pi.Price.ToString("0.00"));
				sb.Append('\t');

				sb.Append(pi.Quantity.ToString("0"));
				sb.Append("\r\n");
			}
			
			textBox1.Text = sb.ToString();
		}
	}
}
