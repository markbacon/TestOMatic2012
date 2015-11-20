using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestOMatic2012 {
	public partial class Form48 : Form {
		public Form48() {
			InitializeComponent();
		}

		private void button2_Click(object sender, EventArgs e) {

			button2.Enabled = false;


			button2.Enabled = true;

		}
		//---------------------------------------------------------------------------------------------------
		private void Unzipit(string LocalFolder) {
			bool exists;

			try {
				//If "unzippedzips" subdirectory doesn't exist, create it
				string subPath = LocalFolder + "unzippedzips";
				exists = System.IO.Directory.Exists(subPath);
				if (!exists) System.IO.Directory.CreateDirectory(subPath);

				//If "pollfiles" subdirectory doesn't exist, create it
				string pollPath = LocalFolder + "Pollfiles";
				exists = System.IO.Directory.Exists(pollPath);
				if (!exists) System.IO.Directory.CreateDirectory(pollPath);

				//If "Archive" subdirectory doesn't exist, create it
				string archivePath = LocalFolder + "Archive";
				exists = System.IO.Directory.Exists(archivePath);
				if (!exists) System.IO.Directory.CreateDirectory(archivePath);

				//Traverse the inbox directory to find files that must be unzipped
				// Put all zip files in root directory into array.
				string[] array1 = Directory.GetFiles(@LocalFolder, "*.ZIP"); // <-- Case-insensitive

				// unzip all zipfiles found after emptying the unzippedzips folder of all files
				foreach (string ZIPname in array1) {
					string[] filePaths = Directory.GetFiles(subPath);
					foreach (string filePath in filePaths)
						File.Delete(filePath);
					System.IO.Compression.ZipFile.ExtractToDirectory(ZIPname, subPath);
					//Now go unzip the unzipped zipfiles before unzipping next master zipfile
					Unzipzips(ZIPname, subPath, pollPath);

					//Now that all content has been unzipped, archive the master zipfile
					string Archivename = Path.Combine(archivePath, Path.GetFileName(ZIPname));
					File.Delete(Archivename);
					File.Move(ZIPname, Archivename);
				}
			}
			catch (Exception ex) {
				eventLog2.Source = "FTPFranchise";
				eventLog2.WriteEntry("Error unzipping from " + LocalFolder + " - " + ex.Message);
			}

		}

	}
}
