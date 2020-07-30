using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipLogic;
using SeaSky_DB.Read;

namespace SeaSky_DB.Factories
{
    public static class SFDB_ReadS
    {
        /**
         * Methode um eine Instanz der Klasse CDB_ReadS zu erstellen
         * param    String der Pfad zur Datenbank
         * return FIDataWrite eine Instanz des Interfaces zur CDB_ReadS
         */
        public static SIDataRead CreateInstance(string dbSourcePath)
        {
            SIDataRead dbRead = null;
            DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory("System.Data.OleDb");
            string connection = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbSourcePath};";

            if(dbProviderFactory == null)
            {
                throw new ApplicationException("DbProviderFactory System.Data.OleDb konnte nicht erzeugt werden");
            }
            dbRead = new CDB_ReadS(dbProviderFactory, connection);

            return dbRead;
        }
    }
}
