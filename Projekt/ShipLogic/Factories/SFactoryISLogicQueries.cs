using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipLogic.Queries;

namespace ShipLogic.Factories
{
    public static class SFactoryISLogicQueries
    {
        public static ISLogicQueries CreateInstance (SIDataRead dbRead)
        {
            return new SLogicQueries(dbRead);
        }
    }
}
