using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using SE_Final_Project.model;

namespace SE_Final_Project
{
    public class Message
    {
        
        
        public string smilFilePath { get;  set; }
        public string smilFileName { get; set; }    
        public string senderName { get; set; }
        public string receiverName { get; set; }
        private bool parallelMessages { get; set; }

        public List<TextMessage> textMessages { get; private set; }
        public List<AudioMessage> audioMessages { get; private set; }
        public List<VideoMessage> videoMessages { get; private set; }
        public List<ImageMessage> imageMessages  { get; private set; }

        



        public Message()
        {
            //potentially generate smil file template for this to be ready for other data
            //not sure if this makes sense to do yet
            senderName = "N/A";
            receiverName = "N/A";

            smilFilePath = "";
            textMessages = new List<TextMessage>();
            audioMessages = new List<AudioMessage>();
            videoMessages = new List<VideoMessage>();
            imageMessages = new List<ImageMessage>();
        }
        //filePath to smil file to parse to Message
        public Message(String filePath)
        {
            senderName = "";
            receiverName = "";
            smilFilePath = "";//need to change to extract file name from filePath
            textMessages = new List<TextMessage>();
            audioMessages = new List<AudioMessage>();
            videoMessages = new List<VideoMessage>();
            //need to generate filename based on date or if we add user named files later that implementation aswell
            ParseMessage(filePath);

        }
        //constructor for handling the recieving the message from the server
        //fileName should include extension
        public Message(String fileName, String fileStringContents)
        {
            senderName = "N/A";
            receiverName = "N/A";

            smilFilePath = "";
            textMessages = new List<TextMessage>();
            audioMessages = new List<AudioMessage>();
            videoMessages = new List<VideoMessage>();
            imageMessages = new List<ImageMessage>();

            try
            {
                File.WriteAllText(Environment.CurrentDirectory + "/" + fileName, fileStringContents);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to generate file from text");
            }
            this.ParseMessage(fileName);
        }
        public Message(string smilFileName, string senderName, string receiverName, List<TextMessage> textMessages, List<AudioMessage> audioMessages, List<VideoMessage> videoMessages) : this(smilFileName)
        {
            this.senderName = senderName;
            this.receiverName = receiverName;
            this.textMessages = textMessages;
            this.audioMessages = audioMessages;
            this.videoMessages = videoMessages;
        }


        public string GetSmilText(string filePath)
        {
            string x = "";
            try
            {
                x=System.IO.File.ReadAllText(filePath);
            }
            catch (FileNotFoundException nofile)
            {
                // TODO
                x = Environment.CurrentDirectory + filePath;
                x = System.IO.File.ReadAllText(x);
            }

            return x;

        }


