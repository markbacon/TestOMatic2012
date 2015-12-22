using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace TestOMatic2012 {
	public partial class Form18 : Form {
		public Form18() {
			InitializeComponent();
		}
		//---------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------
		private List<GiftCardTrans> _giftCardTransList = new List<GiftCardTrans>();



		//---------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			DirectoryInfo di = new DirectoryInfo(AppSettings.CjrDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {

				if (string.Compare(directory.Name, "X11000000", true) > 0) {

					textBox1.Text = "Processing Unit Directory:  " + directory.Name + "\r\n";

					ProcessUnitDirectory(directory);
				}
			}

			StringBuilder sb = new StringBuilder();

			foreach (GiftCardTrans gct in _giftCardTransList) {

				sb.Append(gct.UnitNumber);
				sb.Append("\t");
				sb.Append(gct.FileName);
				sb.Append("\t");
				sb.Append(gct.OrderNumber);
				sb.Append("\t");
				sb.Append(gct.Price.ToString("0.00"));
				sb.Append("\t");
				sb.Append(gct.OrderDate.ToString("yyyy-MM-dd HH:mm:ss"));
				sb.Append("\r\n");
			}

			textBox1.Text = sb.ToString();

			button1.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessFileDirectory(DirectoryInfo di) {

			FileInfo[] files = di.GetFiles("2014*.xml");

			foreach (FileInfo file in files) {

				try {

					XmlDocument xmlDoc = new XmlDocument();
					xmlDoc.Load(file.OpenRead());

					string xpath = "/TLog/Transactions/Transaction/Items/Item[Name='Gift Card']";

					XmlNodeList nodes = xmlDoc.SelectNodes(xpath);

					if (nodes.Count > 0) {
						textBox1.Text += "Gift Cards found in file: " + file.FullName + "\r\n";
					}

					foreach (XmlNode node in nodes) {

						GiftCardTrans gct = new GiftCardTrans();

						gct.Price = Convert.ToDecimal(node.SelectSingleNode("Price").InnerText);

						XmlNode transNode = node.ParentNode.ParentNode;

						gct.OrderDate = Convert.ToDateTime(transNode.SelectSingleNode("DateTime").InnerText);
						gct.FileName = file.Name;
						gct.OrderNumber = transNode.SelectSingleNode("Order/OrderData/Ordernum").InnerText;

						gct.UnitNumber = di.Parent.Name;
						_giftCardTransList.Add(gct);

						WriteGiftCardTrans(gct);

					}
				}
				catch (Exception ex) {
					Logger.Write("An exception occurred processing file:  " + file.FullName + ". Please see error log for details.");
					Logger.WriteError(ex);

				}


			}
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessUnitDirectory(DirectoryInfo di) {

			DirectoryInfo[] directories = di.GetDirectories("2014*");

			foreach (DirectoryInfo directory in directories) {

				ProcessFileDirectory(directory);
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void textBox1_TextChanged(object sender, EventArgs e) {

			if (textBox1.Text.Length > 2048) {
				textBox1.Text = "";
			}



			if (textBox1.Text.Length > 0) {
				textBox1.SelectionStart = textBox1.Text.Length - 1;
				textBox1.ScrollToCaret();
				Application.DoEvents();
			}

		}
		//---------------------------------------------------------------------------------------------------
		private void WriteGiftCardTrans(GiftCardTrans gct) {

			StringBuilder sb = new StringBuilder();

			sb.Append(gct.UnitNumber);
			sb.Append("\t");
			sb.Append(gct.FileName);
			sb.Append("\t");
			sb.Append(gct.OrderNumber);
			sb.Append("\t");
			sb.Append(gct.Price.ToString("0.00"));
			sb.Append("\t");
			sb.Append(gct.OrderDate.ToString("yyyy-MM-dd HH:mm:ss"));

			using (StreamWriter sw = File.AppendText("D:\\Temp\\GCT.log")) {

				sw.WriteLine(sb.ToString());
			}
		}

	}
}
