using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class Subtraction : BinaryOperation
    {
        protected override double EvaluateCore(double[] operands)
        {
            double sum = 0;
            for (int operandsIndex = 1; operandsIndex < operands.Length; operandsIndex++)
            {
                sum += operands[operandsIndex];
            }
            return sum - operands[0];
        }
    }
}
