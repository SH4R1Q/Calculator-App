using AlgebraLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class PostfixConverter
    {
        Dictionary<char, int> operatorPrecedence = new Dictionary<char, int>
            {
                {'(',0}, // lowest
                {'+',1},
                {'-',1},
                {'*',2},
                {'/',2} // highest
            };
        public void ParenthesisMatcher(string expression)
        {
            Stack<char> parenthesisStack = new Stack<char>();
            for(int arrayIndex = 0; arrayIndex < expression.Length; arrayIndex++)
            {
                if (expression[arrayIndex] == '(')
                {
                    parenthesisStack.Push(expression[arrayIndex]);
                }
                if (expression[arrayIndex] == ')')
                {
                    if(parenthesisStack.Count == 0) 
                    {
                        throw new Exception(Resources.ParenthesisMismatch);

                    }
                    parenthesisStack.Pop();
                }
            }
            if(parenthesisStack.Count > 0)
            {
                throw new Exception(Resources.ParenthesisMismatch);
            }
        }
        public string ToPostFix(string inFixExpression)
        {
            ParenthesisMatcher(inFixExpression);
            string postFixExpression = "";
            bool flag = false;
            if (string.IsNullOrEmpty(inFixExpression))
            {
                throw new ArgumentNullException(nameof(inFixExpression));
            }
            Stack<char> operatorStack = new Stack<char>();
            for(int arrayIndex = 0;  arrayIndex < inFixExpression.Length; arrayIndex++)
            {
                if ((int)inFixExpression[arrayIndex] >= 48 && (int)inFixExpression[arrayIndex] <= 57)
                {
                    postFixExpression += inFixExpression[arrayIndex];
                    if (arrayIndex == inFixExpression.Length -1 || inFixExpression[arrayIndex + 1] == ')')
                    {
                        flag = false;
                    }
                    if (!flag)
                    {
                        postFixExpression += ' ';
                    }

                }
                else if ((int)inFixExpression[arrayIndex] >= 65 && (int)inFixExpression[arrayIndex] <= 90 || (int)inFixExpression[arrayIndex] >= 97 && (int)inFixExpression[arrayIndex] <= 122)
                {
                    postFixExpression += inFixExpression[arrayIndex];
                    if (inFixExpression[arrayIndex+1] == '(')
                    {
                        postFixExpression += ' ';
                        flag = true;
                        
                    }
                }
                else
                {
                    if(inFixExpression[arrayIndex] == '(' || inFixExpression[arrayIndex] == ')')
                    {
                        if (inFixExpression[arrayIndex] == '(')
                        {
                            operatorStack.Push(inFixExpression[arrayIndex]);
                        }
                        else if (inFixExpression[arrayIndex] == ')')
                        {
                            while (operatorStack.Peek() != '(' || operatorStack.Count == 0)
                            {
                                postFixExpression += operatorStack.Pop();
                                postFixExpression += ' ';
                            }
                            operatorStack.Pop();
                        }

                    }
                    else if(postFixExpression.Length == 0)
                    {
                        throw new Exception(Resources.StartingWithOperator);
                    }
                    else if (operatorStack.Count == 0)
                    {
                        operatorStack.Push(inFixExpression[arrayIndex]);
                    }
                    else if(operatorPrecedence[operatorStack.Peek()] < operatorPrecedence[inFixExpression[arrayIndex]])
                    {
                        operatorStack.Push(inFixExpression[arrayIndex]);
                    }
                    else if(operatorPrecedence[operatorStack.Peek()] >= operatorPrecedence[inFixExpression[arrayIndex]])
                    {
                        postFixExpression += operatorStack.Pop();
                        postFixExpression += ' ';
                        operatorStack.Push(inFixExpression[arrayIndex]);
                    }
                    
                }

            }
            while(operatorStack.Count != 0)
            {
                postFixExpression += operatorStack.Pop();
                postFixExpression += ' ';
            }
            return postFixExpression;
        }
        public List<Token> Converter(string Expression)
        {
            string postFixExpression = ToPostFix(Expression).Trim(' ');
            List<Token> postFixTokens = new List<Token>();
            string[] tokens = postFixExpression.Split(' ');
            foreach(string token in tokens)
            {
                if (int.TryParse(token,out int number))
                {
                    postFixTokens.Add(new Token(token, TokenType.Number, 0));
                }
                else if (char.TryParse(token,out char character)) 
                {
                    postFixTokens.Add(new Token(token, TokenType.BinaryOperator, operatorPrecedence[character]));
                }
                else
                {
                    postFixTokens.Add(new Token(token, TokenType.UnaryOperator, 5));
                }
            }
            return postFixTokens;
        }
    }
}
