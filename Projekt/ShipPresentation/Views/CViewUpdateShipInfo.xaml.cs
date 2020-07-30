using SeaSkyPresentation.Helper;
using SeaSkyPresentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Interop;

namespace SeaSkyPresentation.Views
{
    /// <summary>
    /// Interaktionslogik für CViewUpdateFlight.xaml
    /// </summary>
    partial class CViewUpdateShipInfo : Window
    {
        private CViewModelUpdateShipInfo _cViewModelUpdateFlight;
        internal CViewUpdateShipInfo(CViewModelUpdateShipInfo cViewModelUpdateFlight)
        {
            _cViewModelUpdateFlight = cViewModelUpdateFlight;
            DataContext = _cViewModelUpdateFlight;
            InitializeComponent();
            this.SourceInitialized += sourceInitialized;
        }


        private void Duration_tf_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Send_btn_Click(object sender, RoutedEventArgs e)
        {
            TextBox starttime = FindName("starttime_tf") as TextBox;
            //Check auf korrekter Zeitschreibweise
            if (!string.IsNullOrEmpty(starttime.Text))
            {
                try
                {
                    DateTime.ParseExact(starttime.Text, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                }
                catch
                {
                    MessageBox.Show("Die Startzeit ist nicht korrekt angegeben! (HH:mm)");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(start_tf.Text) ||
               !string.IsNullOrEmpty(end_tf.Text) ||
               !string.IsNullOrEmpty(company_tf.Text) ||
               !string.IsNullOrEmpty(starttime_tf.Text) ||
               !string.IsNullOrEmpty(shiptype_tf.Text) ||
               duration_tf.Text != "0" ||
               startdate_dp.SelectedDate != new DateTime(01,01,0001,00,00,00))
            {
                _cViewModelUpdateFlight.Send();
                Clear();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Bitte geben Sie eine Änderung ein!");
            }
            
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            this.Hide();
        }

        private void Clear()
        {
            start_tf.Clear();
            end_tf.Clear();
            company_tf.Clear();
            starttime_tf.Clear();
            shiptype_tf.Clear();
            duration_tf.Text = "0";
        }
        private void sourceInitialized(object sender, EventArgs e)
        {
            IntPtr _windowHandle = new WindowInteropHelper(this).Handle;
            WinHandler.DisableMinimizeAndMaximizeButton(_windowHandle);
            WinHandler.DisableCloseButton(_windowHandle);
        }
    }
}
