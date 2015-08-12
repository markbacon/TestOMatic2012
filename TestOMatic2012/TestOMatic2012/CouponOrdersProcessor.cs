using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {
	
	
	class CouponOrdersProcessor {

		public void Run() {

			DateTime startRunTime = DateTime.Now;
			Logger.Write("CouponOrdersProcessor.Run starting...");

			string dataDirectory = "D:\\CouponOrderFiles";

			DirectoryInfo di = new DirectoryInfo(dataDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X15*");

			foreach (DirectoryInfo directory in directories) {

				DateTime startTime = DateTime.Now;
				Logger.Write("Processing directory: " + directory.Name);
				ProcessDirectory(directory);
				Logger.Write("Processing completed for directory: " + directory.Name + " Elapsed time: " + (DateTime.Now - startRunTime).ToString());
			}

			Logger.Write("CouponOrdersProcessor.Run has completed.  Elapsed time: " + (DateTime.Now - startRunTime).ToString());

		}
		//---------------------------------------------------------------------------------------------------------
		private DataAnalysisDataContext _dataContext = new DataAnalysisDataContext();

		//---------------------------------------------------------------------------------------------------------
		private void ProcessDirectory(DirectoryInfo directory) {

			string unitNumber = directory.Name;

			FileInfo[] files = directory.GetFiles("*Coupon_Orders.csv");

			foreach (FileInfo file in files) {

				Logger.Write("Processsing file: " + file.Name);
				ProcessFile(unitNumber, file);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessFile(string unitNumber, FileInfo file) {


			using (StreamReader sr = file.OpenText()) {

				string line = sr.ReadLine();

				string[] items = line.Split(new char[] { ',' });

				CouponOrder co = new CouponOrder();

				co.Amount = Convert.ToDecimal(items[2]);
				co.BusinessDate = Convert.ToDateTime(items[0]);
				co.Quantity = Convert.ToInt32(items[1]);
				co.UnitNumber = unitNumber;

				_dataContext.CouponOrders.InsertOnSubmit(co);
				_dataContext.SubmitChanges();
			}
		}
	}
}
