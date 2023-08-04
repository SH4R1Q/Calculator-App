using AlgebraLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AlgebraLibrary
{
    public class PostfixConverter
    {
        Dictionary<char, int> _operatorPrecedence = new Dictionary<char, int>
            {
                // later when we deserialize the JSON then this dictionary will be  <Token.symbol, Token.precedence> (delete this comment later)
                {'(',0}, // lowest
                {'+',1},
                {'-',1},
                {'*',2},
                {'/',2}// highest

            };
        public void IsNumber(ref bool flag, ref string inFixString, ref int expressionIndex ,ref string postFixString)
        {
            postFixString += inFixString[expressionIndex];
            flag = false;
            if (expressionIndex == inFixString.Length - 1 || ((int)inFixString[expressionIndex + 1] >= 48 && (int)inFixString[expressionIndex + 1] <= 57))
            {
                flag = true;
            }
            if (expressionIndex == inFixString.Length - 1 || inFixString[expressionIndex + 1] == ')')
            {
                flag = false;
            }
            if (!flag)
            {
                postFixString += ' ';
            }
        }
        public void IsAlphabet(ref bool flag, ref string inFixString, ref int expressionIndex, ref string postFixString)
        {
            postFixString += inFixString[expressionIndex];
            if (expressionIndex == inFixString.Length - 1 || inFixString[expressionIndex + 1] == '(')
            {
                postFixString += ' ';
                flag = true;

            }
        }

        public void IsParenthesis(Stack<char> stack, ref string inFixString, ref int expressionIndex, ref string postFixString)
        {
                if (inFixString[expressionIndex] == '(')
                {
                    stack.Push(inFixString[expressionIndex]);
                }
                else if (inFixString[expressionIndex] == ')')
                {
                    while (stack.Peek() != '(' || stack.Count == 0)
                    {
                        postFixString += stack.Pop();
                        postFixString += ' ';
                    }
                    stack.Pop();
                }
        }
        public void IsOperator(Stack<char> stack, ref string inFixString, ref int expressionIndex, ref string postFixString)
        {
            if (postFixString.Length == 0)
            {
                throw new StartingWithBinaryOperatorException();
            }
            else if (stack.Count == 0)
            {
                stack.Push(inFixString[expressionIndex]);
            }
            else if (_operatorPrecedence[stack.Peek()] < _operatorPrecedence[inFixString[expressionIndex]])
            {
                stack.Push(inFixString[expressionIndex]);
            }
            else if (_operatorPrecedence[stack.Peek()] >= _operatorPrecedence[inFixString[expressionIndex]])
            {
                postFixString += stack.Pop();
                postFixString += ' ';
                stack.Push(inFixString[expressionIndex]);
            }
        }
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
                        throw new WrongParenthesisException();

                    }
                    parenthesisStack.Pop();
                }
            }
            if(parenthesisStack.Count > 0)
            {
                throw new WrongParenthesisException();
            }
        }
        public List<Token> Tokenizer(string inFixExpression)
        {
            ParenthesisMatcher(inFixExpression);
            List<Token> tokenizedExpression = new List<Token>();
            string operatorName = string.Empty;
            for (int arrayIndex = 0; arrayIndex < inFixExpression.Length; arrayIndex++)
            {
                // Numbers
                if ((int)inFixExpression[arrayIndex] >= 48 && (int)inFixExpression[arrayIndex] <= 57)
                {
                    operatorName += inFixExpression[arrayIndex];
                    if (arrayIndex < inFixExpression.Length - 1 && ((int)inFixExpression[arrayIndex + 1] >= 48 && (int)inFixExpression[arrayIndex + 1] <= 57))
                    {
                        continue;
                    }
                    tokenizedExpression.Add(new Token(operatorName, TokenType.Number, 1));
                    operatorName = string.Empty;
                }
                // Alphabets(unary operators)
                else if ((int)inFixExpression[arrayIndex] >= 65 && (int)inFixExpression[arrayIndex] <= 90 || (int)inFixExpression[arrayIndex] >= 97 && (int)inFixExpression[arrayIndex] <= 122)
                {
                    operatorName += inFixExpression[arrayIndex];
                    if (arrayIndex < inFixExpression.Length - 1 && inFixExpression[arrayIndex + 1] != '(')
                    {
                        continue;
                    }
                    tokenizedExpression.Add(new Token(operatorName, TokenType.UnaryOperator, 4));
                    operatorName = string.Empty;
                }
                else
                {
                    // Parenthesis
                    if (inFixExpression[arrayIndex] == '(' || inFixExpression[arrayIndex] == ')')
                    {
                        tokenizedExpression.Add(new Token(inFixExpression[arrayIndex].ToString(), TokenType.Parenthesis, 1));
                    }
                    else
                    {
                        // Operators(binary operators)
                        if (inFixExpression[arrayIndex] == '+' || inFixExpression[arrayIndex] == '-')
                        {
                            tokenizedExpression.Add(new Token(inFixExpression[arrayIndex].ToString(), TokenType.BinaryOperator, 2));
                        }
                        else if (inFixExpression[arrayIndex] == '*' || inFixExpression[arrayIndex] == '/')
                        {
                            tokenizedExpression.Add(new Token(inFixExpression[arrayIndex].ToString(), TokenType.BinaryOperator, 3));
                        }
                    }
                }
            }
            return tokenizedExpression;

        }
        public List<Token> ToPostFix(string inFixExpression)
        {
            List<Token> postFixTokens = new List<Token>();
            List<Token> inFixTokens = Tokenizer(inFixExpression);
            Stack<Token> operatorStack = new Stack<Token>();
            foreach (Token token in inFixTokens)
            {
                // Token is a Number
                if (token.Type == TokenType.Number)
                {
                    postFixTokens.Add(token);
                }
                else
                {
                    // Token is Parenthesis
                    if (token.Symbol.Equals("("))
                    {
                        operatorStack.Push(token);
                    }
                    else if (token.Symbol.Equals(")"))
                    {
                        while (operatorStack.Count > 0 && !(operatorStack.Peek().Symbol.Equals("(")))
                        {
                            postFixTokens.Add(operatorStack.Pop());
                        }
                        operatorStack.Pop();
                    }
                    // Token is Operator
                    else if (operatorStack.Count == 0)
                    {
                        operatorStack.Push(token);
                    }
                    else if (operatorStack.Peek().Precedence < token.Precedence)
                    {
                        operatorStack.Push(token);
                    }
                    else if (operatorStack.Peek().Precedence >= token.Precedence)
                    {
                        postFixTokens.Add(operatorStack.Pop());
                        operatorStack.Push(token);
                    }
                }
            }
            while (operatorStack.Count > 0)
            {
                postFixTokens.Add(operatorStack.Pop());
            }
            return postFixTokens;
        }
        public List<Token> Converter(string Expression)
        {
            return ToPostFix(Expression);
        }
    }
}
