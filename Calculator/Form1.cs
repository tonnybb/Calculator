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
                if ((input[i] < '(' || input[i] > '9') && input[i] != ' ')
                {
                    return false;
                }
            }

            return true;
        }

        private bool hasNoDivisionByZero(List<string> splitArray)
        {
            for (int i = 0; i < (splitArray.Count - 1); i++)
            {
                string stringToTest = splitArray[i] + splitArray[i + 1];

                if (stringToTest.Equals("/0"))
                {
                    return false;
                }
            }

            return true;
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            double result = 0.0;

            // clear text field
            txtOutput.Text = "";

            string inputString = txtInput.Text;

            // check for letters in input
            bool hasValidInput = isValidInput(inputString);

            // remove extraneous signs from beginnning of string
            if (inputString[1] == '/' || inputString[1] == '*' || inputString[1] == '+')
                inputString = inputString.Substring(3);

            // Split array based on whitespace
            List<string> splitArray = new List<string>(inputString.Split(' '));

            // check for division by zero
            bool nodivisionByZero = hasNoDivisionByZero(splitArray);

            if (nodivisionByZero)
            {
                for (int i = 0; i < splitArray.Count; i++)
                {
                    if (splitArray[i].Equals("/") || inputString[i].Equals("*"))
                    {
                        // convert left operand to number
                        string leftOperandString = splitArray[i - 1];
                        double leftOperand = Double.Parse(leftOperandString);

                        // convert right operand to number
                        string rightOperandString = splitArray[i + 1];
                        double rightOperand = Double.Parse(rightOperandString);

                        if (splitArray[i].Equals("/"))
                        {
                            result += leftOperand / rightOperand;
                        }
                        else if (inputString[i].Equals("*"))
                        {
                            result += leftOperand * rightOperand;
                        }

                        // remove operands that have already been calculated
                        splitArray.RemoveRange((i - 1), 3);

                        // add result back into splitArray
                        splitArray.Insert((i - 1), result.ToString());

                        // reset 'i' back to beginning of array
                        i = -1;
                    }
                }

                txtOutput.Text = result.ToString();

                // check for division by 0
            }
            else
            {
                txtOutput.Text = "Cannot perform division by 0.";
            }




        }
    }
}