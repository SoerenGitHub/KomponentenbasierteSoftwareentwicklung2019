using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipLogic.Utils;
using ShipLogic.Entities;

namespace ShipLogic.Commands
{
    internal class SLogicCommands : ISLogicCommands
    {
        private SIDataWrite _dbWrite;

        public SLogicCommands(SIDataWrite dbHandler) {
            Log.D("SLogicCommands.Ctor()","");
            _dbWrite = dbHandler;
        }

        public int AddShipping(ShipInfo shipping)
        {
            int addedS = _dbWrite.AddShipping(shipping);
            Log.D("SLogicCommands.AddShipping()", addedS);
            return addedS;
        }
        public int UpdateShipping(ShipInfo shipping)
        {
            int updatedS = _dbWrite.UpdateShipping(shipping);
            Log.D("SLogicCommands.AddShipping()", updatedS);
            return updatedS;
        }
        public int DeleteShipping(string id)
        {
            int deletedS = _dbWrite.DeleteShipping(id);
            Log.D("CLogicCommands.DeleteCar()", deletedS);
            return deletedS;
        }
    }
}
