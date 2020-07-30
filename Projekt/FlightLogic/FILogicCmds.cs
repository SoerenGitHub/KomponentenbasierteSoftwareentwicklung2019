using FlightLogic.Entities;

namespace FlightLogic
{
    public interface FILogicCmds
    {
        int CreateFlight(Flight flight);
        int UpdateFlight(Flight flight);
        int DeleteFlight(Flight flight);
    }
}
