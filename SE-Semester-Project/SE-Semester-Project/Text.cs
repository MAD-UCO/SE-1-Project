namespace SE_Semester_Project
{
    public partial class frmText : Form
    {
        public frmText()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }
        //Handle button press event for btnVideo
        private void btnVideo_Click(object sender, EventArgs e)
        {
            //Hide the current form
            this.Hide();

            //Create a Video form object, define the starting position using current form properties.
            frmVideo videoForm = new frmVideo();
            videoForm.StartPosition = this.StartPosition;

            //Call Show() function that belongs to form objects
            videoForm.Show();
        }

        //Handle button press event for btnAudio
        private void btnAudio_Click(object sender, EventArgs e)
        {
            //Hide the current form
            this.Hide();

            //Create an Audio form object, define the starting position using form properties.
            frmAudio audioForm = new frmAudio();
            audioForm.StartPosition = this.StartPosition;

            //Call Show() function that belongs to form objects
            audioForm.Show();
        }
    }
}