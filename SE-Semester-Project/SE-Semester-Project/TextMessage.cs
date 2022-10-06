using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Semester_Project
{
    class TextMessage
    {
        public string text { get; set; }
        //these are string but would like to change them to int or double
        //Only issue with this is getting the unit of measurement for them
        //From smil docs it seems time measurements can be made in sec or min
        public string duration { get; set; }
        public string beginTime { get; set; }
        public string endTime { get; set; }
        //need to implement this to show region to display to
        //public Region region

        public TextMessage()
        {
            text = "";
            duration = "5s";
            beginTime = "0s";
            endTime = "";
        }
        //Constructor to assign text value
        //might need default timing for messages where users dont specify duration
        public TextMessage(String t)
        {
            text = t;
            duration = "5s";
            beginTime = "0s";
            endTime = "";

        }
    }
}
