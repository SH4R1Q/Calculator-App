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
            for (int expressionIndex = expression.Length - 2; expressionIndex >= 0; expressionIndex--)
            {
                result = expression[expressionIndex] / result;
            }
            return result;
        }
    }
}
