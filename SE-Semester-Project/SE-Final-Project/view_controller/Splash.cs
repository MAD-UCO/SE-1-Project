using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE_Final_Project
{
    public partial class Splash : Form
    { 
        public Splash()
        {
            InitializeComponent();
        }

        //Getters/Setters
        public Label getLblLoading()
        {
            return lblLoading;
        }

        public void setLblLoading(String lblLoading)
        {
            this.lblLoading.Text = lblLoading;
        }
    }
}
