using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class Multiplication : BinaryOperation
    {
        protected override double EvaluateCore(double[] operands)
        {
            double result = 1.0;
            for (int operandsIndex = 0; operandsIndex < operands.Length; operandsIndex++)
            {
                result *= operands[operandsIndex];
            }
            return result;
        }
    }
}
