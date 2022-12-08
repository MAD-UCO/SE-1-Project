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

namespace SE_Final_Project
{
   
    public partial class Messages : Form
    {
        //private variables
        private List<Message> incomingMessages = new List<Message>();
        private List<Label> locationLabels = new List<Label>();
        private Main main = (Main)Application.OpenForms["Main"];

        private string[] begin;
        private string end1,end2, end3 = "";
        private int seconds1, seconds2, seconds3 = 0;
        private string tempText;

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

            //Get new messages from the Network Client when form loads
            incomingMessages = NetworkClient.GetNewMessages();
            foreach(var m in incomingMessages)
            {
                cboMessages.Items.Add(m);
            }
            

            //Set initial visibility for labels to false
            lblDefault.Visible = false;
            lblCenter.Visible = false;
            lblNorth.Visible = false;
            lblSouth.Visible = false;
            lblEast.Visible = false;
            lblWest.Visible = false;
            playerMessages.Visible = false;

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //Create a new frmMain object and display it in the current location.
            this.Hide();
            main.Show();
        }

        private void cboMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            // string end1 = "";

            playerMessages.Visible = true;
            grpTextCanvas.Visible = false;

            SE_Final_Project.Message temp = (SE_Final_Project.Message)cboMessages.SelectedItem;
            List<TextMessage> textMessages = temp.textMessages;
            List<VideoMessage> videoMessages = temp.videoMessages;
            foreach(var t in textMessages)
            {
                lblDefault.Visible = true;
                lblDefault.Text = t.text;
                tempText = t.text;
                
                end1 = t.duration;
                //end[0] = textMessages[0].duration;
            }
            foreach (var v in videoMessages)
            {
                Console.WriteLine(v.filePath);
                playerMessages.URL = v.filePath;
               // end1 = v.endTime;
                Console.WriteLine(v.beginTime);
                Console.WriteLine(v.endTime);
                //end[0] = textMessages[0].duration;
            }

            /*  Console.WriteLine(tempText);
              Console.WriteLine(end1);*/

            /*  Console.WriteLine(textMessages[0].beginTime);
              Console.WriteLine(textMessages[0].duration);*/


            //  begin[0] = textMessages[0].beginTime;
            //  Console.WriteLine(textMessages[0]);

            //  end[0] = textMessages[0].duration;
            // Console.WriteLine(end[0]+ "nice nice");
            /*  Console.WriteLine(begin[0].ToString());
              Console.WriteLine(end[0].ToString());*/
            timer1.Start();
              
            //Display the text message in the correct location
            //displayTextMessage();

        }

        //Getters
        
        //Return cboMessages control object
        public ComboBox getCboMessages()
        {
            return cboMessages;
        }

        //Return all labels
        public List<Label> getLabels()
        {
            //load list and return
            return locationLabels;
        }
        

        //Private Operations

        //Display text message in the correct location
        private void displayTextMessage()
        {
            
            //if (selectedMessage == "testDefault")
            //{
                //Hide media player
             //   playerMessages.Visible = false;

                //Hide all locations other than default
               // grpTextCanvas.Visible = true;

                //lblCenter.Visible = false;
                //lblNorth.Visible = false;
                //lblSouth.Visible = false;
                //lblEast.Visible = false;
                //lblWest.Visible = false;
                //lblDefault.Visible = true;
                //lblDefault.Text = "Default test message";
        }

        //Shut down all processes when user exits program
        private void Messages_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.Shutdown();
            NetworkClient.Logout();
            Application.Exit();
        }

        private void Messages_VisibleChanged(object sender, EventArgs e)
        {
            //Get new messages from the Network Client when form loads
            incomingMessages = NetworkClient.GetNewMessages();
            foreach (var m in incomingMessages)
            {
                cboMessages.Items.Add(m);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           /* if (incomingMessages == null)
            {
                Console.WriteLine("nothing here");
                return;
            }*/
            seconds1++;
            Console.WriteLine(seconds1.ToString());

         /*   if (seconds1.ToString() == begin[0] ){
                lblDefault.Text = tempText;
            }*/
          /*  if (seconds1.ToString().Trim('s') == end[0])
            {
                lblDefault.Text = "";
            }*/

         /*   if(seconds1.ToString() == end1.Trim('s'))
            {
                timer1.Stop();
                seconds1 = 0;
                lblDefault.Text = "";
            }*/
         if(seconds1 == 9)
            {
                timer1.Stop();
                seconds1 = 0;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            /*if (incomingMessages == null)
            {
                Console.WriteLine("nothing here");
                return;
            }*/
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
           /* if (incomingMessages == null)
            {
                Console.WriteLine("nothing here");
                return;
            }*/
        }
    }
}

