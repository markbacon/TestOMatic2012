using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {
	
	class LaborHoursProcessor {

		public string BuildUpdateSQL() {

			string sqlTemplate =
@"UPDATE lf
SET [hours] = !HOURS
	,lf.last_chg_by = 'Ckrcorp\mbacon'
	,lf.last_chg_date = GETDATE()
FROM [INFO2000].[dbo].[labor_fact] lf
INNER JOIN [INFO2000].[dbo].[employee_dim] ed ON lf.employee_id = ed.employee_id
WHERE lf.restaurant_no = !UNIT_NUMBER
AND lf.cal_date = '2014-03-10'
AND ed.employee_ssn = '!SSN'";




			


			
			StringBuilder sb = new StringBuilder();


			using (StreamReader sr = new StreamReader("C:\\Temp\\LaborHours.txt")) {

				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					string[] items = line.Split(new char[] { '\t' });

					//sb.Remove(0, sb.Length);

					sb.Append(sqlTemplate);
					sb.Replace("!UNIT_NUMBER", items[0]);
					sb.Replace("!SSN", items[1].Trim());
					sb.Replace("!HOURS", items[2].Trim());
					sb.Append("\r\n\r\n\r\n");
				}

			}


			return sb.ToString();
		}
		//---------------------------------------------------------------------------------------------------------
		public void Run() {

			string directoryName = "C:\\Temp62";

			DirectoryInfo di = new DirectoryInfo(directoryName);

			FileInfo[] files = di.GetFiles();

			foreach (FileInfo file in files) {

				ProcessFile(file);
			}


		}
		//---------------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------------
		private DataAnalysisDataContext _dataContext = new DataAnalysisDataContext();

		//---------------------------------------------------------------------------------------------------------
		private void ProcessFile(FileInfo file) {

			using (StreamReader sr = new StreamReader(file.OpenRead())) {
					
				while (sr.Peek() != -1) {

					string line = sr.ReadLine();

					if (line.StartsWith("15")) {

						string[] items = line.Split(new char[] { ',' });

						LaborHour laborHour = new LaborHour();
						laborHour.Hours = Convert.ToDecimal(items[4]);
						laborHour.SSN = items[2].Trim();
						laborHour.UnitNumber = items[0].Trim();

						_dataContext.LaborHours.InsertOnSubmit(laborHour);
						_dataContext.SubmitChanges();
					}

				}
			}


		}

	}
}
