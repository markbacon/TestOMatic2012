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

				sb.Replace("!EmployeeId", dr["SSN"].ToString().TrimStart(new char[] {'0'}));
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
