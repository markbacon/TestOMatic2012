using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TestOMatic2012 {

    class TextEncryption {
        //---------------------------------------------------------------------------------------------------
        public static string DecryptText(string encryptedText) {

            byte[] encryptedTextBytes = new byte[encryptedText.Length / 2];

            int pos = 0;
            string hexByte = "";

            for (int i = 0; i < encryptedTextBytes.Length; i++) {

                hexByte = encryptedText.Substring(pos, 2);
                pos += 2;

                encryptedTextBytes[i] = (byte)Int32.Parse(hexByte, System.Globalization.NumberStyles.HexNumber);
            }

            SymmetricAlgorithm sa = DES.Create();

            sa.Key = System.Text.ASCIIEncoding.UTF8.GetBytes("GetBytes");
            sa.IV = System.Text.ASCIIEncoding.UTF8.GetBytes("Algorith");

            MemoryStream msDecrypt = new MemoryStream(encryptedTextBytes);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, sa.CreateDecryptor(), CryptoStreamMode.Read);
            byte[] decryptedTextBytes = new Byte[encryptedTextBytes.Length];
            csDecrypt.Read(decryptedTextBytes, 0, encryptedTextBytes.Length);
            csDecrypt.Close();
            msDecrypt.Close();

            string decryptedTextString = System.Text.UnicodeEncoding.Unicode.GetString(decryptedTextBytes);

            return decryptedTextString;
        }
        //---------------------------------------------------------------------------------------------------
        public static string EncryptText(string text) {


            // Binary representation of plain text string
            byte[] plaintextBytes = (new UnicodeEncoding()).GetBytes(text);

            //Next we need to create a memory stream to hold the result data and an encryption algorithm.
            // Encrypt using DES
            SymmetricAlgorithm sa = DES.Create();

            byte[] key = System.Text.ASCIIEncoding.UTF8.GetBytes("PiggMeat");

            sa.Key = System.Text.ASCIIEncoding.UTF8.GetBytes("GetBytes");
            sa.IV = System.Text.ASCIIEncoding.UTF8.GetBytes("Algorith");

            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, sa.CreateEncryptor(), CryptoStreamMode.Write);
            csEncrypt.Write(plaintextBytes, 0, plaintextBytes.Length);
            csEncrypt.Close();
            byte[] encryptedTextBytes = msEncrypt.ToArray();
            msEncrypt.Close();

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < encryptedTextBytes.Length; i++) {

                sb.Append(encryptedTextBytes[i].ToString("X2"));
            }

            return sb.ToString();

        }
    }
}
