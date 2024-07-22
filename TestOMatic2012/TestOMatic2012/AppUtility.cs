using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {
	class AppUtility {


		//---------------------------------------------------------------------------------------------------------
		public static string CalculateHashValue(string textToHash) {

			try {

				byte[] hashSource = Encoding.ASCII.GetBytes(textToHash);


				SHA256Managed shaMan = new SHA256Managed();
				byte[] hashResult = shaMan.ComputeHash(hashSource);

				//return Encoding.ASCII.GetString(hashResult);

				StringBuilder sb = new StringBuilder();

				for (int i = 0; i < hashResult.Length; i++) {

					sb.Append(hashResult[i].ToString("X2"));
				}

				return sb.ToString();

			}

			catch (Exception ex) {
				Logger.Write("An exception occurred in CalculateHashValue. Please see error log for details.");
				Logger.WriteError(ex);

				return "";
			}
		}

		//---------------------------------------------------------------------------------------------------------
		public static string ComputeHash(FileInfo fi) {

			byte[] fileData = null;
			byte[] hashResult = null;

			try {
				using (FileStream fs = fi.OpenRead()) {

					fileData = new byte[fi.Length];

					if (fi.Length <= Int32.MaxValue) {
						fs.Read(fileData, 0, (int)fi.Length);
					}
					else {
						byte[] buffer = new byte[1000000];
						int numBytesRead = 0;
						int copyIndex = 0;

						while ((numBytesRead = fs.Read(buffer, 0, buffer.Length)) > 0) {
							buffer.CopyTo(fileData, copyIndex);
							copyIndex += numBytesRead;
						}
					}
				}

				SHA256Managed shaMan = new SHA256Managed();
				hashResult = shaMan.ComputeHash(fileData);

				StringBuilder sb = new StringBuilder();

				for (int i = 0; i < hashResult.Length; i++) {

					sb.Append(hashResult[i].ToString("X2"));
				}

				return sb.ToString();
			}

			catch (Exception ex) {
				Logger.Write("An exception occurred in CalculateHashValue. Please see error log for details.");
				Logger.WriteError(ex);

				return "";
			}
		}
		//---------------------------------------------------------------------------------------------------------
		public static string ComputeHash(string filePath) {

			byte[] fileData = null;
			byte[] hashResult = null;

			try {
				FileInfo fi = new FileInfo(filePath);

				using (FileStream fs = fi.OpenRead()) {

					fileData = new byte[fi.Length];

					if (fi.Length <= Int32.MaxValue) {
						fs.Read(fileData, 0, (int)fi.Length);
					}
					else {
						byte[] buffer = new byte[1000000];
						int numBytesRead = 0;
						int copyIndex = 0;

						while ((numBytesRead = fs.Read(buffer, 0, buffer.Length)) > 0) {
							buffer.CopyTo(fileData, copyIndex);
							copyIndex += numBytesRead;
						}
					}
				}

				SHA256Managed shaMan = new SHA256Managed();
				hashResult = shaMan.ComputeHash(fileData);

				StringBuilder sb = new StringBuilder();

				for (int i = 0; i < hashResult.Length; i++) {

					sb.Append(hashResult[i].ToString("X2"));
				}

				return sb.ToString();
			}

			catch (Exception ex) {
				Logger.Write("An exception occurred in CalculateHashValue. Please see error log for details.");
				Logger.WriteError(ex);

				return "";
			}
		}
		public static string GetLaborDataFileName(string unitNumber, DateTime businessDate) {

			string fileName = "";

			if (!unitNumber.StartsWith("X", StringComparison.InvariantCultureIgnoreCase)) {
				fileName = "X" + unitNumber;
			}
			else {
				fileName = unitNumber;
			}

			fileName += "_" + businessDate.ToString("yyyyMMdd") + "_LaborHfs.csv";

			return fileName;
		}

	}
}
