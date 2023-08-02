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
            for (int i = 1; i < expression.Length; i++)
            {
                sum += expression[i];
            }
            return expression[0] - sum;
        }
    }
}
