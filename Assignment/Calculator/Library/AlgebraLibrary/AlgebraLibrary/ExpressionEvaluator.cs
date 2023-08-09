using AlgebraLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;

namespace AlgebraLibrary
{
    public class ExpressionEvaluator
    {
        private static PostfixConverter _converter = new PostfixConverter();
        private static List<Token> _convertertedExpression = new List<Token>();
        private Dictionary<Token, IOperation> _operatorInfo = new Dictionary<Token, IOperation>();
        public ExpressionEvaluator() 
        {           
            List<ConfigureClass> tokens = new List<ConfigureClass>();
            string filePath = "Properties\\ConfigurationFile.json";
            string fileName = File.ReadAllText(filePath);
            tokens = JsonConvert.DeserializeObject<List<ConfigureClass>>(fileName);
            foreach (var token in tokens)
            {
                Type classType = Type.GetType(token.ClassName);
                object instance = Activator.CreateInstance(classType);
                _operatorInfo.Add(new Token(token.Symbol, token.Type, token.Precedence), (IOperation)instance);
            }
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
                            operands[arrayIndex] = Convert.ToDouble(evaluatorStack.Pop().Symbol);
                    }
                    evaluatorStack.Push(new Token(_operatorInfo[token].Evaluate(operands).ToString(), TokenType.Number, 1));
                }
                else if (token.Type == TokenType.BinaryOperator)
                {
                    double[] operands = new double[_operatorInfo[token].OperandCount];
                    for (int arrayIndex = operands.Length-1; arrayIndex >= 0; arrayIndex--)
                    {
                            operands[arrayIndex] = Convert.ToDouble(evaluatorStack.Pop().Symbol);
                    }
                    evaluatorStack.Push(new Token(_operatorInfo[token].Evaluate(operands).ToString(), TokenType.Number, 1));
                }
            }
            return Convert.ToDouble(evaluatorStack.Pop().Symbol);
        }
    }
}
