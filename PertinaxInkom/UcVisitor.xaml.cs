using PertinaxInkom.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Interaction logic for UcVisitor.xaml
    /// </summary>
    public partial class UcVisitor : UserControl
    {
        private DispatcherTimer eidTimer;
        public event EventHandler CloseRequested;

        public UcVisitor()
        {
            InitializeComponent();
            eidTimer = new DispatcherTimer();
            eidTimer.Interval = TimeSpan.FromSeconds(1);
            eidTimer.Tick += eidTimer_Tick;
            eidTimer.Start();

            txbFirstname.TextChanged += TxbFirstname_TextChanged;
        }

        private void eidTimer_Tick(object sender, EventArgs e)
        {
            var result = clsEid.ReadEID();
            txbFirstname.Text = result.eIDData[0];
            txbLastName.Text = result.eIDData[1];
            txbBirthDate.Text = result.eIDData[2];
            txbStreetName.Text = result.eIDData[3];
            txbHouseNumber.Text = result.eIDData[4];
            if (result.eIDData[5] != string.Empty)
            {
                txbBusNumber.Text = result.eIDData[5];
            }
            else
            {
                txbBusNumber.Text = null;
            }
            txbZipcode.Text = result.eIDData[6];
            txbCity.Text = result.eIDData[7];
            txbCountry.Text = result.eIDData[9];
            txtError.Text = result.error;

            //stop de eidTimer if if the eidreader filled in al the fields
            if (!string.IsNullOrEmpty(txtError.Text) && !string.IsNullOrEmpty(txbCountry.Text))
            {
                eidTimer.Stop();
            }
        }

  private void btnSaveAndPrint_Click(object sender, RoutedEventArgs e)
        {
            //get the tickettype info from the DB
            clsTicketTypeDB ticketTypeDB = new clsTicketTypeDB();
            string ticketname = "Visitor";
            var ticketType = ticketTypeDB.GetTicketType(ticketname);

            //get the editionnumber from the Settings
            int edition = ZebraConfig.Default.edition;
            string editionWristband = Convert.ToString(edition);

            //return the age of the visitor
            DateTime.TryParse(txbBirthDate.Text.ToString(), out DateTime birthDate);
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age))
            {
                age--;
            }
            //the reference for clsWalletDB
            clsWalletDB walletDB = new clsWalletDB();

            // generate a new barcode for this edition
            string barcode = clsBarcode.CreateBarcode();

            //check if the visitor already exists in the DB.pertinaxlan
            string nickname = txbFirstname.Text.ToString() + " " + txbLastName.Text.ToString();
            clsUserDB userDB = new clsUserDB();
            clsUser? visitor1 = userDB.GetUserByName(txbFirstname.Text.ToString(), txbLastName.Text.ToString());

            // busnumber shit
            string? busnumber = null;
            if (txbBusNumber.Text.ToString() != string.Empty)
            {
                busnumber = txbBusNumber.Text.ToString();
            }

            //zerbraconfig
            string Zebraprinter = ZebraConfig.Default.IpVisitorPrinter.ToString();

            //extra variables if needed
            int? visitoridpertinaxlan = null;
            string email = "";

            clsRoleDB roleDB = new clsRoleDB();
            clsRole roleVisitor = roleDB.GetRoleByName("visitor");           
            string password = BCrypt.Net.BCrypt.HashPassword(txbLastName.Text.ToString());
            string zebraReturn = "";

            //check if the visitor is here for the first time
            if (visitor1 == null)
            {
                //check if tha address alreasy exsists in the db
                //else save the address to the DB
                int? addressId = null;
                clsAddressDB addressDB = new clsAddressDB();
                addressId = addressDB.GetAddresByFullAddress(txbStreetName.Text.ToString(), txbHouseNumber.Text.ToString(), busnumber,
                            txbZipcode.Text.ToString(), txbCity.Text.ToString(), txbCountry.Text.ToString());
                if (addressId == null)
                {
                    addressId = addressDB.CreateAddress(txbStreetName.Text.ToString(), txbHouseNumber.Text.ToString(), busnumber,
                            txbZipcode.Text.ToString(), txbCity.Text.ToString(), txbCountry.Text.ToString());
                }

                // if user is older then 12 create a wallet
                int? wallet1 = null;
                if (age >= 12)
                {
                    wallet1 = walletDB.CreateWallet(ticketType.Price, Convert.ToInt32(txbZipcode.Text));
                }

                //Parse the birthdate
                DateTime birthdateParsed;
                DateTime.TryParseExact(txtBirthDate.Text, "dd-MM-yyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthdateParsed);

                visitoridpertinaxlan = userDB.CreateUser(addressId, wallet1, nickname, password, txbFirstname.Text.ToString(),
                txbLastName.Text.ToString(), email, barcode, birthdateParsed);

                // create userrole
                clsUserRoleDB userRoleDB = new clsUserRoleDB();
                int userRoleId = userRoleDB.CreateUserRole((int) visitoridpertinaxlan, roleVisitor.Id);

                //send to wristband printer
                string zplcode = clsZebraPrinter.GenerateZPL(editionWristband, "visitor", txbFirstname.Text.ToString(), txbLastName.Text.ToString(), barcode);
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
            // else its not his first time
            else
            {
                //check if address is in the db
                if (visitor1.Address_Id != null)
                {
                    //check if the address is stil te same if not update
                    clsAddressDB addressDB = new clsAddressDB();
                    var address = addressDB.getAddress(Convert.ToInt32(visitor1.Address_Id));
                    bool thesame = true;
                    if (address.Street_Name != txbStreetName.Text.ToString())
                    {
                        thesame = false;
                    }
                    if (address.House_Number != txbHouseNumber.Text.ToString())
                    {
                        thesame = false;
                    }
                    if (address.Bus_number != busnumber)
                    {
                        thesame = false;
                    }
                    if (address.Zip_Code != txbZipcode.Text.ToString())
                    {
                        thesame = false;
                    }
                    if (address.City != txbCity.Text.ToString()) ;
                    {
                        thesame = false;
                    }
                    if (address.Country != txbCountry.Text.ToString())
                    {
                        thesame = false;
                    }
                    //if not update
                    if (thesame == false)
                    {
                        addressDB.UpdateAddress(address.Id, txbStreetName.Text.ToString(), txbHouseNumber.Text.ToString(), busnumber,
                             txbZipcode.Text.ToString(), txbCity.Text.ToString(), txbCountry.Text.ToString());
                    }
                }
                // else save the address to the db
                else
                {
                    clsAddressDB addressDB = new clsAddressDB();
                    visitor1.Address_Id = addressDB.CreateAddress(txbStreetName.Text.ToString(), txbHouseNumber.Text.ToString(), busnumber,
                            txbZipcode.Text.ToString(), txbCity.Text.ToString(), txbCountry.Text.ToString());
                }
                // he is is older then 12 now opdate DB.pertinaxlan to add wallet
                if (age >= 12)
                {

                    visitor1.Wallet_Id = walletDB.CreateWallet(ticketType.Price, Convert.ToInt32(txbZipcode.Text));
                }
                //now we can opdate DB.pertinaxlan
                userDB.UpdateUser(visitor1.Id, visitor1.Address_Id, visitor1.Wallet_Id, visitor1.Nick_Name,
                    visitor1.Password, visitor1.First_Name, visitor1.Last_Name, visitor1.Email, barcode, visitor1.Birth_Date);

                //check if he was a particpant previously if so update to visitor
                clsRole roleParticipant = roleDB.GetRoleByName("Participant");
                clsUserRoleDB userRoleDB = new clsUserRoleDB();
                ObservableCollection<clsUserRole> userRoles = userRoleDB.GetUserRolesByUser(visitor1.Id);

                foreach (clsUserRole userRole in userRoles)
                {
                    if (userRole.RoleId == roleParticipant.Id)
                    {
                        userRoleDB.UpdateUserRole(userRole.Id, roleVisitor.Id);
                    }
                }

                //send to wristband printer
                string zplcode = clsZebraPrinter.GenerateZPL(editionWristband, "visitor", txbFirstname.Text.ToString(), txbLastName.Text.ToString(), barcode);
                zebraReturn = clsZebraPrinter.SendZPLToPrinter(zplcode, Zebraprinter);

                if (zebraReturn == "succes")
                {
                    Thread.Sleep(5000);
                    CloseRequested?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(zebraReturn);
                }
                // if sending failed return error
                else
                {
                    txtError.Text = zebraReturn;
                }

            }
        }

        private void TxbFirstname_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if txb fistname is manualy filled in stop the eidTimer
            if (txtError.Text == "Unable to read the eID" && !string.IsNullOrEmpty(txbFirstname.Text))
            {
                eidTimer.Stop();
            }
        }

        private void txbCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSaveAndPrint_Click(this, new RoutedEventArgs());
            }
        }

        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
