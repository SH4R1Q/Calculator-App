using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class PercentageFunction : BinaryOperation
    {
        protected override double EvaluateCore(double[] operands)
        {
            double result = operands[0]*operands[1];
            return result/100;
        }
    }
}
