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
        public int OperandCount
        {
            get => _operandCount;
            set => _operandCount = 1;
        }
        public void Validate(double[] expression)
        {
            if (expression.Length < OperandCount)
            { }
            throw new ArgumentException(String.Format("SYNTAX ERROR"), "expression");
        }
        public double Evaluate(double[] expression)
        {
            Validate(expression);
            return EvaluateCore(expression);
        }
        protected abstract double EvaluateCore(double[] expression);
    }
}
