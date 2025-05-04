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

namespace PertinaxInkom
{
    /// <summary>
    /// Interaction logic for UcParticipant.xaml
    /// </summary>
    public partial class UcParticipant : UserControl
    {
        public event EventHandler CloseRequested;
        public event EventHandler ChangeRequested;
        public UcParticipant()
        {
            InitializeComponent();
            txbticketbarcode.KeyDown += txbticketbarcode_KeyDown;

            Loaded += (s, e) => txbticketbarcode.Focus();
        }

        private string ConvertQwertytoAzertyNumbers(string input)
        {
            return input
                .Replace('!', '1')
                .Replace('@', '2')
                .Replace('#', '3')
                .Replace('$', '4')
                .Replace('%', '5')
                .Replace('^', '6')
                .Replace('&', '7')
                .Replace('*', '8')
                .Replace('(', '9')
                .Replace(')', '0');
        }

        private void txbticketbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if Enter key is pressed (assuming the scanner sends an Enter key after the scan)
            if (e.Key == Key.Return)
            {
                // Automatically handle the scanned text (already in the TextBox)
                txbticketbarcode.Text = ConvertQwertytoAzertyNumbers(txbticketbarcode.Text);

                string scannedbarcode = txbticketbarcode.Text.ToString();
                if (scannedbarcode != string.Empty)
                {
                    //check if ticketbarcode exist in the DB
                    clsTicketDB ticketDB = new clsTicketDB();
                    var ticket = ticketDB.GetTicketByUuid(scannedbarcode);

                    //send info to next Uc
                    if (ticket != null)
                    {
                        txtError.Text = $"the ticket {scannedbarcode} is found for user: {ticket.User_Id} ther orderdate is {ticket.Order_Date}";
                        // go the next Uc
                        OpenUcParticipant2(ticket.User_Id);
                    }
                    //else return a error
                    else
                    {
                        txtError.Text = $"the ticket {scannedbarcode} is not found";

                    }
                }
            }
        }

        private void OpenUcParticipant2(int userId)
        {
            var mainwindow = Window.GetWindow(this) as MainWindow;
            mainwindow.SwitchToUcParticipant2(userId);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
