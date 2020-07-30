using System;
using static System.Net.Mime.MediaTypeNames;

namespace Startup
{
    class Program
    {
        private string _dataSourcePath;
        private Application _app;
        private FIDialog _dialog;
        private FILogicQueries _logicQueries;
        private FILogicCommands _logicCommands;
        private FIDataRead _dataRead;
        private FIDataWrite _dataWrite;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
