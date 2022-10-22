using SE_Semester_Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE_Final_Project
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (NetworkClient.Login(textBox1.Text, textBox2.Text, true))
            {
                Main frmMain = new Main();
                frmMain.ShowDialog();
                this.Hide();
            }
        }
    }
}
