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

namespace PertinaxInkom
{
    /// <summary>
    /// Interaction logic for UcSettings.xaml
    /// </summary>
    public partial class UcSettings : UserControl
    {
        public event EventHandler CloseRequested;
        public UcSettings()
        {
            InitializeComponent();
            txtIpParticipant.Text = ZebraConfig.Default.IpParticipantPrinter.ToString();
            txtIpCrew.Text = ZebraConfig.Default.IpCrewVolunteerPrinter.ToString();
            txtIpVisitor.Text = ZebraConfig.Default.IpVisitorPrinter.ToString();
            txtedition.Text = ZebraConfig.Default.edition.ToString();
            txtwristbandcode.Text = ZebraConfig.Default.zplcode.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtIpParticipant.Text != ZebraConfig.Default.IpParticipantPrinter.ToString())
            {
                ZebraConfig.Default.IpParticipantPrinter = txtIpParticipant.Text;
                ZebraConfig.Default.Save();
            }
            if (txtIpVisitor.Text != ZebraConfig.Default.IpVisitorPrinter.ToString())
            {
                ZebraConfig.Default.IpVisitorPrinter = txtIpVisitor.Text;
                ZebraConfig.Default.Save();
            }
            if (txtIpCrew.Text != ZebraConfig.Default.IpCrewVolunteerPrinter.ToString())
            {
                ZebraConfig.Default.IpCrewVolunteerPrinter = txtIpCrew.Text;
                ZebraConfig.Default.Save();
            }
            if (txtedition.Text != ZebraConfig.Default.edition.ToString())
            {
                ZebraConfig.Default.edition = Convert.ToInt32(txtedition.Text);
                ZebraConfig.Default.Save();
            }
            if (txtwristbandcode.Text != ZebraConfig.Default.zplcode.ToString())
            {
                ZebraConfig.Default.zplcode = txtwristbandcode.Text;
                ZebraConfig.Default.Save();
            }
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
