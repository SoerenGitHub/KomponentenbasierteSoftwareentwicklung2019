using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShipLogicTest
{
    [TestClass]
    public class ShipInfoTest
    {
        [TestMethod]
        public void ShipInfoCreate()
        {
            //Test zum überprüfen ob Objekte richtig erstellt 
            //werden und die konvertierung innerhalb der Konstruktoren funktioniert.
            //Für Konsolenoutput den Test im Debug-Modus starten ("Test debuggen")

            //arrange
            string[] values = {"Hamburg", "London", "15.10.2019", "Euroshipping", "15:00", "Segel" }; //Start, Ziel, Datum, Firma, Abfahrt, Schiffstyp
            int tspan = 8; //Darf nicht -1 sein
            var date = new System.DateTime(2019, 10, 15, 00, 00, 00);
            var depTime = new System.DateTime(2019, 10, 15, 15, 00, 00);
            System.Diagnostics.Debug.WriteLine("Arrange done!");

            //act
            ShipLogic.Entities.ShipInfo testshipping = new ShipLogic.Entities.ShipInfo(values[0], values[1], date, depTime, tspan, values[5], values[3]);
            System.Diagnostics.Debug.WriteLine("Act done!");

            //assert
            Assert.AreEqual(values[0], testshipping.start);
            Assert.AreEqual(values[1], testshipping.destination);
            Assert.AreEqual(values[2], testshipping.date);
            Assert.AreEqual(values[3], testshipping.company);
            Assert.AreEqual(values[4], testshipping.depTime);
            Assert.AreEqual(tspan, testshipping.travelSpan);
            Assert.AreEqual(values[5], testshipping.shipType);
            Assert.AreNotEqual(testshipping.ID, null);
            System.Diagnostics.Debug.WriteLine("ID:" + testshipping.ID);
            System.Diagnostics.Debug.WriteLine("Assert done!");
        }
    }
}
