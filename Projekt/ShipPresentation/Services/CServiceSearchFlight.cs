using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightLogic.Entities;
using FlightLogic;


namespace SeaSkyPresentation.Services
{
    internal class CServiceSearchFlight
    {

        /*********************GENERALS**************************/

        private FILogicQueries _logicQueries;

        internal CServiceSearchFlight(FILogicQueries logicQueries)
        {
            _logicQueries = logicQueries;
        }

        /*********************Flights**************************/

        public ObservableCollection<Flight> GetFlights(Flight flightcriteria)
        {
                ObservableCollection<Flight> flights =
               new ObservableCollection<Flight>(_logicQueries.ReadFlights(flightcriteria));
               return flights;
        }

        public ObservableCollection<Flight> GetFlights()
        {
            ObservableCollection<Flight> flights =
               new ObservableCollection<Flight>(_logicQueries.ReadFlights());
            return flights;
        }

        public Flight GetFlight(string id)
        {
            Flight flight = _logicQueries.ReadFlight(id);
            return flight;
        }

        public int CountFlights()
        {
            int nAirplanes = _logicQueries.countFlights();
            return nAirplanes;
        }

        public ObservableCollection<string> getAirlines(string start, string end)
        {
            ObservableCollection<string> airlines = new ObservableCollection<string>(_logicQueries.ReadAirlines(start, end));
            return airlines;
        }

        public ObservableCollection<String> getEnd(string airline, string start)
        {
            ObservableCollection<string> end = new ObservableCollection<string>(_logicQueries.ReadEndA(airline, start));
            return end;
        }

        public ObservableCollection<string> getEndCode(string airline, string startcode)
        {
            ObservableCollection<string> endcode = new ObservableCollection<string>(_logicQueries.ReadEndCodeA(airline, startcode));
            return endcode;
        }

        public ObservableCollection<String> getStart(string airline, string end)
        {
            ObservableCollection<string> start = new ObservableCollection<string>(_logicQueries.ReadStartA(airline, end));
            return start;
        }

        public ObservableCollection<string> getStartCode(string airline, string endcode)
        {
            ObservableCollection<string> startcode = new ObservableCollection<string>(_logicQueries.ReadStartCodeA(airline, endcode));
            return startcode;
        }



    }
}
