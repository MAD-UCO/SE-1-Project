using SE_Semester_Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Net;
using static System.Net.WebRequestMethods;
using System.Diagnostics;
using SE_Final_Project.view_controller;
using System.Windows.Forms.VisualStyles;
using System.Collections;

namespace SE_Final_Project
{
    public partial class Main : Form
    {
        
        // private variables
        private string selectedAddress;
        private string selectedFile;
        private List<String> outgoingFilepaths = new List<String>();
        private Message message;
        private TextMessage textMessage;
        private VideoMessage videoMessage;
        private AudioMessage audioMessage;
        private StartTimeDialog frmStartTimeDialog = (StartTimeDialog)Application.OpenForms["StartTimeDialog"];
        private Duration frmDuration = (Duration)Application.OpenForms["frmDuration"];
        private Login login = (Login)Application.OpenForms["Login"];
        private TextRegion region = (TextRegion)Application.OpenForms["TextRegion"];
        private Messages messages = (Messages)Application.OpenForms["Messages"];
        List<Message> incomingMessages = new List<Message>();


        private Timer timer;


        //constants
        private const int HeightAdjustment = 25;

        // Class constructor, do not edit. Use form load event for initialization
        public Main()
        {
            InitializeComponent();
        }

        // Event Handlers
        private void Main_Load(object sender, EventArgs e)
        {
            //Hide media player until a preview is needed
            playerMain.Visible = false;

            //Generate button symbols
            generateButtonSymbols();

            //Initialize timer to call getNewMessages() every second
            initializeTimer();

            //Initialize messages form
            messages = new Messages();


        }

        private void BtnSend_Click(object sender, EventArgs e)
        {

            if(cboAddresses.SelectedItem == null)
            {
                MessageBox.Show("Recipient Not Selected");
            }
            else if(frmStartTimeDialog.getSeconds() == "" || frmDuration.getSeconds() == "")
            {
                MessageBox.Show("Missing start time or end time duration");
            }
            else if(region.getcboDisplayLocation().SelectedItem == null)
            {
                MessageBox.Show("Missing display location");
            }
            else
            {

                //Generate a storage location and pass to message constructor
                String filePath = "";
                message = new Message();

                //Set sender and receiver name
                message.setSenderName(NetworkClient.myClientName);
                message.setReceiverName(cboAddresses.Text.ToString());

                //Store message subtypes
                if (txtOutgoing.Text != "")
                {
                    //Create new text message and assign start/duration
                    textMessage = new TextMessage(txtOutgoing.Text);

                    textMessage.beginTime = frmStartTimeDialog.getSeconds();
                    textMessage.duration = frmDuration.getSeconds();
                    textMessage.region = region.getLocation();
                   
                    //store in message object
                    message.AddTextMessage(textMessage);
                }

                //If a selection was made in the file list combo box
                if (cboFileList.SelectedIndex > -1)
                {
                    //Check file extension from selectedFile and store in the appropriate object
                    if (selectedFile.Contains(".mp3") || selectedFile.Contains(".wav"))
                    {
                        audioMessage = new AudioMessage(selectedFile);

                        //Store in message object
                        message.AddAudioMessage(audioMessage);
                    }
                    else if (selectedFile.Contains(".mp4"))
                    {
                        videoMessage = new VideoMessage(selectedFile);

                        //Store in message object
                        message.AddVideoMessage(videoMessage);
                    }
                }

                //Add sender, reciever, and smilFile name(receiver + current time stamp
                message.setSmilFilePath(filePath + cboAddresses.SelectedItem.ToString() + ".smil");
                message.smilFileName = cboAddresses.SelectedItem.ToString();

                message.GenerateMessageFile();

               
                //Pass message to the network client to store in outgoing queue
                NetworkClient.SendMessage(message);
            }
        }
       
        private void btnUpload_Click(object sender, EventArgs e)
        {
            //Create an OpenFileDialog object which provides file browser functionality
            OpenFileDialog browser = new OpenFileDialog();
         

            //If a file is selected, result == Dialgoue.OK, otherwise result == Dialogue.Cancel
            DialogResult result = browser.ShowDialog();
            String path = browser.FileName;
            String pathTrimmed = Path.GetFileName(path);

            //If a file is selected, store the full path into class List<> and add trimmed path to cboFileList
            if (result == DialogResult.OK)
            {
                outgoingFilepaths.Add(path);
                cboFileList.Items.Add(pathTrimmed);
            }
        }

        private void btnCompose_Click(object sender, EventArgs e)
        {
            //Reset text box contents
            txtOutgoing.Text = "";
        }

