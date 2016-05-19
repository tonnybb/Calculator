using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class InvalidNumberOfParensException: Exception
    {
        public InvalidNumberOfParensException(string message)
            : base(message)
        {

        }
    }
}
