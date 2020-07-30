using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaSkyPresentation.TestEntinity
{
    class Ship
    {

        public Ship(int id, string startdatum, string startzeit, string startort, string zielort, int zeit)
        {
            Id = id;
            Startzeit = startzeit;
            Startdatum = startdatum;
            Startort = startort;
            Zielort = zielort;
            Zeit = zeit;
        }
        public int Id { get; private set; }
        public string Startdatum { get; set; }
        public string Startzeit { get; set; }
        public string Startort { get; set; }
        public string Zielort { get; set; }
        public int Zeit { get; set; }
    }
}
