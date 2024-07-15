using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {
	
	
	
	class INFO2000Data {


		public List<string> GetCaCorporateUnitNumbersByEffDate(DateTime effectiveDate) {

			List<string> unitNumberList = new List<string>();

			StringBuilder sb = new StringBuilder();
			sb.Append(SqlTemplateBroker.Load(SqlTemplateId.GetCaCorporateUnitNumbersByEffDate));
			sb.Replace("!EFFECTIVE_DATE", effectiveDate.ToString("yyyy-MM-dd"));

			DataAccess dac = new DataAccess(AppSettings.INFO2000ConnectionString);
			DataTable dt = dac.ExecuteQuery(sb.ToString());

			foreach (DataRow dr in dt.Rows) {

				unitNumberList.Add(dr["UnitNumber"].ToString().Trim());
			}

			return unitNumberList;
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
		public List<string> GetCorporateUnitList() {

			INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString);

			List<string> unitList =
				(from u in dataContext.ps_c_unit_masters
				 where u.c_owner == 'C'
					&& u.eff_status == 'A'
					&& u.c_um_status == "O"
					&& u.effdt == dataContext.ps_c_unit_masters.Where(m => m.c_unit_no == u.c_unit_no).Select(m => m.effdt).Max()
				 orderby u.c_unit_no
				 select u.c_unit_no.Trim()).ToList();

			return unitList;
		}
		//---------------------------------------------------------------------------------------------------------
		public List<string> GetCorporateUnitList
			
			(DateTime startDate) {

			INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString);

			List<string> unitList = new List<string>();
				//(from u in dataContext.ps_c_unit_masters
				// where u.c_owner == 'C'
				//	&& u.eff_status == 'A'
				//	&& u.c_um_status == "O"
				//	&& u.effdt == dataContext.ps_c_unit_masters.Where(m => m.c_unit_no == u.c_unit_no && m.effdt <= startDate).Select(m => m.effdt).Max()
				// orderby u.c_unit_no
				// select u.c_unit_no.Trim()).ToList();

			return unitList;
		}
		//---------------------------------------------------------------------------------------------------------
		public List<DailySalesData> GetDailySalesDataList(DateTime buinessDate) {

			INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString);

			return (from p in dataContext.pos_facts
					where p.cal_date == buinessDate
					select new DailySalesData() {
						BusinessDate = buinessDate,
						CashOverShort = (decimal)p.total_cashos_amt,
						NetSales = (decimal)p.total_net_sales_amt,
						SalesTax = (decimal)p.total_tax_amt,
						TotalDeposits = (decimal)p.total_deposit_amt,
						TotalPaidIn = (decimal)p.total_paid_i_amt,
						TotalPaidOut = (decimal)p.total_paid_o_amt,
						UnitNumber = p.Restaurant_no.ToString()
					}).ToList();
		}
		//---------------------------------------------------------------------------------------------------------
		public List<deposit_dtl_fact> GetDepositDtlFactList(string unitNumber, DateTime startDate, DateTime endDate) {

            INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString);

            return (from f in dataContext.deposit_dtl_facts
                    join d in dataContext.deposit_dims on f.deposit_id equals d.deposit_id
                    where f.Cal_Date >= startDate
                       && f.Cal_Date <= endDate
                       && f.Restaurant_no == Convert.ToInt32(unitNumber)
                       && d.deposit_type == "1"
                    select f).ToList();
        }
        //---------------------------------------------------------------------------------------------------------
        public int GetDepositId(string unitNumber) {

            using (INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString)) {

                return dataContext.deposit_dims.Where(d => d.Restaurant_no == Convert.ToInt32(unitNumber) && d.deposit_type == "1").Select(d => d.deposit_id).First();
            }
        }
        //---------------------------------------------------------------------------------------------------------
        public List<EmpPunchData> GetEmployeePunchData(string unitNumber, DateTime businessDate) {

            INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString);

			List<TimeCardDetail> timeCardDetailList =
					(from d in dataContext.TimeCardDetails
					join f in dataContext.TimeCardFiles on d.TimeCardFileId equals f.TimeCardFileId
					where f.UnitNumber == unitNumber
						&& d.BusinessDate == businessDate
						&& d.PunchType != "M"
					select d).ToList();

			List<EmpPunchData> empPunchDataList = new List<EmpPunchData>();


			foreach (TimeCardDetail tcd in timeCardDetailList) {
				
				EmpPunchData epd = new EmpPunchData();

				epd.EmployeeId = tcd.EmployeeId;
				epd.PunchInDate = tcd.BusinessDate;

				epd.PunchInDate = epd.PunchInDate.AddHours((double)tcd.PunchIn);

				if (tcd.PunchIn < 3M) {
					epd.PunchInDate = epd.PunchInDate.AddDays(1);
				}

				epd.PunchOutDate = tcd.BusinessDate;

				epd.PunchOutDate = epd.PunchOutDate.AddHours((double)tcd.PunchOut);

				if (tcd.PunchOut <= 3M) {
					epd.PunchOutDate = epd.PunchOutDate.AddDays(1);
				}

				empPunchDataList.Add(epd);
			}

			return empPunchDataList;
		}
		//---------------------------------------------------------------------------------------------------------
		public List<deposit_dtl_fact> GetDepositDtlFactList(string unitNumber, DateTime businessDate) {

			using (INFO2000DataContext dataContext = new INFO2000DataContext()) {

				return (from d in dataContext.deposit_dtl_facts
						join dim in dataContext.deposit_dims on d.deposit_id equals dim.deposit_id
						where d.Restaurant_no == Convert.ToInt32(unitNumber)
						   && d.Cal_Date == businessDate
						   && dim.deposit_group == "CASH"
						select d).ToList();
			}
		}
		//---------------------------------------------------------------------------------------------------------
		public List<ExistingDeposit> GetExistingDeposits() {

			List<ExistingDeposit> depositList = new List<ExistingDeposit>();

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetExistingDeposits);

			DataAccess dac = new DataAccess(AppSettings.INFO2000ConnectionString);
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
		public DateTime? GetFirstPunchDate(string unitNumber) {

			DateTime? firstPunchDate = null;

			INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString);

			TimeCardFile tcf = dataContext.TimeCardFiles.Where(f => f.UnitNumber == unitNumber).OrderBy(f => f.WeekEndingDate).FirstOrDefault();

			if (tcf != null) {
				firstPunchDate = tcf.WeekEndingDate.AddDays(-6);
			}

			return firstPunchDate;
		}
		//---------------------------------------------------------------------------------------------------------
		public List<string> GetHardeesStarPosUnitList() {

			List<string> hardeesStarPosList = new List<string>();

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetHardeesStarPosUnitList);

			DataAccess dac = new DataAccess(AppSettings.INFO2000ConnectionString);
			DataTable dt = dac.ExecuteQuery(sql);

			foreach (DataRow dr in dt.Rows) {

				hardeesStarPosList.Add(dr["UnitNumber"].ToString().Trim());
			}

			return hardeesStarPosList;
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
		public List<string> GetInternationalUnitNumbers() {

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetInternationalUnitNumbers);

			DataAccess dac = new DataAccess(AppSettings.INFO2000ConnectionStringProd);
			List<string> unitNumberList = new List<string>();
	
			DataTable dt = dac.ExecuteQuery(sql);

			foreach (DataRow dr in dt.Rows) {
				unitNumberList.Add(dr["c_unit_no"].ToString());
			}

			return unitNumberList;
		}
        //---------------------------------------------------------------------------------------------------------
        public mp_item_fact GetMpItemFacttRecord(string unitNumber, DateTime businessDate, int mpItemId) {

			INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString);

			return (from m in dataContext.mp_item_facts
                    where m.restaurant_no == Convert.ToInt32(unitNumber)
                       && m.cal_date == businessDate
                       && m.mp_item_id == mpItemId
                    select m).FirstOrDefault();
        }
        //---------------------------------------------------------------------------------------------------------
        public decimal GetNetSales(int restaurantNo, DateTime calDate) {

            INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString);

            return (decimal)(dataContext.pos_facts.Where(p => p.Restaurant_no == restaurantNo && p.cal_date == calDate).Select(p => p.total_net_sales_amt).SingleOrDefault());

        }
		//---------------------------------------------------------------------------------------------------------
		public pos_fact GetPosFact(string unitNumber, DateTime businessDate) {

			pos_fact posFact;

			using (INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString)) {

				posFact = (from p in dataContext.pos_facts
							 where p.Restaurant_no == Convert.ToInt32(unitNumber)
								&& p.cal_date == businessDate
							 select p).Single();
			}

			return posFact;
		}
		//---------------------------------------------------------------------------------------------------------
		public int GetPosFactId(string unitNumber, DateTime businessDate) {

			int posFactId = -1;

			using (INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString)) {

				posFactId = (from p in dataContext.pos_facts
							 where p.Restaurant_no == Convert.ToInt32(unitNumber)
								&& p.cal_date == businessDate
							 select p.pos_fact_id).Single();
			}

			return posFactId;
		}
		//---------------------------------------------------------------------------------------------------------
		public List<int> GetPosFactIdList() {

            INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString);


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

			DataAccess dac = new DataAccess(AppSettings.INFO2000ConnectionString);
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
		public DataTable GetProjectXReceipts() {

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetprojectXReceipts);

			DataAccess dac = new DataAccess(AppSettings.INFO2000ConnectionString);

			return dac.ExecuteQuery(sql);
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
		//---------------------------------------------------------------------------------------------------------
		public List<TimeCardDetail> GetTimeCardDetail(string unitNumber, DateTime businessDate) {

			INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString);

			return (from d in dataContext.TimeCardDetails
					join f in dataContext.TimeCardFiles on d.TimeCardFileId equals f.TimeCardFileId
					where f.UnitNumber == unitNumber
						&& d.BusinessDate == businessDate
						&& d.PunchType != "M"
					select d).ToList();

		}
		//---------------------------------------------------------------------------------------------------
		public string GetTrecsNumber(string unitNumber) {


			string trecsNumber = "";

			//using (INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString)) {

			//	trecsNumber = (from u in dataContext.ps_c_unit_masters
			//				   where u.c_unit_no == unitNumber
			//					   && u.effdt < (dataContext.ps_c_unit_masters.Where(p => p.c_unit_no == unitNumber).Select(p => p.effdt).Max())
							   
			//}

			return trecsNumber;
		}
		//---------------------------------------------------------------------------------------------------
		public List<ps_c_unit_master> GetUnitMasterList(string unitNumber) {

			INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString);


			//return (from u in dataContext.ps_c_unit_masters
			//		where u.c_unit_no == unitNumber
			//			&& u.effdt < Convert.ToDateTime("2017-03-28")
			//		orderby u.effdt
			//		select u).ToList();


			return new List<ps_c_unit_master>();

		}

		//---------------------------------------------------------------------------------------------------
		public bool InsertDepositDtlFactRecord(deposit_dtl_fact depositDtlFact) {

			bool isSuccessful = true;

			using (INFO2000DataContext dataContext = new INFO2000DataContext()) {

				int posFactId = dataContext.pos_facts.Where(p => p.Restaurant_no == depositDtlFact.Restaurant_no && p.cal_date == depositDtlFact.Cal_Date)
													 .Select(p => p.pos_fact_id).SingleOrDefault();

				if (posFactId > 0) {
					depositDtlFact.POS_fact_id = posFactId;

					int depositId = dataContext.deposit_dims.Where(d => d.Restaurant_no == depositDtlFact.Restaurant_no && d.deposit_group == "CASH")
															.Select(d => d.deposit_id).FirstOrDefault();

					if (depositId > 0) {
						depositDtlFact.deposit_id = depositId;

						dataContext.deposit_dtl_facts.InsertOnSubmit(depositDtlFact);
						dataContext.SubmitChanges();
					}
					else {
						Logger.Write("Deposit dim record does not exist.");
						isSuccessful = false;
					}
				}
				else {
					Logger.Write("POS Fact record does not exist.");
					isSuccessful = false;
				}
			}

			return isSuccessful;
		}
		//---------------------------------------------------------------------------------------------------
		public bool IsDomesticUnit(string unitNumber) {

			bool isDomestic = true;

			INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString);

			//if (dataContext.ps_c_unit_masters.Where(p => p.c_unit_no == unitNumber && p.c_owner == 'I').Count() > 0) {
			//	isDomestic = false;
			//}

			return isDomestic;
		}
        //---------------------------------------------------------------------------------------------------
        public void SaveDepositDetail(deposit_dtl_fact depositDtl) {


            INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString);
            dataContext.deposit_dtl_facts.InsertOnSubmit(depositDtl);
			dataContext.SubmitChanges();

		}
        //---------------------------------------------------------------------------------------------------
        public void SaveMpItemFact(mp_item_fact mpItemFact) {

            using (INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString)) {

                dataContext.mp_item_facts.InsertOnSubmit(mpItemFact);
                dataContext.SubmitChanges();

            }
        }
        //---------------------------------------------------------------------------------------------------
        public void UpdateBrinkCashOverShort(string unitNumber, DateTime businessDate) {

            StringBuilder sb = new StringBuilder();
            sb.Append(SqlTemplateBroker.Load(SqlTemplateId.UpdateBrinkCashOverShort));
            sb.Replace("!UNIT_NUMBER", unitNumber);
            sb.Replace("!BUSINESS_DATE", businessDate.ToString("yyyy-MM-dd"));

            DataAccess dac = new DataAccess(AppSettings.INFO2000ConnectionString);

            dac.ExecuteQuery(sb.ToString());
        }
		//---------------------------------------------------------------------------------------------------
		public void UpdateDepositDtlFactRecord(int depositDtlFactId, decimal amount) {

			using (INFO2000DataContext dataContext = new INFO2000DataContext()) {

				deposit_dtl_fact ddf = dataContext.deposit_dtl_facts.Where(d => d.deposit_dtl_id == depositDtlFactId).Single();

				ddf.Deposit_amt = amount;
				ddf.last_chg_by = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
				ddf.last_chg_date = DateTime.Now;

				dataContext.SubmitChanges();
			}
		}
		//---------------------------------------------------------------------------------------------------
		public void UpdateDonationsAmount(int restNo, DateTime businessDate, decimal amount) {

            using (INFO2000DataContext dataContext = new INFO2000DataContext(AppSettings.INFO2000ConnectionString)) {

                pos_fact posFact = dataContext.pos_facts.Where(p => p.Restaurant_no == restNo && p.cal_date == businessDate).Single();

                posFact.total_donations_amt = amount;
                posFact.total_donations_cnt = Convert.ToInt32(amount);
                posFact.last_chg_by = "ckrcorp\\mbacon";
                posFact .last_chg_date = DateTime.Now;

                dataContext.SubmitChanges();
            }
        }
	}
}
