using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

/* 
 * TODO:

    handle duplicate /*-+ signs within input string
    - how about.. when detecting a /*-+ operation, and trying to convert operands
	    into numbers, if the conversion fails, remove the operand from the splitArray
     */

namespace Calculator
{
    public partial class Form1 : Form
    {
        private bool equalsButtonPressed = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (equalsButtonPressed)
            {
                txtInput.Text = "";
                equalsButtonPressed = false;
            }

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
                case ".":
                    txtInput.Text += ".";
                    break;
                case "C":
                    txtInput.Text = "";
                    break;
                case "!":
                    txtInput.Text += " ! ";
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

        private double DoDivisionAndMultiplication(List<string> splitArray, double result)
        {
            double output = 0.0;

            /* If there is only one number in the array, there is no need to do any
             * calculation. Just return the value.
             */
            if (splitArray.Count == 1)
            {
                return Double.Parse(splitArray[0]);
            }

            // This will be a negative number
            if (splitArray.Count == 2)
            {
                string str = splitArray[0] + splitArray[1];
                double number = Double.Parse(str);
                return number;
            }

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

        private double DoAdditionAndSubtraction(List<string> splitArray, double result)
        {
            double output = 0.0;

            /* If there is only one number in the array, there is no need to do any
             * calculation. Just return the value.
             */
            if (splitArray.Count == 1)
            {
                return Double.Parse(splitArray[0]);
            }

            // This will be a negative number
            if (splitArray.Count == 2)
            {
                string str = splitArray[0] + splitArray[1];
                double number = Double.Parse(str);
                return number;
            }

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
                // Locate first set of innermost parentheses
                int leftParensPosition = 0;
                int rightParensPosition = 0;
                int parensFound = 0;
                int i = -1;
                double parensResult = 0.0;

                // This while loop is used to determine the location of the innermost left parentheses
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

                // Find position of innermost right parentheses
                int j = leftParensPosition;
                while (!splitArray[j].Equals(")"))
                {
                    j++;
                }
                rightParensPosition = j;

                // Take the numbers inside the parentheses and put them into a sublist
                int startIndex = leftParensPosition + 1;
                int range = rightParensPosition - leftParensPosition - 1;
                List<string> subList = splitArray.GetRange(startIndex, range);

                // Calculate the result of the numbers found inside parentheses pair
                parensResult = DoDivisionAndMultiplication(subList, parensResult);
                parensResult = DoAdditionAndSubtraction(subList, parensResult);

                // Remove parentheses and numbers within
                int rangeToRemove = rightParensPosition - leftParensPosition + 1;
                splitArray.RemoveRange(leftParensPosition, rangeToRemove);

                // Insert result of calculation within parentheses back into splitArray
                splitArray.Insert(leftParensPosition, parensResult.ToString());

                // One pair of parentheses have been dealt with.
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

        private void RemoveUnnecessarySignsFromBeginningOfInput(List<string> splitArray)
        {
            if (splitArray.Count > 0)
            {
                while (splitArray[0].Equals("/") || splitArray[0].Equals("*") || splitArray[0].Equals("+"))
                {
                    splitArray.RemoveAt(0);
                }
            }
        }

        private void AddMultiplicationInFrontOfParentheses(List<string> splitArray)
        {
            // Convert the splitArray to string so that we can use RegEx on it
            string str = string.Join("", splitArray);

            // This pattern will match any number followed by a "("
            Regex regex = new Regex(@"[1-9]\(");
            Match match = regex.Match(str);

            int count = 1;
            while (match.Success)
            {
                int matchFoundIndex = match.Index;

                /* What is count used for?
                 * When regex.Match has been applied, it has already found all matching patterns within the string.
                 So when we insert stuff into the array, it messes with the index positions of matching patterns that regex.Match found.
                 Therefore, we need to increase the the index position where we insert the * signs every time an insertion is made.
                */
                splitArray.Insert((matchFoundIndex + count), "*");

                // See if there is another match
                match = match.NextMatch();
                count++;
            }
        }

        private void DetectMultipleConsecutiveOperators(List<string> splitArray)
        {
            string[] operators = { "+", "-", "/", "*", "." };

            for (int i = 0; i < (splitArray.Count -1) ; i++)
            {
                for (int j = 0; j < operators.Length; j++)
                {
                    for (int k = 0; k < operators.Length; k++)
                    {
                        if (splitArray[i].Equals(operators[j]) && splitArray[i + 1].Equals(operators[k]))
                        {
                            throw new MultipleConsecutiveOperatorsDetectedException("Multiple, consecutive operators detected in input.");
                        }
                    }
                }
            }
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            

            double result = 0.0;

            string inputString = txtInput.Text;
            //string inputString = "2 +  (  - 5 ) ";

            // Split array based on whitespace
            List<string> splitArray = new List<string>(inputString.Split(' '));

            try
            {
                // Check for invalid number of parens
                CheckValidNumberOfParens(splitArray);

                // Remove empty white space
                RemoveEmptyStrings(splitArray);

                // Show error if users something like "+ + - *"
                DetectMultipleConsecutiveOperators(splitArray);

                // Remove duplicate or unnecessary / * + signs from beginning of input
                RemoveUnnecessarySignsFromBeginningOfInput(splitArray);

                // check for division by zero
                CheckDivisionByZero(splitArray);

                // Insert multiplication signs to the left of parentheses if there is a number immediately preceeding it
                AddMultiplicationInFrontOfParentheses(splitArray);

                // Calculate values inside parentheses
                CalculateParentheses(splitArray);

                // Do division and multiplication
                DoDivisionAndMultiplication(splitArray, result);

                // Do addition and subtraction
                DoAdditionAndSubtraction(splitArray, result);

                /*
                 *  Only display result if this is the first time the equals button has been pressed
                 *   after a calculation has been done
                 */
                if (!equalsButtonPressed) 
                {
                    // Output result of calculation
                    txtInput.Text += " = " + splitArray[0];
                }

                equalsButtonPressed = true;

            }
            catch (System.DivideByZeroException ex)
            {
                txtInput.Text = "Cannot perform division by 0.";
            }
            catch (InvalidNumberOfParensException ex)
            {
                txtInput.Text = ex.Message;
            }
            catch (System.FormatException ex)
            {
                string detailedMessage = ex.Message;
                MessageBox.Show("Something went wrong.\nDetailed message: \n" + detailedMessage);
            }
            catch (MultipleConsecutiveOperatorsDetectedException ex)
            {
                txtInput.Text = ex.Message;
            }
        }
    }
}