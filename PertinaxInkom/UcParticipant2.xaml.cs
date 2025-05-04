using PertinaxInkom.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PertinaxInkom
{
    /// <summary>
    /// Interaction logic for UcParticipant2.xaml
    /// </summary>
    public partial class UcParticipant2 : UserControl
    {
        private int userId;
        private DispatcherTimer eidTimer;
        public event EventHandler CloseRequested;

        public UcParticipant2(int userid)
        {
            InitializeComponent();
            userId = userid;

            eidTimer = new DispatcherTimer();
            eidTimer.Interval = TimeSpan.FromSeconds(1);
            eidTimer.Tick += EidTimer_Tick;
            eidTimer.Start();

            txbFirstnameEID.TextChanged += TxbFirstnameEID_TextChanged;
        }

        private void EidTimer_Tick(object? sender, EventArgs e)
        {

            var result = clsEid.ReadEID();
            txbFirstnameEID.Text = result.eIDData[0];
            txbLastNameEID.Text = result.eIDData[1];
            txbBirthDateEID.Text = result.eIDData[2];
            txbStreetNameEID.Text = result.eIDData[3];
            txbHouseNumberEID.Text = result.eIDData[4];
            if (result.eIDData[5] != string.Empty)
            {
                txbBusNumberEID.Text = result.eIDData[5];
            }
            else
            {
                txbBusNumberEID.Text = null;
            }
            txbZipcodeEID.Text = result.eIDData[6];
            txbCityEID.Text = result.eIDData[7];
            txbCountryEID.Text = result.eIDData[9];
            txtError.Text = result.error;

            //stop de eidTimer if if the eidreader filled in al the fields
            if (!string.IsNullOrEmpty(txtError.Text) && !string.IsNullOrEmpty(txbCountryEID.Text))
            {
                eidTimer.Stop();
            }
        }

        private void TxbFirstnameEID_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if txb fistname is manualy filled in stop the eidTimer
            if (txtError.Text == "Unable to read the eID" && !string.IsNullOrEmpty(txbFirstnameEID.Text))
            {
                eidTimer.Stop();
            }
            // if txbfirstname is emptyd start the eidTimer again
            if (string.IsNullOrEmpty(txbFirstnameEID.Text))
            {
                eidTimer.Start();
            }
        }

        private void btnSaveAndPrint_Click(object sender, RoutedEventArgs e)
        {
            // Collect user from the DB
            clsUserDB userDB = new clsUserDB();
            var user = userDB.GetUserById(userId);

            //Collect tickettype from the DB
            clsTicketTypeDB ticketTypeDB = new clsTicketTypeDB();
            string ticketname = "Participant";
            var ticketType = ticketTypeDB.GetTicketType(ticketname);

            //get the editionnumber from the settings
            int edition = ZebraConfig.Default.edition;
            string editionWristband = Convert.ToString(edition);

            // busnumber shit
            string? busnumber = null;
            if (txbBusNumberEID.Text.ToString() != string.Empty)
            {
                busnumber = txbBusNumberEID.Text.ToString();
            }

            //return the age of the user
            DateTime.TryParse(txbBirthDateEID.Text.ToString(), out DateTime birthDate);
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age))
            {
                age--;
            }

            // generate a new barcode for this edition
            string barcode = clsBarcode.CreateBarcode();

            //zerbraconfig
            string Zebraprinter = ZebraConfig.Default.IpParticipantPrinter.ToString();

            //extra variables if needed
            int? visitoridpertinaxlan = null;
            string zebraReturn = "";

            //if the user fisrname and lastname are the same for the ticket and eid
            if (user.First_Name == txbFirstnameEID.Text && user.Last_Name == txbLastNameEID.Text)
            {
                //check if address is in the db
                if (user.Address_Id != null)
                {
                    //check if the address is stil te same
                    clsAddressDB addressDB = new clsAddressDB();
                    var address = addressDB.getAddress(Convert.ToInt32(user.Address_Id));
                    bool thesame = true;
                    if (address.Street_Name != txbStreetNameEID.Text.ToString())
                    {
                        thesame = false;
                    }
                    if (address.House_Number != txbHouseNumberEID.Text.ToString())
                    {
                        thesame = false;
                    }
                    if (address.Bus_number != busnumber)
                    {
                        thesame = false;
                    }
                    if (address.Zip_Code != txbZipcodeEID.Text.ToString())
                    {
                        thesame = false;
                    }
                    if (address.City != txbCityEID.Text.ToString())
                    {
                        thesame = false;
                    }
                    if (address.Country != txbCountryEID.Text.ToString())
                    {
                        thesame = false;
                    }
                    //if not update
                    if (thesame == false)
                    {
                        addressDB.UpdateAddress(address.Id, txbStreetNameEID.Text.ToString(), txbHouseNumberEID.Text.ToString(), busnumber,
                             txbZipcodeEID.Text.ToString(), txbCityEID.Text.ToString(), txbCountryEID.Text.ToString());
                    }
                }
                // else save the address to the db
                else
                {
                    clsAddressDB addressDB = new clsAddressDB();
                    user.Address_Id = addressDB.CreateAddress(txbStreetNameEID.Text.ToString(), txbHouseNumberEID.Text.ToString(), busnumber,
                             txbZipcodeEID.Text.ToString(), txbCityEID.Text.ToString(), txbCountryEID.Text.ToString());
                }

                //now we update the user in the DB
                userDB.UpdateUser(user.Id, user.Address_Id, user.Wallet_Id, user.Nick_Name, user.Password,
                    user.First_Name, user.Last_Name, user.Email, barcode, user.Birth_Date);

                //send to wristband printer
                string zplcode = clsZebraPrinter.GenerateZPL(editionWristband, user.Nick_Name.ToString(), user.First_Name.ToString(), user.Last_Name.ToString(), barcode);
                zebraReturn = clsZebraPrinter.SendZPLToPrinter(zplcode, Zebraprinter);

                if (zebraReturn == "succes")
                {
                    Thread.Sleep(5000);
                    CloseRequested?.Invoke(this, EventArgs.Empty);
                }
                // if sending failed return error
                else
                {
                    txtError.Text = zebraReturn;
                }
            }
            else
            {
                txtError.Text = "this person isn't the designated user of this ticket";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
