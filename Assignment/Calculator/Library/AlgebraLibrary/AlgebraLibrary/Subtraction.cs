using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class Subtraction : BinaryOperation
    {
        protected override double EvaluateCore(double[] expression)
        {
            double sum = 0;
            for (int expressionIndex = 1; expressionIndex < expression.Length; expressionIndex++)
            {
                sum += expression[expressionIndex];
            }
            return expression[0] - sum;
        }
    }
}
