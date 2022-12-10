using SE_Semester_Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using MoveBitMessaging;
using System.Runtime.Remoting.Messaging;

namespace SE_Final_Project
{
   
    public partial class Messages : Form
    {
        //private variables
        private List<Label> locationLabels = new List<Label>();
        private List<Message> messageObjects = new List<Message>();
        private Main main = (Main)Application.OpenForms["Main"];
        private string[] begin = new string[3];
        private string[] end = new string[3];
        private string[] texts = new string[3];
        private string[] textsStart = new string[3];
        private string[] textsEnd = new string[3];
        private int secondsText, second2, seconds3, seconds4, seconds5 = 0;
        private string videoBegin, videoEnd, videoURL = "";
        private string audioBegin, audioEnd, audioURL = "";
        private SoundPlayer soundPlayer;

        // private string tempText;
        private List<Timer> timers = new List<Timer>();
        Timer timer1, timer2, timer3;
        private string[] displayLocations = new string[3];

        

        //Class constructor, do not edit. Use form load event for initialization
        public Messages()
        {
            InitializeComponent();
        }

        //Event handlers

        //Runs immediately after form loads
        private void Messages_Load(object sender, EventArgs e)
        {
            //Initialize timers
            InitializeTimers();

            //Initialize list of location labels
            locationLabels.Add(lblCenter);
            locationLabels.Add(lblNorth);
            locationLabels.Add(lblSouth);
            locationLabels.Add(lblEast);
            locationLabels.Add(lblWest);
            locationLabels.Add(lblDefault);

            //Set initial visibility for labels to false
            lblDefault.Visible = false;
            lblCenter.Visible = false;
            lblNorth.Visible = false;
            lblSouth.Visible = false;
            lblEast.Visible = false;
            lblWest.Visible = false;
            playerMessages.Visible = false;
          //  textAudio.Visible = false;

        }

        //Executes each time btnBack is clicked by the user
        private void BtnBack_Click(object sender, EventArgs e)
        {
            //Create a new frmMain object and display it in the current location.
            this.Hide();
            main.Show();
            
            
        }

        //Executes each time an item is selected in the drop down box cboMessages
        private void CboMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = cboMessages.SelectedIndex;

            //Use selected index of cboMessages to find message object in parallel list. Then display text
            SE_Final_Project.Message temp = (SE_Final_Project.Message)messageObjects[selectedIndex];
            List<TextMessage> textMessages = temp.textMessages;
            List<AudioMessage> audioMessages = temp.audioMessages;
            List<VideoMessage> videoMessages = temp.videoMessages;
            /*grpTextCanvas.Visible = false;
            playerMessages.Visible = true;*/
            foreach (var v in videoMessages)
            {
                Console.WriteLine(v.filePath);
                // playerMessages.URL = videoMessages[0].filePath;
                videoURL = videoMessages[0].filePath;
                // end1 = v.endTime;
                

                videoBegin = videoMessages[0].beginTime;
                //  begin2 = t.beginTime;
                videoEnd = videoMessages[0].duration;
                timer4.Start();
            }
            foreach (var a in audioMessages)
            {
                Console.WriteLine(a.filePath);
                // playerMessages.URL = videoMessages[0].filePath;
                audioURL = audioMessages[0].filePath;
                // end1 = v.endTime;
                Console.WriteLine(a.beginTime);
                Console.WriteLine(a.duration);

                audioBegin = audioMessages[0].beginTime;
                //  begin2 = t.beginTime;
                audioEnd = audioMessages[0].duration;
                textAudio.Text = "AUDIO ADDED";
                timer5.Start();
            }
            if (textMessages.Count >= 0)
            {

                for (int i = 0; i < textMessages.Count; i++)
                {
                 

                    texts[i] = textMessages[i].text;
                    textsStart[i] = textMessages[i].beginTime;
                    textsEnd[i] = textMessages[i].duration;
                    Console.WriteLine(texts[i]);
                    Console.WriteLine(textsStart[i]);
                    Console.WriteLine(textsEnd[i]);

                    DisplayTextMessage(textMessages[i], i);

                }
            }
            if (textMessages.Count > 0)
            {
                if (textMessages.Count == 1)
                {
                    timers[0].Start();
                }
                if (textMessages.Count == 2)
                {
                    timers[0].Start();
                    timers[1].Start();
                }
                if (textMessages.Count == 3)
                {
                    timers[0].Start();
                    timers[1].Start();
                    timers[2].Start();
                }
            }
            else
            {
             //   grpTextCanvas.Visible = false;
              //  playerMessages.Visible = true;
            }
            main.message = new Message();

        }

