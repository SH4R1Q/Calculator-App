using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class Addition : BinaryOperation
    {
        protected override double EvaluateCore(double[] expression)
        {
            double sum = 0;
            for (int expressionIndex = 0; expressionIndex < expression.Length; expressionIndex++)
            {
                sum += expression[expressionIndex];
            }
            return sum;
        }
    }
}
