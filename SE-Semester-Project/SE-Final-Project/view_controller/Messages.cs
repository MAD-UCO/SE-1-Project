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
        private List<Label> locationLabels = new List<Label>();
        private Main main = (Main)Application.OpenForms["Main"];

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

        private void btnBack_Click(object sender, EventArgs e)
        {
            //Create a new frmMain object and display it in the current location.
            this.Hide();
            main.Show();
        }

        private void cboMessages_SelectedIndexChanged(object sender, EventArgs e)
        {

            SE_Final_Project.Message temp = (SE_Final_Project.Message)cboMessages.SelectedItem;
            List<TextMessage> textMessages = temp.textMessages;
            foreach(var t in textMessages)
            {
                displayMessage(t);
            }
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

        //Shut down all processes when user exits program
        private void Messages_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.Shutdown();
            NetworkClient.Logout();
            Environment.Exit(0);
        }

        private void Messages_VisibleChanged(object sender, EventArgs e)
        {
            //Get new messages from the Network Client when form loads
            //incomingMessages = NetworkClient.GetNewMessages();
            //foreach (var m in incomingMessages)
            //{
              //  cboMessages.Items.Add(m);
            //}

        }

        //Private Operations

        //Display text message in the correct location
        private void displayMessage(TextMessage t)
        {
            //Clear old text
            lblCenter.Text = "";
            lblNorth.Text = "";
            lblSouth.Text = "";
            lblEast.Text = "";
            lblWest.Text = "";

            if(t.region.ToUpper() == "CENTER")
            {
                lblCenter.Visible = true;
                lblCenter.Text = t.text;
            }
            else if(t.region.ToUpper() == "NORTH")
            {
                lblNorth.Visible = true;
                lblNorth.Text = t.text;
            }
            else if(t.region.ToUpper() == "SOUTH")
            {
                lblSouth.Visible = true;
                lblSouth.Text = t.text;
            }
            else if(t.region.ToUpper() == "EAST")
            {
                lblEast.Visible = true;
                lblEast.Text = t.text;
            }
            else if(t.region.ToUpper() == "WEST")
            {
                lblWest.Visible = true;
                lblWest.Text = t.text;
            }
            else
            {
                lblDefault.Visible = true;
                lblDefault.Text = t.text;
            }
        }
    }
}

