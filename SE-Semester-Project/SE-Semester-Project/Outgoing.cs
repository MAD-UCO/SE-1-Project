using System.Collections;

namespace SE_Semester_Project
{
    public partial class Outgoing : Form
    {
        private List<String> outgoingFilepaths = new List<String>();

        public Outgoing()
        {
            InitializeComponent();
        }


        //Run immediately when form loads
        //***note!*** do not modify class constructor. Use this event handler for initialization contents
        private void Outgoing_Load(object sender, EventArgs e)
        {
            //Add test IP to address combo box for presentation
            cboAddresses.Items.Add("Coleman");
            cboAddresses.Items.Add("Mitchell");
            cboAddresses.Items.Add("Tyler");
            cboAddresses.Items.Add("Victor");

            //store compose symbol and display on btnCompose
            int i = 11036;
            char c = (char)i;
            btnCompose.Text = c.ToString();
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

        }

    }
}