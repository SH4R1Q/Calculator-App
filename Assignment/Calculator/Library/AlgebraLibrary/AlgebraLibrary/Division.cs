using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class Division : BinaryOperation
    {
        protected override double EvaluateCore(double[] expression)
        {
            double result = expression[expression.Length - 1];
            for (int i = expression.Length - 2; i >= 0; i--)
            {
                result = expression[i] / result;
            }
            return result;
        }
    }
}
