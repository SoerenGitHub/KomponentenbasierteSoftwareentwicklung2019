using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipLogic.Utils;
using ShipLogic.Entities;

namespace ShipLogic.Queries
{
    internal class SLogicQueries : ISLogicQueries
    {
        private SIDataRead _dbRead; //Initialisieren des Datenbank-Interfaces

        //LogicQuerie Konstruktor
        public SLogicQueries(SIDataRead dbRead)
        {
            Log.D("SLogicQueries.Ctor()", "");
            _dbRead = dbRead;
        }

        //Auslesen eines Shippings aus der DB mit gegebener ID
        public ShipInfo ReadShipping(string id)
        {
            ShipInfo shipping = _dbRead.ReadShipping(id);
            Log.D("SLogicQueries.ReadShipping()", shipping);
            return shipping;
        }

        //Auslesen mehrere Shippings mit Objektvorgabe
        public List<ShipInfo> ReadShippings(ShipInfo shipping)
        {
            List<ShipInfo> shippings = _dbRead.ReadShippings(shipping);
            Log.D("SLogicQueries.ReadShipping(ShipInfo)", shippings);

            return shippings;
        }

        //Auslesen mehrere Shippings
        public List<ShipInfo> ReadShippings()
        {
            List<ShipInfo> shippings = _dbRead.ReadShippings();
            Log.D("SLogicQueries.ReadShipping()", shippings);
            return shippings;
        }

        public ICollection<string> ReadStartH(string company, string destination)
        {
            ICollection<string> readStartH = _dbRead.ReadStartH(company, destination);
            Log.D("SLogicQueries.ReadStartH()", readStartH);
            return readStartH;
        }

        public ICollection<string> ReadEndH(string company, string start)
        {
            ICollection<string> readEndH = _dbRead.ReadEndH(company, start);
            Log.D("SLogicQueries.ReadStartH()", readEndH);
            return readEndH;
        }

        public ICollection<string> ReadCompany(string start, string destination)
        {
            ICollection<string> readCompany = _dbRead.ReadCompany(start, destination);
            Log.D("SLogicQueries.ReadStartH()", readCompany);
            return readCompany;
        }
    }
}
