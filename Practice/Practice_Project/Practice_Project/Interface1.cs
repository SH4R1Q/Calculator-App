using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MathLibrary
{
    public interface IOperation
    {
        public double Evaluate(List<double> expression);
    }
    public abstract class BinaryOperation : IOperation
    {
        public bool Validate(List<double> expression)
        {
            try
            {
                if (expression.Count < 2) throw new ArgumentException(String.Format("SYNTAX ERROR"), "expression");
                return true;
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }
        public abstract double Evaluate(List<double> expression);
    }
    public class Addition : BinaryOperation
    {
        public override double Evaluate(List<double> expression)
        {
            if (Validate(expression))
            {
                return expression.Sum();
            }
            return 0;
        }
    }
    public class Subtraction : BinaryOperation
    {
        public override double Evaluate(List<double> expression)
        {
            if (Validate(expression))
            {
                return 2*expression[0] - expression.Sum();
            }
            return 0;
        }
    }
    public class Multiplication : BinaryOperation
    {
        public override double Evaluate(List<double> expression)
        {
            if (Validate(expression))
            {
                double result = 1.0;
                for (int i = 0; i < expression.Count; i++)
                {
                    result *= expression[i];
                }
                return result;
            }
            return 0;
        }
    }
    public class Division : BinaryOperation
    {
        public override double Evaluate(List<double> expression)
        {
            if (Validate(expression))
            {
                double result = expression[expression.Count-1];
                for (int i = expression.Count-2; i >= 0 ; i--)
                {
                    result = expression[i]/result;
                }
                return result;
            }
            return 0;
        }
    }
    internal class Testing
    {
        public static void Main()
        {
            List<double> testlist = new List<double>();
            testlist.Add(16);
            testlist.Add(4);
            testlist.Add(2);
            Addition add = new Addition();
            Subtraction subtract = new Subtraction();
            Multiplication multiply = new Multiplication();
            Division divide = new Division();
            Console.WriteLine(add.Evaluate(testlist));
            Console.WriteLine(subtract.Evaluate(testlist));
            Console.WriteLine(multiply.Evaluate(testlist));
            Console.WriteLine(divide.Evaluate(testlist));
        }
    }
}
