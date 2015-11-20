using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form43 : Form {
		public Form43() {
			InitializeComponent();
		}

		private const string decimalFormat = "#.000";
		private const string dateFormat = "yyyy/MM/dd";

		//---------------------------------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {

			button1.Enabled = false;

			CreateEmplOut();


			button1.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------
		private void CreateEmplOut() {

			PCNData data = new PCNData();

			DataTable dtEmplOut = data.GetEmployeeOutTable();

			DataTable dt = data.GetEmplOutData();

			foreach (DataRow dr in dt.Rows) {

				BuildEmplOutDataRow(dr, dtEmplOut);
			}


			WriteEmployeeOutFile(dtEmplOut);

		}
		//---------------------------------------------------------------------------------------------------
		private void BuildEmplOutDataRow(DataRow drSource, DataTable dtEmplOut) {

			DataRow drEmplOut = dtEmplOut.NewRow();
			dtEmplOut.Rows.Add(drEmplOut);

			//10/14 mwu check if EmployeeNum length less then 9 (SSN with leading 0 ) will use SSN

			if (drSource["EmployeeNum"].ToString().Length < 9) {
				string strEmployeeNum = PCNCommon.addLeadingzero(drSource["EmployeeNum"].ToString().Trim());
				drEmplOut["EmployeeNum"] = strEmployeeNum.PadRight(dtEmplOut.Columns["EmployeeNum"].MaxLength, ' '); ;
			}
			else
				drEmplOut["EmployeeNum"] = drSource["EmployeeNum"].ToString().Trim().PadRight(dtEmplOut.Columns["EmployeeNum"].MaxLength, ' ');
			if (drSource["SSN"].ToString().Length < 9) {
				string strSSN = PCNCommon.addLeadingzero(drSource["SSN"].ToString().Trim());
				drEmplOut["SSN"] = strSSN.PadRight(dtEmplOut.Columns["SSN"].MaxLength, ' '); ;
			}
			else
				drEmplOut["SSN"] = drSource["SSN"].ToString().Trim().PadRight(dtEmplOut.Columns["SSN"].MaxLength, ' ');

			drEmplOut["ActionCode"] = PCNConstants.ActionCode.kActionTransfer.ToString().PadRight(dtEmplOut.Columns["ActionCode"].MaxLength, ' ');
			drEmplOut["ActionReason"] = PCNConstants.ActionReason.kActionTransferMRR.PadRight(dtEmplOut.Columns["ActionReason"].MaxLength, ' ');

			drEmplOut["TransactionStatus"] = "NOR".PadRight(dtEmplOut.Columns["TransactionStatus"].MaxLength, ' ');
			drEmplOut["ApprovalStatus"] = "".PadRight(dtEmplOut.Columns["ApprovalStatus"].MaxLength, ' ');
			if (drSource["TransactionSequenceNo"] != null && drSource["TransactionSequenceNo"].ToString() != "") {
				drEmplOut["TransactionSequenceNo"] = drSource["TransactionSequenceNo"].ToString().Trim().PadRight(dtEmplOut.Columns["TransactionSequenceNo"].MaxLength, ' ');
			}
			else {
				drEmplOut["TransactionSequenceNo"] = "".PadRight(dtEmplOut.Columns["TransactionSequenceNo"].MaxLength, ' ');
			}
			drEmplOut["sequenceFlag"] = "R".ToString().PadRight(dtEmplOut.Columns["sequenceFlag"].MaxLength, ' ');
			drEmplOut["applicationUser"] = "".PadRight(dtEmplOut.Columns["applicationUser"].MaxLength, ' ');
			drEmplOut["lastName"] = drSource["lastName"].ToString().Trim().Replace('"', '\'').PadRight(dtEmplOut.Columns["lastName"].MaxLength, ' ');
			drEmplOut["firstName"] = drSource["firstName"].ToString().Trim().Replace('"', '\'').PadRight(dtEmplOut.Columns["firstName"].MaxLength, ' ');
			drEmplOut["middleName"] = drSource["middleName"].ToString().Trim().Replace('"', '\'').PadRight(dtEmplOut.Columns["middleName"].MaxLength, ' ');
			if (!(drSource["birthDate"].ToString() == "")) {
				drEmplOut["birthDate"] = Convert.ToDateTime(drSource["birthDate"]).ToString(dateFormat).Replace("/", "").Trim().PadRight(dtEmplOut.Columns["birthDate"].MaxLength, ' ');
			}
			else {
				drEmplOut["birthDate"] = "";
			}
			drEmplOut["sex"] = drSource["sex"].ToString().Trim().PadRight(dtEmplOut.Columns["sex"].MaxLength, ' ');
			//Ethnic group ID should be at 156 position instead of 155 and 155 should be blank.
			string ethnicGroup = " " + drSource["ethnicGroup"].ToString();
			drEmplOut["ethnicGroup"] = ethnicGroup.TrimEnd().PadRight(dtEmplOut.Columns["ethnicGroup"].MaxLength, ' ');
			drEmplOut["address"] = drSource["address"].ToString().Trim().Replace('"', '\'').PadRight(dtEmplOut.Columns["address"].MaxLength, ' ');
			drEmplOut["city"] = drSource["city"].ToString().Trim().Replace('"', '\'').PadRight(dtEmplOut.Columns["city"].MaxLength, ' ');
			drEmplOut["county"] = drSource["county"].ToString().Trim().Replace('"', '\'').PadRight(dtEmplOut.Columns["county"].MaxLength, ' ');
			drEmplOut["state"] = drSource["state"].ToString().Trim().PadRight(dtEmplOut.Columns["state"].MaxLength, ' ');
			drEmplOut["zip"] = drSource["zip"].ToString().Trim().Replace("-", "").PadRight(dtEmplOut.Columns["zip"].MaxLength, ' ');

			if (drSource["phone1"] != null && drSource["phone1"].ToString() != "") {
				drEmplOut["phone1"] = drSource["phone1"].ToString().Trim().Replace("-", "").PadRight(dtEmplOut.Columns["phone1"].MaxLength, ' ');
			}
			else {
				drEmplOut["phone1"] = "".PadRight(dtEmplOut.Columns["phone1"].MaxLength, ' ');
			}

			if (drSource["phone2"] != null && drSource["phone2"].ToString() != "") {
				drEmplOut["phone2"] = drSource["phone2"].ToString().Trim().Replace("-", "").PadRight(dtEmplOut.Columns["phone2"].MaxLength, ' ');
			}
			else {
				drEmplOut["phone2"] = "".PadRight(dtEmplOut.Columns["phone2"].MaxLength, ' ');
			}

			if ((drSource["phone2Type"] != null && drSource["phone2Type"].ToString() != "") && Convert.ToInt32(drSource["phone2Type"].ToString()) > 0) {
				drEmplOut["phone2Type"] = drSource["Phone2Type"].ToString().Trim().PadRight(dtEmplOut.Columns["phone2Type"].MaxLength, ' ');
			}
			else {
				drEmplOut["phone2Type"] = "".PadRight(dtEmplOut.Columns["phone2Type"].MaxLength, ' ');
			}

			if (drSource["disabled"] != null && drSource["disabled"].ToString() != "") {
				drEmplOut["disabled"] = drSource["Disabled"].ToString().Trim().PadRight(dtEmplOut.Columns["disabled"].MaxLength, ' ');
			}
			else {
				drEmplOut["disabled"] = "".PadRight(dtEmplOut.Columns["disabled"].MaxLength, ' ');
			}

			if (drSource["disabledVet"] != null && drSource["disabledVet"].ToString() != "") {
				drEmplOut["disabledVet"] = drSource["Disabledvet"].ToString().Trim().PadRight(dtEmplOut.Columns["disabledVet"].MaxLength, ' ');
			}
			else {
				drEmplOut["disabledVet"] = "".PadRight(dtEmplOut.Columns["disabledVet"].MaxLength, ' ');
			}


			if (drSource["UnitedWayWithholding"] != null && drSource["UnitedWayWithholding"].ToString() != "" && Convert.ToDouble(drSource["UnitedWayWithholding"].ToString()) != 0) {
				if (Convert.ToDouble(drSource["UnitedWayWithholding"]).ToString(decimalFormat).Trim().Length > dtEmplOut.Columns["UnitedWayWithholding"].MaxLength)
					drEmplOut["UnitedWayWithholding"] = Convert.ToDouble(drSource["UnitedWayWithholding"]).ToString(decimalFormat).Trim().Substring(0, dtEmplOut.Columns["UnitedWayWithholding"].MaxLength);
				else
					drEmplOut["UnitedWayWithholding"] = Convert.ToDouble(drSource["UnitedWayWithholding"]).ToString(decimalFormat).Trim().PadRight(dtEmplOut.Columns["UnitedWayWithholding"].MaxLength, ' ');
			}
			else {
				drEmplOut["UnitedWayWithholding"] = "0.000".PadRight(dtEmplOut.Columns["UnitedWayWithholding"].MaxLength, ' ');
			}

			if (drSource["TaxCreditNo"] != null && drSource["TaxCreditNo"].ToString() != "") {
				drEmplOut["TaxCreditNo"] = drSource["TaxCreditNo"].ToString().Trim().PadRight(dtEmplOut.Columns["TaxCreditNo"].MaxLength, ' ');
			}
			else {
				drEmplOut["TaxCreditNo"] = "".PadRight(dtEmplOut.Columns["TaxCreditNo"].MaxLength, ' ');
			}

			if (drSource["PermitExpirationDate"] != null && drSource["PermitExpirationDate"].ToString() != "01/01/1900" && drSource["PermitExpirationDate"].ToString() != "") {
				drEmplOut["PermitExpirationDate"] = Convert.ToDateTime(drSource["PermitExpirationDate"]).ToString(dateFormat).Trim().Replace("/", "").PadRight(dtEmplOut.Columns["PermitExpirationDate"].MaxLength, ' ');
			}
			else {
				drEmplOut["PermitExpirationDate"] = "".PadRight(dtEmplOut.Columns["PermitExpirationDate"].MaxLength, ' ');
			}

			if (drSource["FederalMaritalstatus"] != null && drSource["FederalMaritalstatus"].ToString() != "") {
				drEmplOut["FederalMaritalstatus"] = drSource["FederalMaritalstatus"].ToString().Trim().PadRight(dtEmplOut.Columns["FederalMaritalstatus"].MaxLength, ' ');
			}
			else {
				drEmplOut["FederalMaritalstatus"] = "".PadRight(dtEmplOut.Columns["FederalMaritalstatus"].MaxLength, ' ');
			}

			if (drSource["FederalDependents"] != null && drSource["FederalDependents"].ToString() != "" && Convert.ToDouble(drSource["FederalDependents"].ToString()) != 0) {
				drEmplOut["FederalDependents"] = drSource["FederalDependents"].ToString().Trim().PadRight(dtEmplOut.Columns["FederalDependents"].MaxLength, ' ');
			}
			else {
				drEmplOut["FederalDependents"] = "0".PadRight(dtEmplOut.Columns["FederalDependents"].MaxLength, ' ');
			}

			if (drSource["FederalExempt"] != null && drSource["FederalExempt"].ToString() != "") {
				drEmplOut["FederalExempt"] = drSource["FederalExempt"].ToString().Trim().PadRight(dtEmplOut.Columns["FederalExempt"].MaxLength, ' ');
			}
			else {
				drEmplOut["FederalExempt"] = "".PadRight(dtEmplOut.Columns["FederalExempt"].MaxLength, ' ');
			}

			if (drSource["AdditionalFederalWithholding"] != null && drSource["AdditionalFederalWithholding"].ToString() != "" && Convert.ToDouble(drSource["AdditionalFederalWithholding"].ToString()) != 0) {
				if (Convert.ToDouble(drSource["AdditionalFederalWithholding"]).ToString(decimalFormat).Trim().Length > dtEmplOut.Columns["AdditionalFederalWithholding"].MaxLength)
					drEmplOut["AdditionalFederalWithholding"] = Convert.ToDouble(drSource["AdditionalFederalWithholding"]).ToString(decimalFormat).Trim().Substring(0, dtEmplOut.Columns["AdditionalFederalWithholding"].MaxLength);
				else
					drEmplOut["AdditionalFederalWithholding"] = Convert.ToDouble(drSource["AdditionalFederalWithholding"]).ToString(decimalFormat).Trim().PadRight(dtEmplOut.Columns["AdditionalFederalWithholding"].MaxLength, ' ');
			}
			else {
				drEmplOut["AdditionalFederalWithholding"] = "0.000".PadRight(dtEmplOut.Columns["AdditionalFederalWithholding"].MaxLength, ' ');
			}

			if (drSource["taxState"] != null && drSource["taxState"].ToString() != "") {
				drEmplOut["taxState"] = drSource["taxState"].ToString().Trim().PadRight(dtEmplOut.Columns["taxState"].MaxLength, ' ');
			}
			else {
				drEmplOut["taxState"] = drSource["state"].ToString().PadRight(dtEmplOut.Columns["taxState"].MaxLength, ' ');//"".PadRight(dtEmplOut.Columns["taxState"].MaxLength, ' ');
			}

			if (drSource["stateMaritalStatus"] != null && drSource["stateMaritalStatus"].ToString() != "") {
				drEmplOut["stateMaritalStatus"] = drSource["stateMaritalStatus"].ToString().Trim().PadRight(dtEmplOut.Columns["stateMaritalStatus"].MaxLength, ' ');
			}
			else {
				drEmplOut["stateMaritalStatus"] = "".PadRight(dtEmplOut.Columns["stateMaritalStatus"].MaxLength, ' ');
			}

			if (drSource["StateDependents"] != null && drSource["StateDependents"].ToString() != "") {
				drEmplOut["StateDependents"] = drSource["StateDependents"].ToString().Trim().PadRight(dtEmplOut.Columns["StateDependents"].MaxLength, ' ');
			}
			else {
				drEmplOut["StateDependents"] = "0".PadRight(dtEmplOut.Columns["StateDependents"].MaxLength, ' ');
			}

			if (drSource["stateExempt"] != null && drSource["stateExempt"].ToString() != "") {
				drEmplOut["stateExempt"] = drSource["stateExempt"].ToString().Trim().PadRight(dtEmplOut.Columns["stateExempt"].MaxLength, ' ');
			}
			else {
				drEmplOut["stateExempt"] = "".PadRight(dtEmplOut.Columns["stateExempt"].MaxLength, ' ');
			}

			if (drSource["AdditionalStateWithholding"] != null && drSource["AdditionalStateWithholding"].ToString() != "" && Convert.ToDouble(drSource["AdditionalStateWithholding"].ToString()) != 0) {
				if (Convert.ToDouble(drSource["AdditionalStateWithholding"]).ToString(decimalFormat).Trim().Length > dtEmplOut.Columns["AdditionalStateWithholding"].MaxLength)
					drEmplOut["AdditionalStateWithholding"] = Convert.ToDouble(drSource["AdditionalStateWithholding"]).ToString(decimalFormat).Trim().Substring(0, dtEmplOut.Columns["AdditionalStateWithholding"].MaxLength);
				else
					drEmplOut["AdditionalStateWithholding"] = Convert.ToDouble(drSource["AdditionalStateWithholding"]).ToString(decimalFormat).Trim().PadRight(dtEmplOut.Columns["AdditionalStateWithholding"].MaxLength, ' ');
			}
			else {
				drEmplOut["AdditionalStateWithholding"] = "0.000".PadRight(dtEmplOut.Columns["AdditionalStateWithholding"].MaxLength, ' ');
			}

			if (drSource["percentofFederal"] != null && drSource["percentofFederal"].ToString() != "" && Convert.ToDouble(drSource["percentofFederal"].ToString()) != 0) {
				drEmplOut["percentofFederal"] = Convert.ToDouble(drSource["percentofFederal"]).ToString(decimalFormat).Trim().PadRight(dtEmplOut.Columns["percentofFederal"].MaxLength, ' ');
			}
			else {
				drEmplOut["percentofFederal"] = "0.000".PadRight(dtEmplOut.Columns["percentofFederal"].MaxLength, ' ');
			}



			drEmplOut["companyClass"] = "1".PadRight(dtEmplOut.Columns["companyClass"].MaxLength, ' ');
			drEmplOut["companyNum"] = "10".PadRight(dtEmplOut.Columns["companyNum"].MaxLength, ' ');
			drEmplOut["restaurantNum"] = "1100101".PadRight(dtEmplOut.Columns["restaurantNum"].MaxLength, ' ');


			if (drSource["HireDate"] != null && drSource["HireDate"].ToString() != "") {
				drEmplOut["HireDate"] = Convert.ToDateTime(drSource["HireDate"]).ToString(dateFormat).Trim().Replace("/", "").PadRight(dtEmplOut.Columns["HireDate"].MaxLength, ' ');
			}
			else {
				drEmplOut["HireDate"] = "".PadRight(dtEmplOut.Columns["HireDate"].MaxLength, ' ');
			}

			if (drSource["reviewRating"] != null && drSource["reviewRating"].ToString() != "") {
				drEmplOut["reviewRating"] = drSource["reviewRating"].ToString().Trim().PadRight(dtEmplOut.Columns["reviewRating"].MaxLength, ' ');
			}
			else {
				drEmplOut["reviewRating"] = "0".PadRight(dtEmplOut.Columns["reviewRating"].MaxLength, ' ');
			}

			if (drSource["jobCode"] != null && drSource["jobCode"].ToString() != "") {
				drEmplOut["jobCode"] = drSource["jobCode"].ToString().Trim().PadRight(dtEmplOut.Columns["jobCode"].MaxLength, ' ');
			}
			else {
				drEmplOut["jobCode"] = "".PadRight(dtEmplOut.Columns["jobCode"].MaxLength, ' ');
			}

			drEmplOut["payGroup"] = "".PadRight(dtEmplOut.Columns["payGroup"].MaxLength, ' ');

			if (drSource["effectiveDate"] != null && drSource["effectiveDate"].ToString() != "") {
				drEmplOut["effectiveDate"] = Convert.ToDateTime(drSource["effectiveDate"]).ToString(dateFormat).Replace("/", "").Trim().PadRight(dtEmplOut.Columns["effectiveDate"].MaxLength, ' ');
			}
			else {
				drEmplOut["effectiveDate"] = "".PadRight(dtEmplOut.Columns["effectiveDate"].MaxLength, ' ');
			}
			if (drSource["payRate"] != null && drSource["payRate"].ToString() != "") {
				drEmplOut["payRate"] = Convert.ToDouble(drSource["payRate"]).ToString(decimalFormat).Trim().PadRight(dtEmplOut.Columns["payRate"].MaxLength, ' ');
			}
			else {
				drEmplOut["payRate"] = "".PadRight(dtEmplOut.Columns["payRate"].MaxLength, ' ');
			}

			drEmplOut["rehireEligibility"] = "".PadRight(dtEmplOut.Columns["rehireEligibility"].MaxLength, ' ');
			drEmplOut["rehireDate"] = "".PadRight(dtEmplOut.Columns["rehireDate"].MaxLength, ' ');
			drEmplOut["expectedReturnDate"] = "".PadRight(dtEmplOut.Columns["expectedReturnDate"].MaxLength, ' ');
			drEmplOut["terminationDate"] = "".PadRight(dtEmplOut.Columns["terminationDate"].MaxLength, ' ');
			drEmplOut["lastDayWorked"] = "".PadRight(dtEmplOut.Columns["lastDayWorked"].MaxLength, ' ');
			drEmplOut["injuryDate"] = "".PadRight(dtEmplOut.Columns["injuryDate"].MaxLength, ' ');
			drEmplOut["i9ExpirationDate"] = "".PadRight(dtEmplOut.Columns["i9ExpirationDate"].MaxLength, ' ');
			drEmplOut["referralCode"] = "".PadRight(dtEmplOut.Columns["referralCode"].MaxLength, ' ');
			drEmplOut["emergencyContactLastName"] = "".PadRight(dtEmplOut.Columns["emergencyContactLastName"].MaxLength, ' ');
			drEmplOut["emergencyContactFirstName"] = "".PadRight(dtEmplOut.Columns["emergencyContactFirstName"].MaxLength, ' ');
			drEmplOut["emergencyContactMiddleName"] = "".PadRight(dtEmplOut.Columns["emergencyContactMiddleName"].MaxLength, ' ');
			drEmplOut["emergencyContactPhone"] = "".PadRight(dtEmplOut.Columns["emergencyContactPhone"].MaxLength, ' ');
			drEmplOut["secCode"] = "".PadRight(dtEmplOut.Columns["secCode"].MaxLength, ' ');
			drEmplOut["positionCode"] = "".PadRight(dtEmplOut.Columns["positionCode"].MaxLength, ' ');
			drEmplOut["comment"] = "".PadRight(dtEmplOut.Columns["comment"].MaxLength, ' ');
		}
		//---------------------------------------------------------------------------------------------------------
		private void WriteEmployeeOutFile(DataTable dtEmplOut) {

			string filePath = "C:\\Temp\\x1100101_Empl.out";

			using (StreamWriter sw = new StreamWriter(filePath)) {



				foreach (DataRow dr in dtEmplOut.Rows) {

					StringBuilder sbLine = new StringBuilder();

					for (int i = 0; i < dtEmplOut.Columns.Count; i++) {

						sbLine.Append(dr[i].ToString());
					}

					sw.WriteLine(sbLine.ToString());
				}
			}
		}

	}
}
