using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class ExpressionEvaluator
    {
        Dictionary<Token, IOperation> evaluate = new Dictionary<Token, IOperation>();
        public ExpressionEvaluator() 
        {
            evaluate.Add(new Token("+", TokenType.BinaryOperator, 1), new Addition());
            evaluate.Add(new Token("-", TokenType.BinaryOperator, 1), new Subtraction());
            evaluate.Add(new Token("*", TokenType.BinaryOperator, 2), new Multiplication());
            evaluate.Add(new Token("/", TokenType.BinaryOperator, 2), new Division());
        }
        public ExpressionEvaluator(Dictionary<Token, IOperation> evaluate)
        {
            this.evaluate = evaluate;
        }
        public double Evaluate(string expression)
        {
            PostfixConverter converter= new PostfixConverter();
            List<Token> convertedExpression = new List<Token>();
            convertedExpression = converter.Converter(expression);
            Stack<Token> evaluatorStack = new Stack<Token>();
            foreach (Token token in convertedExpression) 
            {
                if(token.Type == TokenType.Number || token.Type == TokenType.UnaryOperator)
                {
                    evaluatorStack.Push(token);
                }
                if(token.Type == TokenType.BinaryOperator)
                {
                    double[] operands = new double[evaluate[token].OperandCount];
                    for(int arrayIndex =0; arrayIndex < operands.Length; arrayIndex++)
                    {
                        operands[arrayIndex] = Convert.ToDouble(evaluatorStack.Pop().Symbol);
                    }
                    evaluatorStack.Push(new Token(evaluate[token].Evaluate(operands).ToString(), TokenType.Number, 0));
                }
            }
            return Convert.ToDouble(evaluatorStack.Pop().Symbol);
        }
    }
}
