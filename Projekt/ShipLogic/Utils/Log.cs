using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipLogic.Utils
{
    public class Log
    {
        public static void D(string tag, object value)
        {
#if DEBUG
            Console.WriteLine($"{tag} {value}");
#endif
        }
    }
}
