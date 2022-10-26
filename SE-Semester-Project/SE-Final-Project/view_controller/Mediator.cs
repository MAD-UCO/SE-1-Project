/*
 * 
 * Mediator class directs traffic as the user navigates from page to page
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE_Final_Project.view_controller
{
    public class Mediator
    {
        //private  private Form form1;
        private Form form1;
        private Form form2;

        //constructors
        public Mediator(){}

        public Mediator(Form form1, Form form2)
        {
            this.form1 = form1;
            this.form2 = form2;
        }

        public void navigate()
        {
            //Navigate from form1 to form2
            form2.ShowDialog();
            form2.Location = form1.Location;
            form1.Hide();
        }

    }
}
