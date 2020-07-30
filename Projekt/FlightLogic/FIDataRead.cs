using System.Collections.Generic;
using FlightLogic.Entities;

namespace FlightLogic
{
    public interface FIDataRead
    {
        void                        InitDb();
        void                        CloseDb();
        int                         countFlights();
        Flight                      ReadFlight(string ID);
        List<Flight>                ReadFlights(Flight flight);
        List<Flight>                ReadFlights();
        ICollection<string>         ReadStartA(string airline, string end);
        ICollection<string>         ReadStartCodeA(string airline, string endcode);
        ICollection<string>         ReadEndA(string airline, string start);
        ICollection<string>         ReadEndCodeA(string airline, string startcode);
        ICollection<string>         ReadAirlines(string start, string end);
    }
}
