using SE_Semester_Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
#if !USE_COMMANDLINE
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
#else
            AllocConsole();
            NetworkClient.Start();
#endif
        }

#if USE_COMMANDLINE
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
#endif  
    }
}
