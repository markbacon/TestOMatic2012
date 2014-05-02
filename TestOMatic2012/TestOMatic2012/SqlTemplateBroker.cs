using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {

    enum SqlTemplateId {

		DeleteEpassDepositDetail,
		DeleteOrder,
		GetDepositDetail,
		GetDuplicateOrderId,
        GetDuplicateOrders,
		GetEpassDepositDetail,
		GetUnitsWithDeletedDeposits,
		GetLegacyTimeClockData,
		InsertEmployeeClock
    }

    class SqlTemplateBroker {

        public static string Load(SqlTemplateId templateId) {

            string templateName = "";

            switch (templateId) {

				case SqlTemplateId.DeleteEpassDepositDetail:
					templateName = "TestOMatic2012.SQL.DeleteEpassDepositDetail.sql";
					break;

				case SqlTemplateId.DeleteOrder:
					templateName = "TestOMatic2012.SQL.DeleteOrder.sql";
					break;

				case SqlTemplateId.GetDepositDetail:
					templateName = "TestOMatic2012.SQL.GetDepositDetail.sql";
					break;

				case SqlTemplateId.GetDuplicateOrderId:
					templateName = "TestOMatic2012.SQL.GetDuplicateOrderId.sql";
					break;

				case SqlTemplateId.GetDuplicateOrders:
					templateName = "TestOMatic2012.SQL.GetDuplicateOrders.sql";
					break;

				case SqlTemplateId.GetEpassDepositDetail:
					templateName = "TestOMatic2012.SQL.GetEpassDepositDetail.sql";
					break;

				case SqlTemplateId.GetUnitsWithDeletedDeposits:
					templateName = "TestOMatic2012.SQL.GetUnitsWithDeletedDeposits.sql";
					break;

				case SqlTemplateId.GetLegacyTimeClockData:
					templateName = "TestOMatic2012.SQL.GetLegacyTimeClockData.sql";
					break;

				case SqlTemplateId.InsertEmployeeClock:
					templateName = "TestOMatic2012.SQL.InsertEmployeeClock.sql";
					break;


            
            }

            return LoadTemplate(templateName);
        }
        //---------------------------------------------------------------------------------------------------
        private static string LoadTemplate(string templateName) {

            string template = "";

            System.Reflection.Assembly thisExe = System.Reflection.Assembly.GetExecutingAssembly();

            Stream s = thisExe.GetManifestResourceStream(templateName);

            using (StreamReader sr = new StreamReader(s)) {

                template = sr.ReadToEnd();
            }

            return template;
        }
    }
}
