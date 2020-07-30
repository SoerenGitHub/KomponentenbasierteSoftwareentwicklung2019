using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipLogic.Entities;

namespace ShipLogic
{
    public interface ISLogicCommands
    {
        int AddShipping(ShipInfo shipping);
        int UpdateShipping(ShipInfo shipping);
        int DeleteShipping(string id);
    }
}
