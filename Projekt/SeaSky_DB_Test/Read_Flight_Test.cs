using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.OleDb;
using SeaSky_DB;
using SeaSky_DB.Factories;
using SeaSky_DB.Read;
using SeaSky_DB.Write;
using FlightLogic;
using FlightLogic.Entities;
using System.Collections;
using System.Collections.Generic;

namespace SeaSky_DB_Test
{
    [TestClass]
    public class Read_Flight_Test
    {
        FIDataRead fIDataRead;

        [TestMethod]
        public void TestTest()
        {
            Assert.IsTrue(true);
        }

        [TestInitialize]
        public void Initialize()
        {
            string sourcePath = @"G:\Uni\Kompo\seasky.accdb";
            fIDataRead = SFDB_ReadF.CreateInstance(sourcePath);
            Assert.IsNotNull(fIDataRead, "Fehler in Read_Flight_Test.TestInitialize");
            fIDataRead.InitDb();
        }

        [TestCleanup]
        public void CleanUp() => fIDataRead.CloseDb();
        
        [TestMethod]
        public void CountFlights()
        {
            int flights = fIDataRead.countFlights();
            Assert.AreEqual(TestWerte.CountFlights, flights);
        }

        [DataTestMethod]
        [DataRow("24524534")]
        public void GetFlight(string id)
        {
            string expected = TestWerte.TestId;

            Flight flight = fIDataRead.ReadFlight(id);

            Assert.AreEqual(expected, flight.Id);
        }

        [TestMethod]
        public void GetFlights()
        {
            List<Flight> flights = fIDataRead.ReadFlights();

            Assert.AreEqual(TestWerte.TestIds.GetValue(0), flights[0].Id);
            Assert.AreEqual(TestWerte.TestIds.GetValue(1), flights[1].Id);
            Assert.AreEqual(TestWerte.TestIds.GetValue(2), flights[2].Id);
        }

        [TestMethod]
        public void GetFilteredFlights()
        {
            string expected = TestWerte.TestId;
            List<Flight> flights = fIDataRead.ReadFlights(TestWerte.TestFlight);

            Assert.AreEqual(expected, flights[0].Id);
        }

        /*
        [TestMethod]
        public void ReadAirports()
        {
            ICollection StartExpected = TestWerte.StartAirport;
            ICollection DestExpected = TestWerte.DestAirport;

            ICollection startAirports = (ICollection)fIDataRead.ReadStartA("", "");
            ICollection destAirports = (ICollection)fIDataRead.ReadEndA("", "");

            CollectionAssert.AreEqual(StartExpected, startAirports);
            CollectionAssert.AreEqual(DestExpected, destAirports);
        }

        [TestMethod]
        public void ReadAirlines()
        {
            ICollection AirlineExpected = TestWerte.Airlines;

            ICollection airlines = (ICollection)fIDataRead.ReadAirlines("","");

            CollectionAssert.AreEqual(AirlineExpected, airlines);
        }
        */
    }
}
