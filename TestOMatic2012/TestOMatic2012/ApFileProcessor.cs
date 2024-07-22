using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {
	class ApFileProcessor {

		public void Run(FileInfo apFile) {

			try {
				Stopwatch sw1 = new Stopwatch();
				sw1.Start();
				Logger.Write("ApFileProcessor.Run starting for AP file: " + apFile.Name);

				using (StreamReader sr = apFile.OpenText()) {

					while (sr.Peek() != -1) {
						string line = sr.ReadLine();

						if (line.StartsWith("0002")) {

							string invoiceNumber = line.Substring(6, 12).Trim();
							string dateString = line.Substring(22, 10);
							string amountString = line.Substring(42, 13);

							line = sr.ReadLine();

							string unitNumber = line.Substring(22, 7);

							McLaneInvoice mcLaneInvoice = new McLaneInvoice() {
								InvoiceDate = Convert.ToDateTime(dateString),
								InvoiceNumber = invoiceNumber,
								TotalAmount = Convert.ToDecimal(amountString) / 100,
								UnitNumber = unitNumber
							};

							Logger.Write("Saving invoice " + invoiceNumber + " for unit " + unitNumber);
							using (DataAnalysisDataContext dataContext = new DataAnalysisDataContext()) {
								dataContext.McLaneInvoices.InsertOnSubmit(mcLaneInvoice);
								dataContext.SubmitChanges();
							}

						}

					}



				}




				Logger.Write("ApFileProcessor.Run has completed for AP file: " + apFile.Name + ". Elapsed time: " + sw1.Elapsed.ToString());
			}
			catch (Exception ex) {
				Logger.Write("An exception occurred in ApFileProcessor.Run. Please see error log for details.");
				Logger.WriteError(ex);
			}
		}
	}
}
