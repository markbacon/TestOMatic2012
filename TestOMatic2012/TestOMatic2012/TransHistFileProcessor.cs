using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {
	
	class TransHistFileProcessor {

		public void ProcessTransHistDirectories(DateTime businessDate) {


			DirectoryInfo di = new DirectoryInfo(AppSettings.CjrDirectory);

			DirectoryInfo[] directories = di.GetDirectories("X11*");

			foreach (DirectoryInfo directory in directories) {

				Logger.Write("Processing directory: " + directory.FullName);

				ProcessTransHistDirectory(directory, businessDate);
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
					_data.SaveTransHistFile(file, unitNumber);
				}

			}
			else {
				Logger.Write("Subdirectory not found.  Unit number: " + unitNumber + " subdirectory name: " + subDirName);
			}
		}
	
	}
}
