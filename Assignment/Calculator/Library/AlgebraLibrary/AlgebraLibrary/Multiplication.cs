using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class Multiplication : BinaryOperation
    {
        protected override double EvaluateCore(double[] expression)
        {
            double result = 1.0;
            for (int i = 1; i < expression.Length; i++)
            {
                result *= expression[i];
            }
            return result;
        }
    }
}
