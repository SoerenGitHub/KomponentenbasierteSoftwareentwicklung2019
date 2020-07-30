using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace ShipLogic.Entities
{
    public class ShipInfo {
        public string ID { get; private set; } //ID
        public string start { get; set; } //Start
        public string destination { get; set; } //Ziel
        public string date { get; set; } //Abfahrtsdatum
        public string company { get; set; } //Firma
        public string depTime { get; set; } //Abfahrtszeit
        public int travelSpan { get; set; } //Reisezeit
        public string shipType { get; set; } //Schiffstyp

        //Überladene Konstruktoren für Objekterstellung
        public ShipInfo() {
            ID = CreateID();
            travelSpan = 0; //Muss beim Init 0 sein um Abfrage in SeaSkyDB zu ermöglichen
        }

        public ShipInfo(string _start, string _destination, DateTime _date, DateTime _depTime, 
                         int _travelSpan, string _shipType, string _company) {
            start = _start;
            destination = _destination;
            date = _date.ToString("dd.MM.yyyy");
            depTime = _depTime.ToString("HH:mm");
            travelSpan = _travelSpan;
            shipType = _shipType;
            company = _company;
        }

        //Konstruktur für Objekt aus Datenbankinformationen
        public ShipInfo(DbDataReader dbDataReader) {
            ID = dbDataReader.GetString(0);
            start = dbDataReader.GetString(1);
            destination = dbDataReader.GetString(2);
            date = dbDataReader.GetString(3);
            company = dbDataReader.GetString(4);
            travelSpan = (int)dbDataReader.GetValue(5);
            depTime = dbDataReader.GetString(6);
            shipType = dbDataReader.GetString(7);
        }

        private static string CreateID() {
            // 16 Byte Schlüssel
            System.Guid gUID = System.Guid.NewGuid();
            string strgUID = gUID.ToString();
            byte[] bUID = gUID.ToByteArray();
            // nur die ersten 8 Bytes werden verwendet
            long lUID = 0;
            lUID = lUID | (long)bUID[0];
            int shift = 8;
            for (int i = 1; i < 8; i++)
            {
                lUID = lUID | (long)bUID[i] << shift;
                shift = shift + 8;
            }
            return string.Format("{0:d}", lUID);
        }

    }
}
