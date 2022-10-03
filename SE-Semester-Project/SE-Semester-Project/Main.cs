using System.Collections;

namespace SE_Semester_Project
{
    public partial class Main : Form
    {
        private List<String> outgoingFilepaths = new List<String>();

        public Main()
        {
            InitializeComponent();
        }


        //Run immediately when form loads
        //***note!*** do not modify class constructor. Use this event handler for initialization contents
        private void Main_Load(object sender, EventArgs e)
        {
            //Add test IP to address combo box for presentation
            cboAddresses.Items.Add("Coleman");
            cboAddresses.Items.Add("Mitchell ");
            cboAddresses.Items.Add("Tyler");
            cboAddresses.Items.Add("Victor");

            //Hide file dropdown box and file browser button
            //cboFileList.Visible = false;
            //btnUpload.Visible = false;
            

            //store compose symbol and display on btnCompose
            int i = 11036;
            char c = (char)i;
            btnCompose.Text = c.ToString();

            //store compose symbol and display on btnMessages
            int j = 0x00002709;
            char msgChar = (char)j;
            btnMessages.Text = msgChar.ToString();

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //Customize message font before sending
            FontDialog font = new FontDialog();
            font.ShowDialog();

        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            //Create an OpenFileDialog object which provides file browser functionality
            OpenFileDialog browser = new OpenFileDialog();

            //Create a DialogueResult object which converts a selection to "ok" or "cancel" depending what is selected
            //If a file is selected, result == Dialgoue.OK, otherwise result == Dialogue.Cancel
            DialogResult result = browser.ShowDialog();

            //store entire path in a String
            String path = browser.FileName;

            //Trim path to only include filename and extension
            String pathTrimmed = Path.GetFileName(path);
            
            //If a file is selected, store the full path into class List<> and add trimmed path to cboFileList
            if(result == DialogResult.OK)
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

        private void txtOutgoing_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            //Working code for resizing text box as needed
            /*
            const int MAX_TEXTBOX_HEIGHT = 125;
            String message;
            String textBoxContents;
            if (txtOutgoing.Height < MAX_TEXTBOX_HEIGHT)
            {
                txtOutgoing.Height = e.NewRectangle.Height + 2;
            }
            else
            {
                txtOutgoing.Enabled = false;
                textBoxContents = txtOutgoing.Text;
                txtOutgoing.MaxLength = textBoxContents.Length;
                message = "Character Limit Reached!";
                MessageBox.Show(message);
                txtOutgoing.Enabled = true;
            }
            */
        }

        private void btnMessages_Click(object sender, EventArgs e)
        {
            Messages frmMessages = new Messages();
            frmMessages.Show();
            frmMessages.Location = this.Location;
            this.Hide();
        }
    }
}