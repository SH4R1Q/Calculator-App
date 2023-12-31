﻿using AlgebraLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public abstract class UnaryOperation : IOperation
    {
        private int _operandCount;
        private int _precedence;

        public UnaryOperation()
        {
            _operandCount = 1;
        }
        public int OperandCount
        {
            get => _operandCount;
            set => _operandCount = value;
        }
        public int Precedence
        {
            get => _precedence; 
            set => _precedence = value;
        }
        public void Validate(double[] operands)
        {
            if (operands.Length < OperandCount)
            {
                throw new ArgumentException(Resources.WrongSyntax);
            }
        }
        public double Evaluate(double[] operands)
        {
            Validate(operands);
            return EvaluateCore(operands);
        }
        protected abstract double EvaluateCore(double[] operands);
    }
}
