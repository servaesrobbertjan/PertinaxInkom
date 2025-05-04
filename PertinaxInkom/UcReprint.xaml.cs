using PertinaxInkom.Properties;
using Swelio.Engine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
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

namespace PertinaxInkom
{
    /// <summary>
    /// Interaction logic for UcReprint.xaml
    /// </summary>
    public partial class UcReprint : UserControl
    {
        public event EventHandler CloseRequested;
        public UcReprint()
        {
            InitializeComponent();
        }

        private void btnReprint_Click(object sender, RoutedEventArgs e)
        {
            string Zebraprinter = "";
            string zebraReturn = "";

            // Validate UUID
            clsBarcode clsBarcode = new clsBarcode();
            clsUserDB userDB = new clsUserDB();
            clsUser user = userDB.GetUserByUuid(txbwristband.Text);

            if (!clsBarcode.ValidateBarcode(txbwristband.Text))
            {
                txterrors.Text = "Not a valid wristband.";
                return;
            }

            if (user == null)
            {
                txterrors.Text = "User not found.";
                return;
            }

            // Create a new UUID
            string newUuid = clsBarcode.CreateBarcode();

            // Update the user's UUID
            userDB.UpdateUser(
                user.Id,
                user.Address_Id,
                user.Wallet_Id,
                user.Nick_Name,
                user.Password,
                user.First_Name,
                user.Last_Name,
                user.Email,
                newUuid,
                user.Birth_Date
            );

            // Block the old UUID
            clsBlockedUuidsDB BlockedUuidsDB = new clsBlockedUuidsDB();
            int? result = BlockedUuidsDB.CreateBlockedUuid(txbwristband.Text);
            if (result == null)
            {
                txterrors.Text = "Something went wrong. Ask admin to add the wristband in the DB.";
                return;
            }

            // Get user role
            clsUserRoleDB userRoleDB = new clsUserRoleDB();
            ObservableCollection<clsUserRole> userRoles = userRoleDB.GetUserRolesByUser(user.Id);
            if (userRoles.Count == 0)
            {
                txterrors.Text = "User has no assigned role.";
                return;
            }

            clsRoleDB roleDB = new clsRoleDB();
            clsRole role = roleDB.GetRoleById(userRoles[0].RoleId);
            string zplcode = "";

            // Assign printer and generate ZPL
            if (role.RoleName == "Admin" || role.RoleName == "Crew")
            {
                Zebraprinter = ZebraConfig.Default.IpCrewVolunteerPrinter.ToString();
                zplcode = clsZebraPrinter.GenerateZPL(ZebraConfig.Default.edition.ToString(), user.Nick_Name, user.First_Name, user.Last_Name, newUuid);
            }
            else if (role.RoleName == "Participant")
            {
                Zebraprinter = ZebraConfig.Default.IpParticipantPrinter.ToString();
                zplcode = clsZebraPrinter.GenerateZPL(ZebraConfig.Default.edition.ToString(), user.Nick_Name, user.First_Name, user.Last_Name, newUuid);
            }
            else
            {
                Zebraprinter = ZebraConfig.Default.IpVisitorPrinter.ToString();
                zplcode = clsZebraPrinter.GenerateZPL(ZebraConfig.Default.edition.ToString(), "visitor", user.First_Name, user.Last_Name, newUuid);
            }

            // Send to printer
            zebraReturn = clsZebraPrinter.SendZPLToPrinter(zplcode, Zebraprinter);
            if (zebraReturn == "succes")
            {
                Thread.Sleep(5000);
                CloseRequested?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                txterrors.Text = zebraReturn;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnReprint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnReprint_Click(this, new RoutedEventArgs());
            }
        }
    }
}
