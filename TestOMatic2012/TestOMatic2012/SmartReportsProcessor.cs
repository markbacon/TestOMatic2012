using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Ionic.Zip;

namespace TestOMatic2012 {
	
	class SmartReportsProcessor {


		//---------------------------------------------------------------------------------------------------
		public void RunAgainstStoreArchive() {

			try {
				DateTime startTime = DateTime.Now;
				Logger.Write("SmartReportsProcessor.Run starting...");
				DirectoryInfo di = new DirectoryInfo(AppSettings.StoreArchiveDirectory);

				List<string> win7UnitList = Win7NodeUtility.GetWin7NodeList();


				foreach (string win7Unit in win7UnitList) {


					DirectoryInfo[] directories = di.GetDirectories(win7Unit);

					Logger.Write(directories.Length.ToString() + " X15 directories found.");

					foreach (DirectoryInfo directory in directories) {

						ProcessStoreArchiveDirectory(directory);
					}
				}
			}
			catch (Exception ex) {
				Logger.Write("An exception occurred in SmartReportsProcessor.RunAginstStoreArchive. Please see error log for details.");
				Logger.WriteError(ex);
			}
		}
		//---------------------------------------------------------------------------------------------------
		public void RunAgainstStoreArchiveFred() {

			try {
				DateTime startTime = DateTime.Now;
				Logger.Write("SmartReportsProcessor.Run starting...");
				DirectoryInfo di = new DirectoryInfo(AppSettings.StoreArchiveDirectory);

				DirectoryInfo[] directories = di.GetDirectories("x1505621");

				Logger.Write(directories.Length.ToString() + " X15 directories found.");

				foreach (DirectoryInfo directory in directories) {

					ProcessStoreArchiveDirectory(directory);
				}
			}
			catch (Exception ex) {
				Logger.Write("An exception occurred in SmartReportsProcessor.RunAginstStoreArchive. Please see error log for details.");
				Logger.WriteError(ex);
			}
		}
		//---------------------------------------------------------------------------------------------------
		public void ProcessNode(DirectoryInfo di) {

			try {
				string unitNumber = di.Name;

				FileInfo[] files = di.GetFiles(AppSettings.SmartReportsZipFileName);

				if (files.Length > 0) {

					DateTime businessDate = DateTime.Today.AddDays(-1);

					ProcessSmartReportsZipFile(unitNumber, businessDate, files[0]);
				}
			}
			catch (Exception ex) {
				Logger.Write("An exception occurred in SmartReportsProcessor.ProcessNode for directory: " + di.FullName + ". Please see error log for details.");
				Logger.WriteError(ex);
			}
		}
		//---------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------
		private DataAnalysisDataContext _dataContext = new DataAnalysisDataContext();


