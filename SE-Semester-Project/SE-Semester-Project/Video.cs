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
    public partial class frmVideo : Form
    {
        public frmVideo()
        {
            InitializeComponent();
        }

        private void btnText_Click(object sender, EventArgs e)
        {

            //Hide the current form
            this.Hide();

            //Create an a Text form object, define the starting position using current form properties.
            //The object type will be the identical to the "name property" of the form you want to create.
            frmText textForm = new frmText();
            textForm.StartPosition = this.StartPosition;

            //Call Show() function that belongs to form objects
            textForm.Show();
        }

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
