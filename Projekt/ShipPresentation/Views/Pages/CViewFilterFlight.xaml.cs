using FlightLogic.Utils;
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
    /// Interaktionslogik für CViewFoundFlight.xaml
    /// </summary>
    partial class CViewFilterFlight : Page
    {
        private CViewModelFilterFlight _viewModelSearchResult;
        internal CViewFilterFlight(CViewModelFilterFlight viewModelFoundFlight)
        {
            _viewModelSearchResult = viewModelFoundFlight;
            DataContext = viewModelFoundFlight;
            InitializeComponent();
        }

        private void Minseats_tf_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //nur natürliche Zahlen
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Duration_tf_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //nur natürliche Zahlen
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DataGridFlights_Loaded(object sender, RoutedEventArgs e)
        {
            dataGridFlights.ItemsSource = _viewModelSearchResult.Flights;
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                dataGridFlights.ItemsSource = _viewModelSearchResult.Flights;
            }
        }

        private void Startdate_dp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //If-Statement, weil diese Funktionen ebenfalls beim Initialisieren aufgerufen wird
            //-> sonst NullpointerException
            if(dataGridFlights != null && _viewModelSearchResult != null) {
                dataGridFlights.ItemsSource = _viewModelSearchResult.Flights;
            }
        }
    }
}
