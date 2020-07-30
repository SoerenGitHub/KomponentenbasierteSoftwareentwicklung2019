using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipLogic;
using SeaSky_DB.Write;

namespace SeaSky_DB.Factories
{
    public static class SFDB_WriteS
    {
        /**
         * Methode um eine Instanz der Klasse CDB_WriteS zu erstellen
         * param    String der Pfad zur Datenbank
         * return FIDataWrite eine Instanz des Interfaces zur CDB_WriteS
         */
        public static SIDataWrite CreateInstance(string dbSourcePath)
        {
            SIDataWrite dbRead = null;
            DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory("System.Data.OleDb");
            string connection = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbSourcePath};";

            if (dbProviderFactory == null)
            {
                throw new ApplicationException("DbProviderFactory System.Data.OleDb konnte nicht erzeugt werden");
            }
            dbRead = new CDB_WriteS(dbProviderFactory, connection);

            return dbRead;
        }
    }
}
