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

namespace SE_Final_Project
{
    public partial class Messages : Form
    {
        public Messages()
        {
            InitializeComponent();
        }

        //Runs immediately after form loads
        //***note!*** do not modify class constructor. Use this event handler for initialization contents
        private void Messages_Load(object sender, EventArgs e)
        {

            //Hard coded items for visual/test purposes
            cboMessages.Items.Add("testDefault");
            cboMessages.Items.Add("testCenter");
            cboMessages.Items.Add("testNorth");
            cboMessages.Items.Add("testSouth");
            cboMessages.Items.Add("testEast");
            cboMessages.Items.Add("testWest");

            cboMessages.Items.Add("TestMedia");

            //Set initial visibility for labels to false
            lblDefault.Visible = false;
            lblCenter.Visible = false;
            lblNorth.Visible = false;
            lblSouth.Visible = false;
            lblEast.Visible = false;
            lblWest.Visible = false;
            playerMessages.Visible = false;

            //Remove media player from group box so that it can be viewed independently of panel
            //grpTextCanvas.Controls.Remove(playerMessages);


        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //Create a new frmMain object and display it in the current location.
            Main frmMain = new Main();
            frmMain.Show();
            frmMain.Location = this.Location;
            this.Hide();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {

        }

       

        private void cboMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Store the selected combo box item in a string
            String selectedMessage = "";
            selectedMessage = cboMessages.SelectedItem.ToString();

            if (selectedMessage == "testDefault")
            {
                //Hide media player
                playerMessages.Visible = false;

                //Hide all locations other than default
                grpTextCanvas.Visible = true;

                lblCenter.Visible = false;
                lblNorth.Visible = false;
                lblSouth.Visible = false;
                lblEast.Visible = false;
                lblWest.Visible = false;
                lblDefault.Visible = true;
                lblDefault.Text = "Default test message";
            }
            else if (selectedMessage == "testCenter")
            {
                //Hide media player
                playerMessages.Visible = false;

                //Hide all locations other than center
                grpTextCanvas.Visible = true;

                lblDefault.Visible = false;
                lblNorth.Visible = false;
                lblSouth.Visible = false;
                lblEast.Visible = false;
                lblWest.Visible = false;
                lblCenter.Visible = true;
                lblCenter.Text = "Center test message";

            }
            else if (selectedMessage == "testNorth")
            {
                //Hide media player
                playerMessages.Visible = false;

                //Hide all locations other than North
                grpTextCanvas.Visible = true;

                lblDefault.Visible = false;
                lblCenter.Visible = false;
                lblSouth.Visible = false;
                lblEast.Visible = false;
                lblWest.Visible = false;
                lblNorth.Visible = true;
                lblNorth.Text = "North location test message";
            }
            else if (selectedMessage == "testSouth")
            {
                //Hide media player
                playerMessages.Visible = false;

                //Hide all locations other than south
                grpTextCanvas.Visible = true;

                lblDefault.Visible = false;
                lblCenter.Visible = false;
                lblNorth.Visible = false;
                lblEast.Visible = false;
                lblWest.Visible = false;
                lblSouth.Visible = true;
                lblSouth.Text = "South location test message";
            }
            else if (selectedMessage == "testEast")
            {
                //Hide media player
                playerMessages.Visible = false;

                //Hide all locations other than east
                grpTextCanvas.Visible = true;

                lblDefault.Visible = false;
                lblCenter.Visible = false;
                lblNorth.Visible = false;
                lblSouth.Visible = false;
                lblWest.Visible = false;
                lblEast.Visible = true;
                lblEast.Text = "East Location test message";
            }
            else if (selectedMessage == "testWest")
            {
                //Hide media player
                playerMessages.Visible = false;

                //Hide all locations other than west
                grpTextCanvas.Visible = true;

                lblDefault.Visible = false;
                lblCenter.Visible = false;
                lblNorth.Visible = false;
                lblSouth.Visible = false;
                lblEast.Visible = false;
                lblWest.Visible = true;
                lblWest.Text = "West Location test message";
            }
            else
            {
                //Display media player and hide text canvas
                playerMessages.Visible = true;
            }
        }

        private void grpTextCanvas_Enter(object sender, EventArgs e)
        {

        }

        private void lblEast_Click(object sender, EventArgs e)
        {

        }
    }
}

