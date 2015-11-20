using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {
	
	class PCNData {

		public DataTable GetEmplOutData() {

			string sql = SqlTemplateBroker.Load(SqlTemplateId.GetEmplOutData);

			DataAccess dac = new DataAccess(AppSettings.PCNConnectionString);


			return dac.ExecuteQuery(sql);
		}
		//---------------------------------------------------------------------------------------------------
		public DataTable GetEmployeeOutTable() {
			DataTable tbEmpOut = new DataTable();

			DataColumn column = new DataColumn("EmployeeNum", Type.GetType("System.String"));
			column.MaxLength = 11;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("SSN", Type.GetType("System.String"));
			column.MaxLength = 9;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("ActionCode", Type.GetType("System.String"));
			column.MaxLength = 3;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("ActionReason", Type.GetType("System.String"));
			column.MaxLength = 3;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("TransactionStatus", Type.GetType("System.String"));
			column.MaxLength = 3;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("ApprovalStatus", Type.GetType("System.String"));
			column.MaxLength = 3;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("TransactionSequenceNo", Type.GetType("System.String"));
			column.MaxLength = 12;
			tbEmpOut.Columns.Add(column);

			//column = new DataColumn("hireSequence", Type.GetType("System.String"));
			//column.MaxLength = 6;
			//tbEmpOut.Columns.Add(column);

			column = new DataColumn("sequenceFlag", Type.GetType("System.String"));
			column.MaxLength = 1;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("applicationUser", Type.GetType("System.String"));
			column.MaxLength = 30;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("lastName", Type.GetType("System.String"));
			column.MaxLength = 30;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("firstName", Type.GetType("System.String"));
			column.MaxLength = 30;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("middleName", Type.GetType("System.String"));
			column.MaxLength = 10;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("birthDate", Type.GetType("System.String"));
			column.MaxLength = 8;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("sex", Type.GetType("System.String"));
			column.MaxLength = 1;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("ethnicGroup", Type.GetType("System.String"));
			column.MaxLength = 3;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("address", Type.GetType("System.String"));
			column.MaxLength = 35;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("city", Type.GetType("System.String"));
			column.MaxLength = 20;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("county", Type.GetType("System.String"));
			column.MaxLength = 30;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("state", Type.GetType("System.String"));
			column.MaxLength = 2;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("zip", Type.GetType("System.String"));
			column.MaxLength = 9;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("phone1", Type.GetType("System.String"));
			column.MaxLength = 10;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("phone2", Type.GetType("System.String"));
			column.MaxLength = 10;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("phone2Type", Type.GetType("System.String"));
			column.MaxLength = 3;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("disabled", Type.GetType("System.String"));
			column.MaxLength = 1;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("disabledVet", Type.GetType("System.String"));
			column.MaxLength = 1;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("unitedWayWithholding", Type.GetType("System.String"));
			column.MaxLength = 7;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("TaxCreditNo", Type.GetType("System.String"));
			column.MaxLength = 13;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("PermitExpirationDate", Type.GetType("System.String"));
			column.MaxLength = 8;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("FederalMaritalstatus", Type.GetType("System.String"));
			column.MaxLength = 1;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("FederalDependents", Type.GetType("System.String"));
			column.MaxLength = 3;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("FederalExempt", Type.GetType("System.String"));
			column.MaxLength = 1;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("AdditionalFederalWithholding", Type.GetType("System.String"));
			column.MaxLength = 9;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("taxState", Type.GetType("System.String"));
			column.MaxLength = 2;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("stateMaritalStatus", Type.GetType("System.String"));
			column.MaxLength = 1;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("StateDependents", Type.GetType("System.String"));
			column.MaxLength = 3;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("stateExempt", Type.GetType("System.String"));
			column.MaxLength = 1;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("AdditionalStateWithholding", Type.GetType("System.String"));
			column.MaxLength = 9;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("percentofFederal", Type.GetType("System.String"));
			column.MaxLength = 6;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("companyClass", Type.GetType("System.String"));
			column.MaxLength = 1;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("companyNum", Type.GetType("System.String"));
			column.MaxLength = 8;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("restaurantNum", Type.GetType("System.String"));
			column.MaxLength = 10;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("hireDate", Type.GetType("System.String"));
			column.MaxLength = 8;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("reviewRating", Type.GetType("System.String"));
			column.MaxLength = 2;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("jobCode", Type.GetType("System.String"));
			column.MaxLength = 8;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("payGroup", Type.GetType("System.String"));
			column.MaxLength = 3;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("effectiveDate", Type.GetType("System.String"));
			column.MaxLength = 8;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("payRate", Type.GetType("System.String"));
			column.MaxLength = 9;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("rehireEligibility", Type.GetType("System.String"));
			column.MaxLength = 1;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("rehireDate", Type.GetType("System.String"));
			column.MaxLength = 8;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("expectedReturnDate", Type.GetType("System.String"));
			column.MaxLength = 8;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("terminationDate", Type.GetType("System.String"));
			column.MaxLength = 8;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("lastDayWorked", Type.GetType("System.String"));
			column.MaxLength = 8;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("injuryDate", Type.GetType("System.String"));
			column.MaxLength = 8;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("i9ExpirationDate", Type.GetType("System.String"));
			column.MaxLength = 8;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("referralCode", Type.GetType("System.String"));
			column.MaxLength = 3;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("emergencyContactLastName", Type.GetType("System.String"));
			column.MaxLength = 30;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("emergencyContactFirstName", Type.GetType("System.String"));
			column.MaxLength = 30;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("emergencyContactMiddleName", Type.GetType("System.String"));
			column.MaxLength = 10;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("emergencyContactPhone", Type.GetType("System.String"));
			column.MaxLength = 10;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("secCode", Type.GetType("System.String"));
			column.MaxLength = 5;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("positionCode", Type.GetType("System.String"));
			column.MaxLength = 3;
			tbEmpOut.Columns.Add(column);

			//paycard
			column = new DataColumn("paycard", Type.GetType("System.String"));
			column.MaxLength = 16;
			tbEmpOut.Columns.Add(column);

			column = new DataColumn("comment", Type.GetType("System.String"));
			column.MaxLength = 255;
			tbEmpOut.Columns.Add(column);

			return tbEmpOut;
		}

	}
}
