using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace InteractiveMathforKids
{
    public partial class ExitForm : Form
    {
        public ExitForm()
        {
            InitializeComponent();
        }

        double grade; int timesTried;

        private Form m_callingForm = null;
        public Form InstanceRef
        {

            get
            {
                return m_callingForm;

            }
            set
            {
                m_callingForm = value;
                pbx_exit.Image = Image.FromFile("bye.gif");
            }
        }

        private void button2_Click(object sender, EventArgs e)    // method to go back to GameForm
        {
            this.Hide();
            InstanceRef.Show();
        }

        private void button1_Click(object sender, EventArgs e)    // method to exit the whole application
        {
            Application.Exit();
        }

        private void btn_grade_Click(object sender, EventArgs e)    // to compute grade percentage 
        {
            timesTried = int.Parse(lbl_timesTried.Text);
            if (timesTried <= 0)
            {
                lbl_grade.Text = " answer a question first";
            }
            else
            {
                grade = (double.Parse(lbl_cAnswer.Text) / double.Parse(lbl_timesTried.Text));
                if (grade <= 0.7)
                {
                    pbx_grade.Image = Image.FromFile("betterluck.gif");
                    lbl_grade.Text = grade.ToString("P");
                    lbl_grade.ForeColor = System.Drawing.Color.Red;

                }
                else
                {
                    pbx_grade.Image = Image.FromFile("pass1.gif");
                    lbl_grade.Text = grade.ToString("P");
                    lbl_grade.ForeColor = System.Drawing.Color.Green;

                }
            }
        }

        private void lbl_timesTried_Click(object sender, EventArgs e)
        {

        }

    }
}
