using System.Collections.Generic;
using FlightLogic.Entities;
using FlightLogic.Utils;

namespace FlightLogic.Queries
{
    internal class FLogicQueries : FILogicQueries
    {
        private FIDataRead _fIDataRead;
        public int countFlights()
        {
            int flights = _fIDataRead.countFlights();
            FLog.FD("countFlights()", flights);
            return flights;
        }

        internal FLogicQueries(FIDataRead fIDataRead)
        {
            FLog.FD("FLogicQueries.Ctor", "");
            _fIDataRead = fIDataRead;
        }

        public Flight ReadFlight(string Id)
        {
            Flight flight = _fIDataRead.ReadFlight(Id);
            FLog.FD("FlogicQueries.GetFlight()", flight);
            return flight;
        }
        public List<Flight> ReadFlights(Flight flight)
        {
            List<Flight> flights = _fIDataRead.ReadFlights(flight);
            return flights;
        }
        public List<Flight> ReadFlights()
        {
            List<Flight> flights = _fIDataRead.ReadFlights();
            return flights;
        }
        public ICollection<string> ReadAirlines(string start, string end)
        {
            ICollection<string> airline = _fIDataRead.ReadAirlines(start, end);
            FLog.FD("FLogicQueries.GetAirlines()", airline);
            return airline;
        }
        public ICollection<string> ReadStartA(string airline, string end)
        {
            ICollection<string> start = _fIDataRead.ReadStartA(airline, end);
            FLog.FD("FLogicQueries.GetStart()", start);
            return start;
        }
        public ICollection<string> ReadStartCodeA(string airline, string endcode)
        {
            ICollection<string> startCode = _fIDataRead.ReadStartCodeA(airline, endcode);
            FLog.FD("FLogicQueries.GetStartCode()", startCode);
            return startCode;
        }
        public ICollection<string> ReadEndA(string airline, string start)
        {
            ICollection<string> destination = _fIDataRead.ReadEndA(airline, start);
            FLog.FD("FLogicQueries.GetDestination()", destination);
            return destination;
        }
        public ICollection<string> ReadEndCodeA(string airline, string startcode)
        {
            ICollection<string> destinationCode = _fIDataRead.ReadEndCodeA(airline, startcode);
            FLog.FD("FLogicQueries.GetDeastinationCode()", destinationCode);
            return destinationCode;
        }
    }
}
