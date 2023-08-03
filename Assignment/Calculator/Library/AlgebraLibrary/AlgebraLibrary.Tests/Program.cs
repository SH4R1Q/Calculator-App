using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary.Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //double[] exp = {16, 4};
            string exp = "3+2+5";
            /*
            Addition add = new Addition();
            Subtraction subtract = new Subtraction();
            Multiplication multiply = new Multiplication();
            Division divide = new Division();
            Reciprocal reciprocal = new Reciprocal();
            */
            ExpressionEvaluator expression = new ExpressionEvaluator();
            // PostfixConverter converter = new PostfixConverter();
            Dictionary<Token, IOperation> operations = new Dictionary<Token, IOperation>()
            {
                {new Token("+", TokenType.BinaryOperator, 1), new Addition() },
                {new Token("-", TokenType.BinaryOperator, 1), new Subtraction() },
                {new Token("*", TokenType.BinaryOperator, 2), new Multiplication()},
                {new Token("/", TokenType.BinaryOperator, 2), new Division()}
            };
            foreach (var operation in operations)
            {
                Console.WriteLine(operation);
            }
            Token @new = new Token("+",TokenType.BinaryOperator, 1);
            Console.WriteLine(@new);
            try
            {
                /*
                Console.WriteLine(add.Evaluate(exp));
                Console.WriteLine(subtract.Evaluate(exp));
                Console.WriteLine(multiply.Evaluate(exp));
                Console.WriteLine(divide.Evaluate(exp));
                Console.WriteLine(reciprocal.Evaluate(exp));
                
                List<Token> postFixExpression = new List<Token>();
                postFixExpression = converter.Converter(exp);
                foreach (Token token in postFixExpression)
                {
                    Console.WriteLine(token.Symbol + " \tis a " + token.Type + " and has a precedence of " + token.Precedence + "\n");
                }
                */

                //Console.WriteLine(expression.Evaluate(exp));

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }
    }
}
