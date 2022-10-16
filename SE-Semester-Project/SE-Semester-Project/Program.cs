using System;
using System.Net.Sockets;
using System.Text;
using System.Runtime.Serialization;
using MoveBitMessaging;
using System.Runtime.InteropServices;

namespace SE_Semester_Project
{
    internal static class Program
    {
        // TODO Merge winforms stuff with app stuff later
        // TODO better organize this to avoid mixing frontend and backend stuff

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
#if !USE_COMMANDLINE
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
#else
            AllocConsole();
#endif
            NetworkClient.start();
        }

#if USE_COMMANDLINE
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
#endif
    }
}