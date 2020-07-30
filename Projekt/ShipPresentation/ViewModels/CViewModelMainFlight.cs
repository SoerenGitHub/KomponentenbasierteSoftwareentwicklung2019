using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SeaSkyPresentation.Services;
using FlightLogic.Entities;
using FlightLogic.Utils;

namespace SeaSkyPresentation.ViewModels
{
    internal class CViewModelMainFlight : CViewModelBase
    {
        private Flight _selectedFlight;
        private ObservableCollection<Flight> _foundFlights;

        public Flight SelectedFlight
        {
            get => _selectedFlight;
            set => _selectedFlight = value;
        }

        public ObservableCollection<Flight> FoundFlights
        {
            get => _foundFlights;
            set => _foundFlights = value;
        }

        internal CViewModelMainFlight(CServiceSearchFlight serviceSearchFlight)
        {
            FLog.FD("CViewModelMainFlight.Ctor()", "");
            FoundFlights = serviceSearchFlight.GetFlights();

        }
    }
}
