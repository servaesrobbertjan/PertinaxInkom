using Swelio.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PertinaxInkom
{
    class clsEid
    {
        public static (string[] eIDData, string error) ReadEID()
        {
            string[] data = new string[10];
            string error = string.Empty;
            var engine = new Manager()
            {
                Active = true,
            };

            CardReader cardReader = engine.GetReader();

            if (cardReader == null)
            {
                error = "Unable to find to the card reader";
                return (data, error);
            }

            Card eID = engine.GetReader().GetCard(true);

            if (eID == null)
            {
                error = "Unable to read the eID";
                return (data, error);
            }

            Identity identity = eID.ReadIdentity();
            Address address = eID.ReadAddress();

            if ((identity != null) && (address != null))
            {
                data[0] = identity.FirstName1.ToString();
                data[1] = identity.Surname.ToString();
                data[2] = identity.BirthDate.ToString("dd/MM/yyyy");
                (string streetName, string houseNumber, string busNumber) = ParseAndGetAddress(address.Street.ToString());
                data[3] = streetName;
                data[4] = houseNumber;
                data[5] = busNumber;
                data[6] = address.Zip.ToString();
                data[7] = address.Municipality.ToString();
                data[8] = identity.NationalityISO.ToString();
                switch (identity.NationalityISO)
                {
                    case "BE":
                        data[9] = "Belgie";
                        break;
                    case "FR":
                        data[9] = "Frankrijk";
                        break;
                    case "DE":
                        data[9] = "Duitsland";
                        break;
                    case "GB":
                        data[9] = "Verenigd Koninkrijk";
                        break;
                    default:
                        data[9] = string.Empty;
                        break;
                }
            }
            else
            {
                error = "unable to read al of the data";
                return (data, error);
            }

            return (data, error);
        }

        private static (string streetName, string housNumber, string busNumber) ParseAndGetAddress(string address)
        {
            // Split het address via spaties
            string[] addressParts = address.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string streetName = string.Empty;
            string houseNumber = string.Empty;
            string busNumber = string.Empty;

            // als addressparts array 2 values heeft.
            if (addressParts.Length == 2)
            {
                streetName = addressParts[0];
                houseNumber = addressParts[1];
            }
            // Anders maak eenv lijst aan
            else if (addressParts.Length > 2)
            {
                List<string> streetParts = new List<string>();

                // En lopen door de delen en alles dat start met een letter is een straatname
                // Tot we een getal vinden, dit slaan we op als straatnaam en huisnummer
                // En vewijderen alles dat we al gevonden hebben
                for (int i = 0; i < addressParts.Length; i++)
                {
                    if (char.IsLetter(addressParts[i][0]) || addressParts[i][0] == '\'')
                    {
                        streetParts.Add(addressParts[i]);
                    }
                    else if (char.IsDigit(addressParts[i][0]))
                    {
                        houseNumber = addressParts[i];
                        addressParts = addressParts.Skip(i + 1).ToArray();
                        break;
                    }
                }

                streetName = string.Join(" ", streetParts);

                // Als er nog address delem overzijn dan is het laatste deel het busnummer
                if (addressParts.Length > 0)
                {
                    busNumber = addressParts.Last();
                }
            }

            return (streetName, houseNumber, busNumber);
        }
    }
}

