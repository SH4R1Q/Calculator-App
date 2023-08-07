using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class Division : BinaryOperation
    {
        protected override double EvaluateCore(double[] operands)
        {
            if(operands[0] == 0) 
            {
                throw new DivideByZeroException();
            }
            return operands[1]/operands[0];
        }
    }
}
