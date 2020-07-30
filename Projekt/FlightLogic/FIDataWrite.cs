using FlightLogic.Entities;

namespace FlightLogic
{
    public interface FIDataWrite
    {
        void InitDb();
        void CloseDb();
        int CreateFlight(Flight flight);
        int UpdateFlight(Flight flight);
        int DeleteFlight(string Id);
    }
}
