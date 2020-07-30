using System;
using System.Collections.Generic;
using System.Text;

namespace FlightLogic.Utils
{
    public static class FLog
    {
        public static void FD(string tag, object value)
        {
#if DEBUG
            Console.WriteLine($"{tag} {value}");
#endif
        }
    }
}
