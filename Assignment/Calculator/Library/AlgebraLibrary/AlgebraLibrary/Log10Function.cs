using AlgebraLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class Log10Function : UnaryOperation
    {
        protected override double EvaluateCore(double[] operands)
        {
            if (operands[0] < 0)
            {
                throw new ExpressionException(Resources.NegativeLog);
            }
            return Math.Log10(operands[0]);
        }
    }
}
