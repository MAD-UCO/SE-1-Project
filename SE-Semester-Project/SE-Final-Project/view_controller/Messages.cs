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
        private int secondsText = 0;
        private string tempText;
        private Timer timerText;
        private string displayLocation = "";

        

        //Class constructor, do not edit. Use form load event for initialization
        public Messages()
        {
            InitializeComponent();
        }

        //Event handlers

        //Runs immediately after form loads
        private void Messages_Load(object sender, EventArgs e)
        {
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
            foreach(var t in textMessages)
            {
                DisplayTextMessage(t);
            }
        }

        //Executes each time the timer timerText ticks
        private void TimerText_Tick(object sender, EventArgs e)
        {
            //increment secondsText for each 1 second interval of the ticking timer
            secondsText++;

            //When the timer interval matches the message start time. Display message in correct location
            if(secondsText.ToString() == begin[0].Trim('s'))
            {
                if(displayLocation == "DEFAULT")
                {
                    lblDefault.Text = tempText;
                }
                else if(displayLocation == "CENTER")
                {
                    lblCenter.Text = tempText;
                }
                else if(displayLocation == "NORTH")
                {
                    lblNorth.Text = tempText;
                }
                else if(displayLocation == "SOUTH")
                {
                    lblSouth.Text = tempText;
                }
                else if(displayLocation == "EAST")
                {
                    lblEast.Text = tempText;
                }
                else
                {
                    lblWest.Text = tempText;    
                }
            }

            //When the timer interval matches the end time. Hide the message
            if(secondsText.ToString() == end[0].Trim('s'))
            {
                //reset and hide message
                timerText.Stop();
                secondsText = 0;
                if (displayLocation == "DEFAULT")
                {
                    lblDefault.Text = "";
                }
                else if (displayLocation == "CENTER")
                {
                    lblCenter.Text = "";
                }
                else if (displayLocation == "NORTH")
                {
                    lblNorth.Text = "";
                }
                else if (displayLocation == "SOUTH")
                {
                    lblSouth.Text = "";
                }
                else if (displayLocation == "EAST")
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
        private void DisplayTextMessage(TextMessage t)
        {
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
                displayLocation = "DEFAULT";
                tempText = t.text;

                //Setup timer using message start/duration fields
                tempText = t.text;
                begin[0] = t.beginTime;
                end[0] = t.duration;
                InitializeTimers();
                timerText.Start();

            }
            else if (t.region.ToUpper() == "CENTER")
            {
                //Enable the message label and store temp information for direct access from timer_tick event handler
                lblCenter.Visible = true;
                displayLocation = "CENTER";
                tempText = t.text;

                //Setup timer using message start/duration fields
                tempText = t.text;
                begin[0] = t.beginTime;
                end[0] = t.duration;
                InitializeTimers();
                timerText.Start();
            }
            else if (t.region.ToUpper() == "NORTH")
            {
                //Enable the message label and store temp information for direct access from timer_tick event handler
                lblNorth.Visible = true;
                displayLocation = "NORTH";
                tempText = t.text;

                //Setup timer using message start/duration fields
                tempText = t.text;
                begin[0] = t.beginTime;
                end[0] = t.duration;
                InitializeTimers();
                timerText.Start();
            }
            else if (t.region.ToUpper() == "SOUTH")
            {
                //Enable the message label and store temp information for direct access from timer_tick event handler
                lblSouth.Visible = true;
                displayLocation = "SOUTH";
                tempText = t.text;

                //Setup timer using message start/duration fields
                tempText = t.text;
                begin[0] = t.beginTime;
                end[0] = t.duration;
                InitializeTimers();
                timerText.Start();
            }
            else if (t.region.ToUpper() == "EAST")
            {
                //Enable the message label and store temp information for direct access from timer_tick event handler
                lblEast.Visible = true;
                displayLocation = "EAST";
                tempText = t.text;

                //Setup timer using message start/duration fields
                tempText = t.text;
                begin[0] = t.beginTime;
                end[0] = t.duration;
                InitializeTimers();
                timerText.Start();
            }
            else if (t.region.ToUpper() == "WEST")
            {
                //Enable the message label and store temp information for direct access from timer_tick event handler
                lblWest.Visible = true;
                displayLocation = "WEST";
                tempText = t.text;

                //Setup timer using message start/duration fields
                lblWest.Text = "";
                tempText = t.text;
                begin[0] = t.beginTime;
                end[0] = t.duration;
                InitializeTimers();
                timerText.Start();
            }
        }

        //Initialize timers for text, audio, or and video. 
        private void InitializeTimers()
        {
            //Initialize text timer
            timerText = new Timer();
            timerText.Tick += new EventHandler(TimerText_Tick);

            // 1000ms = 1s intervals
            timerText.Interval = 1000;
        }
    }
}

