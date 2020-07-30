using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace FlightLogic_Test
{
    [TestClass]
    public class FlightTest
    {
        [TestMethod]
        public void CreateFlight()
        {
            //arrange
            string start            = "Berlin-Schönefeld";
            string startCode        = "SXF";
            string destination      = "Berlin-Tegel";
            string destinationCode  = "TXL";
            DateTime date           = new DateTime(2020, 02, 01, 00, 00, 00);
            DateTime time           = new DateTime(2020, 01, 01, 21, 15, 00);
            string airline          = "Air Berlin";
            int duration            = 5;
            int seats               = 152;

            string dates            = "01.02.2020";
            string times            = "21:15";

            Debug.WriteLine(dates + "\n" + times);

            Debug.WriteLine("All variables have been arranged.");

            //act
            FlightLogic.Entities.Flight testFlight = new FlightLogic.Entities.Flight(start, startCode, destination, destinationCode, date, time, airline, duration, seats);
            Debug.WriteLine("Act ready.");

            //assert
            Debug.WriteLine(testFlight.Id);
            Assert.AreNotEqual(null, testFlight.Id);
            Assert.AreEqual(start, testFlight.Start);
            Assert.AreEqual(startCode, testFlight.StartCode);
            Assert.AreEqual(destination, testFlight.Destination);
            Assert.AreEqual(destinationCode, testFlight.DestinationCode);
            Assert.AreEqual(date, testFlight.Date);
            Assert.AreEqual(time, testFlight.Time);
            Assert.AreEqual(dates, testFlight.DateS);
            Assert.AreEqual(times, testFlight.TimeS);
            Assert.AreEqual(airline, testFlight.Airline);
            Assert.AreEqual(duration, testFlight.Duration);
            Assert.AreEqual(seats, testFlight.Seats);
            Debug.WriteLine("All asserts have been completed.");
            Debug.WriteLine("All tests have been completed.");
        }
    }
}
