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
using System.Xml;

namespace TestOMatic2012 {
	public partial class Form61 : Form {
		public Form61() {
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;
			List<string> unitList = new List<string>();

			using (StreamReader sr = new StreamReader("C:\\TestData\\PonderUnits.txt")) {

				while (sr.Peek() != -1) {
					unitList.Add(sr.ReadLine().Trim());

				}

			}

			DirectoryInfo di = new DirectoryInfo("C:\\SmartReportsII");

			foreach (string unit in unitList) {

				DirectoryInfo directory = di.GetDirectories(unit).First();

				DateTime businessDate = Convert.ToDateTime("05/02/2016");

				while (businessDate < DateTime.Today) {

					Logger.Write("Processing directory: " + directory.Name);
					ProcessSmartReportsFiles(directory.Name, businessDate, directory);

					businessDate = businessDate.AddDays(7);
				}
			}



			button2.Enabled = true;
		}

		//---------------------------------------------------------------------------------------------------------
		private string GetLaborDataFileName(string unitNumber, DateTime businessDate) {

			string fileName = "";

			if (!unitNumber.StartsWith("X", StringComparison.InvariantCultureIgnoreCase)) {
				fileName = "X" + unitNumber;
			}
			else {
				fileName = unitNumber;
			}

			fileName += "_" + businessDate.ToString("yyyyMMdd") + "_LaborHfs.csv";

			return fileName;
		}		//---------------------------------------------------------"------------------------------------------
		private void ProcessNode(DirectoryInfo di, DateTime businessDate) {

			try {
			
			}
			catch (Exception ex) {
				Logger.Write("An exception occurred in SmartReportsProcessor.ProcessNode for directory: " + di.FullName + ". Please see error log for details.");
				Logger.WriteError(ex);
			}
		}
		//---------------------------------------------------------------------------------------------------------//---------------------------------------------------------------------------------------------------------
		private void ProcessLaborData(string unitNumber,
									  DateTime businessDate,
									  string ckeNodeDirectoryName,
									  MemoryStream weeklyRingOutLaborStream,
									  MemoryStream weeklyLaborSummaryStream) {

			Logger.Write("ProcessLaborData starting...");

			decimal totalHours = ProcessWeeklyRingOutLaborSummaryFile(weeklyRingOutLaborStream);

			decimal totalWages = ProcessWeeklyLaborSummaryFile(weeklyLaborSummaryStream);

			StringBuilder sb = new StringBuilder();

			sb.Append(unitNumber);
			sb.Append(",");
			sb.Append(businessDate.ToString("yyyy-MM-dd"));
			sb.Append(",");
			sb.Append(totalWages.ToString("0.00"));
			sb.Append(",");
			sb.Append(totalHours.ToString("0.00"));
			sb.Append("\r\n");

			
			SaveLaborDataFileToCkeNodeDirectory(unitNumber, businessDate, ckeNodeDirectoryName, sb.ToString());

			Logger.Write("ProcessLaborData has finished.");
		}
		private void ProcessSmartReportsFiles(string unitNumber, DateTime businessDate, DirectoryInfo di) {


			DateTime startTime = DateTime.Now;
			Logger.Write("Begin processing Smart Reports Zip file: " + di.Name);

			MemoryStream weeklyLaborSummaryStream = new MemoryStream();
			MemoryStream weeklyRingOutLaborStream = new MemoryStream();

			DateTime weekEndDate = businessDate;

			string weeklyLaborSummarryFileName = AppSettings.WeeklyLaborSummaryFileNamePrefix + weekEndDate.ToString("yyyyMMdd") + ".xml";
			string weeklyRingOutLaborSummaryFileName = AppSettings.WeeklyRingOutLaborSummaryFileNamePrefix + businessDate.ToString("yyyyMMdd") + ".xml";


			


			FileInfo file = di.GetFiles(weeklyLaborSummarryFileName).Single();
			FileStream fs = file.OpenRead();

			fs.CopyTo(weeklyLaborSummaryStream);

			file = di.GetFiles(weeklyRingOutLaborSummaryFileName).Single();
			fs = file.OpenRead();

			fs.CopyTo(weeklyRingOutLaborStream);


				weeklyLaborSummaryStream.Seek(0, SeekOrigin.Begin);
				weeklyRingOutLaborStream.Seek(0, SeekOrigin.Begin);

				string ckeNodeDirectoryName = "C:\\ckefred";		
				ProcessLaborData(unitNumber, businessDate, ckeNodeDirectoryName, weeklyRingOutLaborStream, weeklyLaborSummaryStream);

			Logger.Write("Completed processing Smart Reports zip file for: " + unitNumber + " Elasped Time: " + (DateTime.Now - startTime).ToString());
		}		//---------------------------------------------------------------------------------------------------------
		private decimal ProcessWeeklyLaborSummaryFile(FileInfo weekLaborFile) {

			decimal totalWages = 0;

			XmlDocument xmlDoc = new XmlDocument();

			xmlDoc.Load(weekLaborFile.FullName);

			XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
			ns.AddNamespace("n", "http://schemas.datacontract.org/2004/07/DataAccess");


			string xpath = "/n:ArrayOfWeeklyLaborSummaryUI/n:WeeklyLaborSummaryUI";

			XmlNodeList nodes = xmlDoc.SelectNodes(xpath, ns);

			foreach (XmlNode node in nodes) {

				if (node != null) {

					XmlNode wageNode = node.SelectSingleNode("n:RegularPay_Crew", ns);

					if (wageNode != null) {

						if (!string.IsNullOrEmpty(wageNode.InnerText)) {
							totalWages += Convert.ToDecimal(wageNode.InnerText);
						}
					}


					wageNode = node.SelectSingleNode("n:OTPay_Crew", ns);

					if (wageNode != null) {
						if (!string.IsNullOrEmpty(wageNode.InnerText)) {
							totalWages += Convert.ToDecimal(wageNode.InnerText);
						}
					}


					wageNode = node.SelectSingleNode("n:RegularPay_Leader", ns);

					if (wageNode != null) {
						if (!string.IsNullOrEmpty(wageNode.InnerText)) {
							totalWages += Convert.ToDecimal(wageNode.InnerText);
						}
					}


					wageNode = node.SelectSingleNode("n:OTPay_Leader", ns);

					if (wageNode != null) {
						if (!string.IsNullOrEmpty(wageNode.InnerText)) {
							totalWages += Convert.ToDecimal(wageNode.InnerText);
						}
					}
				}
			}
			return totalWages;
		}
		//---------------------------------------------------------------------------------------------------------
		private decimal ProcessWeeklyLaborSummaryFile(MemoryStream inStream) {

			decimal totalWages = 0;

			XmlDocument xmlDoc = new XmlDocument();

			xmlDoc.Load(inStream);

			XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
			ns.AddNamespace("n", "http://schemas.datacontract.org/2004/07/DataAccess");


			string xpath = "/n:ArrayOfWeeklyLaborSummaryUI/n:WeeklyLaborSummaryUI";

			XmlNodeList nodes = xmlDoc.SelectNodes(xpath, ns);

			foreach (XmlNode node in nodes) {

				if (node != null) {

					XmlNode wageNode = node.SelectSingleNode("n:RegularPay_Crew", ns);

					if (wageNode != null) {

						if (!string.IsNullOrEmpty(wageNode.InnerText)) {
							totalWages += Convert.ToDecimal(wageNode.InnerText);
						}
					}


					wageNode = node.SelectSingleNode("n:OTPay_Crew", ns);

					if (wageNode != null) {
						if (!string.IsNullOrEmpty(wageNode.InnerText)) {
							totalWages += Convert.ToDecimal(wageNode.InnerText);
						}
					}


					wageNode = node.SelectSingleNode("n:RegularPay_Leader", ns);

					if (wageNode != null) {
						if (!string.IsNullOrEmpty(wageNode.InnerText)) {
							totalWages += Convert.ToDecimal(wageNode.InnerText);
						}
					}


					wageNode = node.SelectSingleNode("n:OTPay_Leader", ns);

					if (wageNode != null) {
						if (!string.IsNullOrEmpty(wageNode.InnerText)) {
							totalWages += Convert.ToDecimal(wageNode.InnerText);
						}
					}
				}
			}
			return totalWages;
		}
		//---------------------------------------------------------------------------------------------------------
		private decimal ProcessWeeklyRingOutLaborSummaryFile(FileInfo weeklyRingOutLaborFile) {

			decimal totalHours = 0;

			XmlDocument xmlDoc = new XmlDocument();

			xmlDoc.Load(weeklyRingOutLaborFile.FullName);
			string xpath = "//n:ArrayOfRingOutWeeklyLaborSummaryUI/n:RingOutWeeklyLaborSummaryUI/n:TotalHrs";

			XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
			ns.AddNamespace("n", "http://schemas.datacontract.org/2004/07/DataAccess");

			XmlNodeList nodes = xmlDoc.SelectNodes(xpath, ns);

			foreach (XmlNode node in nodes) {

				decimal hours = Convert.ToDecimal(node.InnerText);
				totalHours += hours;
			}

			return totalHours;
		}
		//---------------------------------------------------------------------------------------------------------
		private decimal ProcessWeeklyRingOutLaborSummaryFile(MemoryStream inStream) {

			decimal totalHours = 0;

			XmlDocument xmlDoc = new XmlDocument();

			xmlDoc.Load(inStream);
			string xpath = "//n:ArrayOfRingOutWeeklyLaborSummaryUI/n:RingOutWeeklyLaborSummaryUI/n:TotalHrs";

			XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
			ns.AddNamespace("n", "http://schemas.datacontract.org/2004/07/DataAccess");

			XmlNodeList nodes = xmlDoc.SelectNodes(xpath, ns);

			foreach (XmlNode node in nodes) {

				decimal hours = Convert.ToDecimal(node.InnerText);
				totalHours += hours;
			}

			return totalHours;
		}
		//---------------------------------------------------------------------------------------------------------
		private void SaveLaborDataFileToCkeNodeDirectory(string unitNumber, DateTime businessDate, string ckeNodeDirectory, string fileData) {

			string fileName = GetLaborDataFileName(unitNumber, businessDate);
			string filePath = "";
			string directoryName = "";

			if (!unitNumber.StartsWith("X", StringComparison.InvariantCultureIgnoreCase)) {

				directoryName = Path.Combine(ckeNodeDirectory, "X" + unitNumber);
			}
			else {
				directoryName = Path.Combine(ckeNodeDirectory);
			}


			if (!Directory.Exists(directoryName)) {
				Directory.CreateDirectory(directoryName);
			}


			filePath = Path.Combine(directoryName, fileName);


			using (StreamWriter sw = new StreamWriter(filePath)) {

				sw.Write(fileData);
			}
		}
	}
}
