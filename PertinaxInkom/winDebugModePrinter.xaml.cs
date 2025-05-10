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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PertinaxInkom
{
    /// <summary>
    /// Interaction logic for winDebugModePrinter.xaml
    /// </summary>
    public partial class winDebugModePrinter : Window
    {
        private List<clsUser> Users;
        private int counter = 0;
        private DispatcherTimer eidTimer;
        public event EventHandler CloseRequested;

        public winDebugModePrinter(List<clsUser> users)
        {
            InitializeComponent();

            Users = users ?? new List<clsUser>();
            counter = 0;  // Start counter at 0
            eidTimer = new DispatcherTimer();
            eidTimer.Interval = TimeSpan.FromSeconds(5);
            eidTimer.Tick += EidTimer_Tick;
            eidTimer.Start();

            ShowUser(counter);  // Show first user immediately
        }

        private void EidTimer_Tick(object? sender, EventArgs e)
        {
            counter++;

            if (counter == Users.Count)
            {
                eidTimer.Stop();
                CloseRequested?.Invoke(this, EventArgs.Empty);
                this.Close();  // Close the window
            }
            else
            {
                ShowUser(counter);  // Show next user
            }
        }

        private void ShowUser(int index)
        {
            if (index >= 0 && index < Users.Count)
            {
                var user = Users[index];
                txtline1.Text = $"Printing {index + 1} of {Users.Count}";
                txtline2.Text = $"{user.First_Name} {user.Last_Name}";
                txtline3.Text = user.Uuid;
            }
        }
    }

}
