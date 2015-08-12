using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {
	
	
	class EpassData {

		//---------------------------------------------------------------------------------------------------
		public bool CheckIfCouponRecordsExist(string unitNumber, int posFactId, int couponId, DateTime businessDate) {

			bool couponRecordsExist = false;

			StringBuilder sb = new StringBuilder();

			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.CheckIfCouponRecordsExist));

			sb.Replace("!COUPON_ID", couponId.ToString());
			sb.Replace("!POS_FACT_ID", posFactId.ToString());
			sb.Replace("!RESTAURANT_NO", unitNumber);
			sb.Replace("!CAL_DATE", businessDate.ToString("yyyy-MM-dd"));


			DataTable dt = _dac.ExecuteQuery(sb.ToString());

			if (dt.Rows.Count > 0) {
				if (Convert.ToInt32(dt.Rows[0]["CouponRecordCount"]) > 0) {
					couponRecordsExist = true;
				}
			}



			return couponRecordsExist;
		}
		public int GetCouponId(string unitNumber) {

			int couponId = 0;
			
			StringBuilder sb = new StringBuilder();
			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetCouponId));
			sb.Replace("!RESTAURANT_NUMBER", unitNumber);

			DataTable dt = _dac.ExecuteQuery(sb.ToString());

			if (dt.Rows.Count > 0) {
				couponId = Convert.ToInt32(dt.Rows[0]["coupon_id"]);
			}

			return couponId;
		}
		//---------------------------------------------------------------------------------------------------
		public int GetPosFactId(string unitNumber, DateTime businessDate) {

			int posFactId = 0;
			
			StringBuilder sb = new StringBuilder();
			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetPosFactId));
			sb.Replace("!RESTAURANT_NUMBER", unitNumber);
			sb.Replace("!BUSINESS_DATE", businessDate.ToString("yyyy-MM-dd"));

			DataTable dt = _dac.ExecuteQuery(sb.ToString());

			if (dt.Rows.Count > 0) {
				posFactId = Convert.ToInt32(dt.Rows[0]["pos_fact_id"]);
			}

			return posFactId;
		}
		//---------------------------------------------------------------------------------------------------
		public void SaveEpassCouponDetailData(string unitNumber,
												DateTime businessDate,
												int posFactId,
												int couponId,
												decimal hereAmount,
												int hereCount,
												decimal toGoAmount,
												int toGoCount,
												decimal driveThruAmount,
												int driveThruCount) {


													StringBuilder sb = new StringBuilder();

			if (hereAmount != 0) {
				sb.Append(SqlTemplateBroker.Load(SqlTemplateId.InsertEpassCouponDetailFactRecord));

				sb.Replace("!COUPON_ID", couponId.ToString());
				sb.Replace("!POS_FACT_ID", posFactId.ToString());
				sb.Replace("!RESTAURANT_NO", unitNumber);
				sb.Replace("!CAL_DATE", businessDate.ToString("yyyy-MM-dd"));
				sb.Replace("!POD_ID", ((int)DestinationId.Here).ToString());
				sb.Replace("!TOTAL_AMT", hereAmount.ToString("0.00"));
				sb.Replace("!TOTAL_CNT", hereCount .ToString());

				_dac.ExecuteActionQuery(sb.ToString());

				sb.Remove(0, sb.Length);
			}


			if (toGoAmount != 0) {
				sb.Append(SqlTemplateBroker.Load(SqlTemplateId.InsertEpassCouponDetailFactRecord));

				sb.Replace("!COUPON_ID", couponId.ToString());
				sb.Replace("!POS_FACT_ID", posFactId.ToString());
				sb.Replace("!RESTAURANT_NO", unitNumber);
				sb.Replace("!CAL_DATE", businessDate.ToString("yyyy-MM-dd"));
				sb.Replace("!POD_ID", ((int)DestinationId.ToGo).ToString());
				sb.Replace("!TOTAL_AMT", toGoAmount.ToString("0.00"));
				sb.Replace("!TOTAL_CNT", toGoCount .ToString());

				_dac.ExecuteActionQuery(sb.ToString());

				sb.Remove(0, sb.Length);
			}


			if (driveThruAmount != 0) {
				sb.Append(SqlTemplateBroker.Load(SqlTemplateId.InsertEpassCouponDetailFactRecord));

				sb.Replace("!COUPON_ID", couponId.ToString());
				sb.Replace("!POS_FACT_ID", posFactId.ToString());
				sb.Replace("!RESTAURANT_NO", unitNumber);
				sb.Replace("!CAL_DATE", businessDate.ToString("yyyy-MM-dd"));
				sb.Replace("!POD_ID", ((int)DestinationId.DriveThru).ToString());
				sb.Replace("!TOTAL_AMT", driveThruAmount.ToString("0.00"));
				sb.Replace("!TOTAL_CNT", driveThruCount.ToString());

				_dac.ExecuteActionQuery(sb.ToString());

				sb.Remove(0, sb.Length);
			}

			decimal totalCouponAmount = hereAmount + driveThruAmount + toGoAmount;

			if (totalCouponAmount > 0) {
				int totalCouponCount = hereCount + driveThruCount + toGoCount;

				sb.Append(SqlTemplateBroker.Load(SqlTemplateId.UpdatePosFact));

				sb.Replace("!POS_FACT_ID", posFactId.ToString());
				sb.Replace("!SCANNED_COUPON_AMOUNT", totalCouponAmount.ToString("0.00"));
				sb.Replace("!SCANNED_COUPON_COUNT", totalCouponCount.ToString());

				_dac.ExecuteQuery(sb.ToString());

				sb.Remove(0, sb.Length);
			}
		}
		//---------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------
		private enum DestinationId {

			Here = 1,
			ToGo = 2,
			DriveThru = 3
		}
	
		private DataAccess _dac = new DataAccess(AppSettings.INFO2000ConnectionString);
	}
}
