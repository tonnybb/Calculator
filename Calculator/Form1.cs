using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + "9";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + "6";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + "3";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + "0";
        }

        private void btnComma_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + ".";
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + " + ";
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + " - ";
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + " / ";
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + " * ";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInput.Text = "";
            txtOutput.Text = "";
        }

        private bool isValidInput(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if ( (input[i] < '(' || input[i] > '9') && input[i] != ' ' )
                {
                    return false;
                }
            }

            return true;
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            // clear text field
            txtOutput.Text = "";

            string inputString = txtInput.Text;

            // check for letters in input
            bool hasValidInput = isValidInput(inputString);

            // remove extraneous signs from beginnning of string
            if (inputString[1] == '/' || inputString[1] == '*' || inputString[1] == '+')
                inputString = inputString.Substring(3);

            if (hasValidInput)
            {
                // Split array based on whitespace
                ArrayList splitArray = new ArrayList( inputString.Split(' ') );

                //search for * or / and do those first
                double result = 0.0;
                for (int i = 0; i < splitArray.Count; i++)
                {
                    if (splitArray[i].Equals("/") || inputString[i].Equals("*") )
                    {
                        int divideOrMultiplyFoundAt = i;

                        if (splitArray[i].Equals("/"))
                        {
                            result += inputString[i - 1] / inputString[i + 1];
                        }
                        else if (inputString[i].Equals("*"))
                        {
                            result += inputString[i - 1] * inputString[i + 1];
                        }


                        // found / or *

                        // get both operands

                        // get left operand


                    }
                }

                // check for division by 0
            }
            else
            {
                txtOutput.Text = "Invalid input";
            }




        }
    }
}