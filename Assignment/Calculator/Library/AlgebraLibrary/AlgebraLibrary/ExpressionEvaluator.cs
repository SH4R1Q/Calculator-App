using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class ExpressionEvaluator
    {
        Dictionary<Token, IOperation> _operatorInfo = new Dictionary<Token, IOperation>();
        public ExpressionEvaluator() 
        {
            _operatorInfo.Add(new Token("+", TokenType.BinaryOperator, 2), new Addition());
            _operatorInfo.Add(new Token("-", TokenType.BinaryOperator, 2), new Subtraction());
            _operatorInfo.Add(new Token("*", TokenType.BinaryOperator, 3), new Multiplication());
            _operatorInfo.Add(new Token("/", TokenType.BinaryOperator, 3), new Division());
            _operatorInfo.Add(new Token("rcp", TokenType.UnaryOperator, 5), new Reciprocal());
            _operatorInfo.Add(new Token("fact", TokenType.UnaryOperator, 5), new Factorial());
            _operatorInfo.Add(new Token("+", TokenType.UnaryOperator, 4), new PositiveOperator());
            _operatorInfo.Add(new Token("-", TokenType.UnaryOperator, 4), new NegativeOperator());
            _operatorInfo.Add(new Token("sqrt", TokenType.UnaryOperator, 5), new SquareRoot());
            _operatorInfo.Add(new Token("sin", TokenType.UnaryOperator, 5), new SinFunction());
            _operatorInfo.Add(new Token("cos", TokenType.UnaryOperator, 5), new CosFunction());
        }
        public ExpressionEvaluator(Dictionary<Token, IOperation> _operatorInfo)
        {
            this._operatorInfo = _operatorInfo;
        }
        public double Evaluate(string expression)
        {
            PostfixConverter converter= new PostfixConverter();
            List<Token> convertedExpression = new List<Token>();
            convertedExpression = converter.Converter(expression);
            Stack<Token> evaluatorStack = new Stack<Token>();
            foreach (Token token in convertedExpression) 
            {
                if (token.Type == TokenType.Number)
                {
                    evaluatorStack.Push(token);
                }
                else if (token.Type == TokenType.UnaryOperator)
                {
                    double[] operands = new double[_operatorInfo[token].OperandCount];
                    for (int arrayIndex = 0; arrayIndex < operands.Length; arrayIndex++)
                    {
                        operands[arrayIndex] = Convert.ToDouble(evaluatorStack.Pop().Symbol);
                    }
                    evaluatorStack.Push(new Token(_operatorInfo[token].Evaluate(operands).ToString(), TokenType.Number, 1));
                }
                else if (token.Type == TokenType.BinaryOperator)
                {
                    double[] operands = new double[_operatorInfo[token].OperandCount];
                    for (int arrayIndex = 0; arrayIndex < operands.Length; arrayIndex++)
                    {
                        try
                        {
                            operands[arrayIndex] = Convert.ToDouble(evaluatorStack.Pop().Symbol);
                        }
                        catch
                        {
                            if (token.Symbol.Equals("+") || token.Symbol.Equals("-"))
                            {
                                operands[arrayIndex] = 0;
                            }
                            else if(token.Symbol.Equals("*"))
                            {
                                operands[arrayIndex] = 1;
                            }
                        }
                    }
                    evaluatorStack.Push(new Token(_operatorInfo[token].Evaluate(operands).ToString(), TokenType.Number, 1));
                }
            }
            return Convert.ToDouble(evaluatorStack.Pop().Symbol);
        }
    }
}
