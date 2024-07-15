using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {

	class TaskProcessor {

		public bool RunProgram(string command, string commandArgs) {

			bool isSuccessful = true;

			try {
				 
				Logger.Write("Start Task: " + command + " " + commandArgs);

				//string message = "";


				ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(command, commandArgs);
				psi.CreateNoWindow = true;

				//psi.RedirectStandardError = true;
				//psi.RedirectStandardInput = true;
				//psi.RedirectStandardOutput = true;
				psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

				string workingDirectoryName = Path.GetDirectoryName(command);

				if (!string.IsNullOrEmpty(workingDirectoryName)) {
					psi.WorkingDirectory = workingDirectoryName;
				}


				//psi.UseShellExecute = false;

				Process commandProcess = System.Diagnostics.Process.Start(psi);
				//StreamReader sr1 = commandProcess.StandardError;
				//StreamReader sr2 = commandProcess.StandardOutput;

				//while (!commandProcess.HasExited) {
				//	System.Threading.Thread.Sleep(5000);
				//}

				//if (commandProcess.ExitCode != 0) {
				//	isSuccessful = false;
				//}

				//if (isSuccessful) {
				//	message = "Command: " + command + " Successful with an exit code of: " + commandProcess.ExitCode.ToString();
				//	Logger.Write(message);
				//}
				//else {
				//	message = "Command:" + command + " Failed with exit code of: " + commandProcess.ExitCode.ToString();
				//	Logger.Write(message);

				//	string failureReason = "Failure Reason: " + sr1.ReadToEnd();

				//	Logger.Write(failureReason);
				//	Logger.WriteError(message + ". " + failureReason);
				//}
			}
			catch (Exception ex) {
				Logger.Write("Run command failed.  Please see error log for details.");
				Logger.WriteError(ex);

				isSuccessful = false;
			}

			return isSuccessful;
		}

	}
}
