using SE_Semester_Project;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
#if USE_COMMANDLINE
using System.Runtime.InteropServices;
#endif

namespace SE_Final_Project
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

#if PARSING_UNIT_TESTING
            ParsingTester.RunTests();
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            NetworkClient.Start();
            Application.Run(new Login());
            NetworkClient.Shutdown();

            
        }
    }
}
