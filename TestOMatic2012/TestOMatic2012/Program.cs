using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestOMatic2012 {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {


			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (args.Length > 0) {

				DateTime startDate = DateTime.MaxValue;
				DateTime endDate = DateTime.MinValue;

				foreach (string arg in args) {

					string[] items = arg.Split('=');

					if (items[0] == "/StartDate") {
						startDate = Convert.ToDateTime(items[1]);
					}
					else if (items[0] == "/EndDate") {
						endDate = Convert.ToDateTime(items[1]);
					}
				}

				Application.Run(new Form95(startDate, endDate));
			}
			else {
				//Application.Run(new frmSqlBuilder());
				Application.Run(new frmBrinkFile());
				//Application.Run(new frmConvergeTest());
				//Application.Run(new frmBrinkUnitSetup());
				//Application.Run(new frmNetSalesComparison());
				//Application.Run(new Form91());
				//Application.Run(new Form93());
				//Application.Run(new Form1());
				//Application.Run(new Form10());
				//Application.Run(new Form52());
				//Application.Run(new Form45());
				Application.Run(new Form9());
				//Application.Run(new Form86());
				//Application.Run(new Form94());
				//Application.Run(new Form95());
				//Application.Run(new Form80());
				//Application.Run(new Form88());
				//Application.Run(new frmZipArchiveRepair());
				//Application.Run(new frmTLogProcessor());
				//Application.Run(new frmPollFileRecovery());
				//Application.Run(new frmRecoverMissingFiles());
				//Application.Run(new frmScannedCouponEpassUpdate());
				//Application.Run(new frmUsageTest());
				//Application.Run(new frmSmartReportsFix());
			}
		}
	}
}
