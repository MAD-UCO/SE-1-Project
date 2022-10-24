using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Semester_Project
{
    public class TextMessage
    {
        public string text { get; set; }
        //these are string but would like to change them to int or double
        //Only issue with this is getting the unit of measurement for them
        //From smil docs it seems time measurements can be made in sec or min

        //these times are in seconds and do not have implementation that supports minutes yet
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


        //getters are setup to return time in seconds
        //THESE ARE DANGEROUS CURRENTLY!!!! ONLY CAN HANDLE CORRECT INPUT!!!
        public int getDuration()
        {

            if (duration.Contains('m') && !duration.Contains('s'))
            {
                string[] times = duration.Split(' ');
                int x = Int32.Parse(times[1].Remove(times[1].Length - 1, 1));
                x = x + (60 * Int32.Parse(times[0].Remove(times[0].Length - 1, 1)));
                return x;

            }
            else if (duration.Contains('m') && !duration.Contains('s'))
            {
                string s = duration.Remove(duration.Length - 1, 1);
                int x = Int32.Parse(s);
                x = x * 60;
                return x;
            }
            else
            {
                return Int32.Parse(duration.Remove(duration.Length - 1, 1));
            }


        }
        public int getBeginTime()
        {
            if (beginTime.Contains('m') && !beginTime.Contains('s'))
            {
                string[] times = beginTime.Split(' ');
                int x = Int32.Parse(times[1].Remove(times[1].Length - 1, 1));
                x = x + (60 * Int32.Parse(times[0].Remove(times[0].Length - 1, 1)));
                return x;

            }
            else if (beginTime.Contains('m') && !beginTime.Contains('s'))
            {
                string s = beginTime.Remove(beginTime.Length - 1, 1);
                int x = Int32.Parse(s);
                x = x * 60;
                return x;
            }
            else
            {
                return Int32.Parse(beginTime.Remove(beginTime.Length - 1, 1));
            }
        }
        //pretty sure we arent going to use this but im going to ask about it tomorrow
        public int getEndTime()
        {
            return -1;
        }

    }
}
