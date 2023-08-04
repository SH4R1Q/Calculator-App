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
            string exp = "fact(5-2)";
            /*
            Addition add = new Addition();
            Subtraction subtract = new Subtraction();
            Multiplication multiply = new Multiplication();
            Division divide = new Division();
            Reciprocal reciprocal = new Reciprocal();
            */
            ExpressionEvaluator expression = new ExpressionEvaluator();
            // PostfixConverter converter = new PostfixConverter();
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
                List<Token> postFix = converter.Tokenizer(exp);
                foreach(Token token in postFix)
                {
                    Console.WriteLine(token.Symbol + " \tis a " + token.Type + " and has a precedence of " + token.Precedence + "\n");
                }

                foreach (Token token in postFixExpression)
                {
                    Console.WriteLine(token.Symbol + " \tis a " + token.Type + " and has a precedence of " + token.Precedence + "\n");
                }
                */
                Console.WriteLine(expression.Evaluate(exp));

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }
    }
}
