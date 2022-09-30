namespace SE_Semester_Project
{
    public partial class Outgoing : Form
    {
        public Outgoing()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }

        private void btnMessages_Click(object sender, EventArgs e)
        {
            //Hide the current form
            this.Hide();

            //Create an a Video form object, define the starting position using current form properties.
            //The object type will be the identical to the "name property" of the form you want to create.
            Messages messagesForm = new Messages();
            messagesForm.StartPosition = this.StartPosition;

            //Call Show() function that belongs to form objects
            messagesForm.Show();
        }
    }
}