using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Final_Project
{
    // Notification for something the user should know about
    internal class Notification
    {

        public string notification_message;
        public Notification(string message)
        {
            notification_message = message;
        }
    }
}
