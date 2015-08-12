using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {
	
	class AppSettings {

		public static string CjrDirectory {

			get {
				return ConfigurationManager.AppSettings["CjrDirectory"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string CkeTimePollDataConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["CkeTimePollDataConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string DataAnalysisConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["DataAnalysisConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string IMSConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["IMSConnectionString"].ConnectionString;
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
