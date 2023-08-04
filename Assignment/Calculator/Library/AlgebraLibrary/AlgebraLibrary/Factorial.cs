using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class Factorial : UnaryOperation
    {
        protected override double EvaluateCore(double[] operands)
        {
            double result = 1.0;
            for(int arrayIndex = 1;  arrayIndex <= operands[0]; arrayIndex++)
            {
                result *= arrayIndex;
            }
            return result;
        }
    }
}
