using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {

	
	class BusinessDateUtility {

		public static DateTime GetBusinessDate(DateTime testTime) {

			DateTime businessDate = testTime.Date;

			DateTime startDateTime = GetBusinessDateStartTime(businessDate);
			DateTime endDateTime = GetBusinessDateEndTime(businessDate);

			if (businessDate < startDateTime) {
				businessDate = businessDate.AddDays(-1);
			}

			else if (businessDate > endDateTime) {
				businessDate = businessDate.AddDays(1);
			}

			return businessDate;
		}
		//---------------------------------------------------------------------------------------------------------
		public static DateTime GetBusinessDateEndTime(DateTime businessDate) {

			return businessDate.Date.AddDays(1).AddHours(ServiceSettings.BusinessDayStartHour).AddSeconds(-1);
		}
		//---------------------------------------------------------------------------------------------------------
		public static DateTime GetBusinessDateStartTime(DateTime businessDate) {

			return businessDate.Date.AddHours(ServiceSettings.BusinessDayStartHour);
		}
		//---------------------------------------------------------------------------------------------------------
		public static DateTime GetLastWeekEndDate(DateTime testTime) {

			DateTime weekEndDate = testTime.Date;

			while (weekEndDate.DayOfWeek != ServiceSettings.BusinessWeekEndDay) {
				weekEndDate = weekEndDate.AddDays(-1);
			}

			return weekEndDate;
		}
		//---------------------------------------------------------------------------------------------------------
		public static DateTime GetWeeklyPollFileStartTime(DateTime businessDate) {
			
			DateTime lastWeekEndDate = GetLastWeekEndDate(businessDate);

			DateTime pollFileStartTime = GetBusinessDateStartTime(lastWeekEndDate.AddDays(1));

			pollFileStartTime = pollFileStartTime.AddHours(ServiceSettings.PunchEditCutoffHours);

			return pollFileStartTime;
		}
	
	}
}
