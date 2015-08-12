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
	public partial class frmUsageTest : Form {
		public frmUsageTest() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			IdealUsageCalculator idealCalculator = new IdealUsageCalculator();

			DateTime weekEndDate = new DateTime(2014, 9, 29);

			List<InventoryUsage> idealUsageList = idealCalculator.Run(weekEndDate);

			StringBuilder sb = new StringBuilder();

			foreach (InventoryUsage idealUsage in idealUsageList) {
				sb.Append(idealUsage.productNumber);
				sb.Append("\t");
				sb.Append(idealUsage.theoreticalQuantity.ToString("0.00"));
				sb.Append("\r\n");
			}

			textBox1.Text = sb.ToString();

			button1.Enabled = true;
		}
	}
}
