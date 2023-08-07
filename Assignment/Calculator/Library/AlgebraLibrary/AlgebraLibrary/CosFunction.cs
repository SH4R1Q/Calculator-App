using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class CosFunction : UnaryOperation
    {
        protected override double EvaluateCore(double[] operands)
        {
            double radians = (operands[0] * (Math.PI)) / 180;
            return Math.Round(Math.Cos(radians));
        }
    }
}
