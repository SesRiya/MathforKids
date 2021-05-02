using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InteractiveMathforKids
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();


        }

        /* store the form instance reference as a property of one of the classes.
           add a property to the HelpForm class that stores a reference to an instance of a Form1 class
           creating the property using the more general Form class rather than the specific Form1, so a reference to an
           instance of any class derived from the Form class can be stored in the property,
           not just a Form1 instance
           Note the use of a private variable to store the value within the class instance, get and set accessors, 
           and the value keyword to set a new property value.*/

        private Form m_InstanceRef = null;
        public Form InstanceRef
        {
            get
            {
                return m_InstanceRef;
            }
            set
            {
                m_InstanceRef = value;
                pbx_question.Image = Image.FromFile("question.gif");
            }
        }

        private void btn_backTogame_Click(object sender, EventArgs e)   //button to go back to GameForm
        {
            this.Hide();
            InstanceRef.Show();
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {

        }
    }
}
