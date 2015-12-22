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

	public partial class Form7 : Form {
		public Form7() {
			InitializeComponent();

			Logger.LoggerWrite += form8_onLoggerWrite;
		}

		private List<ScannedCouponItem> _couponItemList = new List<ScannedCouponItem>();


		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			//CopyTransHistFiles();

			//string filePath = "D:\\TLogs\\X1500025\\20130902.transhist.xml";
			TransHistCouponProcessor couponProcessor = new TransHistCouponProcessor();

			//couponProcessor.ProcessFile("x1500025", filePath);
			couponProcessor.Run();

			//string filePath = @"C:\Store Data\x1505952\20140722.transhist.xml";
			//XmlDocument xmlDoc = new XmlDocument();
			//xmlDoc.Load(filePath);

			//string xpath = "//Transaction/ScannedCouponItems";

			//XmlNodeList nodes = xmlDoc.SelectNodes(xpath);


			//foreach (XmlNode node in nodes) {


			//	ProcessScannedCouponItemsNode(node);
			//}

			//StringBuilder sb = new StringBuilder();

			//foreach (ScannedCouponItem couponItem in _couponItemList) {

			//	sb.Append(couponItem.CouponId);
			//	sb.Append("\t");
			//	sb.Append(couponItem.Description);
			//	sb.Append("\t");
			//	sb.Append(couponItem.Destination);
			//	sb.Append("\t");
			//	sb.Append(couponItem.Quantity);
			//	sb.Append("\r\n");
			//}

			//textBox1.Text = sb.ToString();

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void CopyTLogFileFromHFSCO_RO(DirectoryInfo localTLogUnitDirectory, DateTime businessDate, string fileName) {

			DirectoryInfo di = new DirectoryInfo(@"\\ckeanafnp01\HFSCO_RO\" + localTLogUnitDirectory.Name + "\\" + businessDate.ToString("yyyyMMdd"));

			if (di.Exists) {
				FileInfo tLogFile = di.GetFiles(fileName).ToList().SingleOrDefault();

				if (tLogFile != null) {
					string filePath = Path.Combine(localTLogUnitDirectory.FullName, fileName);
					Logger.Write("Copying file: " + filePath);
					tLogFile.CopyTo(filePath);
				}
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void CopyTransHistFiles() {

			DirectoryInfo localTLogsDirectory = new DirectoryInfo("D:\\TLogsII");

			DirectoryInfo[] localTLogUnitDirectories = localTLogsDirectory.GetDirectories("X15*");

			foreach (DirectoryInfo localTLogUnitDirectory in localTLogUnitDirectories) {

				List<FileInfo> localTLogFileList = localTLogUnitDirectory.GetFiles("20*.TransHist.xml").ToList();

				ProcessLocalTLogFiles(localTLogUnitDirectory, localTLogFileList);

			}
		}
		//---------------------------------------------------------------------------------------------------
		private string GetNodeValue(string xpath, XmlNode node) {

			string nodeValue = "";


			XmlNode dataNode = node.SelectSingleNode(xpath);

			if (dataNode != null) {

				nodeValue = dataNode.InnerText;

			}

			return nodeValue;
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessLocalTLogFiles(DirectoryInfo localTLogUnitDirectory, List<FileInfo> localTLogFileList) {

			DateTime businessDate = new DateTime(2012, 12, 31);
			DateTime maxDate = new DateTime(2014, 8, 1);

			while (businessDate < maxDate) {

				string fileName = businessDate.ToString("yyyyMMdd") + ".transhist.xml";

				FileInfo tLogFile = localTLogFileList.Where(f => f.Name.ToLower() == fileName).SingleOrDefault();

				if (tLogFile == null) {

					CopyTLogFileFromHFSCO_RO(localTLogUnitDirectory, businessDate, fileName);
				}

				businessDate = businessDate.AddDays(1);
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessScannedCouponItemNode(DateTime transTime, XmlNode node) {

			//string xpath = "./Qty";







		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessScannedCouponItemsNode(XmlNode node) {


			string xpath = "./DateTime";
			DateTime transTime = Convert.ToDateTime(GetNodeValue(xpath, node.ParentNode));

			xpath = "./Order/OrderData/Regnum";
			string registerNumber = GetNodeValue(xpath, node.ParentNode);

			xpath = "./Order/OrderData/Ordernum";
			string orderNumber = GetNodeValue(xpath, node.ParentNode);

			xpath = "./Order/OrderData/Empnum";
			string employeeNumber = GetNodeValue(xpath, node.ParentNode);

			xpath = "./Order/OrderData/Destination";
			string destination = GetNodeValue(xpath, node.ParentNode);

			foreach (XmlNode childNode in node.ChildNodes) {

				ScannedCouponItem couponItem = new ScannedCouponItem();
				couponItem.Destination = destination;
				couponItem.EmployeeNumber = employeeNumber;
				couponItem.OrderNumber = orderNumber;

				xpath = "./Name";
				couponItem.Description = GetNodeValue(xpath, childNode);

				xpath = "./CouponId";
				couponItem.CouponId = GetNodeValue(xpath, childNode);

				xpath = "./Qty";
				couponItem.Quantity = Convert.ToInt32(GetNodeValue(xpath, childNode));

				xpath = "./Price";
				couponItem.Price = Convert.ToDecimal(GetNodeValue(xpath, childNode));

				_couponItemList.Add(couponItem);

			}
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
