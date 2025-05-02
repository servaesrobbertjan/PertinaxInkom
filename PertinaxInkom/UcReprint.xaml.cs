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

            CloseRequested?.Invoke(this, EventArgs.Empty);
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
