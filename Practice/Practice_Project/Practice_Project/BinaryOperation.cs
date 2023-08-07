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
        public int OperandCount
        {
            get => _operandCount;
            set => _operandCount = 2;
        }
        public bool Validate(double[] expression)
        {
            try
            {
                if (expression.Length < OperandCount) throw new ArgumentException(String.Format("SYNTAX ERROR"), "expression");
                return true;
            }
            catch(ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }
        public double Evaluate(double[] expression)
        {
            if (Validate(expression))
            { 
                return EvaluateCore(expression);
            }
            return 0;
        }
        protected abstract double EvaluateCore(double[] expression);
    }
}
