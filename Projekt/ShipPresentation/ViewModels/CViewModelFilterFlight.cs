using FlightLogic.Entities;
using FlightLogic.Utils;
using SeaSkyPresentation.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaSkyPresentation.ViewModels
{
    internal class CViewModelFilterFlight : CViewModelBase
    {
        private CViewModelMainFlight _viewModelMain;
        private CServiceSearchFlight _serviceSearch;

        private ObservableCollection<string> _airlines;
        private ObservableCollection<string> _dates;
        private ObservableCollection<string> _time;
        private ObservableCollection<string> _destinations;
        private ObservableCollection<string> _destinationcodes;
        private ObservableCollection<int> _durations;
        private ObservableCollection<int> _seats;
        private ObservableCollection<string> _ids;
        private ObservableCollection<string> _starts;
        private ObservableCollection<string> _startcodes;
        private ObservableCollection<string> _ends;
        private ObservableCollection<string> _endcodes;

        private Flight _searchCriteria;

        public ObservableCollection<Flight> Flights { get => _viewModelMain.FoundFlights; }

        internal CViewModelFilterFlight(CServiceSearchFlight serviceSearch, CViewModelMainFlight viewModelMain)
        {
            FLog.FD("CViewModelFoundFlight.Ctor()", "");
            _viewModelMain = viewModelMain;
            _serviceSearch = serviceSearch;
            _airlines = new ObservableCollection<string>();
            _dates = new ObservableCollection<string>();
            _time = new ObservableCollection<string>();
            _destinations = new ObservableCollection<string>();
            _destinationcodes = new ObservableCollection<string>();
            _durations = new ObservableCollection<int>();
            _seats = new ObservableCollection<int>();
            _ids = new ObservableCollection<string>();
            _starts = new ObservableCollection<string>();
            _startcodes = new ObservableCollection<string>();
            _ends = new ObservableCollection<string>();
            _endcodes = new ObservableCollection<string>();


            _searchCriteria = new Flight();
        }

        public string SelectedAirline
        {
            get => _searchCriteria.Airline;
            set
            {
                if(value == "")
                {
                    value = null;
                }
                _searchCriteria.Airline = value;
                OnPropertyChanged();
                FLog.FD("CViewModelFilterFlight.", "SelectedAirline");
                _viewModelMain.FoundFlights = _serviceSearch.GetFlights(_searchCriteria);
            }
        }

        public string SelectedStart
        {
            get => _searchCriteria.Start;
            set
            {
                if (value == "")
                {
                    value = null;
                }
                _searchCriteria.Start = value;
                OnPropertyChanged();
                FLog.FD("CViewModelFilterFlight.", "SelectedAirline");
                _viewModelMain.FoundFlights = _serviceSearch.GetFlights(_searchCriteria);
            }
        }

        public string SelectedDestination
        {
            get => _searchCriteria.Destination;
            set
            {
                if (value == "")
                {
                    value = null;
                }
                _searchCriteria.Destination = value;
                OnPropertyChanged();
                FLog.FD("CViewModelFilterFlight.", "SelectedAirline");
                _viewModelMain.FoundFlights = _serviceSearch.GetFlights(_searchCriteria);
            }
        }

        public string SelectedTimeS
        {
            get => _searchCriteria.TimeS;
            set
            {
                if (value == "")
                {
                    value = null;
                }
                _searchCriteria.TimeS = value;
                OnPropertyChanged();
                FLog.FD("CViewModelFilterFlight.", "SelectedAirline");
                _viewModelMain.FoundFlights = _serviceSearch.GetFlights(_searchCriteria);
            }
        }

        public string SelectedDateS
        {
            get => _searchCriteria.DateS;
            set
            {
                if (value == "")
                {
                    value = null;
                }
                _searchCriteria.DateS = value;
                OnPropertyChanged();
                FLog.FD("CViewModelFilterFlight.", "SelectedAirline");
                _viewModelMain.FoundFlights = _serviceSearch.GetFlights(_searchCriteria);
            }
        }

        public int SelectedSeats
        {
            get => _searchCriteria.Seats;
            set
            {
                _searchCriteria.Seats = value;
                OnPropertyChanged();
                FLog.FD("CViewModelFilterFlight.", "SelectedAirline");
                _viewModelMain.FoundFlights = _serviceSearch.GetFlights(_searchCriteria);
            }
        }

        public int SelectedDuration
        {
            get => _searchCriteria.Duration;
            set
            {
                _searchCriteria.Duration = value;
                OnPropertyChanged();
                FLog.FD("CViewModelFilterFlight.", "SelectedAirline");
                _viewModelMain.FoundFlights = _serviceSearch.GetFlights(_searchCriteria);
            }
        }

        internal void Clear()
        {
            _searchCriteria = new Flight();
        }

        internal void Send()
        {
            _viewModelMain.FoundFlights = _serviceSearch.GetFlights(_searchCriteria);
        }

    }
}
