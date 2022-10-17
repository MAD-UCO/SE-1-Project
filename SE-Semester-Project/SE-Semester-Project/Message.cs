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
        //smilFileName should be in the working directory
        public string smilFileName { get;  set; }
        //need to figure out naming convention for smilFiles
        public string senderName { get;  set; }
        public string receiverName { get;  set; }

        public List<TextMessage> textMessages { get; private set; }
        public List<AudioMessage> audioMessages { get; private set;}
        public List<VideoMessage> videoMessages { get; private set;}



        public Message()
        {
            //potentially generate smil file template for this to be ready for other data
            //not sure if this makes sense to do yet
            senderName = "N/A";
            receiverName = "N/A";

            smilFileName = "";
            textMessages = new List<TextMessage>();
            audioMessages = new List<AudioMessage>();
            videoMessages = new List<VideoMessage>();
        }
        //filePath to smil file to parse to Message
        public Message(String filePath)
        {
            senderName = "";
            receiverName = "";
            smilFileName = "";//need to change to extract file name from filePath
            textMessages = new List<TextMessage>();
            audioMessages = new List<AudioMessage>();
            videoMessages = new List<VideoMessage>();
            //need to generate filename based on date or if we add user named files later that implementation aswell
            parseMessage(filePath);

        }
        public Message(string smilFileName, string senderName, string receiverName, List<TextMessage> textMessages, List<AudioMessage> audioMessages, List<VideoMessage> videoMessages) : this(smilFileName)
        {
            this.senderName = senderName;
            this.receiverName = receiverName;
            this.textMessages = textMessages;
            this.audioMessages = audioMessages;
            this.videoMessages = videoMessages;
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

        public void generateMessageFile()
        {
            //XmlDocument doc = new XmlDocument();
            //doc may not be needed when creating files, although it might have more uses?
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            //settings.NewLineOnAttributes = true;
            XmlWriter writer = XmlWriter.Create(smilFileName, settings);

            writer.WriteStartElement("smil");
            writer.WriteStartElement("head");
            writer.WriteStartElement("body");

            if (textMessages.Count >= 1)
            {

                for (int i = 0; i < textMessages.Count; i++)
                {
                    writer.WriteStartElement("smilText");
                    writer.WriteAttributeString("dur", textMessages[i].duration);
                    writer.WriteAttributeString("begin", textMessages[i].beginTime);
                    //not including end for now cause it seems like it might not be needed
                    writer.WriteString(textMessages[i].text);

                    writer.WriteEndElement();
                }

            }
            if (audioMessages.Count >= 1)
            {

                for (int i = 0; i < audioMessages.Count; i++)
                {
                    writer.WriteStartElement("audio");
                    writer.WriteAttributeString("dur", audioMessages[i].duration);
                    writer.WriteAttributeString("begin", audioMessages[i].beginTime);
                    writer.WriteEndElement();
                }
            }
            if (videoMessages.Count >= 1)
            {

                for (int i = 0; i < videoMessages.Count; i++)
                {
                    writer.WriteStartElement("video");
                    writer.WriteAttributeString("dur", videoMessages[i].duration);
                    writer.WriteAttributeString("begin", videoMessages[i].beginTime);
                    writer.WriteEndElement();
                }
            }
            writer.Flush();
            writer.Close();

        }

        public void EditMessageFile(string filePath)
        {
            //check if fileName matches smil file in directory
            //might just have it delete file contents and rewrite the whole file so this may not get used
        }

        public void AddTextMessage(TextMessage text)
        {
            textMessages.Add(text);
        }
        public void AddAudioMessage(AudioMessage audio)
        {
            audioMessages.Add(audio);
        }
        public void AddVideoMessage(VideoMessage video)
        {
            videoMessages.Add(video);
        }
        /*public Message generateMessage(string text, string smilFileName)
        {

        }
        public Message generateMessage(string[] text)
        {

        }
        public Message generateMessage(TextMessage text)
        {
            //generate node
        }
        public Message generateMessage(TextMessage text, AudioMessage audio)
        {

        }
        public Message generateMessage(TextMessage text, AudioMessage audio, VideoMessage video)
        {

        }*/
    }
}
