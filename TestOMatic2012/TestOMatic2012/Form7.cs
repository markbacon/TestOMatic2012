using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TestOMatic2012 {

	public partial class Form7 : Form {
		public Form7() {
			InitializeComponent();
		}

		private List<ScannedCouponItem> _couponItemList = new List<ScannedCouponItem>();


		//---------------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			string filePath = "D:\\TLogs\\X1500025\\20130902.transhist.xml";
			TransHistCouponProcessor couponProcessor = new TransHistCouponProcessor();
			couponProcessor.ProcessFile("x1500025", filePath);

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
		private string GetNodeValue(string xpath, XmlNode node) {

			string nodeValue = "";


			XmlNode dataNode = node.SelectSingleNode(xpath);

			if (dataNode != null) {

				nodeValue = dataNode.InnerText;

			}

			return nodeValue;
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
	}
}
