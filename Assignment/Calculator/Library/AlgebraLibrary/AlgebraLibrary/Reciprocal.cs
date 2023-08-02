using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class Reciprocal : UnaryOperation
    {
        protected override double EvaluateCore(double[] expression)
        {
            return 1 / expression[0];
        }
    }
}
