using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TestOMatic2012 {

	class HashOMatic {

        public static string ComputeHash(string plainText) {

            // Binary representation of plain text string
            byte[] plainTextBytes = (new UnicodeEncoding()).GetBytes(plainText);

            SHA256Managed shaMan = new SHA256Managed();
            byte[] hashedTextBytes = shaMan.ComputeHash(plainTextBytes);


            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hashedTextBytes.Length; i++) {

                sb.Append(hashedTextBytes[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
