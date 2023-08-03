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
            for (int expressionIndex = 0; expressionIndex < expression.Length; expressionIndex++)
            {
                result *= expression[expressionIndex];
            }
            return result;
        }
    }
}
