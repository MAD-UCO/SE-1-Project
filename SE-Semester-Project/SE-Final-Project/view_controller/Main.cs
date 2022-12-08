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
using File = System.IO.File;

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
        private StartTimeDialog frmStartTimeDialog = new StartTimeDialog();
        private Duration frmDuration = new Duration();
        private Login login = (Login)Application.OpenForms["Login"];
        private Messages messages = (Messages)Application.OpenForms["Messages"];

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

            frmStartTimeDialog.setSeconds("2s");
            frmDuration.setSeconds("8s");
            cboAddresses.Items.Add("vt");

        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            
            if(cboAddresses.SelectedItem == null)
            {
                cboAddresses.SelectedItem = "vt";
                MessageBox.Show("Recipient Not Selected");
            }
            else if(frmStartTimeDialog.getSeconds() == "" || frmDuration.getSeconds() == "")
            {
                frmStartTimeDialog.setSeconds("2s");
                frmDuration.setSeconds("8s");
                // MessageBox.Show("Missing start time or end time duration");
            }
            else
            {

                //Generate a storage location and pass to message constructor
                //String filePath = @"C:\Users\vicat\source\repos\SE-1-Project\SE-Development\";
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

                    //store in message object
                    message.AddTextMessage(textMessage);
                    Console.WriteLine("added successfully");
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
                       // string dir = @"C:\Users\vicat\source\repos\SE-1-Project\SE-Project-3\SE-Semester-Project\SE-Final-Project\bin\Debug\" + cboAddresses.SelectedItem.ToString();
                        string dir = @"C:\Users\vicat\source\repos\SE-1-Project\SE-Development\SE-Semester-Project\SE-Final-Project\bin\Debug\";
                        Console.WriteLine(dir);
                      //  if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                        videoMessage = new VideoMessage(selectedFile);
                        //                        string[] pathsInFolder = Directory.GetFiles(C:\Users\vicat\source\repos\SE-1-Project\SE-Project-3\SE-Semester-Project\SE-Final-Project\bin\Debug)
                        /*tring sourceFile = @"C:\Users\vicat\Videos\" + selectedFile;
                        string destFile = @"C:\Users\vicat\source\repos\SE-1-Project\SE-Development\SE-Semester-Project\SE-Final-Project\bin\Debug\" + selectedFile;
                        File.Copy(sourceFile, destFile);*/
                        //Store in message object
                        //message.AddVideoMessage(videoMessage);
                       // videoMessage = new VideoMessage(selectedFile);

                        //Store in message object
                        message.AddVideoMessage(videoMessage);
                    }
                }

                //Add sender, reciever, and smilFile name(receiver + current time stamp
                try
                {
                    message.setSmilFilePath(filePath + cboAddresses.SelectedItem.ToString() + ".smil");
                    message.smilFileName = cboAddresses.SelectedItem.ToString();


                    message.GenerateMessageFile();


                    //Pass message to the network client to store in outgoing queue
                    NetworkClient.SendMessage(message);
                } 
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
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

            //If this is the first time navigating to messages create a new form. Else reload existing form
            if(messages == null)
            {
                messages = new Messages();
                //Set location to center of user screen (adjusted higher by 25 pixels
                messages.Location = new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (messages.Size.Width / 2),
                    (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (messages.Size.Height / 2) - HeightAdjustment);
            }
            messages.Show();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //Set location to center of user screen (adjusted higher by 25 pixels)
            frmStartTimeDialog.Location = new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (frmStartTimeDialog.Size.Width / 2),
                (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (frmStartTimeDialog.Size.Height / 2) - HeightAdjustment);

            //Display StartTimeDialog form
            frmStartTimeDialog.Show();
            
        }

        private void btnDuration_Click(object sender, EventArgs e)
        {
            //Set location to center of user screen (adjusted higher by 25 pixels)
            frmDuration.Location = new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (frmDuration.Size.Width / 2),
                (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (frmDuration.Size.Height / 2) - HeightAdjustment);

            //Display Duration form
            frmDuration.Show();
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

        //Shut down all processes when user exits program
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkClient.Shutdown();
            NetworkClient.Logout();
            Application.Exit();
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

        //Clear old contents for all forms after logging out
        private void clearOldContents()
        {
            //clear Main.cs text box
            txtOutgoing.Clear();

            //clear Main.cs address combo box items and text
            cboAddresses.Items.Clear();
            cboAddresses.Text = "";

            //Clear Duration.cs and StartTimeDialog.cs textboxes
            frmStartTimeDialog.getTxtSS().Text = "";
            frmDuration.getTxtSS().Text = "";

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
        }
    }
}
