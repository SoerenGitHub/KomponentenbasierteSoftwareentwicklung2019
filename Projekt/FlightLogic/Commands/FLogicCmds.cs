using FlightLogic.Utils;
using FlightLogic.Entities;

namespace FlightLogic.Command
{
    internal class FLogicCmds : FILogicCmds
    {
        private FIDataWrite _fdatawrite;

        internal FLogicCmds(FIDataWrite fidatawrite)
        {
            FLog.FD("FLogicCmds.Ctor", "");
            _fdatawrite = fidatawrite;
        }

        public int CreateFlight(Flight flight)
        {
            FLog.FD("FLogicCmds.CreateFlight()", flight.Id);
            return _fdatawrite.CreateFlight(flight);
        }

        public int UpdateFlight(Flight flight)
        {
            FLog.FD("FLogicCmds.UpdateFlight()", flight.Id);
            return _fdatawrite.UpdateFlight(flight);
        }
        
        public int DeleteFlight(Flight flight)
        {
            FLog.FD("FLogicCmds.DeleteFlight", flight.Id);
            return _fdatawrite.DeleteFlight(flight.Id);
        }
    }
}
