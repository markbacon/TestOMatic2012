using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {
	
	class AppSettings {

		//---------------------------------------------------------------------------------------------------------
		public static string BrinkApiDataConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["BrinkApiDataConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------------
		public static string CarlsEmployeeFilePath {

			get {
				return ConfigurationManager.AppSettings["CarlsEmployeeFilePath"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string CrunchTimeCloudConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["CrunchTimeCloudConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string CjrDirectory {

			get {
				return ConfigurationManager.AppSettings["CjrDirectory"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string CkeNodeDirectory {

			get {
				return ConfigurationManager.AppSettings["CkeNodeDirectory"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string EcfDestDirectory {

			get {
				return ConfigurationManager.AppSettings["EcfDestDirectory"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string EcfFileName {

			get {
				return ConfigurationManager.AppSettings["EcfFileName"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string EcfSourceDirectory {

			get {
				return ConfigurationManager.AppSettings["EcfSourceDirectory"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string EmailPassword {

			get {
				return ConfigurationManager.AppSettings["EmailPassword"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static int EmailPort {

			get {
				return Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"]);
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string EmailServer {

			get {
				return ConfigurationManager.AppSettings["EmailServer"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string EmailUserName {

			get {
				return ConfigurationManager.AppSettings["EmailUserName"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string HardeesEmployeeFilePath {

			get {
				return ConfigurationManager.AppSettings["HardeesEmployeeFilePath"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string CkeTimePollDataConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["TransactionDataConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string DataAnalysisConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["DataAnalysisConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string ElavonProcessingConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["ElavonProcessingConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string IMSConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["IMSConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string HardeesTransactionDataConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["TransactionDataConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string HfsDirectory {

			get {
				return ConfigurationManager.AppSettings["HfsDirectory"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string INFO2000ConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["INFO2000ConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string INFO2000ConnectionStringProd {

			get {
				return ConfigurationManager.ConnectionStrings["INFO2000ConnectionStringProd"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string IRISConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["IRISConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string LocalLaborHfsDirectory {

			get {
				return ConfigurationManager.AppSettings["LocalLaborHfsDirectory"];
			}
		}
		//---------------------------------------------------------------------------------------------------------
		public static string OdsPosEntitiesDevConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["OdsPosEntitiesDevConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------------
		public static string OdsPosIntegrationConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["TestOMatic2012.Properties.Settings.ODS_PosIntegrationConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------------
		public static string OdsTestConnectionString {

            get {
                return ConfigurationManager.ConnectionStrings["OdsTestConnectionString"].ConnectionString;
            }
        }
		//---------------------------------------------------------------------------------------------------
		public static string OloPayConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["OloPayConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string OutboxFileNamePrefix {

			get {
				return ConfigurationManager.AppSettings["OutboxFileNamePrefix"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string PCNConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["TransactionDataConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string RBIConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["RBIConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string RbiDataDirectory {

			get {
				return ConfigurationManager.AppSettings["RbiDataDirectory"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string RecipesConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["RecipesConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string RecipesDevConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["RecipesDevConnectionString"].ConnectionString;
			}
		}
        //---------------------------------------------------------------------------------------------------------
        public static string REDConnectionString {

            get {
                return ConfigurationManager.ConnectionStrings["REDConnectionString"].ConnectionString;
            }
        }
        //---------------------------------------------------------------------------------------------------
		public static string RedStorageConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["RedStorageConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string RsUnitsConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["RsUnitsConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string SmartReportsZipFileName {

			get {
				return ConfigurationManager.AppSettings["SmartReportsZipFileName"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string StoreArchiveDirectory {

			get {
				return ConfigurationManager.AppSettings["StoreArchiveDirectory"];
			}
		}

		
		//---------------------------------------------------------------------------------------------------
		public static string TimeFileAnalysisConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["TestOMatic2012.Properties.Settings.TimeFileAnalysisConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string TransactionDataConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["TransactionDataConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string WeeklyLaborSummaryByDayFileNamePrefix {

			get {
				return ConfigurationManager.AppSettings["WeeklyLaborSummaryByDayFileNamePrefix"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string WeeklyLaborSummaryFileNamePrefix {

			get {
				return ConfigurationManager.AppSettings["WeeklyLaborSummaryFileNamePrefix"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string WeeklyRingOutLaborSummaryFileNamePrefix {

			get {
				return ConfigurationManager.AppSettings["WeeklyRingOutLaborSummaryFileNamePrefix"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string Win7UnitListFilePath {

			get {
				return ConfigurationManager.AppSettings["Win7UnitListFilePath"];
			}
		}
	}
}
