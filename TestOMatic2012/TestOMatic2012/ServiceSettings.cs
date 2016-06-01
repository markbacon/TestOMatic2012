using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {
	
	class ServiceSettings {

		public static int BusinessDayStartHour {

			get {
				//-- Variable is initalized as -1
				if (_businessDayStartTime == -1) {
					_businessDayStartTime = 3;
				}

				return _businessDayStartTime;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static DayOfWeek BusinessWeekEndDay {

			get {

				DayOfWeek weekStartDay = BusinessWeekStartDay;

				DayOfWeek weekEndDay = DayOfWeek.Monday;

				switch (weekStartDay) {

					case DayOfWeek.Sunday:
						weekEndDay = DayOfWeek.Saturday;
						break;

					case DayOfWeek.Monday:
						weekEndDay = DayOfWeek.Sunday;
						break;

					case DayOfWeek.Tuesday:
						weekEndDay = DayOfWeek.Monday;
						break;

					case DayOfWeek.Wednesday:
						weekEndDay = DayOfWeek.Tuesday;
						break;

					case DayOfWeek.Thursday:
						weekEndDay = DayOfWeek.Wednesday;
						break;

					case DayOfWeek.Friday:
						weekEndDay = DayOfWeek.Thursday;
						break;

					case DayOfWeek.Saturday:
						weekEndDay = DayOfWeek.Friday;
						break;

				}

				return weekEndDay;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static DayOfWeek BusinessWeekStartDay {

			get {
				try {

					if (_weekStartDayOfWeek == null) {
						HFSDBData data = new HFSDBData();

						short dayNum = data.GetBusinessWeekStartDay();

						//-- DayOfWeek enum is zero based.
						_weekStartDayOfWeek = (DayOfWeek)dayNum - 1;
					}

					return (DayOfWeek)_weekStartDayOfWeek;
				}
				catch (Exception ex) {
					Logger.Write("An error occurred in ServiceSettings.BusinessWeekStartDay. Please see error log for details.");
					Logger.WriteError(ex);
					return DayOfWeek.Tuesday;
				}
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static double DefaultTimerInterval {

			get {
				double timerIntervalHours = Convert.ToDouble(ConfigurationManager.AppSettings["DefaultTimerIntervalHours"]);

				return timerIntervalHours * Constants.MILLISECONDS_PER_HOUR;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static double DefaultTimerIntervalHours {

			get {
				return Convert.ToDouble(ConfigurationManager.AppSettings["DefaultTimerIntervalHours"]);
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string FileCreationLogFileName {

			get {
				return ConfigurationManager.AppSettings["FileCreationLogFileName"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string FileGeneratorFilePath {

			get {
				return ConfigurationManager.ConnectionStrings["FileGeneratorFilePath"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string HFSDBConnectionString {

			get {
				return ConfigurationManager.ConnectionStrings["HFSDBConnectionString"].ConnectionString;
			}
		}
		//---------------------------------------------------------------------------------------------------
		public static double PunchEditCutoffHours {

			get {
				//-- TODO Retrieve this vale from the database once table changes are completed.
				return 7;

			}
		}
		//---------------------------------------------------------------------------------------------------
		public static string WeeklyTimeFileCommandLineArguments {

			get {
				return ConfigurationManager.AppSettings["WeeklyTimeFileCommandLineArguments"];
			}
		}
		//---------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------

		private static int _businessDayStartTime = -1;
		private static DayOfWeek? _weekStartDayOfWeek = null;

	}
}