        //Executes each time the timer timerText ticks
        private void Timer1_Tick(object sender, EventArgs e)
        {
            //increment secondsText for each 1 second interval of the ticking timer
            secondsText++;

            //When the timer interval matches the message start time. Display message in correct location
            if(secondsText.ToString() == textsStart[0].Trim('s'))
            {
                if (displayLocations[0] == "DEFAULT")
                {
                    lblDefault.Text = texts[0];
                }
                else if(displayLocations[0] == "CENTER")
                {
                    lblCenter.Text = texts[0];
                }
                else if(displayLocations[0] == "NORTH")
                {
                    lblNorth.Text = texts[0];
                }
                else if(displayLocations[0] == "SOUTH")
                {
                    lblSouth.Text = texts[0];
                }
                else if(displayLocations[0] == "EAST")
                {
                    lblEast.Text = texts[0];
                }
                else
                {
                    lblWest.Text = texts[0];    
                }
            }

            //When the timer interval matches the end time. Hide the message
            if(secondsText.ToString() == textsEnd[0].Trim('s'))
            {
                //reset and hide message
                timer1.Stop();
                secondsText = 0;
                if (displayLocations[0] == "DEFAULT")
                {
                    lblDefault.Text = "";
                }
                else if (displayLocations[0] == "CENTER")
                {
                    lblCenter.Text = "";
                }
                else if (displayLocations[0] == "NORTH")
                {
                    lblNorth.Text = "";
                }
                else if (displayLocations[0] == "SOUTH")
                {
                    lblSouth.Text = "";
                }
                else if (displayLocations[0] == "EAST")
                {
                    lblEast.Text = "";
                }
                else
                {
                    lblWest.Text = "";
                }
            }
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            seconds5++;
            Console.WriteLine(seconds5.ToString());

            if (seconds5.ToString() == audioBegin.Trim('s'))
            {
              //  grpTextCanvas.Visible = false;
              //  playerMessages.Visible = false;
                textAudio.Visible = true;
                textAudio.Text = "AUDIO PLAYING... ENJOY";

                soundPlayer = new SoundPlayer(@"" + audioURL);
              //  soundPlayer.SoundLocation = audioURL;
                soundPlayer.Play();
                

                Console.WriteLine(seconds5.ToString() + "   seconds here at 5");
            }


            if (seconds5.ToString() == audioEnd.Trim('s'))
            {
                //lblDefault.Text = tempText;
                Console.WriteLine("end @ 5");
                soundPlayer.Stop();
                textAudio.Text = "";

              //  seconds5 = 0;
            }

            if (seconds5 > 11)
            {
                //lblDefault.Text = tempText;
                timer5.Stop();
                seconds5 = 0;
                textAudio.Visible = false;
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            //increment secondsText for each 1 second interval of the ticking timer
            second2++;

            //When the timer interval matches the message start time. Display message in correct location
            if (second2.ToString() == textsStart[1].Trim('s'))
            {
                if (displayLocations[1] == "DEFAULT")
                {
                    lblDefault.Text = texts[1];
                }
                else if (displayLocations[1] == "CENTER")
                {
                    lblCenter.Text = texts[1];
                }
                else if (displayLocations[1] == "NORTH")
                {
                    lblNorth.Text = texts[1];
                }
                else if (displayLocations[1] == "SOUTH")
                {
                    lblSouth.Text = texts[1];
                }
                else if (displayLocations[1] == "EAST")
                {
                    lblEast.Text = texts[1];
                }
                else
                {
                    lblWest.Text = texts[1];
                }
            }

            //When the timer interval matches the end time. Hide the message
            if (second2.ToString() == textsEnd[1].Trim('s'))
            {
                //reset and hide message
                timer2.Stop();
                 second2 = 0;
                if (displayLocations[1] == "DEFAULT")
                {
                    lblDefault.Text = "";
                }
                else if (displayLocations[1] == "CENTER")
                {
                    lblCenter.Text = "";
                }
                else if (displayLocations[1] == "NORTH")
                {
                    lblNorth.Text = "";
                }
                else if (displayLocations[1] == "SOUTH")
                {
                    lblSouth.Text = "";
                }
                else if (displayLocations[1] == "EAST")
                {
                    lblEast.Text = "";
                }
                else
                {
                    lblWest.Text = "";
                }
            }
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            //increment secondsText for each 1 second interval of the ticking timer
            seconds3++;

            if (seconds3.ToString() == textsStart[2].Trim('s'))
            {
                if (displayLocations[2] == "DEFAULT")
                {
                    lblDefault.Text = texts[2];
                }
                else if (displayLocations[2] == "CENTER")
                {
                    lblCenter.Text = texts[2];
                }
                else if (displayLocations[2] == "NORTH")
                {
                    lblNorth.Text = texts[2];
                }
                else if (displayLocations[2] == "SOUTH")
                {
                    lblSouth.Text = texts[2];
                }
                else if (displayLocations[2] == "EAST")
                {
                    lblEast.Text = texts[2];
                }
                else
                {
                    lblWest.Text = texts[2];
                }
            }

            //When the timer interval matches the end time. Hide the message
            if (seconds3.ToString() == textsEnd[2].Trim('s'))
            {
                Console.WriteLine("Made it to timer stop 3");
                //reset and hide message
                timer3.Stop();
                seconds3 = 0;
                if (displayLocations[2] == "DEFAULT")
                {
                    lblDefault.Text = "";
                }
                else if (displayLocations[2] == "CENTER")
                {
                    lblCenter.Text = "";
                }
                else if (displayLocations[2] == "NORTH")
                {
                    lblNorth.Text = "";
                }
                else if (displayLocations[2] == "SOUTH")
                {
                    lblSouth.Text = "";
                }
                else if (displayLocations[2] == "EAST")
                {
                    lblEast.Text = "";
                }
                else
                {
                    lblWest.Text = "";
                }
            }
        }

        //Getters

        //Return cboMessages control object
        public ComboBox GetCboMessages()
        {
            return cboMessages;
        }

        //Return all labels
        public List<Label> GetLabels()
        {
            //load list and return
            return locationLabels;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            seconds4++;
            Console.WriteLine(seconds4.ToString());

            if (seconds4.ToString() == videoBegin.Trim('s'))
            {
                playerMessages.Visible = true;
                playerMessages.URL = videoURL;
                playerMessages.Ctlcontrols.play();
                textAudio.Visible = false;


                Console.WriteLine(seconds4.ToString() + "seconds here at 4");
            }


            if (seconds4.ToString() == videoEnd.Trim('s'))
            {
                //lblDefault.Text = tempText;
                Console.WriteLine("end @ 4");
                playerMessages.Ctlcontrols.stop();

            }

            if (seconds4 > 11)
            {
                //lblDefault.Text = tempText;
                timer4.Stop();
                seconds4 = 0;
                textAudio.Visible = true;

            }


        }

        //Shut down all processes when user exits program
        private void Messages_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.Shutdown();
            NetworkClient.Logout();
            Environment.Exit(0);
        }

