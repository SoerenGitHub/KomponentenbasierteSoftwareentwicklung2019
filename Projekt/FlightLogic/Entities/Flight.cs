using System;
using System.Data.Common;

namespace FlightLogic.Entities
{
    public class Flight
    {
        //define public variables
        public string Id                { get; private set; }
        public string Start             { get; set; }
        public string StartCode         { get; set; }
        public string Destination       { get; set; }
        public string DestinationCode   { get; set; }
        public int Duration             { get; set; }
        public DateTime Date            { get; set; }
        public DateTime Time            { get; set; }
        public string DateS             { get; set; }
        public string TimeS             { get; set; }
        public int Seats                { get; set; }
        public string Airline           { get; set; }

        public Flight()
        {
            Id = createId();
        }

        public Flight(string start, string startCode, string destination, string destinationCode, DateTime date, DateTime time, string airline, int duration = 0, int seats = 0)
        {
            Id = createId();
            Start = start;
            StartCode = startCode;
            Destination = destination;
            DestinationCode = destinationCode;
            Duration = duration;
            Date = date;
            DateS = date.ToString("dd.MM.yyyy");
            Console.WriteLine("Datum :" + DateS);
            Time = time;
            TimeS = time.ToString("HH:mm");
            Seats = seats;
            Airline = airline;
        }

        public Flight(DbDataReader dbDataReader)
        {
            Id = dbDataReader.GetString(0);
            Start = dbDataReader.GetString(1);
            StartCode = dbDataReader.GetString(2);
            Destination = dbDataReader.GetString(3);
            DestinationCode = dbDataReader.GetString(4);
            DateS = dbDataReader.GetString(5);
            Airline = dbDataReader.GetString(6);
            Duration = dbDataReader.GetInt32(7);
            TimeS = dbDataReader.GetString(8);
            Seats = dbDataReader.GetInt32(9);

        }

        private static string createId()
        {
            Guid guid = Guid.NewGuid();
            string strguid = guid.ToString();
            byte[] buid = guid.ToByteArray();
            long luid = 0;
            luid = luid | (long)buid[0];
            int shift = 8;
            for (int i = 0; i < 7; i++)
            {
                luid = luid | (long)buid[i + 1] << shift;
                shift = shift + 8;
            }
            return string.Format("{0:d}", luid);
        }
    }
}
