using System;
using System.Windows;                // add references: PresentationFramework + WindowsBase
using SeaSkyPresentation.Factories;
using CarLogic;
using CarLogic.Utils;
using CarLogic.Factories;
using CarData.Factories;

namespace App
{

    class Test_Injector
    {

        private string _dataSourcePath;
        private Application _app;
        private IDialog _dialog;
        private ILogicQueries _logicQueries;
        private ILogicCommands _logicCommands;
        private IDataRead _dataRead;
        private IDataWrite _dataWrite;

        void Run()
        {

            // MSAccess
            _dataSourcePath = @"C:\Users\Sören\Downloads\drive-download-20190707T124853Z-001\CarDatabase.accdb";

            // SQLite
            //_dataSourcePath   = @"C:\0Daten\CarDatabase.db";

            try
            {
                // CarData Layer
                _dataRead = SFactoryIDataRead.CreateInstance(EDataType.Access, _dataSourcePath);
                _dataRead.InitDb();
                _dataWrite = SFactoryIDataWrite.CreateInstance(EDataType.Access, _dataSourcePath);
                _dataWrite.InitDb();

                // CarLogic Layer
                _logicQueries = SFactoryILogicQueries.CreateInstance(_dataRead);
                _logicCommands = SFactoryILogicCommands.CreateInstance(_dataWrite);

                // CarPresentation Layer
                _dialog = SFactoryIDialog.CreateInstance(_logicQueries, _logicCommands);

                // Start the App
                _app = new Application();
                _app.Run(_dialog as Window);
            }
            catch (ApplicationException e)
            {
                MessageBox.Show(e.Message, "Fehler in der Anwendung", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Fehler in der Anwendung", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                if (_dataRead != null) _dataRead.CloseDb();
                if (_dataWrite != null) _dataWrite.CloseDb();
            }
        }

        [STAThread]
        static void Main()
        {

            new Test_Injector().Run();
        }
    }
}
