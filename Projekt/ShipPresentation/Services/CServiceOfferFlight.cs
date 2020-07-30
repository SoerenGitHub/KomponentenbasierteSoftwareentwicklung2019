using FlightLogic;
using FlightLogic.Entities;
using FlightLogic.Utils;
namespace SeaSkyPresentation.Services
{
    internal class CServiceOfferFlight
    {

        private FILogicCmds _logicCommands;


        internal CServiceOfferFlight(FILogicCmds logicCommands)
        {
            _logicCommands = logicCommands;
        }

        public void CreateFlight(Flight flight)
        {
            _logicCommands.CreateFlight(flight);
        }

        public void UpdateFlight(Flight flight)
        {
            _logicCommands.UpdateFlight(flight);
        }
        public void DeleteFlight(Flight flight)
        {
            _logicCommands.DeleteFlight(flight);
        }
    }
}