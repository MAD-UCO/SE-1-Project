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

namespace SE_Final_Project
{
    public partial class Main : Form
    {
        
        // private fields
        private string selectedAddress;
        private string selectedFile;
        private List<String> outgoingFilepaths = new List<String>();

        private Message message;
        private TextMessage textMessage;
        private VideoMessage videoMessage;
        private AudioMessage audioMessage;
        private StartTimeDialog frmStartTimeDialog = new StartTimeDialog();
        private Duration frmDuration = new Duration();
        private Login login = (Login)Application.OpenForms["Login"];
        private Messages messages = (Messages)Application.OpenForms["Messages"];

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
            else
            {

                //Generate a storage location and pass to message constructor
                String filePath = "C:/OCCC_UCO/Fall 2022/Project Test Files";
                message = new Message();

                //Set sender and receiver name
                message.setSenderName("Movebit User");
                message.setReceiverName(cboAddresses.SelectedItem.ToString());

                //Store message subtypes
                if (txtOutgoing.Text != "")
                {
                    //Create new text message and assign start/duration
                    textMessage = new TextMessage(txtOutgoing.Text);
                    //textMessage.beginTime = frmStartTimeDialog.getSeconds();
                    //textMessage.duration = frmDuration.getSeconds();


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

                //Add host sender, reciever, and smilFile name(receiver + current time stamp)
                message.setSenderName(NetworkClient.myClientName);
                message.setReceiverName(cboAddresses.Text.ToString());
                message.setSmilFilePath(filePath + "/" + cboAddresses.SelectedItem.ToString() + ".smil");

                //Generate message file
                message.GenerateMessageFile();

                /*
                 * 
                 * Pass message along to the server
                 * 
                 */

                NetworkClient.AddMessageToOutQueue(
                    new MoveBitMessaging.SimpleTextMessage(message.receiverName, message.senderName, message.GetSmilText(message.smilFileName))
                    );
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
            //Enable composition controls
            txtOutgoing.Enabled = true;
            btnUpload.Enabled = true;
            cboFileList.Enabled = true;

            //Change file combo box and file browser button visibility to true
            cboFileList.Visible = true;
            btnUpload.Visible = true;
        }

        private void btnMessages_Click(object sender, EventArgs e)
        {
            this.Hide();

            //If this is the first time navigating to messages create a new form. Else reload existing form
            if(messages == null)
            {
                messages = new Messages();
            }
         
            messages.Show();
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            frmStartTimeDialog.Show();
            frmStartTimeDialog.Location = new Point(this.Location.X + 150, this.Location.Y + 150);
        }

        private void btnDuration_Click(object sender, EventArgs e)
        {
            frmDuration.Show();
            frmDuration.Location = new Point(this.Location.X + 150, this.Location.Y + 150);
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
            //Navigate back to the login form and logout user
            this.Hide();
            login.Show();
            NetworkClient.Logout();
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

        //Shut down all processes when user exits program
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.Shutdown();
            NetworkClient.Logout();
            Application.Exit();
        }
    }
}
