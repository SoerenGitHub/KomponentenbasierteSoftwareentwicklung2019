

using SeaSkyPresentation.ViewModels;
using FlightLogic;
using FlightLogic.Utils;

using System.Windows;
using System.Windows.Markup;
using System.Threading;
using System.Globalization;
using SeaSkyPresentation.Services;
using FlightLogic.Entities;
using System.Windows.Media;
using System.Windows.Controls;
using System;
using System.Text.RegularExpressions;
using SeaSkyPresentation.Views.Pages;

namespace SeaSkyPresentation.Views
{
    /// <summary>
    /// Interaktionslogik für CViewMain.xaml
    /// </summary>
    partial class CViewMain : Window, FIDialog
    {

        private readonly CViewModelMainFlight _viewModelMainFlight;
        private readonly CViewModelMainShipInfo _viewModelMainShipInfo;
        private CViewModelBase _selectedModelMain;
        private CViewFilterFlight _viewFilterFlight;
        private CViewFilterShipInfo _viewFilterShipInfo;
        private CViewAdministrationFlight _viewAdministrationFlight;
        private CViewAdministrationShipInfo _cViewAdministrationShipInfo;
        private CViewUpdateFlight _cViewUpdateFlight;
        private CViewUpdateShipInfo _cViewUpdateShipInfo;

        internal CViewMain(CViewModelMainFlight viewModelMainFlight, CViewModelMainShipInfo viewModelMainShipInfo, 
            CViewFilterFlight viewFoundFlight, CViewAdministrationFlight viewAdministrationFlight, 
            CViewAdministrationShipInfo cViewAdministrationShipInfo, CViewFilterShipInfo viewFilterShipInfo, CViewUpdateFlight cViewUpdateFlight, CViewUpdateShipInfo cViewUpdateShipInfo)
        {
            FLog.FD("CViewMain.Ctor()", "");
            InitializeComponent();
            // Change cultureInfo in all XAML View, e.z. to de-DE
            this.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentUICulture.Name);
            var language = CultureInfo.CurrentUICulture.Name;
            FrameworkElement.LanguageProperty.OverrideMetadata(
               typeof(FrameworkElement),
               new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(language)));

            _viewModelMainFlight = viewModelMainFlight;
            _viewModelMainShipInfo = viewModelMainShipInfo;
            _viewFilterShipInfo = viewFilterShipInfo;
            _viewFilterFlight = viewFoundFlight;
            _viewAdministrationFlight = viewAdministrationFlight;
            _cViewAdministrationShipInfo = cViewAdministrationShipInfo;
            _cViewUpdateFlight = cViewUpdateFlight;
            _cViewUpdateShipInfo = cViewUpdateShipInfo;
            DataContext = viewModelMainFlight;
            _selectedModelMain = viewModelMainFlight;
            //Load Page
            pageloader.Content = _viewFilterFlight;
            //Menue itemcolor
            airplane_btn.Foreground = Brushes.DarkSlateBlue;
        }

        private void Administration_btn_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedModelMain == _viewModelMainFlight)
            {
                if (administration_btn.Foreground == Brushes.Red)
                {
                    administration_btn.Foreground = Brushes.Black;
                    //Load Page
                    pageloader.Content = _viewFilterFlight;
                }
                else
                {
                    //Load Page
                    administration_btn.Foreground = Brushes.Red;
                    pageloader.Content = _viewAdministrationFlight;
                }
            }
            else
            {
                if (administration_btn.Foreground == Brushes.Red)
                {
                    administration_btn.Foreground = Brushes.Black;
                    //Load Page
                    pageloader.Content = _viewFilterShipInfo;
                }
                else
                {
                    //Load Page
                    administration_btn.Foreground = Brushes.Red;
                    pageloader.Content = _cViewAdministrationShipInfo;
                }
            }
        }

        private void Ship_btn_Click(object sender, RoutedEventArgs e)
        {
            _selectedModelMain = _viewModelMainShipInfo;
            Button administration = FindName("administration_btn") as Button;
            if (administration.Foreground == Brushes.Red)
            {
                pageloader.Content = _cViewAdministrationShipInfo;
            }
            else
            {
                pageloader.Content = _viewFilterShipInfo;
            }

            ship_btn.Foreground = Brushes.DarkSlateBlue;
            airplane_btn.Foreground = Brushes.Black;
        }

        private void Airplane_btn_Click(object sender, RoutedEventArgs e)
        {
            _selectedModelMain = _viewModelMainFlight;
            Button administration = FindName("administration_btn") as Button;
            if (administration.Foreground == Brushes.Red)
            {
                pageloader.Content = _viewAdministrationFlight;
            }
            else
            {
                pageloader.Content = _viewFilterFlight;
            }
            airplane_btn.Foreground = Brushes.DarkSlateBlue;
            ship_btn.Foreground = Brushes.Black;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _cViewUpdateFlight.Close();
            _cViewUpdateShipInfo.Close();
        }
    }
}
