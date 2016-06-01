using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace TestOMatic2012 {

	public enum FileType {
		WeeklyTimeFile
	}

	
	class FileCreationLog {


		public static DateTime GetLastRunBusinessDate(FileType fileType) {

			DateTime businessDate = DateTime.MinValue;
			XmlDocument xmlDoc = LoadXmlDoc();

			string xpath = "//Log/File[@FileType='" + fileType.ToString() + "']";
			XmlNodeList nodes = xmlDoc.SelectNodes(xpath);

			foreach (XmlElement element in nodes) {

				DateTime tempDate = Convert.ToDateTime(element.GetAttribute("BusinessDate"));

				if (tempDate > businessDate) {
					businessDate = tempDate;
				}
			}

			return businessDate;
		}
		//---------------------------------------------------------------------------------------------------------
		public static void Write(FileType fileType, DateTime fileBusinessDate) {

			XmlDocument xmlDoc = LoadXmlDoc();

			XmlElement element = xmlDoc.CreateElement("File");

			element.SetAttribute("FileType", fileType.ToString());
			element.SetAttribute("BusinessDate", fileBusinessDate.ToString("MM/dd/yyyy"));
			element.SetAttribute("CreateDate", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

			xmlDoc.DocumentElement.AppendChild(element);

			xmlDoc.Save(GetFilePath());
		}
		//---------------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------------
		private static string GetFilePath() {

			return Path.Combine(GetLogFileDirectory(), ServiceSettings.FileCreationLogFileName);
		}
		//---------------------------------------------------------------------------------------------------
		private static string GetLogFileDirectory() {

			string logFileDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";

			//Create directory if it doesn't exist
			if (!Directory.Exists(logFileDirectory)) {
				Directory.CreateDirectory(logFileDirectory);
			}

			return logFileDirectory;
		}
		//---------------------------------------------------------------------------------------------------
		private static XmlDocument LoadXmlDoc() {

			string filePath = GetFilePath();

			XmlDocument xmlDoc = new XmlDocument();

			if (File.Exists(filePath)) {

				xmlDoc.Load(filePath);
			}
			else {
				xmlDoc.LoadXml("<Log/>");
			}



			return xmlDoc;
		}



	}
}
