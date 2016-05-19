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

        private void Button_Click(object sender, EventArgs e)
        {
            string buttonText = ((Button)sender).Text;

            switch (buttonText)
            {
                case "0":
                    txtInput.Text += "0";
                    break;
                case "1":
                    txtInput.Text += "1";
                    break;
                case "2":
                    txtInput.Text += "2";
                    break;
                case "3":
                    txtInput.Text += "3";
                    break;
                case "4":
                    txtInput.Text += "4";
                    break;
                case "5":
                    txtInput.Text += "5";
                    break;
                case "6":
                    txtInput.Text += "6";
                    break;
                case "7":
                    txtInput.Text += "7";
                    break;
                case "8":
                    txtInput.Text += "8";
                    break;
                case "9":
                    txtInput.Text += "9";
                    break;
                case "+":
                    txtInput.Text += " + ";
                    break;
                case "-":
                    txtInput.Text += " - ";
                    break;
                case "/":
                    txtInput.Text += " / ";
                    break;
                case "*":
                    txtInput.Text += " * ";
                    break;
                case "(":
                    txtInput.Text += " ( ";
                    break;
                case ")":
                    txtInput.Text += " ) ";
                    break;
                case "C":
                    txtInput.Text = "";
                    txtOutput.Text = "";
                    break;
            }
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

        private double doDivisionAndMultiplication(List<string> splitArray, double result)
        {
            double output = 0.0;

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
                        output = leftOperand / rightOperand;
                    }
                    else if (splitArray[i].Equals("*"))
                    {
                        output = leftOperand * rightOperand;
                    }

                    // remove operands that have already been calculated
                    splitArray.RemoveRange((i - 1), 3);

                    // add result back into splitArray
                    splitArray.Insert((i - 1), output.ToString());

                    // reset 'i' back to beginning of array
                    i = -1;
                }
            }

            return result + output;
        }

        private double doAdditionAndSubtraction(List<string> splitArray, double result)
        {
            double output = 0.0;

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
                        output = leftOperand + rightOperand;
                    }
                    else if (splitArray[i].Equals("-"))
                    {
                        output = leftOperand - rightOperand;
                    }

                    // remove operands and operators that have already been calculated
                    splitArray.RemoveRange((i - 1), 3);

                    // add result back into splitArray
                    splitArray.Insert((i - 1), output.ToString());

                    // reset 'i' back to beginning of array
                    i = -1;
                }
            }

            return result + output;
        }

        private void CalculateParentheses(List<string> splitArray)
        {
            // count number of parentheses
            int parensCount = 0;
            for (int i = 0; i < splitArray.Count; i++)
            {
                if (splitArray[i].Equals("("))
                {
                    parensCount++;
                }
            }

            // Run until there are no more parentheses in input
            while (parensCount > 0)
            {
                // find first set of inner most parentheses
                int leftParensPosition = 0;
                int rightParensPosition = 0;
                int parensFound = 0;
                int i = -1;
                double parensResult = 0.0;

                // This while loop is used to determine the location of the inner most left parentheses
                while (parensFound < parensCount)
                {
                    i++;

                    if (splitArray[i].Equals("("))
                    {
                        // Count how many parentheses we have found so far
                        parensFound++;
                    }
                }

                leftParensPosition = i;

                int j = leftParensPosition;
                while (!splitArray[j].Equals(")"))
                {
                    j++;
                }
                rightParensPosition = j;

                // Range has been found.. now calculate the result of number inside the parens pair
                // Can we make a new, smaller list/array of numbers, and pass of to doDivAndMult + doAddandSub?
                int startIndex = leftParensPosition + 1;
                int range = rightParensPosition - leftParensPosition - 1;
                List<string> subList = splitArray.GetRange(startIndex, range);

                parensResult = doDivisionAndMultiplication(subList, parensResult);
                parensResult = doAdditionAndSubtraction(subList, parensResult);

                // remove parens and numbers within
                int rangeToRemove = rightParensPosition - leftParensPosition + 1;
                splitArray.RemoveRange(leftParensPosition, rangeToRemove);

                // insert result back into splitArray
                splitArray.Insert(leftParensPosition, parensResult.ToString());

                // Decrement number of parentheses found
                parensCount--;
            }
        }

        private void RemoveEmptyStrings(List<string> splitArray)
        {
            for (int i = 0; i < splitArray.Count; i++)
            {
                if (splitArray[i].Equals(""))
                {
                    splitArray.RemoveAt(i);
                }
            }
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            double result = 0.0;

            // clear text field
            txtOutput.Text = "";

            string inputString = txtInput.Text;

            // remove extraneous signs from beginnning of string
            if (inputString.Length > 1 )
            {
                if (inputString[1] == '/' || inputString[1] == '*' || inputString[1] == '+')
                    inputString = inputString.Substring(3);
            }

            // Split array based on whitespace
            List<string> splitArray = new List<string>(inputString.Split(' '));

            try
            {
                // check for division by zero
                CheckDivisionByZero(splitArray);

                // Check for invalid number of parens
                CheckValidNumberOfParens(splitArray);

                // remove empty white space
                RemoveEmptyStrings(splitArray);

                // Calculate values inside parentheses
                CalculateParentheses(splitArray);

                // Do division and multiplication
                result = doDivisionAndMultiplication(splitArray, result);

                // Do addition and subtraction
                result = doAdditionAndSubtraction(splitArray, result);

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