using SE_Final_Project.view_controller;
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
using System.Media;
using AxWMPLib;

namespace SE_Final_Project
{
    public partial class Main : Form
    {

        public bool hardClose = false;
        
        //private fields
        private string selectedAddress;
        private string selectedFile;
        private List<String> outgoingFilepaths = new List<String>();
        private List<String> paths = new List<String>();
        private int seconds = 0;
        string begin, end = "";



        private Message message;
        private TextMessage textMessage;
        private VideoMessage videoMessage;
        private AudioMessage audioMessage;
        private SoundPlayer soundPlayer;    
        private StartTimeDialog frmStartTimeDialog = new StartTimeDialog();
        private Duration frmDuration = new Duration();


        //Class constructor, do not edit. Use form load event for initialization
        public Main()
        {

            this.FormClosing += main_FormClosing;
            InitializeComponent();


        }

        //Event handlers

        private void Main_Load(object sender, EventArgs e)
        {
            //Hide media player until a preview is needed
            playerMain.Visible = false;
            tableLayoutPanel1.Visible = false;
            //Generate button symbols
            generateButtonSymbols();

        }

        private void btnSend_Click(object sender, EventArgs e)
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
                // String filePath = "C:\\Users\\vicat\\source\\repos\\SE-1-Project\\SE-Project-3\\SE-Semester-Project";
                String filePath = @"C:\\Users\\vicat\\source\\repos\\SE-1-Project\\SE-Final\";

                //                String filePath = "C:/OCCC_UCO/Fall 2022/Project Test Files";
                message = new Message();

                //Set sender and receiver name
                message.setSenderName("Movebit User");
                message.setReceiverName(cboAddresses.SelectedItem.ToString());

                //Store message subtypes
                if (txtOutgoing.Text != "")
                {
                    //Create new text message and assign start/duration
                    textMessage = new TextMessage(txtOutgoing.Text);
                    textMessage.beginTime = frmStartTimeDialog.getSeconds();
                    textMessage.duration = frmDuration.getSeconds();


                    //store in message object
                    message.AddTextMessage(textMessage);
                }

                //If a selection was made in the file list combo box
                //If a selection was made in the file list combo box
                if (cboFileList.SelectedIndex > -1)
                {
                    //Check file extension from selectedFile and store in the appropriate object
                    if (selectedFile.Contains(".mp3") || selectedFile.Contains(".wav"))
                    {
                        audioMessage = new AudioMessage(selectedFile);
                  /*      string sourcePath = @"C:\Users\vicat\Videos";
                        string moveTo = @"C:\Users\vicat\source\repos\SE-1-Project\SE-Project-3\SE-Semester-Project\SE-Final-Project\bin\Debug";

                        string sourceFile = Path.Combine(sourcePath, selectedFile);
                        string destinationFile = Path.Combine(moveTo, selectedFile);
                        Directory.CreateDirectory(moveTo);
                        File.Copy(sourceFile, destinationFile, true);
                        if (Directory.Exists(sourcePath))
                        {
                            string[] files = Directory.GetFiles(sourcePath);

                            // Copy the files and overwrite destination files if they already exist.
                            foreach (string s in files)
                            {
                                if (s == selectedFile)
                                {
                                    // Use static Path methods to extract only the file name from the path.
                                    selectedFile = Path.GetFileName(s);
                                    destinationFile = Path.Combine(moveTo, selectedFile);
                                    System.IO.File.Copy(s, destinationFile, true);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Source path does not exist!");
                        }*/
                        //Store in message object

                        message.AddAudioMessage(audioMessage);
                    }
                    else if (selectedFile.Contains(".mp4"))
                    {
                        string dir = @"C:\Users\vicat\source\repos\SE-1-Project\SE-Final\SE-Semester-Project\SE-Final-Project\bin\Debug\" + cboAddresses.SelectedItem.ToString();
                        Console.WriteLine(dir);
                        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                        videoMessage = new VideoMessage(selectedFile);
                        //                        string[] pathsInFolder = Directory.GetFiles(C:\Users\vicat\source\repos\SE-1-Project\SE-Project-3\SE-Semester-Project\SE-Final-Project\bin\Debug)
                        string sourceFile = @"C:\Users\vicat\Videos\" + selectedFile;
                       // string destFile = @"C:\Users\vicat\source\repos\SE-1-Project\SE-Final\SE-Semester-Project\SE-Final-Project\bin\Debug\" + selectedFile;
                        string destFile = @""+dir + @"\" + selectedFile;
                        // dir = dir + "\";
                        //string slash = @"\";
                        Console.WriteLine(destFile);
  //                      string destFile = @":\Users\vicat\source\repos\SE-1-Project\SE-Final\SE-Semester-Project\SE-Final-Project\bin\Debug\"+ cboAddresses.SelectedItem.ToString()  + selectedFile;

                        File.Copy(sourceFile, destFile);
                        //Store in message object
                        message.AddVideoMessage(videoMessage);
                    }
                }

                //Add host name (IP Address) sender, reciever, and smilFile name(receiver + current time stamp)
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

         /*       NetworkClient.AddMessageToOutQueue(
                    new MoveBitMessaging.SimpleTextMessage(message.receiverName, message.senderName, message.GetSmilText(message.smilFileName))
                    );*/

