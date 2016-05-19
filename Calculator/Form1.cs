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

        private void btnLeftParens_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + "(";
        }

        private void btnRightParens_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text + ")";
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

        private void CheckDivisionByZero(List<string> splitArray)
        {
            for (int i = 0; i < (splitArray.Count - 1); i++)
            {
                string stringToTest = splitArray[i] + splitArray[i + 1];

                if (stringToTest.Equals("/0"))
                {
                    throw new System.DivideByZeroException();
                }
            }
        }

        private void CheckValidNumberOfParens(List<string> splitArray)
        {
            int leftParens = 0;
            int rightParens = 0;

            for (int i = 0; i < splitArray.Count; i++)
            {
                if (splitArray[i].Equals("("))
                {
                    leftParens++;
                }

                if (splitArray[i].Equals(")"))
                {
                    rightParens++;
                }
            }

            if (leftParens != rightParens)
            {
                throw new InvalidNumberOfParensException("Left and right parenthesis must match.");
            }
        }

        private double doDivisionAndMultiplication(List<string> splitArray)
        {
            double result = 0.0;

            for (int i = 0; i < splitArray.Count; i++)
            {
                if (splitArray[i].Equals("/") || splitArray[i].Equals("*"))
                {
                    // convert left operand to number
                    double leftOperand = Double.Parse(splitArray[i - 1]);

                    // convert right operand to number
                    double rightOperand = Double.Parse(splitArray[i + 1]);

                    if (splitArray[i].Equals("/"))
                    {
                        result = leftOperand / rightOperand;
                    }
                    else if (splitArray[i].Equals("*"))
                    {
                        result = leftOperand * rightOperand;
                    }

                    // remove operands that have already been calculated
                    splitArray.RemoveRange((i - 1), 3);

                    // add result back into splitArray
                    splitArray.Insert((i - 1), result.ToString());

                    // reset 'i' back to beginning of array
                    i = -1;
                }
            }

            return result;
        }

        private double doAdditionAndSubtraction(List<string> splitArray)
        {
            double result = 0.0;

            for (int i = 0; i < splitArray.Count; i++)
            {

                if (splitArray[i].Equals("+") || splitArray[i].Equals("-"))
                {
                    // convert left operand to number
                    double leftOperand = Double.Parse(splitArray[i - 1]);

                    // convert right operand to number
                    double rightOperand = Double.Parse(splitArray[i + 1]);

                    if (splitArray[i].Equals("+"))
                    {
                        result = leftOperand + rightOperand;
                    }
                    else if (splitArray[i].Equals("-"))
                    {
                        result = leftOperand - rightOperand;
                    }

                    // remove operands that have already been calculated
                    splitArray.RemoveRange((i - 1), 3);

                    // add result back into splitArray
                    splitArray.Insert((i - 1), result.ToString());

                    // reset 'i' back to beginning of array
                    i = -1;
                }
            }

            return result;
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            double result = 0.0;

            // clear text field
            txtOutput.Text = "";

            string inputString = txtInput.Text;

            // remove extraneous signs from beginnning of string
            if (inputString[1] == '/' || inputString[1] == '*' || inputString[1] == '+')
                inputString = inputString.Substring(3);

            // Split array based on whitespace
            List<string> splitArray = new List<string>(inputString.Split(' '));

            try
            {
                // check for division by zero
                CheckDivisionByZero(splitArray);

                // Check for invalid number of parens
                CheckValidNumberOfParens(splitArray);

                int parensCount = 0;
                // count number of parentheses
                for (int i = 0; i < splitArray.Count; i++)
                {
                    if (splitArray[i].Equals("("))
                    {
                        parensCount++;
                    }
                }

                while (parensCount > 0)
                {
                    // find first set of inner most parentheses
                    int leftParensPosition = 0;
                    int rightParensPosition = 0;
                    int parensFound = 0;

                    for (int i = 0; i < splitArray.Count; i++)
                    {
                        if (splitArray[i].Equals("("))
                        {
                            // Count how many parentheses we have found so far
                            parensFound++;

                            // This means we are at the inner most pair of parentheses
                            if (parensFound == parensCount)
                            {
                                leftParensPosition = i;
                            }
                        }
                    }

                    // find right parens position
                    for (int j = leftParensPosition; j < splitArray.Count; j++)
                    {
                        if (splitArray[j].Equals(")"))
                        {
                            rightParensPosition = j;
                        }
                    }

                    // Range has been found.. now calculate the result of number inside the parens pair
                    // Can we make a new, smaller list/array of numbers, and pass of to doDivAndMult + doAddandSub?

                }

                // Do division and multiplication
                result = doDivisionAndMultiplication(splitArray);

                // Do addition and subtraction
                result = doAdditionAndSubtraction(splitArray);

                // Output result of calculation
                txtOutput.Text = result.ToString();
            }
            catch (System.DivideByZeroException ex)
            {
                txtOutput.Text = "Cannot perform division by 0.";
            }
            catch (InvalidNumberOfParensException ex)
            {
                txtOutput.Text = ex.Message;
            }
        }
    }
}