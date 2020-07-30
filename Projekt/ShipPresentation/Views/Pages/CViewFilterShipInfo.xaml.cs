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
    /// Interaktionslogik für CViewFoundFlight.xaml
    /// </summary>
    partial class CViewFilterShipInfo : Page
    {
        private CViewModelFilterShipInfo _viewModelSearchResult;
        internal CViewFilterShipInfo(CViewModelFilterShipInfo viewModelFoundShipInfo)
        {
            _viewModelSearchResult = viewModelFoundShipInfo;
            DataContext = _viewModelSearchResult;
            InitializeComponent();
        }

       
        private void Duration_tf_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //nur natürliche Zahlen
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ItemsControlShipInfo_Loaded(object sender, RoutedEventArgs e)
        {
            itemsControlShipInfo.ItemsSource = _viewModelSearchResult.ShipInfos;
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                itemsControlShipInfo.ItemsSource = _viewModelSearchResult.ShipInfos;
            }
        }

        private void Startdate_dp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //If-Statement, weil diese Funktionen ebenfalls beim Initialisieren aufgerufen wird
            //-> sonst NullpointerException
            if(itemsControlShipInfo != null && _viewModelSearchResult != null) {
                itemsControlShipInfo.ItemsSource = _viewModelSearchResult.ShipInfos;
            }
        }

    }
}
