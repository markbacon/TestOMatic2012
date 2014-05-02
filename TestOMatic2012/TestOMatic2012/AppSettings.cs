using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {
	
	class AppSettings {

		public static string DataAnalysisConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["DataAnalysisConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string INFO2000ConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["INFO2000ConnectionString"].ConnectionString;
			}
		}



	}
}
