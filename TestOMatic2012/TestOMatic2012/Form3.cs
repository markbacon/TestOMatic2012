using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TestOMatic2012 {
	public partial class Form3 : Form {
		public Form3() {
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------------------------------
		//-- Private Members 
		//---------------------------------------------------------------------------------------------------------
		private class MonthYearCount {
			public int Month = 0;
			public int Year = 0;
			public int Count = 0;
		}
		
		
		private DataAnalysisDataContext _dataContext = new DataAnalysisDataContext();
		//---------------------------------------------------------------------------------------------------------

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			GetTLogCounts();

			//ProcessDepositDetail();

			button1.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------------
		private void GetTLogCounts() {

			StringBuilder sb = new StringBuilder();

			DirectoryInfo di = new DirectoryInfo("D:\\TLogsII");

			DirectoryInfo[] directories = di.GetDirectories("X1500025");

			foreach (DirectoryInfo directory in directories) {

				List<FileInfo> fileInfoList = directory.GetFiles("20*.TransHist.xml").ToList();

				ProcessFileList(directory.Name, fileInfoList, sb);


				//sb.Append(directory.Name);
				//sb.Append('\t');
				//sb.Append(directory.GetFiles().Count());
				//sb.Append("\r\n");
			}


			textBox1.Text = sb.ToString();
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDepositDetail() {

			string rootDatatDirectoryName = "D:\\DepositDetails";

			DirectoryInfo di = new DirectoryInfo(rootDatatDirectoryName);

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {

				textBox1.Text += "Processing Directory: " + directory.Name + "\r\n";
				Application.DoEvents();

				ProcessDepositDetailDirecory(directory);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDepositDetailDirecory(DirectoryInfo dataDirectory) {

			string unitNumber = dataDirectory.Name;

			FileInfo[] files = dataDirectory.GetFiles("depositDetail*.xml");

			foreach (FileInfo file in files) {

				////depositDetail20140504

				string temp = Path.GetFileNameWithoutExtension(file.Name).Substring(13);

				int fileNum = Convert.ToInt32(temp);

				if (fileNum > 20140505) {

					ProcessDepositDetailFile(unitNumber, file);
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDepositDetailFile(string unitNumber, FileInfo depositDetailFile) {
			
			XmlDocument xmlDoc = new XmlDocument();

			xmlDoc.Load(depositDetailFile.FullName);

			XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
			ns.AddNamespace("n", "http://schemas.datacontract.org/2004/07/DataAccess");
			//ns.AddNamespace("z", "http://schemas.microsoft.com/2003/10/Serialization");

			string xpath = "/n:ArrayOfDepositDetail/n:DepositDetail";

			XmlNodeList nodes = xmlDoc.SelectNodes(xpath, ns);
			//XmlNodeList nodes = xmlDoc.SelectNodes(xpath);

			//XmlNodeList nodes = xmlDoc.DocumentElement.ChildNodes;

			foreach (XmlNode node in nodes) {

				DepositDetail depDetail = new DepositDetail();





				xpath = "./n:Amount";
				XmlNode dataNode = node.SelectSingleNode(xpath, ns);
				//XmlNode dataNode = xmlDoc.SelectSingleNode(xpath);

				if (dataNode != null) {
					depDetail.Amount = Convert.ToDecimal(dataNode.InnerText);
				}

				depDetail.CreateDateTime = DateTime.Now;

				xpath = "n:DepositDTM";
				dataNode = node.SelectSingleNode(xpath, ns);

				if (dataNode != null) {
					depDetail.DepositDate = Convert.ToDateTime(dataNode.InnerText.Replace("T", " "));
				}

				xpath = "n:DepositID";
				dataNode = node.SelectSingleNode(xpath, ns);

				if (dataNode != null) {
					depDetail.HFSDB_DepositId = Convert.ToInt32(dataNode.InnerText);
				}


				xpath = "n:Status";
				dataNode = node.SelectSingleNode(xpath, ns);

				if (dataNode != null) {
					depDetail.Status = dataNode.InnerText;
				}

				depDetail.UnitNumber = unitNumber;

				_dataContext.DepositDetails.InsertOnSubmit(depDetail);
				_dataContext.SubmitChanges();

			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFileList(string unitNumber, List<FileInfo> fileInfoList, StringBuilder sb) {

			List<DateTime> fileDateList = new List<DateTime>();
			List<MonthYearCount> mycList = new List<MonthYearCount>();

			foreach (FileInfo file in fileInfoList) {

				if (char.IsDigit(file.Name[0])) {

					string tempDateString = file.Name.Substring(0, 8);

					DateTime tempDate;

					if (DateTime.TryParseExact(tempDateString, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDate)) {
						fileDateList.Add(tempDate);
					}
				}
			}

			int yearNum = 2013;

			while (yearNum <= 2014) {

				for (int i = 1; i <= 12; i++) {

					MonthYearCount myc = new MonthYearCount();
					myc.Month = i;
					myc.Year = yearNum;
					myc.Count = fileDateList.Where(f => f.Month == i && f.Year == yearNum).Count();

					mycList.Add(myc);

					if (i == 7 && yearNum == 2014) {
						break;
					}
				}

				yearNum++;
			}

			foreach (MonthYearCount myc in mycList) {
				sb.Append(unitNumber);
				sb.Append("\t");
				
				sb.Append(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(myc.Month));
				sb.Append("-");
				sb.Append(myc.Year);
				sb.Append("\t");
				sb.Append(myc.Count);
				sb.Append("\r\n");
			}



		}
	}
}