                //Display simple message for testing

            }
        }
       
        private void btnUpload_Click(object sender, EventArgs e)
        {
            //Create an OpenFileDialog object which provides file browser functionality
            OpenFileDialog browser = new OpenFileDialog();
            browser.Multiselect = true;
         

            //If a file is selected, result == Dialgoue.OK, otherwise result == Dialogue.Cancel
            DialogResult result = browser.ShowDialog();
            String path = browser.FileName;
            String pathTrimmed = Path.GetFileName(path);

            String[] paths = browser.FileNames;
            String[] files = browser.SafeFileNames;
            outgoingFilepaths = browser.FileNames.ToList();
            //If a file is selected, store the full path into class List<> and add trimmed path to cboFileList
            if (result == DialogResult.OK)
            {
                /*outgoingFilepaths.Add(path);
                cboFileList.Items.Add(pathTrimmed);*/
                for (int i = 0; i < outgoingFilepaths.Count; i++)

                {
                    Console.WriteLine(files[i].ToString());
                    //outgoingFilepaths.Add(paths[i]);
                    //  cboFileList.Items.Add(pathTrimmed);
                    // safe file names cboFileList.Items.Add(files[i]);    
                    // paths ref
                    cboFileList.Items.Add(outgoingFilepaths[i]);
                }
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
            playerMain.Visible = false;
            /*   this.Hide();
               Messages frmMessages = new Messages();
               frmMessages.Show();
               frmMessages.Location = this.Location;
               message = new Message();*/
            message = new Message();
            bool yes;
            yes = message.ParseMessage(@"C:\Users\vicat\source\repos\SE-1-Project\SE-Project-3\SE-Semester-Project\cole.smil");
            string x = "";
            end = message.textMessages[0].duration;
            begin = message.textMessages[0].beginTime;
            x = message.textMessages[0].text;
            txtOutgoing.Text = x;
        //    string x = "";
          //  string xy = "";
           // x = message.audioMessages[0].filePath;
        //    xy = Environment.CurrentDirectory;
        //    String pathTrimmed = Path.GetFileName(x);
          //  Console.WriteLine(pathTrimmed);
           // Console.WriteLine(xy);
            //playerMain.Visible = true;
            // playerMain.URL = Path.GetFileName(x);
          //  playerMain.URL = pathTrimmed;
            string xy = "";
            xy = Environment.CurrentDirectory;
            Console.WriteLine(xy);
            /*   this.Hide();
               Messages frmMessages = new Messages();
               frmMessages.Show();
               frmMessages.Location = this.Location;*/
            timer1.Start();

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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //Create new Login form, display, and hide the current form
            this.Hide();
            NetworkClient.Logout();
            hardClose = false;
            //Login frmLogin = new Login();
            //frmLogin.Show();
            
        }

        private void main_FormClosing(object sender, FormClosingEventArgs closingArgs)
        {
            //Debug.Assert(false);
            //NetworkClient.Shutdown();
            if (NetworkClient.GetClientState() != ClientState.NotLoggedIn)
            {
                hardClose = true;
                NetworkClient.Logout();
            }
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

        private void clearbtn_Click(object sender, EventArgs e)
        {

            cboFileList.Items.Clear();
            cboFileList.Update();
            cboFileList.Text = "";
            outgoingFilepaths.Clear();
        }

        private void preview_Click(object sender, EventArgs e)
        {
            txtOutgoing.Visible = false;
            playerMain.Visible = false;
            tableLayoutPanel1.Visible = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txtOutgoing.Visible = false;
            playerMain.Visible = false;
            tableLayoutPanel1.Visible = true;
            radioButton1.Enabled = false;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            txtOutgoing.Visible = false;
            playerMain.Visible = false;
            tableLayoutPanel1.Visible = true;
            //radioButton1.
            //radioButton1.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                txtOutgoing.Visible = false;
                playerMain.Visible = false;
                tableLayoutPanel1.Visible = true;
                //radioButton1.Enabled = false;
            }
            else
            {
                txtOutgoing.Visible = true;
                playerMain.Visible = true;
                tableLayoutPanel1.Visible = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //timer1.Start();
            seconds = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;
            /*  while(seconds >= begin && seconds <= end)
              {
                  textBox1.Text = seconds.ToString();
              }*/
            if (seconds.ToString().Insert(1,"s") == begin)
            {
                // textBox1.Text = _firstText;
                // textBox1.Visible = false;
                // axWindowsMediaPlayer1.URL = @"C:\Users\vicat\Videos\test.wav";
                txtOutgoing.Visible = true;
                txtOutgoing.Text = "HI";

                Console.WriteLine("started");
            }
            if (seconds.ToString().Insert(1,"s") == end)
            {
                txtOutgoing.Visible = false;
                /*                textBox1.Text = _secText;
                                textBox1.Visible = true;
                                axWindowsMediaPlayer1.Ctlcontrols.stop();*/
                Console.WriteLine("done");
            }

            Console.WriteLine(seconds.ToString());
            if (seconds == 6)
            {
                //new change
                //one more
                Console.WriteLine("at 6");
                //change this is new.

            }// going = false; timer1.Stop(); testing = false; }

            if (seconds > 9)
            {
                timer1.Stop();
            }
        }
    }
}
