using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightLogic;
using FlightLogic.Entities;

namespace SeaSky_DB.Write
{
    internal class CDB_WriteF : ADB_Handler, FIDataWrite
    {
        internal CDB_WriteF(DbProviderFactory dbProviderFactory, string connectionString)
            :base(dbProviderFactory, connectionString)
        {
            //
        }

        /**
         * Methode zum Hinzufuegen eines Fluges
         * param    Flug der hinzugefuegt werden soll
         * return   true wenn der Flug hinzugefuegt wurde, sonst false 
         */
        public int CreateFlight(Flight newFlight)
        {
            int dbCheck = -1;
            try
            {
                if(this.Open())
                {
                    _dbTransaction = _dbConnection.BeginTransaction();
                    _dbCommand.Transaction = _dbTransaction;
                    _dbCommand.CommandType = CommandType.Text;
                    _dbCommand.Parameters.Clear();
                    _dbCommand.CommandText =
                        $"INSERT INTO Flight" +
                        $" (flightID, start, start_kurz, destination, dest_kurz, datum, airline, duration, zeit, seats )" +
                        $" VALUES " +
                        $" (@flightID, @start, @start_kurz, @end, @end_kurz, @datum, @airline, @duration, @time, @seats);";
                    AddParameter(_dbCommand, "@flightID", newFlight.Id);
                    AddParameter(_dbCommand, "@start", newFlight.Start);
                    AddParameter(_dbCommand, "@start_kurz", newFlight.StartCode);
                    AddParameter(_dbCommand, "@destination", newFlight.Destination);
                    AddParameter(_dbCommand, "@dest_kurz", newFlight.DestinationCode);
                    AddParameter(_dbCommand, "@datum", newFlight.DateS);
                    AddParameter(_dbCommand, "@airline", newFlight.Airline);
                    AddParameter(_dbCommand, "@duration", newFlight.Duration);
                    AddParameter(_dbCommand, "@zeit", newFlight.TimeS);
                    AddParameter(_dbCommand, "@seats", newFlight.Seats);
                    var result = _dbCommand.ExecuteNonQuery();
                    dbCheck = Convert.ToInt32(result);
                    _dbTransaction.Commit();
                }
                this.Close();
            }
            catch(Exception e)
            {
                _dbTransaction.Rollback();
                this.Close();
                throw new Exception(e.Message);
            }
            return dbCheck;
        }

        /**
         * Methode zum Aendern eines Fluges
         * param    der Flug der geaendert werden soll
         * return   Anzahl geaenderter Zeilen
         */
        public int UpdateFlight(Flight changeFlight)
        {
            int dbCheck = -1;
            try
            {
                if(this.Open())
                {
                    Console.WriteLine("UpdateFlight: start");
                    _dbTransaction = _dbConnection.BeginTransaction();
                    _dbCommand.Transaction = _dbTransaction;
                    _dbCommand.CommandType = CommandType.Text;
                    _dbCommand.Parameters.Clear();
                    _dbCommand.CommandText =
                        $"UPDATE Flight SET " +
                        $"start = ?, start_kurz = ?, destination = ?, dest_kurz = ?, datum = ?, airline = ?, duration = ?, zeit = ?, seats = ? " +
                        $"WHERE flightID = ?;";
                    Console.WriteLine(_dbCommand.CommandText);
                    AddParameter(_dbCommand, "start", changeFlight.Start);
                    AddParameter(_dbCommand, "start_kurz", changeFlight.StartCode);
                    AddParameter(_dbCommand, "destination", changeFlight.Destination);
                    AddParameter(_dbCommand, "dest_kurz", changeFlight.DestinationCode);
                    AddParameter(_dbCommand, "datum", changeFlight.DateS);
                    AddParameter(_dbCommand, "airline", changeFlight.Airline);
                    AddParameter(_dbCommand, "duration", changeFlight.Duration);
                    AddParameter(_dbCommand, "zeit", changeFlight.TimeS);
                    AddParameter(_dbCommand, "seats", changeFlight.Seats);
                    AddParameter(_dbCommand, "flightID", changeFlight.Id);
                    var result = _dbCommand.ExecuteNonQuery();
                    dbCheck = Convert.ToInt32(result);
                    _dbTransaction.Commit();
                    Console.WriteLine("UpdateFlight: end");
                }
                this.Close();
            }
            catch(Exception e)
            {
                _dbTransaction.Rollback();
                this.Close();
                throw new Exception(e.Message);
            }
            return dbCheck;
        }

        /**
         * Methode zum Loeschen eines Flugs
         * param    die ID des Flugs der geloescht werden soll
         * return   true wenn der Flug geloescht wurde, sonst false 
         */
        public int DeleteFlight(string flightID)
        {
            int dbCheck = -1;
            if(this.Open())
            {
                _dbCommand.CommandType = CommandType.Text;
                //_dbCommand.Parameters.Clear();
                _dbCommand.CommandText = $"DELETE FROM Flight WHERE flightID = ?;";
                _dbCommand.Parameters.Clear();
                AddParameter(_dbCommand, "flightID", flightID);
                dbCheck = _dbCommand.ExecuteNonQuery();
            }
            this.Close();
            return dbCheck;
        }
    }
}
