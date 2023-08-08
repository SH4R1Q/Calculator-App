using AlgebraLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class LogEFunction : UnaryOperation
    {
        protected override double EvaluateCore(double[] operands)
        {
            if (operands[0] < 0)
            {
                throw new ExpressionException(Resources.NegativeLog);
            }
            return Math.Log(operands[0]);
        }
    }
}
