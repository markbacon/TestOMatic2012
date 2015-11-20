using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.XPath;
using System.Text;
using System.Configuration;
using System.Collections;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;


namespace TestOMatic2012 {

    public class PCNCommon
    {

        public static string addLeadingzero(string strENum)
        {
            string strleadingzero = "";
            int slength = strENum.Length;
            while (slength < 9)
            {
                strleadingzero += "0";
                slength += 1;
            }
            return (strleadingzero + strENum);

        }
    }
    
}
