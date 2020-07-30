using FlightLogic;
using FlightLogic.Factories;
using FlightLogic.Utils;
using SeaSky_DB;
using SeaSky_DB.Factories;
using SeaSkyPresentation.Factories;
using ShipLogic;
using ShipLogic.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Startup_Layer
{
    class Injector
    {
        private string _dataSourcePath;
        private Application _app;
        private FIDialog _dialog;
        private FILogicQueries _FlogicQueries;
        private FILogicCmds _FlogicCommands;
        private ISLogicQueries _SlogicQueries;
        private ISLogicCommands _SlogicCommands;
        private FIDataRead _FdataRead;
        private FIDataWrite _FdataWrite;
        private SIDataRead _SdataRead;
        private SIDataWrite _SdataWrite;

        void Run()
        {

            // MSAccess
            _dataSourcePath = @"seasky.accdb";

            // SQLite
            //_dataSourcePath   = @"C:\0Daten\CarDatabase.db";

            try
            {
                // FlightData Layer
                _FdataRead = SFDB_ReadF.CreateInstance(_dataSourcePath);
                _FdataRead.InitDb();
                _FdataWrite = SFDB_WriteF.CreateInstance(_dataSourcePath);
                _FdataWrite.InitDb();
                // ShipInfoData Layer
                _SdataRead = SFDB_ReadS.CreateInstance(_dataSourcePath);
                _SdataRead.InitDb();
                _SdataWrite = SFDB_WriteS.CreateInstance(_dataSourcePath);
                _SdataWrite.InitDb();

                // FlightLogic Layer
                _FlogicQueries = FFactoryQueries.CreateInstance(_FdataRead);
                _FlogicCommands = FFactoryILogicCmds.CreateInstance(_FdataWrite);
                // FlightLogic Layer
                _SlogicQueries = SFactoryISLogicQueries.CreateInstance(_SdataRead);
                _SlogicCommands = SFactoryISLogicCommands.CreateInstance(_SdataWrite);


                // CarPresentation Layer
                _dialog = SFactoryIDialog.CreateInstance(_FlogicQueries, _FlogicCommands, _SlogicQueries, _SlogicCommands);

                // Start the App
                _app = new Application();
                _app.Run(_dialog as Window);
            }
            catch (ApplicationException e)
            {
                MessageBox.Show(e.Message, "Fehler in der Anwendung 1", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Fehler in der Anwendung 2", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                if (_FdataRead != null) _FdataRead.CloseDb();
                if (_FdataWrite != null) _FdataWrite.CloseDb();
                if (_SdataRead != null) _SdataRead.CloseDb();
                if (_SdataWrite != null) _SdataWrite.CloseDb();
            }
        }

        [STAThread]
        static void Main()
        {

            new Injector().Run();
        }
    }
}
