using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOMatic2012 {



	class AccessCardNumber {
		/*-------------------------------------------------------------------------------------------------------
		 * 
		 * The Challenge:	Create a six character code to allow GMs to reset their access card.  The code
		 *					needs to expire after 30 minutes.  It must not be easily discerned that is represents
		 *					a date/time value
		 * 
		 * The Solution:	
		 *	  To Encode:	1.  Get the current time in in univeral coordinated time (UCT)
		 *					2.  Add 30 minutes to the current time, to create an expiration time
		 *					3.  Get the number of minutes between January 1 and today's date
		 *					4.  Convert this value to a string
		 *					5.  Reverse the string
		 *					6.  Add 2 to each digit. Note that 8 becomes 0 and 9 becomes 1
		 *					7.  Map the digits to alpha characters. The digit to character mapping has been chosen at random
		 *						so that the sequence isn't obvious
		 *					8.  Return code
		 *					
		 *    To Decode:	1. Retrieve the code as a string
		 *					2. Map the alpha characters to digits
		 *					3. Subtract 2 from each digit.  Note that 0 becomes 8 and 1 becomes 9
		 *					4. Reverse the string
		 *					5. Convert the string to an integer represent total minutes since midnight January 1.
		 *					6. Add the total minutes to Janauary 1 at midnight
		 *					7. This date/time value can now be compared to current UTC on the back office.  If it
		 *					   is less than the current time then allow the GM to create a new card number	
		 * 
		 *   Comments:		I realize that this is not the most sophisticated solution, but it should be sufficient
		 *					for our purposes.
		 * 
		 * 
		 * ----------------------------------------------------------------------------------------------------*/
		public String CreateCode() {

			StringBuilder sb = new StringBuilder();

			//-- Get the current time in in univeral coordinated time (UCT)
			DateTime expirationTime = DateTime.UtcNow;

			//-- Add 30 minutes to the current time, to create an expiration time
			//-- TODO - Put minutes in app settings
			expirationTime = expirationTime.AddMinutes(30);

			//-- Get the number of minutes between January 1 and today's date
			DateTime beginningOfYear = new DateTime(expirationTime.Year, 1, 1);
			int totalMinutes = (int)(expirationTime - beginningOfYear).TotalMinutes;

			//-- Convert total minutes to a string
			string workingValue = totalMinutes.ToString("000000");

			//-- Reverse the string
			workingValue = new string(workingValue.Reverse().ToArray());

			//--Add 2 to each digit. Note that 8 becomes 0 and 9 becomes 1
			for (int i = 0; i < workingValue.Length; i++) {

				char digit = workingValue[i];

				if (digit == '8') {
					digit = '0';
				}
				else if (digit == '9') {
					digit = '1';
				}
				else {
					int charVal = (int)digit;
					charVal += 2;

					digit = (char)charVal;
				}

				//-- Map the digits to alpha characters
				sb.Append(MapDigitToAlphaChar(digit));
			}


			return sb.ToString(); ;
		}
		//---------------------------------------------------------------------------------------------------
		public DateTime Decode(string accessCode) {

			char[] digits = new char[accessCode.Length];

			//-- Map alpha chars to digits
			for (int i = 0; i < accessCode.Length; i++) {

				digits[i] = MapAlphaCharToDigit(accessCode[i]);
			}

			//-- Subtract 2 from each digit.  Note that 0 becomes 8 and 1 becomes 9
			for (int i = 0; i < digits.Length; i++) {

				char digit = digits[i];

				if (digit == '0') {
					digit = '8';
				}
				else if (digit == '1') {
					digit = '9';
				}
				else {
					int charVal = (int)digit;
					charVal -= 2;

					digit = (char)charVal;
				}

				digits[i] = digit;
			}

			//-- Reverse the array and convert to string
			string workingValue = new string(digits.Reverse().ToArray());

			//-- Convert the string to an integer represent total minutes since midnight January 1.
			int totalMinutes = Convert.ToInt32(workingValue);

			//-- Add the total minutes to Janauary 1 at midnightAdd the total minutes to Janauary 1 at midnight
			DateTime expirationDate = new DateTime(DateTime.UtcNow.Year, 1, 1);

			expirationDate = expirationDate.AddMinutes(totalMinutes);


			return expirationDate;
		}
		//---------------------------------------------------------------------------------------------------
		//-- Private Members
		//---------------------------------------------------------------------------------------------------
		private char MapAlphaCharToDigit(char alphaChar	) {

			char digit = ' ';

			switch (alphaChar) {

				case 'Y':
					digit = '0';
					break;

				case 'D':
					digit = '1';
					break;

				case 'U':
					digit = '2';
					break;

				case 'J':
					digit = '3';
					break;

				case 'S':
					digit = '4';
					break;

				case 'H':
					digit = '5';
					break;

				case 'Q':
					digit = '6';
					break;

				case 'F':
					digit = '7';
					break;

				case 'W':
					digit = '8';
					break;

				case 'B':
					digit = '9';
					break;
			}

			return digit;
		}//---------------------------------------------------------------------------------------------------
		private char MapDigitToAlphaChar(char digit) {

			char alphaChar = ' ';

			switch (digit) {

				case '0':
					alphaChar = 'Y';
					break;

				case '1':
					alphaChar = 'D';
					break;

				case '2':
					alphaChar = 'U';
					break;

				case '3':
					alphaChar = 'J';
					break;

				case '4':
					alphaChar = 'S';
					break;

				case '5':
					alphaChar = 'H';
					break;

				case '6':
					alphaChar = 'Q';
					break;

				case '7':
					alphaChar = 'F';
					break;

				case '8':
					alphaChar = 'W';
					break;

				case '9':
					alphaChar = 'B';
					break;


			}



			return alphaChar;
		}
	}
}
