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
	public partial class Form2 : Form {
		public Form2() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			int folderNum = 20140226;

			while (folderNum > 20121231) {


				string dirPath = Path.Combine(@"\\ckeanafnp01\HFSCO_RO\x1501680", folderNum.ToString());


				DirectoryInfo di = new DirectoryInfo(dirPath);

				if (di.Exists) {

					FileInfo[] files = di.GetFiles("R*.rpt");

					foreach (FileInfo file in files) {

						textBox1.Text = "Copying file: " + file.Name;
						Application.DoEvents();

						file.CopyTo(Path.Combine("C:\\Temp52", file.Name), true);

					}
				}

				folderNum--;
			}

			button1.Enabled = true;
		}

		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			HFSDBData data = new HFSDBData();

			DataTable dt = data.GetLegacyTimeClockData();

			StringBuilder sb = new StringBuilder();

			int counter = 0;

			foreach (DataRow dr in dt.Rows) {
				sb.Append(SqlTemplateBroker.Load(SqlTemplateId.InsertEmployeeClock));

				sb.Replace("!EmployeeId", dr["SSN"].ToString().TrimStart(new char[] { '0' }));
				sb.Replace("!ClockInOutTime", Convert.ToDateTime(dr["ClockInOutTime"]).ToString("yyyy-MM-dd HH:mm:ss"));
				sb.Replace("!InOrOut", dr["InOrOut"].ToString());

				sb.Append("\r\n\r\n");

				if (++counter % 100 == 0) {
					textBox1.Text = counter.ToString() + "Records Processed.";
					Application.DoEvents();
				}
			}





			textBox1.Text = sb.ToString();

			button2.Enabled = true;
		}

		private void button3_Click(object sender, EventArgs e) {

			button3.Enabled = false;

			string pollFileRootDirName = "C:\\PollFile\\PollFiles";
			string searchPattern = "X15*_InvSummary.xml";

			DirectoryInfo di = new DirectoryInfo(Path.Combine(pollFileRootDirName, "Consolidations"));
			FileInfo[] files = di.GetFiles(searchPattern);


			di = new DirectoryInfo(pollFileRootDirName);
			DirectoryInfo[] directories = di.GetDirectories("X15*");



			foreach (FileInfo file in files) {

				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(file.FullName);

				DateTime businessDate = new DateTime(2015, 10, 1);

				//int counter = 1;

				while (businessDate < DateTime.Today.AddDays(-5)) {

					xmlDoc.DocumentElement.SetAttribute("BusinessDate", businessDate.ToString("MM/dd/yyyy"));

					foreach (DirectoryInfo directory in directories) {


						string unitNumber = directory.Name.Substring(1);
						string xpath = "//InvSummaries/InvSum/UnitNumber";

						XmlNodeList nodes = xmlDoc.SelectNodes(xpath);

						foreach (XmlNode node in nodes) {
							node.InnerText = unitNumber;
						}

						string fileName = directory.Name + "_" + businessDate.ToString("yyyyMMdd") + "_InvSummary.xml";

						xmlDoc.Save(Path.Combine(directory.FullName, fileName));
					}
				}

			}
			button3.Enabled = true;
		}

		private void button4_Click(object sender, EventArgs e) {

			button4.Enabled = false;

			string destRootDirName = "C:\\PollFile\\PollFiles";

			DirectoryInfo di = new DirectoryInfo("T:\\");

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {

				string dirPath = Path.Combine(destRootDirName, directory.Name);

				if (!Directory.Exists(dirPath)) {
					Directory.CreateDirectory(dirPath);
				}
			}

			button4.Enabled = true;

		}

		private void button5_Click(object sender, EventArgs e) {

			button5.Enabled = false;

			string pollFileRootDirName = "C:\\PollFile\\PollFiles";
			string searchPattern = "X15*_InvSummary.xml";

			DirectoryInfo di = new DirectoryInfo(pollFileRootDirName);
			FileInfo[] files = di.GetFiles(searchPattern);


			di = new DirectoryInfo(pollFileRootDirName);
			DirectoryInfo[] directories = di.GetDirectories("X15*");



			foreach (FileInfo file in files) {




				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(file.FullName);


				foreach (DirectoryInfo directory in directories) {

					textBox1.Text += "Processing directory:  " + directory.Name + "\r\n";
					Application.DoEvents();

					DateTime businessDate = new DateTime(2015, 9, 1);
					int counter = 1;

					while (businessDate < DateTime.Today) {



						xmlDoc.DocumentElement.SetAttribute("BusinessDate", businessDate.ToString("MM/dd/yyyy"));


						string unitNumber = directory.Name.Substring(1);
						string xpath = "//InvSummaries/InvSum/UnitNumber";

						XmlNodeList nodes = xmlDoc.SelectNodes(xpath);

						foreach (XmlNode node in nodes) {
							node.InnerText = unitNumber;
						}

						xpath = "//InvSummaries/InvSum/BeginCnt";

						nodes = xmlDoc.SelectNodes(xpath);

						foreach (XmlNode node in nodes) {
							node.InnerText = counter.ToString();
						}

						xpath = "//InvSummaries/InvSum/NetSales";

						nodes = xmlDoc.SelectNodes(xpath);

						foreach (XmlNode node in nodes) {
							node.InnerText = (counter * 100).ToString();
						}

						xpath = "//InvSummaries/InvSum/Purchase";

						nodes = xmlDoc.SelectNodes(xpath);

						foreach (XmlNode node in nodes) {
							node.InnerText = counter.ToString();
						}

						xpath = "//InvSummaries/InvSum/TransferIn";

						nodes = xmlDoc.SelectNodes(xpath);

						foreach (XmlNode node in nodes) {
							node.InnerText = counter.ToString();
						}

						xpath = "//InvSummaries/InvSum/TransferOut";

						nodes = xmlDoc.SelectNodes(xpath);

						foreach (XmlNode node in nodes) {
							node.InnerText = counter.ToString();
						}

						xpath = "//InvSummaries/InvSum/MenuWaste";

						nodes = xmlDoc.SelectNodes(xpath);

						foreach (XmlNode node in nodes) {
							node.InnerText = counter.ToString();
						}

						xpath = "//InvSummaries/InvSum/ProductWaste";

						nodes = xmlDoc.SelectNodes(xpath);

						foreach (XmlNode node in nodes) {
							node.InnerText = counter.ToString();
						}


						xpath = "//InvSummaries/InvSum/OnHand";

						nodes = xmlDoc.SelectNodes(xpath);

						foreach (XmlNode node in nodes) {
							node.InnerText = counter.ToString();
						}


						xpath = "//InvSummaries/InvSum/ActualUsage";

						nodes = xmlDoc.SelectNodes(xpath);

						foreach (XmlNode node in nodes) {
							node.InnerText = counter.ToString();
						}


						xpath = "//InvSummaries/InvSum/ActualPercent";

						nodes = xmlDoc.SelectNodes(xpath);

						foreach (XmlNode node in nodes) {
							node.InnerText = counter.ToString();
						}


						xpath = "//InvSummaries/InvSum/IdealUsage";

						nodes = xmlDoc.SelectNodes(xpath);

						foreach (XmlNode node in nodes) {
							node.InnerText = counter.ToString();
						}


						xpath = "//InvSummaries/InvSum/IdealPercent";

						nodes = xmlDoc.SelectNodes(xpath);

						foreach (XmlNode node in nodes) {
							node.InnerText = counter.ToString();
						}


						xpath = "//InvSummaries/InvSum/Variance";

						nodes = xmlDoc.SelectNodes(xpath);

						foreach (XmlNode node in nodes) {
							node.InnerText = counter.ToString();
						}


						xpath = "//InvSummaries/InvSum/VariancePercent";

						nodes = xmlDoc.SelectNodes(xpath);

						foreach (XmlNode node in nodes) {
							node.InnerText = counter.ToString();
						}

						string fileName = directory.Name + "_" + businessDate.ToString("yyyyMMdd") + "_InvSummary.xml";

						xmlDoc.Save(Path.Combine(directory.FullName, fileName));

						counter++;
						businessDate = businessDate.AddDays(1);
					}
				}

			}
			button5.Enabled = true;

		}
	}

	//	StringBuilder sb = new StringBuilder();

	//HFSDBData data = new HFSDBData();

	//DataTable dt = data.GetDuplicateOrders();

	//foreach (DataRow dr in dt.Rows) {

	//	int orderNumber = Convert.ToInt32(dr["OrderNumber"]);
	//	DateTime businessDate = Convert.ToDateTime(dr["BusinessDate"]);

	//	int orderId = data.GetDuplicateOrderId(orderNumber, businessDate);

	//	sb.Append(SqlTemplateBroker.Load(SqlTemplateId.DeleteOrder));

	//	sb.Replace("!ORDER_ID", orderId.ToString());
	//	sb.Append("\r\n\r\n");
	//}

	//textBox1.Text = sb.ToString();

}
