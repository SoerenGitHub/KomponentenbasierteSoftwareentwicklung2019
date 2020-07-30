using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipLogic.Commands;

namespace ShipLogic.Factories
{
    public static class SFactoryISLogicCommands
    {
        public static ISLogicCommands CreateInstance (SIDataWrite dbWrite)
        {
            return new SLogicCommands(dbWrite);
        }
    }
}
