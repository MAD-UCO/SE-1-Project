using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace SE_Semester_Project
{
    class Message
    {
        //Not sure how we are going to get the sender/receiver from message creation yet
        //Should be able to grab from UI and pass to network
        public string senderName { get; private set; }
        public string receiverName { get; private set; }

        public List<TextMessage> textMessages { get; private set; }


        //attributes below will be added once functionality for them is added

        //public List<AudioMessage> audioMessages { get; private set;}
        //public List<VideoMessage> videoMessages { get; private set;}

        public Message()
        {
            //potentially generate smil file template for this to be ready for other data
            //not sure if this makes sense to do yet
            senderName = "N/A";
            receiverName = "N/A";

            textMessages = new List<TextMessage>();
        }
        //filePath to smil file to parse to Message
        public Message(String filePath)
        {
            senderName = "";
            receiverName = "";
            textMessages = new List<TextMessage>();
            parseMessage(filePath);

        }

        //file path needs to be directly to file and not just file name
        public void parseMessage(String filePath) //Used to parse existing message into the container to be used
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading file to parse, path may not be correct");
                Console.WriteLine(e.Message);
                //Might be useful to add more detailed breakdown, probably get to this later
            }
            XmlNodeList headNodes = doc.GetElementsByTagName("head");
            XmlNodeList nodes = doc.GetElementsByTagName("body");

            foreach (XmlNode node in nodes)
            {
                XmlAttributeCollection collection = node.Attributes;
                foreach (XmlNode child in node)
                {
                    XmlAttributeCollection childCollection = child.Attributes;
                    if (child.Name == "smilText")
                    {
                        TextMessage text = new TextMessage(child.InnerText);
                        foreach (XmlAttribute attr in childCollection)
                        {
                            if (attr.Name == "dur")
                            {
                                text.duration = child.Attributes[attr.Name].Value;
                            }
                            else if (attr.Name == "begin")
                            {
                                text.beginTime = child.Attributes[attr.Name].Value;
                            }
                        }
                        textMessages.Add(text);
                    } //add more if statements or change to switch statement for audio and video
                      // looking into more efficient way to handle this but with the small file sizes im not sure speed will be a factor



                }
            }
        }
    }
}
