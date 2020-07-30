using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipLogic.Entities;

namespace ShipLogic
{
    public interface SIDataRead
    {
        ShipInfo ReadShipping(string id);
        List<ShipInfo> ReadShippings(ShipInfo shipping);
        List<ShipInfo> ReadShippings();
        ICollection<string> ReadStartH(string company, string destination);
        ICollection<string> ReadEndH(string company, string start);
        ICollection<string> ReadCompany(string start, string destination);
        void InitDb();
        void CloseDb();
    }
}
