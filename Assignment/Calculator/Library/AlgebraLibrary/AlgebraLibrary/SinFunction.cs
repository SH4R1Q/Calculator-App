using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class SinFunction : UnaryOperation
    {
        protected override double EvaluateCore(double[] operands)
        {
            double radians = (operands[0]*(Math.PI))/180;
            return Math.Sin(radians);
        }
    }
}
