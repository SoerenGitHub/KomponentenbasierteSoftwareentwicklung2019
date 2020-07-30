using ShipLogic.Entities;
using ShipLogic.Utils;
using SeaSkyPresentation.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaSkyPresentation.ViewModels
{
    internal class CViewModelFilterShipInfo : CViewModelBase
    {
        private CViewModelMainShipInfo _viewModelMain;
        private CServiceSearchShipInfo _serviceSearch;

        private ObservableCollection<string> _company;
        private ObservableCollection<string> _dates;
        private ObservableCollection<string> _time;
        private ObservableCollection<string> _destinations;
        private ObservableCollection<string> _destinationcodes;
        private ObservableCollection<int> _durations;
        private ObservableCollection<string> _shiptype;
        private ObservableCollection<string> _ids;
        private ObservableCollection<string> _starts;
        private ObservableCollection<string> _startcodes;
        private ObservableCollection<string> _ends;
        private ObservableCollection<string> _endcodes;

        private ShipInfo _searchCriteria;

        public ObservableCollection<ShipInfo> ShipInfos { get => _viewModelMain.FoundShipInfos; }

        internal CViewModelFilterShipInfo(CServiceSearchShipInfo serviceSearch, CViewModelMainShipInfo viewModelMain)
        {
            Log.D("CViewModelFoundShipInfo.Ctor()", "");
            _viewModelMain = viewModelMain;
            _serviceSearch = serviceSearch;
            _company = new ObservableCollection<string>();
            _dates = new ObservableCollection<string>();
            _time = new ObservableCollection<string>();
            _destinations = new ObservableCollection<string>();
            _destinationcodes = new ObservableCollection<string>();
            _durations = new ObservableCollection<int>();
            _shiptype = new ObservableCollection<string>();
            _ids = new ObservableCollection<string>();
            _starts = new ObservableCollection<string>();
            _startcodes = new ObservableCollection<string>();
            _ends = new ObservableCollection<string>();
            _endcodes = new ObservableCollection<string>();


            _searchCriteria = new ShipInfo();
        }

        public string SelectedCompany
        {
            get => _searchCriteria.company;
            set
            {
                if(value == "")
                {
                    value = null;
                }
                _searchCriteria.company = value;
                OnPropertyChanged();
                Log.D("CViewModelFilterShipInfo.", "SelectedAirline");
                _viewModelMain.FoundShipInfos = _serviceSearch.GetShipInfos(_searchCriteria);
            }
        }

        public string SelectedStart
        {
            get => _searchCriteria.start;
            set
            {
                if (value == "")
                {
                    value = null;
                }
                _searchCriteria.start = value;
                OnPropertyChanged();
                Log.D("CViewModelFilterShipInfo.", "SelectedAirline");
                _viewModelMain.FoundShipInfos = _serviceSearch.GetShipInfos(_searchCriteria);
            }
        }

        public string SelectedDestination
        {
            get => _searchCriteria.destination;
            set
            {
                if (value == "")
                {
                    value = null;
                }
                _searchCriteria.destination = value;
                OnPropertyChanged();
                Log.D("CViewModelFilterShipInfo.", "SelectedAirline");
                _viewModelMain.FoundShipInfos = _serviceSearch.GetShipInfos(_searchCriteria);
            }
        }

        public string SelectedTimeS
        {
            get => _searchCriteria.depTime;
            set
            {
                if (value == "")
                {
                    value = null;
                }
                _searchCriteria.depTime = value;
                OnPropertyChanged();
                Log.D("CViewModelFilterShipInfo.", "SelectedAirline");
                _viewModelMain.FoundShipInfos = _serviceSearch.GetShipInfos(_searchCriteria);
            }
        }

        public string SelectedDateS
        {
            get => _searchCriteria.date;
            set
            {
                if (value == "")
                {
                    value = null;
                }
                _searchCriteria.date = value;
                OnPropertyChanged();
                Log.D("CViewModelFilterShipInfo.", "SelectedAirline");
                _viewModelMain.FoundShipInfos = _serviceSearch.GetShipInfos(_searchCriteria);
            }
        }

        public string SelectedShipType
        {
            get => _searchCriteria.shipType;
            set
            {
                if (value == "")
                {
                    value = null;
                }
                _searchCriteria.shipType = value;
                OnPropertyChanged();
                Log.D("CViewModelFilterShipInfo.", "SelectedAirline");
                _viewModelMain.FoundShipInfos = _serviceSearch.GetShipInfos(_searchCriteria);
            }
        }

        public int SelectedDuration
        {
            get => _searchCriteria.travelSpan;
            set
            {
                _searchCriteria.travelSpan = value;
                OnPropertyChanged();
                Log.D("CViewModelFilterShipInfo.", "SelectedAirline");
                _viewModelMain.FoundShipInfos = _serviceSearch.GetShipInfos(_searchCriteria);
            }
        }

        internal void Clear()
        {
            _searchCriteria = new ShipInfo();
        }

        internal void Send()
        {
            _viewModelMain.FoundShipInfos = _serviceSearch.GetShipInfos(_searchCriteria);
        }

    }
}
