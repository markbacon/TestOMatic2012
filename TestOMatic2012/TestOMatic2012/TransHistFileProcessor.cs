using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace TestOMatic2012 {
	
	class TransHistFileProcessor {

		public void ProcessTransHistDirectories(DateTime businessDate) {


			DateTime startTime = DateTime.Now;
			Logger.Write("ProcessTransHistDirectories starting for business date: " + businessDate.ToString("MM/dd/yyyy"));


			DirectoryInfo di = new DirectoryInfo(AppSettings.CjrDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {

				if (directory.Name.ToUpper() == "X110000" || directory.Name.ToUpper().StartsWith("X1109")) {
					continue;
				}



				Logger.Write("Processing directory: " + directory.FullName);
				DateTime dirStartTime = DateTime.Now;
				ProcessTransHistDirectory(directory, businessDate);
				Logger.Write("Processing for directory: " + directory.FullName + " has completed. Elapsed time: " + (DateTime.Now - dirStartTime).ToString());

			}

			Logger.Write("ProcessTransHistDirectories has completed. Elapsed time: " + (DateTime.Now - startTime).ToString());


		}
		//---------------------------------------------------------------------------------------------------------
		public void ScanTransHistFiles(DirectoryInfo unitDirectory) {

			FileInfo[] transHistFiles = unitDirectory.GetFiles("????????.TransHist.xml");


			foreach (FileInfo transHistFile in transHistFiles) {

				ScanTransHistFile(transHistFile);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------------
		private TransactionData _data = new TransactionData();

		//---------------------------------------------------------------------------------------------------------
		private void ProcessTransHistDirectory(DirectoryInfo di, DateTime businessDate) {

			string unitNumber = di.Name.Substring(1);

			string subDirName = businessDate.ToString("yyyyMMdd");

			DirectoryInfo subDirectory = di.GetDirectories(subDirName).SingleOrDefault();

			if (subDirectory != null) {
				Logger.Write("Processing subdirectory: " + subDirectory.FullName);
				FileInfo[] files = subDirectory.GetFiles("*transhist.xml");

				foreach (FileInfo file in files) {
					Logger.Write("Saving file to database. File name: " + file.FullName);
					//_data.SaveTransHistFile(file, unitNumber);
				}

			}
			else {
				Logger.Write("Subdirectory not found.  Unit number: " + unitNumber + " subdirectory name: " + subDirName);
			}
		}
		//---------------------------------------------------------------------------------------------------------
		private void ScanTransHistFile(FileInfo transHistFile) {

			XmlDocument xmlDoc = new XmlDocument();

			xmlDoc.Load(transHistFile.OpenRead());

			string xpath = "//TLog/Transactions/Transaction[Trans_Type='Order']/Items/Item[Qty > 1]";

			XmlNodeList nodes = xmlDoc.SelectNodes(xpath);

			if (nodes.Count > 0) {


				foreach (XmlNode node in nodes) {

					string foo = node.InnerXml;


				}
			}

		}
	}
}
