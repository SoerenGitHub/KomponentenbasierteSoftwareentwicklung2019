using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightLogic;
using FlightLogic.Entities;

namespace SeaSky_DB.Read
{
    internal class CDB_ReadF : ADB_Handler, FIDataRead
    {
        internal CDB_ReadF(DbProviderFactory dbProviderFactory, string connectionString)
            : base(dbProviderFactory, connectionString)
        {
            //
        }

        /**
         * zaehlt die Anzahl Fluege in Datenbank
         * return Anzahl Fluege
         */
        public virtual int countFlights()
        {
            int count = 0;
            if(this.Open())
            {
                _dbCommand.CommandText = "SELECT COUNT(*) FROM Flight;";
                count = Convert.ToInt32(_dbCommand.ExecuteScalar());
            }
            this.Close();
            return count;
        }

        /**
         * liest einen Flug aus Db aus
         * param    int die ID eines Fluges
         * return   der gesuchte Flug
         */
        public virtual Flight ReadFlight(string flightID)
        {
            Flight selected = new Flight();
            if(this.Open())
            {
                _dbCommand.CommandType = CommandType.Text;
                _dbCommand.Parameters.Clear();
                _dbCommand.CommandText = $"SELECT * FROM Flight WHERE flightID = ?;";
                _dbCommand.Parameters.Clear();
                AddParameter(_dbCommand, "flightID", flightID);

                DbDataReader dbDataReader = _dbCommand.ExecuteReader();
                if (dbDataReader != null)
                {
                    while(dbDataReader.Read())
                    {
                        selected = new Flight(dbDataReader);
                    }
                }
                if (!dbDataReader.IsClosed) dbDataReader.Close();
            }
            this.Close();
            return selected;
        }

        /**
         * liest alle Fluege aus Db aus
         * return Liste ueber alle Fluege in Db
         */
        public virtual List<Flight> ReadFlights()
        {
            List<Flight> flights = new List<Flight>();
            if (this.Open())
            {
                _dbCommand.CommandType = CommandType.Text;
                _dbCommand.Parameters.Clear();
                _dbCommand.CommandText = "SELECT * FROM Flight;";

                DbDataReader dbDataReader = _dbCommand.ExecuteReader();
                if (dbDataReader != null)
                {
                    while (dbDataReader.Read())
                    {
                        Flight selected = new Flight(dbDataReader);
                        flights.Add(selected);
                    }
                }
                if (!dbDataReader.IsClosed) dbDataReader.Close();
            }
            this.Close();
            return flights;
        }

