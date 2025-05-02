using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenUserControl(UserControl Uc)
        {
            if (MainRight.Children.Count > 0)
            {
                MainRight.Children.RemoveAt(0);
            }
            Grid.SetColumn(Uc, 0);
            Grid.SetRow(Uc, 0);
            MainRight.Children.Add(Uc);

            switch (Uc)
            {
                case UcVisitor _UcVisitor:
                    _UcVisitor.CloseRequested += CloseRequested;
                    break;
                case UcParticipant _UcParticipant:
                    _UcParticipant.CloseRequested += CloseRequested;
                    break;
                case UcReprint _UcReprint:
                    _UcReprint.CloseRequested += CloseRequested;
                    break;
                case UcSettings _UcSettings:
                    _UcSettings.CloseRequested += CloseRequested;
                    break;
                default:
                    break;
            }
        }

        public void SwitchToUcParticipant2(int userId)
        {
            // Remove _UcParticipant from grdMain (assuming it's already added)
            if (MainRight.Children.Count > 1)
            {
                MainRight.Children.RemoveAt(1);
            }

            // Create an instance of _UcParticipant2 and pass the userId
            var Uc = new UcParticipant2(userId);

            // Add _UcParticipant2 to grdMain
            Grid.SetColumn(Uc, 1);
            Grid.SetRow(Uc, 0);
            MainRight.Children.Add(Uc);
            Uc.CloseRequested += CloseRequested;
        }

        private void CloseRequested(object sender, EventArgs e)
        {
            if (sender is UserControl Uc)
            {
                MainRight.Children.Remove(Uc);

                //load the pertinax.ico image
                var image = new Image();
                var bitMap = new BitmapImage();
                bitMap.BeginInit();
                bitMap.UriSource = new Uri("pack://application:,,,/Images/pertinax.ico");
                bitMap.EndInit();

                image.Source = bitMap;
                image.Margin = new Thickness(20);
                image.Opacity = 0.3;
                image.Stretch = Stretch.Uniform;
                Grid.SetColumn(image, 1);
                Grid.SetRow(image, 0);
                MainRight.Children.Add(image);
            }
        }

        private void btnVisitors_Click(object sender, RoutedEventArgs e)
        {
            UcVisitor _UcVisitor = new UcVisitor();
            OpenUserControl(_UcVisitor);
        }

        private void btnParticipant_Click(object sender, RoutedEventArgs e)
        {
            UcParticipant _UcParticipant = new UcParticipant();
            OpenUserControl(_UcParticipant);
        }

        private void btnReprint_Click(object sender, RoutedEventArgs e)
        {
            UcReprint _UcReprint = new UcReprint();
            OpenUserControl(_UcReprint);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            UcSettings _UcSettings = new UcSettings();
            OpenUserControl (_UcSettings);
        }
    }
}