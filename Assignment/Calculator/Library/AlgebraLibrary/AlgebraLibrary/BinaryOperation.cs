using AlgebraLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public abstract class BinaryOperation : IOperation
    {
        private int _operandCount;
        private int _precedence;

        public BinaryOperation()
        {
            _operandCount = 2;
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
        public void Validate(double[] expression)
        {
            if (expression.Length < OperandCount)
            {
                throw new ArgumentException(Resources.WrongSyntax);
            }
        }
        public double Evaluate(double[] expression)
        {
            Validate(expression);
            return EvaluateCore(expression);
        }
        protected abstract double EvaluateCore(double[] expression);
    }
}
