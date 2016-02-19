using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form23 : Form {
		public Form23() {
			InitializeComponent();
		}

		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;

			string filePath = "C:\\Temp62\\TLogs_20150420.zip";
			string host = "ftp://ftps.revenuemanage.com";
			string userName = "cke";
			string password = "8Xt@*5y1";

			SendBinaryFile(filePath, host, userName, password);

			button2.Enabled = true;
		}
		//---------------------------------------------------------------------------------------------------
		private void SendBinaryFile(string filePath, string host, string userName, string password) {

			try {
				DateTime startTime = DateTime.Now;
				Logger.Write("TLogProcessor.SendBinary starting...");

				// Get the object used to communicate with the server.
				FtpWebRequest request = (FtpWebRequest)WebRequest.Create(host + "/" + Path.GetFileName(filePath));
				request.Method = WebRequestMethods.Ftp.UploadFile;
				request.EnableSsl = true;
				request.UseBinary = true;

				request.Credentials = new NetworkCredential(userName, password);

				// Copy the contents of the file to the request stream.
				byte[] fileContents = File.ReadAllBytes(filePath);
				request.ContentLength = fileContents.Length;

				Stream requestStream = request.GetRequestStream();
				requestStream.Write(fileContents, 0, fileContents.Length);
				requestStream.Close();

				FtpWebResponse response = (FtpWebResponse)request.GetResponse();

				Logger.Write("TLogProcessor.SendBinary has completed with a status of: " + response.StatusCode.ToString());

				response.Close();

			}
			catch (Exception ex) {

				Logger.Write("An exception occurred in TLogFtpProcessor.SendBinary.  Please see error log for details.");
				Logger.WriteError(ex);
			}
		}

		private void textBox1_TextChanged(object sender, EventArgs e) {

		}

		private void button1_Click(object sender, EventArgs e) {
			
			button1.Enabled = false;

			string dirPath = @"C:\Projects\FTPUnits\FTPUnits\bin\Debug\Xpient\Wednesday";

			DirectoryInfo di = new DirectoryInfo(dirPath);

			FileInfo[] files = di.GetFiles("*.zip.zip");

			foreach (FileInfo file in files) {

				string destFilePath = Path.Combine(file.Directory.FullName, Path.GetFileNameWithoutExtension(file.FullName));

				file.CopyTo(destFilePath, true);
				file.Delete();

			}



			button1.Enabled = true;
			

		}

	}
}
