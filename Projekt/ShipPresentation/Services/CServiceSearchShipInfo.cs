using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipLogic.Entities;
using ShipLogic;


namespace SeaSkyPresentation.Services
{
    internal class CServiceSearchShipInfo
    {

        /*********************GENERALS**************************/

        private ISLogicQueries _logicQueries;

        internal CServiceSearchShipInfo(ISLogicQueries logicQueries)
        {
            _logicQueries = logicQueries;
        }

        /*********************ShipInfos**************************/

        public ObservableCollection<ShipInfo> GetShipInfos(ShipInfo shipcriteria)
        {
                ObservableCollection<ShipInfo> shipinfos =
               new ObservableCollection<ShipInfo>(_logicQueries.ReadShippings(shipcriteria));
               return shipinfos;
        }

        public ObservableCollection<ShipInfo> GetShipInfos()
        {
            ObservableCollection<ShipInfo> shipinfos =
               new ObservableCollection<ShipInfo>(_logicQueries.ReadShippings());
            return shipinfos;
        }

        public ShipInfo GetShipInfo(string id)
        {
            ShipInfo shipinfo = _logicQueries.ReadShipping(id);
            return shipinfo;
        }

        public ObservableCollection<string> getCompanys(string start, string end)
        {
            ObservableCollection<string> companys = new ObservableCollection<string>(_logicQueries.ReadCompany(start, end));
            return companys;
        }

        public ObservableCollection<String> getEnd(string company, string start)
        {
            ObservableCollection<string> end = new ObservableCollection<string>(_logicQueries.ReadEndH(company, start));
            return end;
        }

       
        public ObservableCollection<String> getStart(string company, string end)
        {
            ObservableCollection<string> start = new ObservableCollection<string>(_logicQueries.ReadStartH(company, end));
            return start;
        }

    }
}
