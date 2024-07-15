using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {

	
	class ValidationFileBuilder {

		public string BuildCarlsFile(DateTime businessDate) {

			try {



				INFO2000Data data = new INFO2000Data();

				DataTable dt = data.GetCarlsValidationData(businessDate);


				string filePath = GetCarlsFilePath(businessDate);

				FileInfo validationFile = new FileInfo(filePath);

				if (validationFile.Exists) {
					validationFile.Delete();
				}

				using (StreamWriter sw = new StreamWriter(validationFile.Open(FileMode.Create))) {

					sw.Write(BuildValidationDetail(dt));
				}


				return filePath;
			}
			catch (Exception ex) {
				Logger.Write("An exception occurred in BuildCarlsFile.  Please see error log for details.");
				Logger.WriteError(ex);

				return "";
			}
		}

		//---------------------------------------------------------------------------------------------------
		public string BuildHardeesFile(DateTime businessDate) {

			try {

				INFO2000Data data = new INFO2000Data();

				DataTable dt = data.GetHardeesValidationData(businessDate);


				string filePath = GetHardeesFilePath(businessDate);

				FileInfo validationFile = new FileInfo(filePath);

				if (validationFile.Exists) {
					validationFile.Delete();
				}

				using (StreamWriter sw = new StreamWriter(validationFile.Open(FileMode.Create))) {

					sw.Write(BuildValidationDetail(dt));
				}


				return filePath;
			}
			catch (Exception ex) {
				Logger.Write("An exception occurred in BuildCarlsFile.  Please see error log for details.");
				Logger.WriteError(ex);

				return "";
			}

		}
		//---------------------------------------------------------------------------------------------------
		private const string VALIDATION_FILE_HEADER = "DATE\tUNIT #\tTOTAL SALES\tBREAKFAST SALES\tLUNCH SALES\tDINNER SALES\tOVERNIGHT SALES\tTOTAL TRANS\tBREAKFAST TRANS\tLUNCH TRANS\tDINNER TRANS\tOVERNIGHT TRANS";


		private string BuildValidationDetail(DataTable dt) {

			StringBuilder sb = new StringBuilder();

			sb.Append(BuildValidationFileHeader());
			sb.Append("\r\n");

			foreach (DataRow dr in dt.Rows) {

				sb.Append(Convert.ToDateTime(dr["cal_date"]).ToString("ddMMMyyyy"));
				sb.Append('\t');

				sb.Append(dr["restaurant_no"].ToString());
				sb.Append('\t');

				sb.Append(Convert.ToDecimal(dr["TotalNetSales"]).ToString("0.00"));
				sb.Append('\t');


				sb.Append(Convert.ToDecimal(dr["BreakfastNetSales"]).ToString("#.##"));
				sb.Append('\t');

				sb.Append(Convert.ToDecimal(dr["LunchNetSales"]).ToString("#.##"));
				sb.Append('\t');

				sb.Append(Convert.ToDecimal(dr["DinnerNetSales"]).ToString("#.##"));
				sb.Append('\t');

				sb.Append(Convert.ToDecimal(dr["LateNightNetSales"]).ToString("#.##"));
				sb.Append('\t');

				sb.Append(Convert.ToInt32(dr["TotalTransactionCount"]).ToString("#"));
				sb.Append('\t');

				sb.Append(Convert.ToInt32(dr["BreakfastTransactionCount"]).ToString("#"));
				sb.Append('\t');

				sb.Append(Convert.ToInt32(dr["LunchTransactionCount"]).ToString("#"));
				sb.Append('\t');

				sb.Append(Convert.ToInt32(dr["DinnerTransactionCount"]).ToString("#"));
				sb.Append('\t');

				sb.Append(Convert.ToInt32(dr["LateNightTransactionCount"]).ToString("#"));
				sb.Append("\r\n");
			}

			return sb.ToString();
		}

		//---------------------------------------------------------------------------------------------------
		private string BuildValidationFileHeader() {

			return VALIDATION_FILE_HEADER;
		}
		//---------------------------------------------------------------------------------------------------
		private string GetCarlsFilePath(DateTime businessDate) {

			string directoryName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Work");

			if (!Directory.Exists(directoryName)) {
				Directory.CreateDirectory(directoryName);
			}

			return Path.Combine(directoryName, "CarlsValidation" + businessDate.ToString("yyyyMMdd") + ".txt");

		}
		//---------------------------------------------------------------------------------------------------
		private string GetHardeesFilePath(DateTime businessDate) {

			string directoryName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Work");

			if (!Directory.Exists(directoryName)) {
				Directory.CreateDirectory(directoryName);
			}

			return Path.Combine(directoryName, "HardeesValidation" + businessDate.ToString("yyyyMMdd") + ".txt");

		}

	}
}
