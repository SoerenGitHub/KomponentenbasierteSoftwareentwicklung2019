﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightLogic;
using SeaSky_DB.Write;

namespace SeaSky_DB.Factories
{
    public static class SFDB_WriteF
    {
        /**
         * Methode um eine Instanz der Klasse CDB_WriteF zu erstellen
         * param    String der Pfad zur Datenbank
         * return FIDataWrite eine Instanz des Interfaces zur CDB_WriteF
         */
        public static FIDataWrite CreateInstance(string dbSourcePath)
        {
            FIDataWrite dbRead = null;
            DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory("System.Data.OleDb");
            string connection = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbSourcePath};";

            if (dbProviderFactory == null)
            {
                throw new ApplicationException("DbProviderFactory System.Data.OleDb konnte nicht erzeugt werden");
            }
            dbRead = new CDB_WriteF(dbProviderFactory, connection);

            return dbRead;
        }
    }
}
