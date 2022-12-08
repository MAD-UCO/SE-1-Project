using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE_Final_Project.view_controller
{
    public partial class TextRegion : Form
    {
        private string location = "";
        public TextRegion()
        {
            InitializeComponent();
            cboDisplayLocation.Items.Add("Default");
            cboDisplayLocation.Items.Add("Center");
            cboDisplayLocation.Items.Add("North");
            cboDisplayLocation.Items.Add("South");
            cboDisplayLocation.Items.Add("East");
            cboDisplayLocation.Items.Add("West");
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            if(cboDisplayLocation.SelectedItem != null)
            {
                location = cboDisplayLocation.SelectedItem.ToString();
            }
            this.Hide();
        }

        //Getters
        public String GetLocation()
        {
            return this.location;
        }

        public ComboBox GetcboDisplayLocation()
        {
            return cboDisplayLocation;
        }
    }
}