        //file path needs to be directly to file and not just file name
        public bool ParseMessage(String filePath) //Used to parse existing message into the container to be used
        {
            filePath = filePath.Trim();
            smilFileName = Path.GetFileNameWithoutExtension(filePath);

            if (filePath == null)
            {
                return false;
            }
            XmlDocument doc = new XmlDocument();
            
            try
            {
                doc.Load(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading file to parse, path may not be correct");
                Console.WriteLine(e.Message);
                return false;
                //Might be useful to add more detailed breakdown, probably get to this later
            }
            XmlNodeList headNodes = doc.GetElementsByTagName("head");
            XmlNodeList nodes = doc.GetElementsByTagName("body");
            XmlNodeList parNodes = doc.GetElementsByTagName("par");

            if (parNodes.Count > 0)
            {
                parallelMessages = true;
                foreach (XmlNode node in parNodes)
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
                                else if (attr.Name == "region")
                                {
                                    text.region = child.Attributes[attr.Name].Value;
                                }
                            }
                            textMessages.Add(text);
                        }
                        else if (child.Name == "audio")
                        {
                            AudioMessage audio = new AudioMessage(child.InnerText);
                            foreach (XmlAttribute attr in childCollection)
                            {
                                if (attr.Name == "src")
                                {
                                    audio.filePath = child.Attributes[attr.Name].Value;
                                }
                                else if (attr.Name == "dur")
                                {
                                    audio.duration = child.Attributes[attr.Name].Value;
                                }
                                else if (attr.Name == "begin")
                                {
                                    audio.beginTime = child.Attributes[attr.Name].Value;
                                }
                            }
                            audioMessages.Add(audio);

                        }
                        else if (child.Name == "video")
                        {
                            VideoMessage video = new VideoMessage(child.InnerText);
                            foreach (XmlAttribute attr in childCollection)
                            {
                                if (attr.Name == "src")
                                {
                                    video.filePath = child.Attributes[attr.Name].Value;
                                }
                                else if (attr.Name == "dur")
                                {
                                    video.duration = child.Attributes[attr.Name].Value;
                                }
                                else if (attr.Name == "begin")
                                {
                                    video.beginTime = child.Attributes[attr.Name].Value;
                                }
                            }
                            videoMessages.Add(video);
                        }
                        else if (child.Name == "img")
                        {
                            ImageMessage image = new ImageMessage(child.InnerText);
                            foreach(XmlAttribute attr in childCollection)
                            {
                                if (attr.Name == "src")
                                {
                                    image.filePath = child.Attributes[attr.Name].Value;
                                }
                                else if (attr.Name == "dur")
                                {
                                    image.duration = child.Attributes[attr.Name].Value;
                                }
                                else if (attr.Name == "begin")
                                {
                                    image.beginTime = child.Attributes[attr.Name].Value;
                                }
                            }
                        }




                    }
                }
                return true;
            }
            else
            {
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
                                else if (attr.Name == "region")
                                {
                                    text.region = child.Attributes[attr.Name].Value;
                                }
                            }
                            textMessages.Add(text);
                        }
                        else if (child.Name == "audio")
                        {
                            AudioMessage audio = new AudioMessage(child.InnerText);
                            foreach (XmlAttribute attr in childCollection)
                            {
                                if (attr.Name == "src")
                                {
                                    audio.filePath = child.Attributes[attr.Name].Value;
                                }
                                else if (attr.Name == "dur")
                                {
                                    audio.duration = child.Attributes[attr.Name].Value;
                                }
                                else if (attr.Name == "begin")
                                {
                                    audio.beginTime = child.Attributes[attr.Name].Value;
                                }
                            }
                            audioMessages.Add(audio);

                        }
                        else if (child.Name == "video")
                        {
                            VideoMessage video = new VideoMessage(child.InnerText);
                            foreach (XmlAttribute attr in childCollection)
                            {
                                if (attr.Name == "src")
                                {
                                    video.filePath = child.Attributes[attr.Name].Value;
                                }
                                else if (attr.Name == "dur")
                                {
                                    video.duration = child.Attributes[attr.Name].Value;
                                }
                                else if (attr.Name == "begin")
                                {
                                    video.beginTime = child.Attributes[attr.Name].Value;
                                }
                            }
                            videoMessages.Add(video);
                        }
                        else if (child.Name == "img")
                        {
                            ImageMessage image = new ImageMessage(child.InnerText);
                            foreach(XmlAttribute attr in childCollection)
                            {
                                if (attr.Name == "src")
                                {
                                    image.filePath = child.Attributes[attr.Name].Value;
                                }
                                else if (attr.Name == "dur")
                                {
                                    image.duration = child.Attributes[attr.Name].Value;
                                }
                                else if (attr.Name == "begin")
                                {
                                    image.beginTime = child.Attributes[attr.Name].Value;
                                }
                            }
                        }



                    }
                }
                return true; 
            }

        }

        //potentially change this to return bool to show if generation worked
        //this will currently overwrite files without warning if the file already exists
        public bool GenerateMessageFile()
        {
            
            try
            { 
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                //settings.NewLineOnAttributes = true;
                XmlWriter writer = XmlWriter.Create(Environment.CurrentDirectory + smilFilePath, settings);

                writer.WriteStartElement("smil");
                writer.WriteStartElement("head");
                writer.WriteStartElement("body");
                if (parallelMessages)
                {
                    writer.WriteStartElement("par");
                }

                if (textMessages.Count >= 1)
                {

                    for (int i = 0; i < textMessages.Count; i++)
                    {
                        writer.WriteStartElement("smilText");
                        writer.WriteAttributeString("dur", textMessages[i].duration);
                        writer.WriteAttributeString("begin", textMessages[i].beginTime);
                        writer.WriteAttributeString("region", textMessages[i].region);
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
                        writer.WriteAttributeString("src", audioMessages[i].filePath);
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
                        writer.WriteAttributeString("src", videoMessages[i].filePath);
                        writer.WriteAttributeString("dur", videoMessages[i].duration);
                        writer.WriteAttributeString("begin", videoMessages[i].beginTime);
                        writer.WriteEndElement();
                    }
                }
                if( imageMessages.Count >= 1)
                {
                    for( int i = 0; i < imageMessages.Count; i++)
                    {
                        writer.WriteStartAttribute("img");
                        writer.WriteAttributeString("src", imageMessages[i].filePath);
                        writer.WriteAttributeString("dur", imageMessages[i].duration);
                        writer.WriteAttributeString("begin", imageMessages[i].beginTime);
                        writer.WriteEndElement();
                    }
                }
                writer.Flush();
                writer.Close();
                return true;
            }
            catch(Exception ex)
            {
                return false;
                Console.WriteLine("Message Generation failed");
            }
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
        public string getFileName()
        {
            if(Path.HasExtension(smilFilePath) && Path.IsPathRooted(smilFilePath))
            {
                return Path.GetFileName(smilFilePath);
            }
            else if (Path.HasExtension(smilFilePath))
            {
                return Path.GetFileName(smilFilePath);
            }


            return "No file name found";
        }

        public void SetFilePath(string fileName)
        {
            //potentially needs changing if we decide how the files are going to be stored
            smilFilePath = Environment.CurrentDirectory + fileName;
        }

        public void UpdateFilePaths()
        {
            
            foreach(AudioMessage audio in audioMessages)
            {
                audio.filePath =Environment.CurrentDirectory + Path.GetFileName(audio.filePath);
            }
            foreach(VideoMessage video in videoMessages)
            {
                video.filePath =Environment.CurrentDirectory + Path.GetFileName(video.filePath);
            }

        }
        public void setSmilFilePath(String smilFilePath)
        {
            this.smilFilePath = smilFilePath;
        }

        public void setSenderName(String senderName)
        {
            this.senderName = senderName;
        }

        public void setReceiverName(String receiverName)
        {
            this.receiverName = receiverName;

        }
    }
}