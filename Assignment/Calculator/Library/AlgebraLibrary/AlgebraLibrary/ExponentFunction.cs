using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class ExponentFunction : BinaryOperation
    {
        protected override double EvaluateCore(double[] operands)
        {
            double result = 1.0;
            for (int arrayIndex = 1; arrayIndex <= operands[1]; arrayIndex++)
            {
                result *= operands[0];
            }
            return result;
        }
    }
}
