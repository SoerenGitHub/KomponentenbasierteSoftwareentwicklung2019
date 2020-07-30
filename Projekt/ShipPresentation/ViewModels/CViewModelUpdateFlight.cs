using FlightLogic.Entities;
using FlightLogic.Utils;
using SeaSkyPresentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SeaSkyPresentation.ViewModels
{
    internal class CViewModelUpdateFlight : CViewModelBase
    {
        private readonly CServiceSearchFlight _serviceSearch;
        private readonly CServiceOfferFlight _serviceOffer;
        private readonly CViewModelMainFlight _viewModelMainFlight;
        private Flight _flight;


        internal CViewModelUpdateFlight(CServiceSearchFlight serviceSearch, CServiceOfferFlight serviceOffer, CViewModelMainFlight viewModelMainFlight )
        {
            FLog.FD("CViewModelUpdateFlight.Ctor()", "");
            _viewModelMainFlight = viewModelMainFlight;
            _serviceSearch = serviceSearch;
            _serviceOffer = serviceOffer;
            InitializeFlight();
        }

        public void InitializeFlight()
        {
            if (_viewModelMainFlight.SelectedFlight == null)
            {
                _flight = new Flight();
            }
            else
            {
                _flight = _viewModelMainFlight.SelectedFlight;
            }
        }

        public Flight Flight
        {
            set
            {
                _flight = value;
                OnPropertyChanged();
                FLog.FD("CViewModelUpdateFlight.setFlight()", value);
            }
        }
        public string Airline
        {
            get { return _flight.Airline; }
            set
            {
                _flight.Airline = value;
                OnPropertyChanged();
            }
        }
        public DateTime Date
        {
            get { return _flight.Date; }
            set
            {
                _flight.Date = value;
                OnPropertyChanged();
            }
        }

        public string DateS
        {
            get { return _flight.DateS; }
            set
            {
                _flight.DateS = value;
                OnPropertyChanged();
            }
        }
        public string Destination
        {
            get { return _flight.Destination; }
            set
            {
                _flight.Destination = value;
                OnPropertyChanged();
            }
        }
        public string DestinationCode
        {
            get { return _flight.DestinationCode; }
            set
            {
                _flight.DestinationCode = value;
                OnPropertyChanged();
            }
        }
        public int Duration
        {
            get { return _flight.Duration; }
            set
            {
                _flight.Duration = value;
                OnPropertyChanged();
            }
        }

        public int Seats
        {
            get { return _flight.Seats; }
            set
            {
                _flight.Seats = value;
                OnPropertyChanged();
            }
        }

        public string Start
        {
            get { return _flight.Start; }
            set
            {
                _flight.Start = value;
                OnPropertyChanged();
            }
        }

        public string StartCode
        {
            get { return _flight.StartCode; }
            set
            {
                _flight.StartCode = value;
                OnPropertyChanged();
            }
        }
        public DateTime Time
        {
            get { return _flight.Time; }
            set
            {
                _flight.Time = value;
                OnPropertyChanged();
            }
        }
        public string TimeS
        {
            get { return _flight.TimeS; }
            set
            {
                _flight.TimeS = value;
                OnPropertyChanged();
            }
        }

        public void Send()
        {
            _serviceOffer.UpdateFlight(_flight);
            _viewModelMainFlight.FoundFlights = _serviceSearch.GetFlights();
        }
    }
}
