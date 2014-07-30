using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form5 : Form {
		public Form5() {
			InitializeComponent();
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

			string sql = textBox1.Text;

			DataAccess dac = new DataAccess(AppSettings.IRISConnectionString);

			DataTable dt = dac.ExecuteQuery(sql);

			dt.TableName = "DayPartSales";

			dt.WriteXmlSchema("C:\\temp\\DayPartSales.xsd");



			button2.Enabled = true;
		}
	}
}