        /**
         * liest Fluege in Abhaengigkeit der Suchparameter aus Db aus
         * param    Flight Objekt mit den Suchparametern als Attribut
         * return   Liste ueber alle Fluege in Db
         */
        public virtual List<Flight> ReadFlights(Flight searchFlight)
        {
            List<Flight> flights = new List<Flight>();
            if (this.Open())
            {
                Console.WriteLine("create query");
                _dbCommand.CommandType = CommandType.Text;
                _dbCommand.Parameters.Clear();
                _dbCommand.CommandText = "SELECT * FROM Flight;";

                DbDataReader dbDataReader = _dbCommand.ExecuteReader();
                if (dbDataReader != null)
                {
                    while (dbDataReader.Read())
                    {
                        try
                        {
                            if (CheckParam(dbDataReader, searchFlight))
                            {
                                Flight selected = new Flight(dbDataReader);
                                flights.Add(selected);
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
            return flights;
        }

        /**
         * Hilfsmethode zum Filtern der ausgelesenen Liste aus der Datenbank
         * param    DbDataReader der Stream der aktuell aus der Datenbank gelesen wird
         * param    ShipInfo das Objekt mit den Suchparametern
         * return   Boolean true wenn mindestens ein Suchparameter erfuellt wird,
         *                  false wenn ein Suchparameter nicht mit dem Db-Stream uebereinstimmt
         */
        internal Boolean CheckParam(DbDataReader dbDataReader, Flight flight)
        {
            if (flight.Start != null            && !flight.Start.Equals(dbDataReader.GetString(1)))         return false;
            if (flight.Destination != null      && !flight.Destination.Equals(dbDataReader.GetString(3)))   return false;
            if (flight.DateS != null            && !flight.DateS.Equals(dbDataReader.GetString(5)))         return false;
            if (flight.Airline != null          && !flight.Airline.Equals(dbDataReader.GetString(6)))       return false;
            if (flight.TimeS != null            && !flight.TimeS.Equals(dbDataReader.GetString(8)))         return false;
            if (flight.Duration > 0             && dbDataReader.GetInt32(7) > flight.Duration)              return false;
            if (flight.Seats != 0               && dbDataReader.GetInt32(9) < flight.Seats)                 return false;
            return true;
        }

        /**
         * aktuell nicht verwendet
         * gibt eine ICollection ueber alle Start-Flughaefen zurueck
         * param    string die gewaehlte Airline
         * param    string der gewaehlte Endflughafen
         * return   ICollection Liste mit den Startflughaefen
         */
        public virtual ICollection<String> ReadStartA(string airline, string end)
        {
            ICollection<string> airports = new List<string>();
            if (this.Open())
            {
                _dbCommand.CommandType = CommandType.Text;
                // Parameter leer
                if (airline.Equals("") && end.Equals(""))
                {
                    _dbCommand.CommandText = "SELECT DISTINCT start FROM Flight ORDER BY start;";
                }
                // Parameter airline leer
                else if (airline.Equals("") && !end.Equals(""))
                {
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT start FROM Flight WHERE destination = ? ORDER BY start;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "destination", end);
                }
                // Parameter end leer
                else if (!airline.Equals("") && end.Equals(""))
                {
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT start FROM Flight WHERE airline = ? ORDER BY start;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "airline", airline);
                }
                // beide Parameter nicht leer
                else
                {
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT start FROM Flight WHERE airline = @airline AND " +
                        @"destination = @destination AND " +
                        @"ORDER BY start;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "@airline", airline);
                    AddParameter(_dbCommand, "@destination", end);
                }

                DbDataReader dbDataReader = _dbCommand.ExecuteReader();
                if (dbDataReader.HasRows)
                {
                    while (dbDataReader.Read())
                    {
                        airports.Add(dbDataReader[0].ToString());
                    }
                }
                if (!dbDataReader.IsClosed) dbDataReader.Close();
            }
            this.Close();
            return airports;
        }

        /**
         * aktuell nicht verwendet
         * gibt eine ICollection ueber alle Kuerzel der Start-Flughaefen zurueck
         * param    string die gewaehlte Airline
         * param    string der gewaehlte Endflughafen
         * return   ICollection Liste mit den Kuerzeln der Startflughaefen
         */
        public virtual ICollection<String> ReadStartCodeA(string airline, string endcode)
        {
            return null;
        }
        /*
        ICollection<string> airports = new List<string>();
        if (this.Open())
        {
            _dbCommand.CommandType = CommandType.Text;
            // Parameter leer
            if (airline.Equals("") && endcode.Equals(""))
            {
                _dbCommand.CommandText = "SELECT DISTINCT start_kurz FROM Flight ORDER BY start_kurz;";
            }
            // Parameter airline leer
            if (airline.Equals("") && !endcode.Equals(""))
            {
                _dbCommand.CommandText =
                    @"SELECT DISTINCT start_kurz FROM Flight WHERE dest_kurz = ? ORDER BY start_kurz;";
                _dbCommand.Parameters.Clear();
                AddParameter(_dbCommand, "dest_kurz", endcode);
            }
            // Parameter end leer
            if (!airline.Equals("") && endcode.Equals(""))
            {
                _dbCommand.CommandText =
                    @"SELECT DISTINCT start_kurz FROM Flights WHERE airline = ? ORDER BY start_kurz;";
                _dbCommand.Parameters.Clear();
                AddParameter(_dbCommand, "airline", airline);
            }
            // beide Parameter nicht leer
            else
            {
                _dbCommand.CommandText =
                    @"SELECT DISTINCT start_kurz FROM Flights WHERE airline = @airline AND " +
                    @"dest_kurz = @dest_kurz AND " +
                    @"ORDER BY start_kurz;";
                _dbCommand.Parameters.Clear();
                AddParameter(_dbCommand, "@airline", airline);
                AddParameter(_dbCommand, "@dest_kurz", endcode);
            }

            DbDataReader dbDataReader = _dbCommand.ExecuteReader();
            if (dbDataReader.HasRows)
            {
                while (dbDataReader.Read())
                {
                    airports.Add(dbDataReader[0].ToString());
                }
            }
            if (!dbDataReader.IsClosed) dbDataReader.Close();
        }
        this.Close();
        return airports;
    }
*/
        
        /**
         * aktuell nicht verwendet
         * gibt eine ICollection ueber alle End-Flughaefen zurueck
         * param    string die gewaehlte Airline
         * param    string der gewaehlte Startflughafen
         * return   ICollection Liste mit den Endflughaefen
         */
        public virtual ICollection<String> ReadEndA(string airline, string start)
        {
            ICollection<string> airports = new List<string>();
            if (this.Open())
            {
                _dbCommand.CommandType = CommandType.Text;
                // Parameter leer
                if (airline.Equals("") && start.Equals(""))
                {
                    _dbCommand.CommandText = "SELECT DISTINCT destination FROM Flight ORDER BY destination;";
                }
                // Parameter airline leer
                else if (airline.Equals("") && !start.Equals(""))
                {
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT destination FROM Flight WHERE start = ? ORDER BY destination;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "start", start);
                }
                // Parameter start leer
                else if (!airline.Equals("") && start.Equals(""))
                {
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT destination FROM Flight WHERE airline = ? ORDER BY destination;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "airline", airline);
                }
                else
                {
                    //fuer zwei Parameter anpassen
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT destination FROM Flight WHERE airline = @airline AND " +
                        @"start = @start AND " +
                        @"ORDER BY destination;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "@airline", airline);
                    AddParameter(_dbCommand, "@start", start);
                }

                DbDataReader dbDataReader = _dbCommand.ExecuteReader();
                if (dbDataReader.HasRows)
                {
                    while (dbDataReader.Read())
                    {
                        airports.Add(dbDataReader[0].ToString());
                    }
                }
                if (!dbDataReader.IsClosed) dbDataReader.Close();
            }
            this.Close();
            return airports;
        }

        /**
         * aktuell nicht verwendet
         * gibt eine ICollection ueber alle Kuerzel der End-Flughaefen zurueck
         * param    string die gewaehlte Airline
         * param    string der gewaehlte Startflughafen
         * return   ICollection Liste mit den Kuerzeln der Endflughaefen
         */
        public virtual ICollection<String> ReadEndCodeA(string airline, string startcode)
        {
            return null;
        }
        /*
            ICollection<string> airports = new List<string>();
            if (this.Open())
            {
                _dbCommand.CommandType = CommandType.Text;
                // Parameter leer
                if (airline.Equals("") && startcode.Equals(""))
                {
                    _dbCommand.CommandText = "SELECT DISTINCT dest_kurz FROM Flight ORDER BY dest_kurz;";
                }
                // Parameter airline leer
                if (airline.Equals("") && !startcode.Equals(""))
                {
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT dest_kurz FROM Flight WHERE start_kurz = ? ORDER BY dest_kurz;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "start_kurz", startcode);
                }
                // Parameter start leer
                if (!airline.Equals("") && startcode.Equals(""))
                {
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT dest_kurz FROM Flights WHERE airline = ? ORDER BY dest_kurz;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "airline", airline);
                }
                else
                {
                    //fuer zwei Parameter anpassen
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT dest_kurz FROM Flights WHERE airline = @airline AND " +
                        @"start_kurz = @start_kurz AND " +
                        @"ORDER BY dest_kurz;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "@airline", airline);
                    AddParameter(_dbCommand, "@start_kurz", startcode);
                }

                DbDataReader dbDataReader = _dbCommand.ExecuteReader();
                if (dbDataReader.HasRows)
                {
                    while (dbDataReader.Read())
                    {
                        airports.Add(dbDataReader[0].ToString());
                    }
                }
                if (!dbDataReader.IsClosed) dbDataReader.Close();
            }
            this.Close();
            return airports;
        }
*/

        /**
         * aktuell nicht verwendet
         * gibt eine ICollection ueber alle Airlines zurueck
         * param    string der gewaehlte Startflughafen
         * param    string der gewaehlte Endflughafen
         * return   ICollection Liste mit den Airlines
         */
        public virtual ICollection<String> ReadAirlines(string start, string end)
        {
            ICollection<string> airlines = new List<string>();
            if (this.Open())
            {
                _dbCommand.CommandType = CommandType.Text;
                // Parameter leer
                if (start.Equals("") && end.Equals(""))
                {
                    _dbCommand.CommandText = "SELECT DISTINCT airline FROM Flight ORDER BY airline;";
                }
                // Parameter airline leer
                else if (start.Equals("") && !end.Equals(""))
                {
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT airline FROM Flight WHERE destination = ? ORDER BY airline;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "destination", end);
                }
                // Parameter start leer
                else if (!start.Equals("") && end.Equals(""))
                {
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT airline FROM Flight WHERE start = ? ORDER BY airline;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "start", start);
                }
                else
                {
                    //fuer zwei Parameter anpassen
                    _dbCommand.CommandText =
                        @"SELECT DISTINCT airline FROM Flight WHERE start = @start AND " +
                        @"destination = @destination AND " +
                        @"ORDER BY airline;";
                    _dbCommand.Parameters.Clear();
                    AddParameter(_dbCommand, "@start", start);
                    AddParameter(_dbCommand, "@destination", end);
                }

                DbDataReader dbDataReader = _dbCommand.ExecuteReader();
                if (dbDataReader.HasRows)
                {
                    while (dbDataReader.Read())
                    {
                        airlines.Add(dbDataReader[0].ToString());
                    }
                }
                if (!dbDataReader.IsClosed) dbDataReader.Close();
            }
            this.Close();
            return airlines;
        }
    }
}
