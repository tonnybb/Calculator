using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class MultipleConsecutiveOperatorsDetectedException: Exception
    {
        public MultipleConsecutiveOperatorsDetectedException(string message)
            : base(message)
        {

        }
    }
}
