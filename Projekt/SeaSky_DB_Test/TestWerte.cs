using System;
using System.Collections.Generic;
using System.Text;
using FlightLogic.Entities;
using ShipLogic.Entities;

namespace SeaSky_DB_Test
{
    public static class TestWerte
    {
        public static int CountFlights { get { return 3; } }

        public static string TestId { get { return "24524534"; } }

        public static string[] TestIds = new string[] { "24524534", "274657857", "44654478963" };

        public static Flight TestFlight = new Flight(null, null, "Tokyo", null, DateTime.Parse("24.08.2019"), DateTime.Parse("13:15:00"), null, 15, 500);

        /*
        public static List<string> Airlines
        {
            get
            {
                return new List<string> { "Air France", "Emirates", "Lufthansa" };
            }
        }

        public static List<string> StartAirport
        {
            get
            {
                return new List<string> { "Hamburg", "Köln", "New York" };
            }
        }

        public static List<string> DestAirport
        {
            get
            {
                return new List<string> { "Berlin", "Paris", "Tokyo" };
            }
        }

        public static String DestAirportFHH { get { return "Berlin"; } }
        */
    }
}
