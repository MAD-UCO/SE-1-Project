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
using File = System.IO.File;
using System.Media;

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
        SoundPlayer soundPlayer;
        private Timer timer, timer2, timer3;
        string filePath;
        Dictionary<string, string> relToAbsolute= new Dictionary<string, string>();


        //constants
        private const int HeightAdjustment = 25;

        // Class constructor, do not edit. Use form load event for initialization
        public Main()
        {
            InitializeComponent();
        }

        // Event Handlers

        //Executes as soon as Main.cs is loaded
        private void Main_Load(object sender, EventArgs e)
        {
            //Hide media player until a preview is needed
            playerMain.Visible = false;

            //Hide table layout panel
            pnlStartDuration.Visible = false;

            //Generate button symbols
            GenerateButtonSymbols();

            //Initialize timer to call getNewMessages() every second
            InitializeTimer();

            //Initialize messages form
            messages = new Messages();

            //Initialize text region form
            region = new TextRegion();

            //Intiliaze Message object
            message = new Message();

        }

        //Executes each time btnSend is clicked by the user
        private void BtnSend_Click(object sender, EventArgs e)
        {
            //Error handling messages for missing fields
            if(cboAddresses.SelectedItem == null)
            {
                MessageBox.Show("Recipient Not Selected");
            }
            else if(txtTextStart.Text == "" || txtTextDuration.Text == "")
            {
                MessageBox.Show("Missing start time or end time duration");
            }
            else if(region.GetcboDisplayLocation().SelectedItem == null)
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

                    textMessage.beginTime = txtTextStart.Text + "s";
                    textMessage.duration = txtTextDuration.Text + "s";
                    textMessage.region = region.GetLocation();
                   
                    //store in message object
                    message.AddTextMessage(textMessage);
                }

                //If a selection was made in the file list combo box
                if (cboFileList.SelectedIndex > -1)
                {
                    selectedFile = relToAbsolute[selectedFile];
                    //Check file extension from selectedFile and store in the appropriate object
                    if (selectedFile.Contains(".mp3") || selectedFile.Contains(".wav"))
                    {
                        audioMessage = new AudioMessage(selectedFile);
                        audioMessage.beginTime = txtTextStart.Text + "s";
                        audioMessage.duration = txtTextDuration.Text + "s";

                        //Store in message object
                        message.AddAudioMessage(audioMessage);
                    }
                    else if (selectedFile.Contains(".mp4"))
                    {
                        videoMessage = new VideoMessage(selectedFile);
                        videoMessage.beginTime = txtVideoStart.Text + "s";
                        videoMessage.duration = txtVideoDuration.Text + "s";

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
       
        //Executes each time btnUpload is clicked by the user
        private void BtnUpload_Click(object sender, EventArgs e)
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
                relToAbsolute[pathTrimmed] = path;
            }
        }

        //Executes each time btnCompose is clicked by the user
        private void BtnCompose_Click(object sender, EventArgs e)
        {
            if (txtOutgoing.Enabled == false)
            {
                txtOutgoing.Enabled = true;
                txtOutgoing.BackColor = Color.White;
                return;
            }
            filePath = "";

            //Set sender and receiver name
            message.setSenderName(NetworkClient.myClientName);
            message.setReceiverName(cboAddresses.Text.ToString());

            //Store message subtypes
            if (txtOutgoing.Text != "")
            {
                //Create new text message and assign start/duration
                textMessage = new TextMessage(txtOutgoing.Text);

                textMessage.beginTime = txtTextStart.Text + "s";
                textMessage.duration = txtTextDuration.Text + "s";
                textMessage.region = region.GetLocation();

                //store in message object
                message.AddTextMessage(textMessage);
                Console.WriteLine("added succcessfully");
            }

            //If a selection was made in the file list combo box
            if (cboFileList.SelectedIndex > -1)
            {
                //Check file extension from selectedFile and store in the appropriate object
                if (selectedFile.Contains(".mp3") || selectedFile.Contains(".wav"))
                {
                    audioMessage = new AudioMessage(selectedFile);
                    audioMessage.beginTime = txtTextStart.Text + "s";
                    audioMessage.duration = txtTextDuration.Text + "s";

                    //Store in message object
                    message.AddAudioMessage(audioMessage);
                }
                else if (selectedFile.Contains(".mp4"))
                {
                    videoMessage = new VideoMessage(selectedFile);
                    videoMessage.beginTime = txtVideoStart.Text + "s";
                    videoMessage.duration = txtVideoDuration.Text + "s";

                    //Store in message object
                    message.AddVideoMessage(videoMessage);
                }
            }
        }

        //Executes each time btnMessages is clicked by the user
        private void BtnMessages_Click(object sender, EventArgs e)
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
            messages.Location = new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (messages.Size.Width / 2),
                    (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (messages.Size.Height / 2) - HeightAdjustment);
            messages.Show();
        }

        //Executes each time btnStart is clicked by the user
        private void BtnStart_Click(object sender, EventArgs e)
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

        //Executes each time btnDuration is clicked by the user
        private void BtnDuration_Click(object sender, EventArgs e)
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

        //Executes each tiem btnRegion is clicked by the user
        private void BtnRegion_Click(object sender, EventArgs e)
        {
            //Set location to center of user screen (adjusted higher by 25 pixels)
            region.Location = new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (region.Size.Width / 2),
                    (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (region.Size.Height / 2) - HeightAdjustment);
            //Display Duration form
            region.Show();
        }

        //Executes each time an item is selected in the cboAddresses combo box
        private void CboAddresses_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Store selected address in a string variable
            selectedAddress = cboAddresses.SelectedItem.ToString();
        }

        //Executes when a user has pressed the enter button after entering text into cboAddresses 
        private void CboAddresses_KeyUp(object sender, KeyEventArgs e)
        {
            //After the user has typed the address and pressed enter, add it to the list
            if(e.KeyCode == Keys.Enter)
            {
                cboAddresses.Items.Add(cboAddresses.Text);
                MessageBox.Show("Recipient Successfully Added");
                cboAddresses.Text = "";
            }
        }

        //Executes each time an item is selected in the cboFileList combo box
        private void CboFileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Display media player for preview if audio or video file is selected.
            selectedFile = cboFileList.SelectedItem.ToString();

            //Identify the file type and handle accordingly
            if (selectedFile.Contains("txt"))
            {
                playerMain.Visible = false;
                txtOutgoing.Visible = true;
                string viewText = File.ReadAllText(outgoingFilepaths[cboFileList.SelectedIndex]);
                txtOutgoing.Text = viewText;
            }
            else if (selectedFile.Contains("mp3") || selectedFile.Contains(".wav"))
            {
                soundPlayer = new SoundPlayer(@"" + cboFileList.SelectedItem.ToString());
                soundPlayer.Play();

                playerMain.Visible = false;
                txtOutgoing.Visible = true;
                txtOutgoing.Text = "AUDIO PLAYING...";
            }
            else
            {
                playerMain.Visible = true;
                txtOutgoing.Visible = false;

                playerMain.URL = outgoingFilepaths[cboFileList.SelectedIndex];
            }
        }

        //Executes each time btnLogout is clicked by the user
        private void BtnLogout_Click(object sender, EventArgs e)
        {

            //Clear old contents for new user
            ClearOldContents();

            //Navigate back to the login form and logout user
            this.Hide();
            login.Show();
            Message.UnsetUsername();
            NetworkClient.Logout();
        }

        //Call getNewMessages() every second
        private void Timer_Tick(object sender, EventArgs e)
        {
            //Add messages names to the message combo box and the objects to a hidden list
            incomingMessages = NetworkClient.GetNewMessages();
            foreach (var m in incomingMessages)
            {
                btnNewMessageIcon.Visible = true;
                messages.GetCboMessages().Items.Add(m.smilFileName + ": " + DateTime.Now.ToString());
                messages.GetMessageObjects().Add(m);
            }
        }

        //Executes each time Main.cs closes
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Shut down all processes except the server
            NetworkClient.Shutdown();
            NetworkClient.Logout();
            Environment.Exit(0);
        }

        //Executes each time chkStartDuration is selected or unselected by the user
        private void ChkStartDuration_CheckedChanged(object sender, EventArgs e)
        {
            //Display table layout panel for users to select start and duration times
            if (chkStartDuration.Checked)
            {
                pnlStartDuration.Visible = true;
            }
            else
            {
                pnlStartDuration.Visible = false;
            }
        }

        //Getters
        public string GetSelectedAddress()
        {
            return selectedAddress;
        }

        public string GetSelectedFile()
        {
            return selectedFile;
        }

        public List<string> GetOutgoingFilePaths()
        {
            return outgoingFilepaths;
        }

        public Message GetMessage()
        {
            return message;
        }

        public List<Message> GetIncomingMessages()
        {
            return incomingMessages;
        }

        public TextBox GetTxtTextStart()
        {
            return txtTextStart;
        }

        public TextBox GetTxtTextDuration()
        {
            return txtTextDuration;
        }

        public TextBox GetTxtAudioStart()
        {
            return txtAudioStart;
        }

        public TextBox GetTxtAudioDuration()
        {
            return txtAudioDuration; 
        }

        public TextBox GetTxtVideoStart()
        {
            return txtVideoStart;
        }

        public TextBox GetTxtVideoDuration()
        {
            return txtVideoDuration;
        }

        //Operations

        //Generate button symbols during initialization
        private void GenerateButtonSymbols()
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
        private void ClearOldContents()
        {
            //clear Main.cs text box
            txtOutgoing.Clear();

            //clear Main.cs address combo box items and text
            cboAddresses.Items.Clear();
            cboAddresses.Text = "";

            //clear files from cboFileList
            cboFileList.Items.Clear();
            cboFileList.Text = "";

            //Clear TextRegion.cs combo box text
            region.GetcboDisplayLocation().Text = "";

            //Clear start time and duration text boxes
            txtTextStart.Text = "";
            txtTextDuration.Text = "";
            txtAudioStart.Text  = "";
            txtAudioDuration.Text = "";
            txtVideoStart.Text = "";
            txtAudioStart.Text = "";

            //Clear Messages.cs combo box and labels (If the form has been loaded at least once)
            if(messages != null)
            {
                messages.GetCboMessages().Items.Clear();
                messages.GetCboMessages().Text = "";
                foreach (var label in messages.GetLabels())
                {
                    label.Text = "";
                }
            }

            //Clear messages messageObject List
            messages.GetMessageObjects().Clear();
        }

        //Start timer to continuously call getMessages()
        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(Timer_Tick);

            // 1000ms = 1s intervals
            timer.Interval = 1000;
            timer.Start();

        }

    }
}
