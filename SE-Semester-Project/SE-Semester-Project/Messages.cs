using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE_Semester_Project
{
    public partial class Messages : Form
    {
        public Messages()
        {
            InitializeComponent();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            //Hide the current form
            this.Hide();

            //Create an a Video form object, define the starting position using current form properties.
            //The object type will be the identical to the "name property" of the form you want to create.
            Outgoing outgoingForm = new Outgoing();
            outgoingForm.StartPosition = this.StartPosition;

            //Call Show() function that belongs to form objects
            outgoingForm.Show();
        }
    }
}
