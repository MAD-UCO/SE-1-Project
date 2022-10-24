using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Semester_Project
{
    public class AudioMessage
    {
        public string fileName { get; set; }
        public string duration { get; set; }
        public string beginTime { get; set; }
        public string endTime { get; set; }

        public AudioMessage()
        {
            fileName = "";
            duration = "1s";
            beginTime = "1s";
            endTime = "2s";
        }
        public AudioMessage(string fileName)
        {
            this.fileName = fileName;
            duration = "10s";
            beginTime = "0s";
            endTime = "10s";
        }
        public AudioMessage(string fileName, string duration, string beginTime, string endTime)
        {
            this.fileName = fileName;
            this.duration = duration;
            this.beginTime = beginTime;
            this.endTime = endTime;
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
    }
}