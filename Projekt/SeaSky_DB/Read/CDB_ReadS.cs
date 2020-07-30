using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipLogic;
using ShipLogic.Entities;

namespace SeaSky_DB.Read
{
    internal class CDB_ReadS : ADB_Handler, SIDataRead
    {
        internal CDB_ReadS(DbProviderFactory dbProviderFactory, string connectionString)
            : base(dbProviderFactory, connectionString)
        {
            //
        }

        /**
         * zaehlt die Anzahl Schifffahrten in Datenbank
         * return Anzahl Schifffahrten
         */
        public virtual int countShippings()
        {
            int count = 0;
            if(this.Open())
            {
                _dbCommand.CommandText = "SELECT COUNT(*) FROM Shipping;";
                count = Convert.ToInt32(_dbCommand.ExecuteScalar());
            }
            this.Close();
            return count;
        }

        /**
         * liest eine Schifffahrt aus Db aus
         * param    int die ID einer Schifffahrt
         * return   die gesuchte Schifffahrt
         */
        public virtual ShipInfo ReadShipping(string shippingID)
        {
            ShipInfo selected = new ShipInfo();
            if(this.Open())
            {
                _dbCommand.CommandType = CommandType.Text;
                _dbCommand.Parameters.Clear();
                _dbCommand.CommandText = $"SELECT * FROM Shipping WHERE shippingID = ?;";
                _dbCommand.Parameters.Clear();
                AddParameter(_dbCommand, "shippingID", shippingID);

                DbDataReader dbDataReader = _dbCommand.ExecuteReader();
                if(dbDataReader != null)
                {
                    while(dbDataReader.Read())
                    {
                        selected = new ShipInfo(dbDataReader);
                    }
                }
                if (!dbDataReader.IsClosed) dbDataReader.Close();
            }
            this.Close();
            return selected;
        }

        /**
         * liest alle Schifffahrten aus Db aus
         * return   Liste ueber alle Schifffahrten in Db
         */
        public virtual List<ShipInfo> ReadShippings()
        {
            List<ShipInfo> shippings = new List<ShipInfo>();
            if (this.Open())
            {
                _dbCommand.CommandType = CommandType.Text;
                _dbCommand.Parameters.Clear();
                _dbCommand.CommandText = "SELECT * FROM Shipping;";

                DbDataReader dbDataReader = _dbCommand.ExecuteReader();
                if (dbDataReader != null)
                {
                    while (dbDataReader.Read())
                    {
                        ShipInfo selected = new ShipInfo(dbDataReader);
                        shippings.Add(selected);
                    }
                }
                if (!dbDataReader.IsClosed) dbDataReader.Close();
            }
            this.Close();
            return shippings;
        }

        /**
         * liest Schifffahrten in Abhaengigkeit der Suchparameter aus Db aus
         * param    ShipInfo Objekt mit den Suchparametern als Attribut
         * return   Liste ueber alle Schifffahrten in Db
         */
        public virtual List<ShipInfo> ReadShippings(ShipInfo searchShip)
        {
            List<ShipInfo> shippings = new List<ShipInfo>();
            if (this.Open())
            {
                _dbCommand.CommandType = CommandType.Text;
                _dbCommand.Parameters.Clear();
                _dbCommand.CommandText = "SELECT * FROM Shipping;";

                DbDataReader dbDataReader = _dbCommand.ExecuteReader();
                if (dbDataReader != null)
                {
                    while (dbDataReader.Read())
                    {
                        try
                        {
                            if (CheckParam(dbDataReader, searchShip))
                            {
                                ShipInfo selected = new ShipInfo(dbDataReader);
                                shippings.Add(selected);
                            }
                        }
                        catch
                        {
                            throw new Exception("Fehler beim Filtern");
                        }
                    }
                }
                if (!dbDataReader.IsClosed) dbDataReader.Close();
            }
            this.Close();
            return shippings;
        }

        /**
         * Hilfsmethode zum Filtern der ausgelesenen Liste aus der Datenbank
         * param    DbDataReader der Stream der aktuell aus der Datenbank gelesen wird
         * param    ShipInfo das Objekt mit den Suchparametern
         * return   Boolean true wenn mindestens ein Suchparameter erfuellt wird,
         *                  false wenn ein Suchparameter nicht mit dem Db-Stream uebereinstimmt
         */
        internal Boolean CheckParam(DbDataReader dbDataReader, ShipInfo ship)
        {
            if (ship.start != null          && !ship.start.Equals(dbDataReader.GetString(1)))       return false;
            if (ship.destination != null    && !ship.destination.Equals(dbDataReader.GetString(2))) return false;
            if (ship.date != null           && !ship.date.Equals(dbDataReader.GetString(3)))        return false;
            if (ship.company != null        && !ship.company.Equals(dbDataReader.GetString(4)))     return false;
            if (ship.depTime != null        && !ship.depTime.Equals(dbDataReader.GetString(6)))     return false;
            if (ship.shipType != null       && !ship.shipType.Equals(dbDataReader.GetString(7)))    return false;
            if ((ship.travelSpan > 0)       && (dbDataReader.GetInt32(5) > ship.travelSpan))        return false;
            return true;
        }

