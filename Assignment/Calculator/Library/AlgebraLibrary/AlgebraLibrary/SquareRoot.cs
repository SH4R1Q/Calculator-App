﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class SquareRoot : UnaryOperation
    {
        protected override double EvaluateCore(double[] operands)
        {
            if (operands[0] < 0)
            {
                throw new NegativeRootException();
            }
            return Math.Sqrt(operands[0]);
        }
    }
}
