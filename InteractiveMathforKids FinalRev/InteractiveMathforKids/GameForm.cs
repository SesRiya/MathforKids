using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InteractiveMathforKids
{
    public partial class InteractiveMath : Form
    {
        // public static InteractiveMath staticVar = null;       // setting a static var so HelpForm goes back to mainform and won't create a new mainform

        public InteractiveMath()
        {
            InitializeComponent();
        }
        // declare global variables
        int answer = 0, userAnswer;
        int correctCount = 0, totalTimestried = 0;

        bool levelTwo = false;                          // for Level Two activation
        bool levelThree = false;                        // for Level Three activation
        //initialize Soundplayer for added soundeffects
        SoundPlayer mySound = new SoundPlayer(@"C:\Windows\Media\notify.wav");
        SoundPlayer mySoundans = new SoundPlayer(@"C:\Windows\Media\tada.wav");
        SoundPlayer mySoundwrong = new SoundPlayer(@"C:\Windows\Media\Windows Hardware Fail.wav");

        private void btn_question_Click(object sender, EventArgs e)
        {
            mySound.Play();
            Random seed = new Random();              //generate a random number seed so that two numbers will be different
            int num1 = seed.Next(1, 11);             // generates number from 1 to 10
            int num2 = seed.Next(1, 11);
            int num3 = seed.Next(1, 11);
            lblTostring(num1, num2, num3);
            Gif(num1, num2, num3);                 //Call Method Gif to assign gif image to the picturebox
            Clear();                               //Call Method Clear to clear label fields and textbox

            /*LEVEL ONE: FIXED ADDITION AND MULTIPLICATION METHOD*/
            LevelOne(num1, num2, num3);             //Call method for addition multiplication
            btn_answer.Enabled = true;             //btn_answer disabled button enabled after make question is clicked


            /* LEVEL TWO: FIXED MULTIPLICATION AND SUBTRACTION METHOD BUT MAKE SURE NO NEGATIVE ANSWER*/
            if (levelTwo == true)
            {
                LevelTwo(num1, num2, num3);
                int i = 0;
                while (answer < 0)              // just to make sure we won't generate a problem with a negative answer
                {
                    Random Seed = new Random();
                    num1 = Seed.Next(1, 11);
                    num2 = Seed.Next(1, 11);
                    num3 = Seed.Next(1, 11);
                    if (i >= 100)
                    {
                        num3 = 1;
                        break;
                    }

                    answer = num1 * num2 - num3;
                    lblTostring(num1, num2, num3);
                    Gif(num1, num2, num3);
                }

            }
            /* LEVEL THREE: RANDOM BUT MAKE SURE NO NEGATIVE ANSWER*/
            if (levelThree == true)
            {
                LevelThree(num1, num2, num3);
                int i = 0;
                while (answer < 0)              // just to make sure we won't generate a problem with a negative answer
                {
                    Random Seed = new Random();
                    num1 = Seed.Next(1, 11);
                    num2 = Seed.Next(1, 11);
                    num3 = Seed.Next(1, 11);
                    if (i >= 100)
                    {
                        num3 = 1;
                        break;
                    }
                    i++;
                    answer = num1 * num2 - num3;                        // if answer is still negative we set to this equation so 
                    lbl_1stOperator.Text = " * ";
                    lbl_2ndOperator.Text = " - ";

                    pbx5.Image = Image.FromFile("multiplication.gif");  // less likely to get a negative answer
                    pbx6.Image = Image.FromFile("minus.gif");
                    lblTostring(num1, num2, num3);
                    Gif(num1, num2, num3);
                }
            }
        }

        private void btn_answer_Click(object sender, EventArgs e)
        {
            lbl_answer.Text = answer.ToString();                     // convert computer answer to string
            btn_answer.Enabled = false;                              //btn_answer disabled since user already saw the answer so that he can generate new questions

            catchTry();                                             // method to make sure user inputs a valid number and a number within minimum and maximum range
            ifElse();                                               // comparison of user answer vs generated computer answer

            totalTimestried = totalTimestried + 1;                   // counter to count times tried
            lbl_correctAnswer.Text = correctCount.ToString();       //converting counters to string
            lbl_timesTried.Text = totalTimestried.ToString();

            if (correctCount >= 5)                                  //criteria for moving to Level Two
            {
                btn_levelTwo.Enabled = true;
            }
            if (correctCount >= 10)                                 // criteria for moving to level three
            {
                btn_levelThree.Enabled = true;
            }
        }

        private void btn_levelOne_Click(object sender, EventArgs e)  //click to go back to level one
        {
            levelTwo = false;
            levelThree = false;
            btn_answer.Enabled = false;
            lbl_Level.Text = ("Level 1");
            lbl_1stOperator.Text = " + ";
            lbl_2ndOperator.Text = " * ";
        }

        private void btn_levelTwo_Click(object sender, EventArgs e)   // click to go to level two
        {
            levelTwo = true;
            levelThree = false;
            btn_answer.Enabled = false;
            lbl_Level.Text = ("Level 2");
            Clear();
        }

        private void btn_levelThree_Click(object sender, EventArgs e) // click to go to level three
        {
            levelThree = true;
            levelTwo = false;
            btn_answer.Enabled = false;
            lbl_Level.Text = ("Level 3");
            Clear();
        }

        private void btn_help_Click(object sender, EventArgs e)   // to display HelpForm
        {
            this.Hide();
            HelpForm helpForm = new HelpForm();
            helpForm.InstanceRef = this;
            helpForm.Show();
        }

        private void btn_exit_Click(object sender, EventArgs e)   // to exit game
        {
            this.Hide();
            ExitForm exitGame = new ExitForm();                
            exitGame.InstanceRef = this;
            exitGame.Show();
            exitGame.lbl_cAnswer.Text = correctCount.ToString();       // to pass variable correctCOunt and totaltimestried to ExitForm
            exitGame.lbl_timesTried.Text = totalTimestried.ToString();
        }

        /* METHODS USED IN THIS PROGRAM */

        private void lblTostring(int w1, int w2, int w3)
        {
            lbl_num1.Text = w1.ToString();       // convert int to string for label fields
            lbl_num2.Text = w2.ToString();
            lbl_num3.Text = w3.ToString();
        }

        private void Gif(int x1, int x2, int x3)                //method to assign gif to picturebox
        {
            pbx1.Image = Image.FromFile("giphy" + x1.ToString() + ".gif");
            pbx2.Image = Image.FromFile("giphy" + x2.ToString() + ".gif");
            pbx3.Image = Image.FromFile("giphy" + x3.ToString() + ".gif");
            pbx4.Image = Image.FromFile("pooh.gif");
        }

        private void Clear()                      //method to Clear labels and textbox
        {
            lbl_answer.Text = " ";
            txt_answer.Text = "";
            txt_answer.Focus();
            lbl_goodTry.Text = " ";
        }

        private void catchTry()                                  // try and catch method to ensure user enters numbers and within the range only
        {                                                        // exception handling
            try
            {
                userAnswer = int.Parse(txt_answer.Text);

                if (userAnswer < 0 || userAnswer > 1000)       // set a limit of answers from minimum answer of 0 to maximum answer of 1000
                    MessageBox.Show("Enter a number from 0 to 1000 only");
            }
            catch
            {
                MessageBox.Show("Enter a number from 0 to 1000 only");    //user asks to enter numbers only and not characters
            }
        }

        private void ifElse()                     // method to compare useranswer to answer generated by computer
        {
            Random seed = new Random();
            int picCorrect = seed.Next(1, 5);     // random pics on correct and wrong answer
            int picWrong = seed.Next(1, 5);       // 4 gif's to choose from

            if (userAnswer == answer)          // comparing answer to count correct times 
            {
                pbx4.Image = Image.FromFile("goodjob" + picCorrect.ToString() + ".gif");
                correctCount = correctCount + 1;
                lbl_goodTry.Text = (" Good Job! ");
                lbl_goodTry.ForeColor = System.Drawing.Color.CornflowerBlue;
                mySoundans.Play();
            }
            else
            {
                pbx4.Image = Image.FromFile("tryagain" + picWrong.ToString() + ".gif");
                lbl_goodTry.Text = (" Try Again ");
                lbl_goodTry.ForeColor = System.Drawing.Color.Sienna;
                mySoundwrong.Play();
            }
        }

        private void LevelOne(int x1, int x2, int x3)      // method to compute addition&multiplication
        {
            answer = x1 + x2 * x3;
            pbx5.Image = Image.FromFile("plus.gif");
            pbx6.Image = Image.FromFile("multiplication.gif");

        }

        private void LevelTwo(int x1, int x2, int x3)      //method for LevelTwo
        {
            lbl_1stOperator.Text = " * ";
            lbl_2ndOperator.Text = " - ";

            answer = x1 * x2 - x3;           // compute new answer using multiplication and subtraction

            pbx5.Image = Image.FromFile("multiplication.gif");
            pbx6.Image = Image.FromFile("minus.gif");
        }

        private void LevelThree(int z1, int z2, int z3)   //method for LevelThree
        {
            Random Seed = new Random();
            int operatorOne = Seed.Next(1, 3);      // generate random operator one only * or +
            int operatorTwo = Seed.Next(1, 4);     // 2nd operator can be + or - or *

            if (operatorOne == 1 && operatorTwo == 1)
            {
                lbl_1stOperator.Text = " * ";
                lbl_2ndOperator.Text = " * ";
                answer = z1 * z2 * z3;
            }
            else if (operatorOne == 1 && operatorTwo == 2)
            {
                lbl_1stOperator.Text = " * ";
                lbl_2ndOperator.Text = " + ";
                answer = z1 * z2 + z3;
            }
            else if (operatorOne == 1 && operatorTwo == 3)
            {
                lbl_1stOperator.Text = " * ";
                lbl_2ndOperator.Text = " - ";
                answer = z1 * z2 - z3;
            }
            else if (operatorOne == 2 && operatorTwo == 1)
            {
                lbl_1stOperator.Text = " + ";
                lbl_2ndOperator.Text = " * ";
                answer = z1 + z2 * z3;
            }
            else if (operatorOne == 2 && operatorTwo == 2)
            {
                lbl_1stOperator.Text = " + ";
                lbl_2ndOperator.Text = " + ";
                answer = z1 + z2 + z3;
            }
            else if (operatorOne == 2 && operatorTwo == 3)
            {
                lbl_1stOperator.Text = " + ";
                lbl_2ndOperator.Text = " - ";
                answer = z1 + z2 - z3;
            }
            pbx5.Image = Image.FromFile("operator" + operatorOne.ToString() + ".gif");
            pbx6.Image = Image.FromFile("operator" + operatorTwo.ToString() + ".gif");
        }
    }
}