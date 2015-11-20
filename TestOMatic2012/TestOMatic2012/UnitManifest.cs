using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace TestOMatic2012 {
	
	
	class UnitManifest {


		//public UnitBusinessWeek GetUnitBusinessWeek(string unitNumber) {

		//	if (_xmlDoc == null) {


		//	}



		//}
		//---------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------
		private XmlDocument _xmlDoc = null;

		private string GetXmlFilePath() {

			string xmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UnitManifest.xml");


			return xmlFilePath;
		}
		//---------------------------------------------------------------------------------------------------
		private void InitXmlDoc() {

			_xmlDoc = new XmlDocument();

			string xmlFilePath = GetXmlFilePath();

			if (File.Exists(xmlFilePath)) {

				_xmlDoc.Load(xmlFilePath);

				DateTime lastUpdated = Convert.ToDateTime(_xmlDoc.DocumentElement.GetAttribute("LastUpdated"));

				if (lastUpdated < DateTime.Today) {


				}
			}
			else {
				_xmlDoc.LoadXml("<UnitData />");
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void ProcessUnitDirectory(DirectoryInfo dirInfo) {

			DateTime businessDate = DateTime.Today;

			bool done = false;

			while (!done) {

				DirectoryInfo di = dirInfo.GetDirectories(businessDate.ToString("yyyyMMdd")).SingleOrDefault();

				if (di != null) {

				}

				businessDate = businessDate.AddDays(-1);

				
				//-- If we haven't found a weekly Smart Report in the last 30 days then assume that there are none
				if (DateTime.Today.AddDays(-30) >= businessDate) {
					done = true;
				}
			}
		}
		//---------------------------------------------------------------------------------------------------
		private void UpdateXmlDoc() {

			string reportDirectoryName = @"\\ckeanafnp01\HFSCO_RO";

			DirectoryInfo di = new DirectoryInfo(reportDirectoryName);

			List<DirectoryInfo> directoryInfoList = di.GetDirectories().ToList();


			foreach (DirectoryInfo dirInfo in directoryInfoList) {








			}
		}
	}
}
