using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace TestOMatic2012 {
	
	class Unzipper {

		public static void UnzipDatabaseZipFile(string filePath, string unzipDirectory) {


			using (ZipFile zippy = new ZipFile(filePath)) {
				zippy.ExtractAll(unzipDirectory);
			}
		}



	}
}
