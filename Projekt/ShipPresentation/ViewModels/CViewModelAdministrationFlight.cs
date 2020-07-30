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
    internal class CViewModelAdministrationFlight : CViewModelBase
    {
        private readonly CServiceOfferFlight _serviceOffer;
        private Flight _flight;
        private CViewModelMainFlight _viewModelMain;
        private CServiceSearchFlight _serviceSearch;
        private readonly CViewModelUpdateFlight _viewModelUpdateFlight;
        public ObservableCollection<Flight> Flights { get => _viewModelMain.FoundFlights; }

        internal CViewModelAdministrationFlight(CServiceOfferFlight serviceOffer, CViewModelMainFlight viewModelMain, CServiceSearchFlight serviceSearch, CViewModelUpdateFlight viewModelUpdateFlight)
        {
            FLog.FD("CViewModelCreateFlight.Ctor()", "");
            _viewModelMain = viewModelMain;
            _serviceOffer = serviceOffer;
            _serviceSearch = serviceSearch;
            _viewModelUpdateFlight = viewModelUpdateFlight;
            _flight = new Flight();

        }

        public Flight Flight
        {
            set
            {
                _flight = value;
                FLog.FD("CViewModelCreateFlight.setFlight()", value);
            }
        }

        public string Airline
        {
            get => _flight.Airline;
            set
            {
                _flight.Airline = value;
                FLog.FD("CViewModelCreateFlight.setAirline()", value);
                this.OnPropertyChanged();
            }
        }
        public DateTime Date
        {
            get => _flight.Date;
            set
            {
                _flight.Date = value;
                FLog.FD("CViewModelCreateFlight.setDate()", value);
                this.OnPropertyChanged();
            }
        }

        public string DateS
        {
            get => _flight.DateS;
            set
            {
                if (value == "")
                {
                    value = null;
                }
                _flight.DateS = value;
                FLog.FD("CViewModelCreateFlight.setDateS()", value);
                this.OnPropertyChanged();
            }
        }
        public string Destination
        {
            get => _flight.Destination;
            set
            {
                _flight.Destination = value;
                FLog.FD("CViewModelCreateFlight.setDestination()", value);
                this.OnPropertyChanged();
            }
        }
        public string Destinationcode
        {
            get => _flight.DestinationCode;
            set
            {
                _flight.DestinationCode = value;
                FLog.FD("CViewModelCreateFlight.setDestinationCode()", value);
                this.OnPropertyChanged();
            }
        }
        public int Duration
        {
            get => _flight.Duration;
            set
            {
                _flight.Duration = value;
                FLog.FD("CViewModelCreateFlight.setDuration()", value);
                this.OnPropertyChanged();
            }
        }

        public int Seats
        {
            get => _flight.Seats;
            set
            {
                _flight.Seats = value;
                FLog.FD("CViewModelCreateFlight.setSeats()", value);
                this.OnPropertyChanged();
            }
        }
        public string Start
        {
            get => _flight.Start;
            set
            {
                _flight.Start = value;
                FLog.FD("CViewModelCreateFlight.setStart()", value);
                this.OnPropertyChanged();
            }
        }
        public string StartCode
        {
            get => _flight.StartCode;
            set
            {
                _flight.StartCode = value;
                FLog.FD("CViewModelCreateFlight.setStartcode()", value);
                this.OnPropertyChanged();
            }
        }
        public DateTime Time
        {
            get => _flight.Time;
            set
            {
                _flight.Time = value;
                FLog.FD("CViewModelCreateFlight.setTime()", value);
                this.OnPropertyChanged();
            }
        }

        public string TimeS
        {
            get => _flight.TimeS;
            set
            {
                _flight.TimeS = value;
                FLog.FD("CViewModelCreateFlight.setTimeS()", value);
                this.OnPropertyChanged();
            }
        }

        public void Clear()
        {
            _flight = new Flight();
        }

        public void create()
        {
            
            FLog.FD("CViewModelCreateFlight.create()", "DestCode: "+Destinationcode);
            //Nicht verwendet
            Destinationcode = "NONE";
            StartCode = "NONE";

            _serviceOffer.CreateFlight(_flight);
            this.Clear();
            _viewModelMain.FoundFlights = _serviceSearch.GetFlights();
        }

        public void delete(string id)
        {
            FLog.FD("CViewModelCreateFlight.delete()", "ID: " + id);
            Flight flight = _serviceSearch.GetFlight(id);
            if (flight != null)
            {
                _serviceOffer.DeleteFlight(flight);
                _viewModelMain.FoundFlights = _serviceSearch.GetFlights();
            }
        }

        public void update(string id)
        {
            FLog.FD("CViewModelCreateFlight.delete()", "ID: " + id);
            Flight flight = _serviceSearch.GetFlight(id);
            if (flight != null)
            {
                _viewModelMain.SelectedFlight = flight;
                _viewModelUpdateFlight.InitializeFlight();
            }
        }
    }
}
