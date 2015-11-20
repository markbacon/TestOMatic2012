using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {
	
	
	
	class INFO2000Data {

		public string ExecuteCashOverShortUpdate(int posFactId) {

			string errMessage = "";

			INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000_testConnectionString);
			dataContext.usp_cash_os_upd(posFactId, ref errMessage);

			return errMessage;
		}
		//---------------------------------------------------------------------------------------------------------
		public List<string> GetCarlsStarPosUnitList() {

			List<string> carlsStarPosList = new List<string>();

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetCarlsStarPosUnitList);

			DataAccess dac = new DataAccess(AppSettings.INFO2000ConnectionString);
			DataTable dt = dac.ExecuteQuery(sql);

			foreach (DataRow dr in dt.Rows) {

				carlsStarPosList.Add(dr["UnitNumber"].ToString().Trim());
			}

			return carlsStarPosList;
		}
		//---------------------------------------------------------------------------------------------------------
		public DataTable GetCarlsValidationData(DateTime businessDate) {

			StringBuilder sb = new StringBuilder();
			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetCarlsValidation));
			sb.Replace("!BUSINESS_DATE", businessDate.ToString("yyyy-MM-dd"));

			DataAccess dac = new DataAccess(AppSettings.INFO2000ConnectionStringProd);

			return dac.ExecuteQuery(sb.ToString());
		}
		//---------------------------------------------------------------------------------------------------------
		public List<ExistingDeposit> GetExistingDeposits() {

			List<ExistingDeposit> depositList = new List<ExistingDeposit>();

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetExistingDeposits);

			DataAccess dac = new DataAccess(AppSettings.INFO2000_testConnectionString);
			DataTable dt = dac.ExecuteQuery(sql);

			foreach (DataRow dr in dt.Rows) {
				ExistingDeposit dep = new ExistingDeposit();
				dep.CalDate = Convert.ToDateTime(dr["cal_date"]);
				dep.DepositAmount = Convert.ToDecimal(dr["deposit_amt"]);
				dep.DepositId = Convert.ToInt32(dr["deposit_id"]);
				dep.RestaurantNumber = Convert.ToInt32(dr["restaurant_no"]);

				depositList.Add(dep);
			}


			return depositList;
		}
		//---------------------------------------------------------------------------------------------------------
		public DataTable GetHardeesValidationData(DateTime businessDate) {

			StringBuilder sb = new StringBuilder();
			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetHardeesValidation));
			sb.Replace("!BUSINESS_DATE", businessDate.ToString("yyyy-MM-dd"));

			DataAccess dac = new DataAccess(AppSettings.INFO2000ConnectionStringProd);

			return dac.ExecuteQuery(sb.ToString());
		}
		//---------------------------------------------------------------------------------------------------------
		public List<int> GetPosFactIdList() {

			INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000_testConnectionString);


			List<int> posFactIdList =
				(from p in dataContext.pos_facts
				 where p.System_id == 2
					&& p.cal_date >= new DateTime(2015, 8, 10)
					&& p.cal_date <= new DateTime(2015, 9, 7)
				 select p.pos_fact_id).ToList();

			return posFactIdList;
		}
		//---------------------------------------------------------------------------------------------------
		public List<deposit_dtl_fact> GetProductionDeposits() {

			List<deposit_dtl_fact> depositDetailList = new List<deposit_dtl_fact>();

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetProductionDeposits);

			DataAccess dac = new DataAccess(AppSettings.INFO2000_testConnectionString);
			DataTable dt = dac.ExecuteQuery(sql);

			foreach (DataRow dr in dt.Rows) {

				deposit_dtl_fact depositDetail = new deposit_dtl_fact();

				depositDetail.Cal_Date = Convert.ToDateTime(dr["cal_date"]);
				depositDetail.create_by = dr["create_by"].ToString();
				depositDetail.create_date = Convert.ToDateTime(dr["create_date"]);
				depositDetail.Deposit_amt = Convert.ToDecimal(dr["deposit_amt"]);
				depositDetail.deposit_id = Convert.ToInt32(dr["deposit_id"]);
				depositDetail.last_chg_by = dr["last_chg_by"].ToString();
				depositDetail.last_chg_date = Convert.ToDateTime(dr["last_chg_date"]);
				depositDetail.POS_fact_id = Convert.ToInt32(dr["pos_fact_id"]);
				depositDetail.Restaurant_no = Convert.ToInt32(dr["restaurant_no"]);
				depositDetail.source = dr["source"].ToString();
				depositDetail.System_id = Convert.ToInt32(dr["system_id"]);

				depositDetailList.Add(depositDetail);
			}


			return depositDetailList;
		}
		//---------------------------------------------------------------------------------------------------------
		public List<StarPosUnit> GetStarPosUnitList() {

			List<StarPosUnit> starPosList = new List<StarPosUnit>();

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetCarlsStarPosUnitList);

			DataAccess dac = new DataAccess(AppSettings.INFO2000ConnectionStringProd);
			DataTable dt = dac.ExecuteQuery(sql);

			foreach (DataRow dr in dt.Rows) {
				StarPosUnit spu = new StarPosUnit();

				spu.EffectiveDate = Convert.ToDateTime(dr["EFFDT"]);
				spu.UnitNumber = dr["UnitNumber"].ToString().Trim();

				starPosList.Add(spu);
			}

			return starPosList;
		}
		//---------------------------------------------------------------------------------------------------
		public void SaveDepositDetail(deposit_dtl_fact depositDtl) {


			INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000_testConnectionString);
			dataContext.deposit_dtl_facts.InsertOnSubmit(depositDtl);
			dataContext.SubmitChanges();



		}
	}

}
