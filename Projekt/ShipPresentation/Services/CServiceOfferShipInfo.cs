using ShipLogic;
using ShipLogic.Entities;
using FlightLogic.Utils;
namespace SeaSkyPresentation.Services
{
    internal class CServiceOfferShipInfo
    {

        private ISLogicCommands _logicCommands;


        internal CServiceOfferShipInfo(ISLogicCommands logicCommands)
        {
            _logicCommands = logicCommands;
        }

        public void CreateShipInfo(ShipInfo shipinfo)
        {
            _logicCommands.AddShipping(shipinfo);
        }

        public void UpdateShipInfo(ShipInfo shipinfo)
        {
            _logicCommands.UpdateShipping(shipinfo);
        }
        public void DeleteShipInfo(ShipInfo shipinfo)
        {
            _logicCommands.DeleteShipping(shipinfo.ID);
        }
    }
}