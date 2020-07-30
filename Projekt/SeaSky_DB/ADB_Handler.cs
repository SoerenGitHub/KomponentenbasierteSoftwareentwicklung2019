using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace SeaSky_DB
{
    internal abstract class ADB_Handler
    {
        private readonly string _connectionString;
        private readonly DbProviderFactory _dbProviderFactory;
        protected DbConnection _dbConnection;
        protected DbCommand _dbCommand;
        protected DbTransaction _dbTransaction;

        /**
         * Konstruktor fuer den ADB_Handler
         * setzt die DbProviderFactory und den ConnectionString fuer das Objekt
         */
        internal ADB_Handler(DbProviderFactory dbProviderFactory, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ApplicationException("ConnectionString ist leer");
            _dbProviderFactory = dbProviderFactory ?? throw new ApplicationException("DbProviderFactory ist null");
            _connectionString = connectionString;
        }

        /**
         * Methode zum Initialisieren der Datenbankverbindung
         */
        public virtual void InitDb()
        {
            _dbConnection = _dbProviderFactory.CreateConnection();
            if (_dbConnection == null) throw new ApplicationException("DbConnection konnte nicht erzeugt werden");
            _dbConnection.ConnectionString = _connectionString;

            _dbConnection.Open();
            if (_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }
            else
            {
                throw new ApplicationException($"Datenbank {_connectionString} konnte nicht geoeffnet werden");
            }

            _dbCommand = _dbProviderFactory.CreateCommand();
            if (_dbCommand == null)
                throw new ApplicationException("DbCommand konnte nicht erzeugen");
            _dbCommand.CommandType = CommandType.Text;
            _dbCommand.Connection = _dbConnection;
        }

        /**
         * Methode zum Schliessen einer Datenbankverbindung
         */
        public virtual void CloseDb()
        {
            this.Close();
        }

        /**
         * oeffnet eine Datenbankverbindung fuer ein Objekt
         */
        internal virtual bool Open()
        {
            _dbConnection.Open();
            if (_dbConnection.State == ConnectionState.Open) return true;
            return false;
        }

        /**
         * schliesst eine Datenbankverbindung fuer ein Objekt
         */
        internal void Close()
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }

        /**
         * fuegt weitere Parameter zum Command hinzu
         */
        internal static void AddParameter(DbCommand dbCommand, string parameterName, object value)
        {
            DbParameter dbParameter = dbCommand.CreateParameter();
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;
            dbCommand.Parameters.Add(dbParameter);
        }
    }
}