		//---------------------------------------------------------------------------------------------------
		private string GetDateStringFromSmartReportsFileName(string smartReportsFileName) {

			string temp = Path.GetFileNameWithoutExtension(smartReportsFileName);

			int pos = temp.Length - "20131224".Length;

			string dateString = temp.Substring(pos);

			return dateString;
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessHFSCO_RO_Directory(DirectoryInfo di) {

			string unitNumber = di.Name;

			DateTime workingDate = DateTime.Today.AddDays(-1);

			int directoryNumber = (workingDate.Year * 10000) + (workingDate.Month * 100) + workingDate.Day;

			DirectoryInfo smartReportsDirectory = di.GetDirectories(directoryNumber.ToString()).FirstOrDefault();

			if (smartReportsDirectory != null) {

				if (smartReportsDirectory.Exists) {

					FileInfo[] smartReportsFiles = smartReportsDirectory.GetFiles("*.xml");
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessLaborWages(string unitNumber, FileInfo[] smartReportFiles) {

			FileInfo weeklyLaborFile =
				(from f in smartReportFiles
				 where f.Name.IndexOf(AppSettings.WeeklyLaborSummaryFileNamePrefix) > -1
					&& f.Name.IndexOf(AppSettings.WeeklyLaborSummaryByDayFileNamePrefix) == -1
				 select f).FirstOrDefault();

			if (weeklyLaborFile != null) {

				ProcessWeeklyLaborSummaryFile(unitNumber, weeklyLaborFile);

			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessSmartReportsZipFile(string unitNumber, DateTime businessDate, FileInfo zfile) {

			Logger.Write("Begin processing Smart Reports Zip file: " + zfile.FullName);

			string tempDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp-SmartReportsExtract");

			DirectoryInfo tempDirInfo = new DirectoryInfo(tempDirectory);

			if (!tempDirInfo.Exists) {
				tempDirInfo.Create();
			}

			using (ZipFile zippy = new ZipFile(zfile.FullName)) {

				zippy.ExtractAll(tempDirectory, ExtractExistingFileAction.OverwriteSilently);
			}

			FileInfo[] smartReportsFiles = tempDirInfo.GetFiles("*.xml");

			Logger.Write(smartReportsFiles.Length.ToString() + " Smart Reports files found in zip.");

			if (smartReportsFiles.Length > 0) {

				ProcessLaborWages(unitNumber, smartReportsFiles);
			}

			foreach (FileInfo smartReportsFile in smartReportsFiles) {
				smartReportsFile.Delete();
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ProcessStoreArchiveDirectory(DirectoryInfo di) {

			string tempDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp");

			DirectoryInfo tempDirInfo = new DirectoryInfo(tempDirectory);

			if (!tempDirInfo.Exists) {
				tempDirInfo.Create();
			}

			string unitNumber = di.Name;

			FileInfo[] files = di.GetFiles("*.zip");

			foreach (FileInfo zfile in files) {

				string temp = Path.GetFileNameWithoutExtension(zfile.Name);

				int year = 2014;
				int month = Convert.ToInt32(temp.Substring(0, 2));
				int day = Convert.ToInt32(temp.Substring(2, 2));

				DateTime businessDate = new DateTime(year, month, day);


				using (ZipFile zippy = new ZipFile(zfile.FullName)) {

					zippy.ExtractAll(tempDirectory, ExtractExistingFileAction.OverwriteSilently);
				}

				FileInfo[] smartReportsZipFiles = tempDirInfo.GetFiles("SmartRpts.zip");

				if (smartReportsZipFiles.Count() > 0) {

					ProcessSmartReportsZipFile(unitNumber, businessDate, smartReportsZipFiles[0]);
				}

				FileInfo[] filesToDelete = tempDirInfo.GetFiles();

				foreach (FileInfo fileToDelete in filesToDelete) {
					fileToDelete.Delete();
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private decimal ProcessWeeklyLaborSummaryFile(string unitNumber, FileInfo weekLaborFile) {


			WeeklyLaborFile wkLaborFile = new WeeklyLaborFile();
			wkLaborFile.FileDate = weekLaborFile.LastWriteTime;
			wkLaborFile.FileName = weekLaborFile.Name;
			wkLaborFile.UnitNumber = unitNumber;

			_dataContext.WeeklyLaborFiles.InsertOnSubmit(wkLaborFile);
			_dataContext.SubmitChanges();

			int weekLaborFileId = wkLaborFile.WeeklyLaborFileId;



			decimal totalWages = 0;

			XmlDocument xmlDoc = new XmlDocument();

			xmlDoc.Load(weekLaborFile.FullName);

			XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
			ns.AddNamespace("n", "http://schemas.datacontract.org/2004/07/DataAccess");


			string xpath = "/n:ArrayOfWeeklyLaborSummaryUI/n:WeeklyLaborSummaryUI";

			XmlNodeList nodes = xmlDoc.SelectNodes(xpath, ns);

			foreach (XmlNode node in nodes) {

				if (node != null) {

					WeeklyLaborDetail wkLaborDetail = new WeeklyLaborDetail();
					wkLaborDetail.WeekLaborFileId = weekLaborFileId;


					XmlNode dateNode = node.SelectSingleNode("n:BusinessDate", ns);

					string temp = dateNode.InnerText.Substring(0, 10);

					wkLaborDetail.BusinessDate = Convert.ToDateTime(temp);
					wkLaborDetail.OverTimePayCrew = 0;
					wkLaborDetail.OverTimePayLeader = 0;
					wkLaborDetail.RegularPayCrew = 0;
					wkLaborDetail.RegularPayLeader = 0;
					

					XmlNode wageNode = node.SelectSingleNode("n:RegularPay_Crew", ns);

					if (wageNode != null) {

						if (!string.IsNullOrEmpty(wageNode.InnerText)) {
							wkLaborDetail.RegularPayCrew = Convert.ToDecimal(wageNode.InnerText);
							totalWages += Convert.ToDecimal(wageNode.InnerText);
						}
					}


					wageNode = node.SelectSingleNode("n:OTPay_Crew", ns);

					if (wageNode != null) {
						if (!string.IsNullOrEmpty(wageNode.InnerText)) {
							wkLaborDetail.OverTimePayCrew = Convert.ToDecimal(wageNode.InnerText);
							totalWages += Convert.ToDecimal(wageNode.InnerText);
						}
					}


					wageNode = node.SelectSingleNode("n:RegularPay_Leader", ns);

					if (wageNode != null) {
						if (!string.IsNullOrEmpty(wageNode.InnerText)) {
							wkLaborDetail.RegularPayLeader = Convert.ToDecimal(wageNode.InnerText);
							totalWages += Convert.ToDecimal(wageNode.InnerText);
						}
					}


					wageNode = node.SelectSingleNode("n:OTPay_Leader", ns);

					if (wageNode != null) {
						if (!string.IsNullOrEmpty(wageNode.InnerText)) {
							wkLaborDetail.OverTimePayLeader = Convert.ToDecimal(wageNode.InnerText);
							totalWages += Convert.ToDecimal(wageNode.InnerText);
						}
					}

					_dataContext.WeeklyLaborDetails.InsertOnSubmit(wkLaborDetail);
					_dataContext.SubmitChanges();
				}
			}
			return totalWages;
		}
		//---------------------------------------------------------------------------------------------------------
		private decimal ProcessWeeklyRingOutLaborSummaryFile(FileInfo weeklyRingOutLaborFile) {

			decimal totalHours = 0;

			XmlDocument xmlDoc = new XmlDocument();

			xmlDoc.Load(weeklyRingOutLaborFile.FullName);

			string xpath = "//n:ArrayOfRingOutWeeklyLaborSummaryUI/n:RingOutWeeklyLaborSummaryUI/n:TotalHrs";

			XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
			ns.AddNamespace("n", "http://schemas.datacontract.org/2004/07/DataAccess");

			XmlNodeList nodes = xmlDoc.SelectNodes(xpath, ns);

			foreach (XmlNode node in nodes) {

				decimal hours = Convert.ToDecimal(node.InnerText);
				totalHours += hours;
			}

			return totalHours;
		}
	}
}
