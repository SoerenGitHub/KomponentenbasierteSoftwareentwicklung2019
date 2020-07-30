using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipLogic;
using ShipLogic.Entities;

namespace SeaSky_DB.Write
{
    internal class CDB_WriteS : ADB_Handler, SIDataWrite
    {
        internal CDB_WriteS(DbProviderFactory dbProviderFactory, string connectionString)
            : base(dbProviderFactory, connectionString)
        {
            //
        }

        /**
         * Methode zum Hinzufuegen einer Schifffahrt
         * param    Schifffahrt die hinzugefuegt werden soll
         * return   Anzahl der hinzugefuegten Zeilen
         */
        public int AddShipping(ShipInfo newShipping)
        {
            int dbCheck = -1;
            try
            {
                if (this.Open())
                {
                    _dbTransaction = _dbConnection.BeginTransaction();
                    _dbCommand.Transaction = _dbTransaction;
                    _dbCommand.CommandType = CommandType.Text;
                    _dbCommand.Parameters.Clear();
                    _dbCommand.CommandText =
                        $"INSERT INTO Shipping" +
                        $" (shippingID, start, destination, datum, company, duration, zeit, shiptype) " +
                        $" VALUES" +
                        $" (@shippingID, @start, @destination, @datum, @company, @duration, @zeit, @shiptype);";
                    AddParameter(_dbCommand, "@shippingID", newShipping.ID);
                    AddParameter(_dbCommand, "@start", newShipping.start);
                    AddParameter(_dbCommand, "@destination", newShipping.destination);
                    AddParameter(_dbCommand, "@datum", newShipping.date);
                    AddParameter(_dbCommand, "@company", newShipping.company);
                    AddParameter(_dbCommand, "@duration", newShipping.travelSpan);
                    AddParameter(_dbCommand, "@zeit", newShipping.depTime);
                    AddParameter(_dbCommand, "@shiptype", newShipping.shipType);
                    var result = _dbCommand.ExecuteNonQuery();
                    dbCheck = Convert.ToInt32(result);
                    _dbTransaction.Commit();
                }
                this.Close();
            }
            catch (Exception e)
            {
                _dbTransaction.Rollback();
                this.Close();
                throw new Exception(e.Message);
            }
            return dbCheck;
        }

        /**
         * Methode zum Updaten einer Schifffahrt
         * param    Schifffahrt die geupdatet werden soll
         * return   Anzahl geaenderter Zeilen
         */
        public int UpdateShipping(ShipInfo changeShipping)
        {
            int dbCheck = -1;
            try
            {
                if (this.Open())
                {
                    _dbTransaction = _dbConnection.BeginTransaction();
                    _dbCommand.Transaction = _dbTransaction;
                    _dbCommand.CommandType = CommandType.Text;
                    _dbCommand.Parameters.Clear();
                    _dbCommand.CommandText =
                        $"UPDATE Shipping SET " +
                        $"start = ?, destination = ?, datum = ?, company = ?, duration = ?, zeit = ?, shiptype = ? " +
                        $"WHERE shippingID = ?;";
                    AddParameter(_dbCommand, "start", changeShipping.start);
                    AddParameter(_dbCommand, "destination", changeShipping.destination);
                    AddParameter(_dbCommand, "datum", changeShipping.date);
                    AddParameter(_dbCommand, "company", changeShipping.company);
                    AddParameter(_dbCommand, "duration", changeShipping.travelSpan);
                    AddParameter(_dbCommand, "zeit", changeShipping.depTime);
                    AddParameter(_dbCommand, "shiptype", changeShipping.shipType);
                    AddParameter(_dbCommand, "shippingID", changeShipping.ID);
                    var result = _dbCommand.ExecuteNonQuery();
                    dbCheck = Convert.ToInt32(result);
                    _dbTransaction.Commit();
                }
                this.Close();
            }
            catch (Exception e)
            {
                _dbTransaction.Rollback();
                this.Close();
                throw new Exception(e.Message);
            }
            return dbCheck;
        }

        /**
         * Methode zum Loeschen einer Schifffahrt
         * param    die ID der Schifffahrt der geloescht werden soll
         * return   true wenn die Schifffahrt geloescht wurde, sonst false 
         */
        public int DeleteShipping(string shippingID)
        {
            int dbCheck = -1;
            if (this.Open())
            {
                _dbCommand.CommandType = CommandType.Text;
                //_dbCommand.Parameters.Clear();
                _dbCommand.CommandText = $"DELETE FROM Shipping WHERE shippingID = ?;";
                _dbCommand.Parameters.Clear();
                AddParameter(_dbCommand, "shippingID", shippingID);
                dbCheck = _dbCommand.ExecuteNonQuery();
            }
            this.Close();
            return dbCheck;
        }
    }
}