        //return message object list
        public List<Message> GetMessageObjects()
        {
            return this.messageObjects;
        }

        //Private Operations

        //Display text message in the correct location
        private void DisplayTextMessage(TextMessage t, int i)
        {
            int count = i;
            //Clear old text
            lblDefault.Text = "";
            lblCenter.Text = "";
            lblNorth.Text = "";
            lblSouth.Text = "";
            lblEast.Text = "";
            lblWest.Text = "";

            if (t.region.ToUpper() == "DEFAULT")
            {
                //Enable the message label and store temp information for direct access from timer_tick event handler
                lblDefault.Visible = true;
                displayLocations[count] = "DEFAULT";

                //Setup timer using message start/duration fields
                texts[count] = t.text;
                textsStart[count] = t.beginTime;
                textsEnd[count] = t.duration;
            }
            else if (t.region.ToUpper() == "CENTER")
            {
                //Enable the message label and store temp information for direct access from timer_tick event handler
                lblCenter.Visible = true;
                displayLocations[count] = "CENTER";

                //Setup timer using message start/duration fields
                texts[count] = t.text;
                textsStart[count] = t.beginTime;
                textsEnd[count] = t.duration;
            }
            else if (t.region.ToUpper() == "NORTH")
            {
                //Enable the message label and store temp information for direct access from timer_tick event handler
                lblNorth.Visible = true;
                displayLocations[count] = "NORTH";

                //Setup timer using message start/duration fields
                texts[count] = t.text;
                textsStart[count] = t.beginTime;
                textsEnd[count] = t.duration;
            }
            else if (t.region.ToUpper() == "SOUTH")
            {
                //Enable the message label and store temp information for direct access from timer_tick event handler
                lblSouth.Visible = true;
                displayLocations[count] = "SOUTH";

                //Setup timer using message start/duration fields
                texts[count] = t.text;
                textsStart[count] = t.beginTime;
                textsEnd[count] = t.duration;

            }
            else if (t.region.ToUpper() == "EAST")
            {
                //Enable the message label and store temp information for direct access from timer_tick event handler
                lblEast.Visible = true;
                displayLocations[count] = "EAST";

                //Setup timer using message start/duration fields
                texts[count] = t.text;
                textsStart[count] = t.beginTime;
                textsEnd[count] = t.duration;
            }
            else if (t.region.ToUpper() == "WEST")
            {
                //Enable the message label and store temp information for direct access from timer_tick event handler
                lblWest.Visible = true;
                displayLocations[count] = "WEST";

                //Setup timer using message start/duration fields
                lblWest.Text = "";
                texts[count] = t.text;
                textsStart[count] = t.beginTime;
                textsEnd[count] = t.duration;
            }

        }

        //Initialize timers for text, audio, or and video. 
        private void InitializeTimers()
        {
            //Initialize text timer
            timer1 = new Timer();
            timers.Add(timer1);
            timers[0].Tick += new EventHandler(Timer1_Tick);
            timers[0].Interval = 1000;

            timer2 = new Timer();
            timers.Add(timer2);
            timers[1].Tick += new EventHandler(Timer2_Tick);
            timers[1].Interval = 1000;

            timer3 = new Timer();
            timers.Add(timer3);
            timers[2].Tick += new EventHandler(Timer3_Tick);
            timers[2].Interval = 1000;
        }
    }
}