        /**
         * aktuell nicht verwendet
         * gibt eine ICollection ueber alle Start-Haefen zurueck
         * param    string die gewaehlte Firma
         * param    string der gewaehlte Endhafen
         * return   ICollection Liste mit den Starthaefen
         */
        public virtual ICollection<String> ReadStartH(string company, string end)
        {
            ICollection<string> harbours = new List<string>();
            if (this.Open())
            {
                _dbCommand.CommandType = CommandType.Text;
                // Parameter leer
                if (company.Equals("") && end.Equals(""))
                {
                    _dbCommand.CommandText = "SELECT DISTINCT start FROM Shipping ORDER BY start;";
                }
                // Parameter company leer
                else if (company.Equals("") && !end.Equals(""))
                {
                    _dbCommand.CommandText = @"SELECT DISTINCT start FROM Shipping WHERE destination = ? ORDER BY start;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "destination", end);
                }
                // Parameter end leer
                else if (!company.Equals("") && end.Equals(""))
                {
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT start FROM Shipping WHERE company = ? ORDER BY start;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "company", company);
                }
                // beide Parameter nicht leer
                else
                {
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT start FROM Shipping WHERE company = @company AND " +
                        @"destination = @destination AND " +
                        @"ORDER BY start;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "@company", company);
                    AddParameter(_dbCommand, "@destination", end);
                }

                DbDataReader dbDataReader = _dbCommand.ExecuteReader();
                if (dbDataReader.HasRows)
                {
                    while (dbDataReader.Read())
                    {
                        harbours.Add(dbDataReader[0].ToString());
                    }
                }
                if (!dbDataReader.IsClosed) dbDataReader.Close();
            }
            this.Close();
            return harbours;
        }

        /**
         * aktuell nicht verwendet
         * gibt eine ICollection ueber alle End-Haefen zurueck
         * param    string die gewaehlte Firma
         * param    string der gewaehlte Starthafen
         * return   ICollection Liste mit den Endhaefen
         */
        public virtual ICollection<String> ReadEndH(string company, string start)
        {
            ICollection<string> harbours = new List<string>();
            if (this.Open())
            {
                _dbCommand.CommandType = CommandType.Text;
                // Parameter leer
                if (company.Equals("") && start.Equals(""))
                {
                    _dbCommand.CommandText = "SELECT DISTINCT destination FROM Shipping ORDER BY destination;";
                }
                // Parameter company leer
                else if (company.Equals("") && !start.Equals(""))
                {
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT destination FROM Shipping WHERE start = ? ORDER BY destination;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "start", start);
                }
                // Parameter start leer
                else if (!company.Equals("") && start.Equals(""))
                {
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT destination FROM Shipping WHERE company = ? ORDER BY destination;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "company", company);
                }
                else
                {
                    //fuer zwei Parameter anpassen
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT destination FROM Shipping WHERE company = @company AND " +
                        @"start = @start AND " +
                        @"ORDER BY destination;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "@company", company);
                    AddParameter(_dbCommand, "@start", start);
                }

                DbDataReader dbDataReader = _dbCommand.ExecuteReader();
                if (dbDataReader.HasRows)
                {
                    while (dbDataReader.Read())
                    {
                        harbours.Add(dbDataReader[0].ToString());
                    }
                }
                if (!dbDataReader.IsClosed) dbDataReader.Close();
            }
            this.Close();
            return harbours;
        }

        /**
         * aktuell nicht verwendet
         * gibt eine ICollection ueber alle Firmen zurueck
         * param    string der gewaehlte Starthafen
         * param    string der gewaehlte Endhafen
         * return   ICollection Liste mit den Firmen
         */
        public virtual ICollection<String> ReadCompany(string start, string end)
        {
            ICollection<string> companys = new List<string>();
            if (this.Open())
            {
                _dbCommand.CommandType = CommandType.Text;
                // Parameter leer
                if (start.Equals("") && end.Equals(""))
                {
                    _dbCommand.CommandText = "SELECT DISTINCT company FROM Shipping ORDER BY company;";
                }
                // Parameter airline leer
                else if (start.Equals("") && !end.Equals(""))
                {
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT company FROM Shipping WHERE destination = ? ORDER BY company;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "destination", end);
                }
                // Parameter start leer
                else if (!start.Equals("") && end.Equals(""))
                {
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT company FROM Shipping WHERE start = ? ORDER BY company;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "start", start);
                }
                else
                {
                    //fuer zwei Parameter anpassen
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT airline FROM Shipping WHERE start = @start AND " +
                        @"destination = @destination AND " +
                        @"ORDER BY companys;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "@start", start);
                    AddParameter(_dbCommand, "@destination", end);
                }

                DbDataReader dbDataReader = _dbCommand.ExecuteReader();
                if (dbDataReader.HasRows)
                {
                    while (dbDataReader.Read())
                    {
                        companys.Add(dbDataReader[0].ToString());
                    }
                }
                if (!dbDataReader.IsClosed) dbDataReader.Close();
            }
            this.Close();
            return companys;
        }

    }
}
