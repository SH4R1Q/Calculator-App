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
            for (int i = 0; i < expression.Length; i++)
            {
                sum += expression[i];
            }
            return sum;
        }
    }
}
