using AlgebraLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class ExpressionEvaluator
    {
        private static PostfixConverter _converter = new PostfixConverter();
        private static List<Token> _convertertedExpression = new List<Token>();
        private Dictionary<Token, IOperation> _operatorInfo = new Dictionary<Token, IOperation>();
        public ExpressionEvaluator() 
        {
            
        }
        public ExpressionEvaluator(Dictionary<Token, IOperation> _operatorInfo)
        {
            this._operatorInfo = _operatorInfo;
        }
        public double Evaluate(string expression)
        {
            _convertertedExpression = _converter.Converter(expression);
            Stack<Token> evaluatorStack = new Stack<Token>();
            foreach (Token token in _convertertedExpression) 
            {
                if (token.Type == TokenType.Number)
                {
                    evaluatorStack.Push(token);
                }
                else if (token.Type == TokenType.UnaryOperator)
                {
                    double[] operands = new double[_operatorInfo[token].OperandCount];
                    for (int arrayIndex = operands.Length -1; arrayIndex >= 0 ; arrayIndex--)
                    {
                        try
                        {
                            operands[arrayIndex] = Convert.ToDouble(evaluatorStack.Pop().Symbol);
                        }
                        catch
                        {
                            operands[arrayIndex] = 0;
                        }
                    }
                    evaluatorStack.Push(new Token(_operatorInfo[token].Evaluate(operands).ToString(), TokenType.Number, 1));
                }
                else if (token.Type == TokenType.BinaryOperator)
                {
                    double[] operands = new double[_operatorInfo[token].OperandCount];
                    for (int arrayIndex = operands.Length-1; arrayIndex >= 0; arrayIndex--)
                    {
                        try
                        {
                            operands[arrayIndex] = Convert.ToDouble(evaluatorStack.Pop().Symbol);
                        }
                        catch
                        {
                            if (token.Symbol.Equals(Resources.Plus) || token.Symbol.Equals(Resources.Minus))
                            {
                                operands[arrayIndex] = 0;
                            }
                            else if(token.Symbol.Equals(Resources.Multiply))
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
