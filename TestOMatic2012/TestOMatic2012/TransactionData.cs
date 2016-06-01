using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {

	class TransactionData {

		public int SaveTransHistFile(FileInfo file, string unitNumber) {

			TransHistFile thf = new TransHistFile();

			thf.BusinessDate = DateTime.ParseExact(file.Name.Substring(0, 8), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
			thf.CreateDate = DateTime.Now;
			thf.UnitNumber = unitNumber;

			using (StreamReader sr = file.OpenText()) {
				thf.XmlData = sr.ReadToEnd();
			}


			TransactionDataContext dataContext = new TransactionDataContext(AppSettings.TransactionDataConnectionString);
			dataContext.TransHistFiles.InsertOnSubmit(thf);
			dataContext.SubmitChanges();

			return thf.TransHistFileId;

		}
	
	}
}
