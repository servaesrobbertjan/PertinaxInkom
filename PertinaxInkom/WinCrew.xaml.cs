using PertinaxInkom.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PertinaxInkom
{
    /// <summary>
    /// Interaction logic for WinCrew.xaml
    /// </summary>
    public partial class WinCrew : Window
    {
        public WinCrew()
        {
            InitializeComponent();
            Loaded += (s, e) => pwbPasword.Focus();
        }

        private async void btnPrintCrew_Click(object sender, RoutedEventArgs e)
        {
            //check if pwbPasword is correct
            if (pwbPasword.Password.ToString() == "Depart8500")
            {
                //collect al crew
                txterrors.Text = "Fetching Crew";
                await Task.Delay(1000);
                clsUserDB userDB = new clsUserDB();
                List<clsUser> Crew = userDB.GetCrew();

                // check if in debug mode
                bool debugMode = ZebraConfig.Default.DebugMode;
                if (debugMode)
                {
                    winDebugModePrinter winDebugModePrinter = new winDebugModePrinter(Crew);
                    winDebugModePrinter.Show();
                    this.Close();
                }
                else
                {
                    // assing the printer
                    string Zebraprinter = ZebraConfig.Default.IpCrewVolunteerPrinter.ToString();
                    int counter = 0;
                    //foreach crew
                    foreach (clsUser user in Crew)
                    {
                        counter++;

                        //generate a Uuid
                        string Uuid = "";  
                        Uuid = clsBarcode.CreateBarcode();

                        //if wallet is null generate a wallet update the DB
                        txterrors.Text = $"{counter} / {Crew.Count} : Updating wallet {user.Nick_Name}";
                        await Task.Delay(500);
                        clsWalletDB walletDB = new clsWalletDB();


                        if (user.Wallet_Id == null)
                        {
                            //if the address is not given
                            if (user.Address_Id == null)
                            {
                                clsAddressDB addressDB = new clsAddressDB();
                                clsAddress address = addressDB.getAddress((int)user.Address_Id);
                                user.Wallet_Id = walletDB.CreateWallet(Convert.ToDecimal(20.00), 0000);
                            }
                            else
                            {
                                clsAddressDB addressDB = new clsAddressDB();
                                clsAddress address = addressDB.getAddress((int)user.Address_Id);
                                user.Wallet_Id = walletDB.CreateWallet(Convert.ToDecimal(20.00), Convert.ToInt32(address.Zip_Code));
                            }
                        }
                        else
                        {
                            // update the ballanse
                            clsWallet wallet = walletDB.GetWallet(user.Id);
                            walletDB.UpdateWallet(wallet.Id, Convert.ToDecimal(20.00), wallet.Pincode);
                        }
                        // Block the old UUID
                        clsBlockedUuidsDB BlockedUuidsDB = new clsBlockedUuidsDB();
                        int? result = BlockedUuidsDB.CreateBlockedUuid(user.Uuid);

                        // update user in the DB
                        userDB.UpdateUser(user.Id, user.Address_Id, user.Wallet_Id, user.Nick_Name, user.Password, user.First_Name, user.Last_Name, user.Email, Uuid, user.Birth_Date);

                        //generate ZPL /and send to printer then wait 10 seconds
                        txterrors.Text = $"{counter} / {Crew.Count} : Printing {user.Nick_Name}";
                        string zplcode = clsZebraPrinter.GenerateZPL(ZebraConfig.Default.edition.ToString(), user.Nick_Name, user.First_Name, user.Last_Name, Uuid);
                        string webraReturn = clsZebraPrinter.SendZPLToPrinter(zplcode, Zebraprinter);
                        await Task.Delay(2000);
                    }

                    txterrors.Text = "Finished Printing";
                    await Task.Delay(2500);
                    this.Close();
                }
            }
            else
            {
                pwbPasword.Clear();
                txterrors.Text = "wrong password";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnPrintCrew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnPrintCrew_Click(this, new RoutedEventArgs());
            }
        }
    }
}
