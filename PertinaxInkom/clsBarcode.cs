using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
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

        public static string GenerateNewBarcode(string oldbarcode)
        {
            string edition = oldbarcode.Substring(0, 2);
            string rand1 = oldbarcode.Substring(2, 3);
            string barcodetype = oldbarcode.Substring(5, 1);
            string rand2 = oldbarcode.Substring(6, 6);
            string leeftijdgroep = oldbarcode.Substring(12, 1);
            string rand3 = oldbarcode.Substring(13, 8);
            string controldigits = oldbarcode.Substring(15, 2);
            string newbarcode = "";

            clsUserDB userDB = new clsUserDB();
            Random random = new Random();
            do
            {
                int newrand1 = 0;
                do
                {
                    newrand1 = random.Next(100, 1000);
                } while (newrand1 % 2 == 0);

                int newrand2 = 0;
                do
                {
                    newrand2 = random.Next(100000, 1000000);
                } while (newrand2 % 2 != 0);

                int newrand3 = 0;
                newrand3 = random.Next(10000000, 100000000);

                BigInteger partialbarcode = BigInteger.Parse(edition + newrand1.ToString() + barcodetype + newrand2.ToString() + leeftijdgroep + newrand3.ToString());
                controldigits = (98 - (partialbarcode % 97)).ToString();
                if (Convert.ToInt32(controldigits) <= 9)
                {
                    controldigits = "0" + controldigits;
                }
                if (Convert.ToInt32(controldigits) < 99)
                {
                    int stringlengt = controldigits.Length;
                    controldigits = controldigits.Substring(stringlengt - 2, 2);
                }

                newbarcode = partialbarcode.ToString() + controldigits;
            } while (userDB.GetUserByUuid(newbarcode) != null);
            

            return newbarcode;
        }

        public static string CreateBarcode(int edition, string ticketType, int age)
        {
            string agegroup = "";
            string barcodetype = "";
            string barcode = "";
            clsUserDB userdb = new clsUserDB();
            do
            {
                switch (true)
                {
                    case var _ when (age < 12):
                        agegroup = "1";
                        break;
                    case var _ when (age < 16):
                        agegroup = "5";
                        break;
                    default:
                        agegroup = "8";
                        break;
                }

                Random random = new Random();

                int newrand1 = 0;
                do
                {
                    newrand1 = random.Next(100, 1000);
                } while (newrand1 % 2 == 0);

                int newrand2 = 0;
                do
                {
                    newrand2 = random.Next(100000, 1000000);
                } while (newrand2 % 2 != 0);

                int newrand3 = 0;
                newrand3 = random.Next(10000000, 100000000);

                switch (ticketType)
                {
                    case "Ticket":
                        barcodetype = random.Next(1, 4).ToString();
                        break;
                    case "Visitor":
                        barcodetype = "6";
                        break;
                    case "Participant":
                        barcodetype = "5";
                        break;
                    case "Volunteer":
                        barcodetype = "8";
                        break;
                    case "Crew":
                        barcodetype = "7";
                        break;

                }


                BigInteger partialbarcode = BigInteger.Parse(edition + newrand1.ToString() + barcodetype + newrand2.ToString() + agegroup + newrand3.ToString());
                string controldigits = (98 - (partialbarcode % 97)).ToString();
                if (Convert.ToInt32(controldigits) <= 9)
                {
                    controldigits = "0" + controldigits;
                }
                if (Convert.ToInt32(controldigits) < 99)
                {
                    int stringlengt = controldigits.Length;
                    controldigits = controldigits.Substring(stringlengt - 2, 2);
                }

                        barcode = partialbarcode.ToString() + controldigits;

            } while (userdb.GetUserByUuid(barcode) != null);

            return barcode;
        }
    }
}
