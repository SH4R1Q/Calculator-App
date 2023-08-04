﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class Division : BinaryOperation
    {
        protected override double EvaluateCore(double[] operands)
        {
            double result = operands[operands.Length - 1];
            for (int operandsIndex = operands.Length - 2; operandsIndex >= 0; operandsIndex--)
            {
                result = result / operands[operandsIndex] ;
            }
            return result;
        }
    }
}
