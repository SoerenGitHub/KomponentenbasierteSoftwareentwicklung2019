using ShipLogic.Entities;
using ShipLogic.Utils;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeaSkyPresentation.Views.Pages
{
    /// <summary>
    /// Interaktionslogik für CViewAdministration.xaml
    /// </summary>
    partial class CViewAdministrationShipInfo : Page
    {

        private CViewModelAdministrationShipInfo _cViewModelAdministrationShipInfo;
        private CViewUpdateShipInfo _viewUpdateShipInfo;
        internal CViewAdministrationShipInfo(CViewModelAdministrationShipInfo viewModelAdministrationShipInfo, CViewUpdateShipInfo viewUpdateShipInfo)
        {
            _cViewModelAdministrationShipInfo = viewModelAdministrationShipInfo;
            _viewUpdateShipInfo = viewUpdateShipInfo;
            DataContext = _cViewModelAdministrationShipInfo;
            InitializeComponent();
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            //deklarieren, definieren
            TextBox start = FindName("start_tf") as TextBox;
            TextBox end = FindName("end_tf") as TextBox;
            TextBox airline = FindName("company_tf") as TextBox;
            DatePicker startdate = FindName("startdate_dp") as DatePicker;
            TextBox starttime = FindName("starttime_tf") as TextBox;
            TextBox minseats = FindName("shiptype_tf") as TextBox;
            TextBox duration = FindName("duration_tf") as TextBox;

            //Prüfen auf Vollständigkeit
            if (!string.IsNullOrWhiteSpace(start.Text) &&
               !string.IsNullOrWhiteSpace(end.Text) &&
               !string.IsNullOrWhiteSpace(airline.Text) &&
               !string.IsNullOrWhiteSpace(starttime.Text) &&
               !string.IsNullOrWhiteSpace(minseats.Text) &&
               !string.IsNullOrWhiteSpace(duration.Text) &&
               _cViewModelAdministrationShipInfo.DateS != null)
            {
                //Check auf korrekter Zeitschreibweise
                try
                {
                    DateTime.ParseExact(starttime.Text, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                }
                catch
                {
                    MessageBox.Show("Die Startzeit ist nicht korrekt angegeben! (HH:mm)");
                    return;
                }

                _cViewModelAdministrationShipInfo.create();
                itemsControlShipInfo.ItemsSource = _cViewModelAdministrationShipInfo.ShipInfos;
                clearTextBoxes();
            }
            else
            {
                MessageBox.Show("Bitte alle Felder ausfüllen!");
                return;
            }
        }

        private void Duration_tf_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ItemsControlShipping_Loaded(object sender, RoutedEventArgs e)
        {
            itemsControlShipInfo.ItemsSource = _cViewModelAdministrationShipInfo.ShipInfos;
        }

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button)e.Source;
            string id = clicked.Tag.ToString();
            if (MessageBox.Show("Schiffsfahrt löschen?", "Bestätigung zum Löschen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _cViewModelAdministrationShipInfo.delete(id);
                itemsControlShipInfo.ItemsSource = _cViewModelAdministrationShipInfo.ShipInfos;
            }
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button)e.Source;
            string id = clicked.Tag.ToString();
            _cViewModelAdministrationShipInfo.update(id);
            trackEditView();
        }


        private void trackEditView()
        {
            while ((bool)_viewUpdateShipInfo.ShowDialog());
            itemsControlShipInfo.ItemsSource = _cViewModelAdministrationShipInfo.ShipInfos;
        }
        private void clearTextBoxes()
        {
            start_tf.Clear();
            end_tf.Clear();
            shiptype_tf.Clear();
            starttime_tf.Clear();
            company_tf.Clear();
            duration_tf.Text = "0";
            startdate_dp.SelectedDate = DateTime.Now.Date;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            startdate_dp.SelectedDate = DateTime.Now.Date;
        }
    }
}
