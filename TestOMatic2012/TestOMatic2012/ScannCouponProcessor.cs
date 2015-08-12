using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {

	class ScannCouponProcessor {

		public void ProcessUnit(string unitNumber) {

			DateTime startTime = DateTime.Now;
			Logger.Write("Begin processing scanned coupon data for unit:  " + unitNumber);

			EpassData eData = new EpassData();
			//-- TODO Handle non-existent coupon id
			int couponId = eData.GetCouponId(unitNumber);

			if (couponId != 0) {


				DataAnalysisData daData = new DataAnalysisData();

				DataTable dt = daData.GetScannedCouponUnitData(unitNumber);

				foreach (DataRow dr in dt.Rows) {

					ProcessDataRow(unitNumber, dr);
				}
			}
			else {
				Logger.Write("No coupon found for unit: " + unitNumber);
			}

			
			Logger.Write("Scanned coupon processing completed for unit:  " + unitNumber + " Elapsed time:  " + (DateTime.Now - startTime).ToString());
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessDataRow(string unitNumber, DataRow dr) {

			EpassData eData = new EpassData();

			DateTime businessDate = Convert.ToDateTime(dr["BusinessDate"]);

			Logger.Write("Processing Scanned Coupon data for unit: " + unitNumber + " and business date:  " + businessDate.ToString("MM/dd/yyyy"));

			int posFactId = eData.GetPosFactId(unitNumber, businessDate);

			if (posFactId != 0) {

				//-- TODO Handle non-existent coupon id
				int couponId = eData.GetCouponId(unitNumber);

				decimal hereAmount = Convert.ToDecimal(dr["HereAmount"]);
				int hereCount = Convert.ToInt32(dr["HereCount"]);

				decimal toGoAmount = Convert.ToDecimal(dr["ToGoAmount"]);
				int toGoCount = Convert.ToInt32(dr["ToGoCount"]);

				decimal driveThruAmount = Convert.ToDecimal(dr["DriveThruAmount"]);
				int driveThruCount = Convert.ToInt32(dr["DriveThruCount"]);

				if (!eData.CheckIfCouponRecordsExist(unitNumber, posFactId, couponId, businessDate)) {
					eData.SaveEpassCouponDetailData(unitNumber, businessDate, posFactId, couponId, hereAmount, hereCount, toGoAmount, toGoCount, driveThruAmount, driveThruCount);
				}

			}
			else {
				Logger.Write("No pos fact found for unit: " + unitNumber);
			}
		}
	}
}