        private void btnMessages_Click(object sender, EventArgs e)
        {
            this.Hide();

            //hide new message icon
            btnNewMessageIcon.Visible = false;

            //If this is the first time navigating to messages create a new form. Else reload existing form
            if(messages == null)
            {
                //Messages form gets created early during initialization so getNewMessages() can be called immediately
                //Set location to center of user screen (adjusted higher by 25 pixels
                messages.Location = new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (messages.Size.Width / 2),
                    (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (messages.Size.Height / 2) - HeightAdjustment);
            }
            messages.Show();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(frmStartTimeDialog == null)
            {
                frmStartTimeDialog = new StartTimeDialog();
                //Set location to center of user screen (adjusted higher by 25 pixels)
                frmStartTimeDialog.Location = new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (frmStartTimeDialog.Size.Width / 2),
                    (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (frmStartTimeDialog.Size.Height / 2) - HeightAdjustment);
            }
            //Display StartTimeDialog form
            frmStartTimeDialog.Show();
        }

        private void btnDuration_Click(object sender, EventArgs e)
        {
            if(frmDuration == null)
            {
                frmDuration = new Duration();
                //Set location to center of user screen (adjusted higher by 25 pixels)
                frmDuration.Location = new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (frmDuration.Size.Width / 2),
                    (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (frmDuration.Size.Height / 2) - HeightAdjustment);

            }
            //Display Duration form
            frmDuration.Show();
        }

        private void btnRegion_Click(object sender, EventArgs e)
        {
            if(region == null)
            {
                region = new TextRegion();
                //Set location to center of user screen (adjusted higher by 25 pixels)
                region.Location = new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (region.Size.Width / 2),
                    (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (region.Size.Height / 2) - HeightAdjustment);
            }
            //Display Duration form
            region.Show();
        }

        private void cboAddresses_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Store selected address in a string variable
            selectedAddress = cboAddresses.SelectedItem.ToString();
        }

        //Add an address to the combo box that has been typed in by the user
        private void cboAddresses_KeyUp(object sender, KeyEventArgs e)
        {
            //After the user has typed the address and pressed enter, add it to the list
            if(e.KeyCode == Keys.Enter)
            {
                cboAddresses.Items.Add(cboAddresses.Text);
                MessageBox.Show("Recipient Successfully Added");
                cboAddresses.Text = "";
            }
        }

        private void cboFileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Display media player for preview if audio or video file is selected.
            selectedFile = cboFileList.SelectedItem.ToString();

            if(selectedFile == "test.wav" || selectedFile == "test.mp3")
            {
                playerMain.Visible = true;
            }
            else
            {
                playerMain.Visible = false;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {

            //Clear old contents for new user
            clearOldContents();

            //Navigate back to the login form and logout user
            this.Hide();
            login.Show();
            NetworkClient.Logout();
        }

        //Call getNewMessages() every second
        private void timer_Tick(object sender, EventArgs e)
        {
            //Add messages names to the message combo box and the objects to a hidden list
            incomingMessages = NetworkClient.GetNewMessages();
            foreach (var m in incomingMessages)
            {
                btnNewMessageIcon.Visible = true;
                messages.getCboMessages().Items.Add(m.smilFileName + ": " + DateTime.Now.ToString());
                messages.getMessageObjects().Add(m);
            }
        }

        //Shut down all processes when user exits program
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Shut down program
            NetworkClient.Shutdown();
            NetworkClient.Logout();
            Environment.Exit(0);
        }

        //Getters
        public string getSelectedAddress()
        {
            return selectedAddress;
        }

        public string getSelectedFile()
        {
            return selectedFile;
        }

        public List<string> getOutgoingFilePaths()
        {
            return outgoingFilepaths;
        }

        public Message getMessage()
        {
            return message;
        }

        public List<Message> getIncomingMessages()
        {
            return incomingMessages;
        }

        //Operations

        //Generate button symbols during initialization
        private void generateButtonSymbols()
        {
            //store compose symbol and display on btnCompose
            int i = 11036;
            char c = (char)i;
            btnCompose.Text = c.ToString();

            //store message symbol and display on btnMessages
            int j = 0x00002709;
            char msgChar = (char)j;
            btnMessages.Text = msgChar.ToString();

            //store send symbol and display on btnSend
            int k = 8628;
            char sndChar = (char)k;
            btnSend.Text = sndChar.ToString();
        }

        //Clear old contents for all forms after logging out
        private void clearOldContents()
        {
            //clear Main.cs text box
            txtOutgoing.Clear();

            //clear Main.cs address combo box items and text
            cboAddresses.Items.Clear();
            cboAddresses.Text = "";

            //Clear Duration.cs, StartTimeDialog.cs, and Region.cs textboxes
            frmStartTimeDialog.getTxtSS().Text = "";
            frmDuration.getTxtSS().Text = "";
            region.getcboDisplayLocation().Text = "";

            //Clear Messages.cs combo box and labels (If the form has been loaded at least once)
            if(messages != null)
            {
                messages.getCboMessages().Items.Clear();
                messages.getCboMessages().Text = "";
                foreach (var label in messages.getLabels())
                {
                    label.Text = "";
                }
            }

            //Clear messages messageObject List
            messages.getMessageObjects().Clear();
        }

        //Start timer to continuously call getMessages()
        private void initializeTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);

            // 1000ms = 1s intervals
            timer.Interval = 1000;
            timer.Start();

        }
    }
}
