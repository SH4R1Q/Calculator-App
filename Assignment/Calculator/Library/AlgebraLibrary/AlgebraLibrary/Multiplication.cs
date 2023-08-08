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
            return operands[0] * operands[1];
        }
    }
}
