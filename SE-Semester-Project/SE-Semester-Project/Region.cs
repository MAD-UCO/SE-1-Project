using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Semester_Project
{
    class Region
    {

        //might change these to read only style constants for a set list of positions to use
       
        public string regionID { get; private set; }
        public string regionTop { get; private set; }
        public string regionLeft { get; private set; }

        public string regionWidth { get; private set; }
        public string regionHeight { get; private set; }
        public string textWrapOption { get; private set; }

        public Region(string id, string top, string left, string width, string height, string textWrap)
        {
            regionID = id;
            regionTop = top;
            regionLeft = left;
            regionWidth = width;
            regionHeight = height;
            textWrapOption = textWrap;
        }
    }
}
