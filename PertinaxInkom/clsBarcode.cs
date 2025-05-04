using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PertinaxInkom
{
    class clsBarcode
    {
        public static bool ValidateBarcode(string barcode)
        {
            BigInteger toValidate = BigInteger.Parse(barcode.Substring(0, barcode.Length - 2));
            BigInteger checkDigit = BigInteger.Parse(barcode.Substring(barcode.Length - 2));

            if ((98 - (toValidate % 97)) == checkDigit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetBarcodeType(string barcode)
        {
            int type = Convert.ToInt32(barcode.Substring(5, 1));
            string typestring = "";

            switch (type)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    typestring = "Ticket";
                    break;
                case 5:
                    typestring = "Participant";
                    break;
                case 6:
                    typestring = "Visitor";
                    break;
                case 7:
                    typestring = "Crew";
                    break;
                case 8:
                    typestring = "Volunteer";
                    break;
                default:
                    typestring = "invalid wristband";
                    break;
            }
            return typestring;
        }

        public static string GetBarcodeTypeString(string selectedContent)
        {
            Random randomtype = new Random();
            string barcodetypestr = "";

            switch (selectedContent)
            {
                case "Ticket":
                    barcodetypestr = randomtype.Next(1, 4).ToString();
                    break;
                case "Visitor":
                    barcodetypestr = "6";
                    break;
                case "Participant":
                    barcodetypestr = "5";
                    break;
                case "Volunteer":
                    barcodetypestr = "8";
                    break;
                case "Crew":
                    barcodetypestr = "7";
                    break;
                default:
                    barcodetypestr = "Invalid selection";
                    break;
            }

            return barcodetypestr;
        }

        public static string CreateBarcode()
        {
            string barcode = "";
            clsUserDB userdb = new clsUserDB();
            clsBlockedUuidsDB blockeddb = new clsBlockedUuidsDB();

            do
            {
                var bytes = new byte[11]; // ≈ 88 bits, up to 26 decimal digits
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(bytes);
                }

                BigInteger result = new BigInteger(bytes);
                result = BigInteger.Abs(result); // Ensure positive
                string numberStr = result.ToString();

                // Keep only 20 digits and prepend '1' to make 21 digits starting with 1
                if (numberStr.Length >= 20)
                {
                    numberStr = "1" + numberStr.Substring(0, 20);
                }
                else
                {
                    numberStr = "1" + numberStr.PadLeft(20, '0');
                }

                BigInteger partialbarcode = BigInteger.Parse(numberStr);

                // Calculate control digits
                int mod97 = (int)(partialbarcode % 97);
                string controldigits = (98 - mod97).ToString("D2");

                barcode = partialbarcode.ToString() + controldigits;

            } while ((userdb.GetUserByUuid(barcode) != null) || (blockeddb.GetBlockedUidByUuid(barcode) != null));

            return barcode;
        }
    }
}
