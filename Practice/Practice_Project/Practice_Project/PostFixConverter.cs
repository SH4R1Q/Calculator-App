using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using AlgebraLibrary;

namespace AlgebraLibrary
{
    public class PostFixConverter
    {
        List<Token> tokens = new List<Token>();
        string jsonString = File.ReadAllText("D:\\Shariq\\Practice\\Practice_Project\\Practice_Project\\jsconfig1.json");
        tokens  = JsonConvert.DeserializeObject<List<Token>>(jsonString);
        public void ParenthesisMatcher(string expression)
        {
            Stack<char> parenthesisStack = new Stack<char>();
            for (int arrayIndex = 0; arrayIndex < expression.Length; arrayIndex++)
            {
                if (expression[arrayIndex] == '(')
                {
                    parenthesisStack.Push(expression[arrayIndex]);
                }
                if (expression[arrayIndex] == ')')
                {
                    if (parenthesisStack.Count == 0)
                    {
                        throw new Exception("Wrong Parenthesis");

                    }
                    parenthesisStack.Pop();
                }
            }
            if (parenthesisStack.Count > 0)
            {
                throw new Exception("Wrong Parenthesis");
            }
        }
        public List<Token> Tokenizer(string inFixExpression)
        {
            ParenthesisMatcher(inFixExpression);
            List<Token> tokenizedExpression = new List<Token>();
            string operatorName = string.Empty;
            for (int arrayIndex = 0;  arrayIndex < inFixExpression.Length; arrayIndex++)
            {
                if ((int)inFixExpression[arrayIndex] >= 48 && (int)inFixExpression[arrayIndex] <= 57)
                {
                    operatorName += inFixExpression[arrayIndex];
                    if(arrayIndex < inFixExpression.Length - 1 && ((int)inFixExpression[arrayIndex+1] >= 48 && (int)inFixExpression[arrayIndex+1] <= 57))
                    {
                        continue;
                    }
                    tokenizedExpression.Add(new Token(operatorName, TokenType.Number, 1));
                    operatorName = string.Empty;
                }
                else if ((int)inFixExpression[arrayIndex] >= 65 && (int)inFixExpression[arrayIndex] <= 90 || (int)inFixExpression[arrayIndex] >= 97 && (int)inFixExpression[arrayIndex] <= 122)
                {
                    operatorName += inFixExpression[arrayIndex];
                    if (arrayIndex < inFixExpression.Length - 1 && inFixExpression[arrayIndex+1] != '(')
                    {
                        continue;
                    }
                    tokenizedExpression.Add(new Token(operatorName, TokenType.UnaryOperator, 4));
                    operatorName = string.Empty;
                }
                else
                {
                    if (inFixExpression[arrayIndex] == '(' || inFixExpression[arrayIndex] == ')')
                    {
                        tokenizedExpression.Add(new Token(inFixExpression[arrayIndex].ToString(), TokenType.Parenthesis, 1));
                    }
                    else
                    {
                        if (inFixExpression[arrayIndex] == '+' || inFixExpression[arrayIndex] == '-')
                        {
                            tokenizedExpression.Add(new Token(inFixExpression[arrayIndex].ToString(), TokenType.BinaryOperator, 2));
                        }
                        else if(inFixExpression[arrayIndex] == '*' || inFixExpression[arrayIndex] == '/')
                        {
                            tokenizedExpression.Add(new Token(inFixExpression[arrayIndex].ToString(), TokenType.BinaryOperator, 3));
                        }
                    }
                }
            }
            return tokenizedExpression;

        }
         public List<Token> Converter(string inFixExpression)
        {
            List<Token> postFixTokens = new List<Token>();
            List<Token> inFixTokens = Tokenizer(inFixExpression);
            Stack<Token> operatorStack = new Stack<Token>();
            foreach (Token token in inFixTokens)
            {
                if (token.Type == TokenType.Number)
                {
                    postFixTokens.Add(token);
                }
                else
                {
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
                    else if (operatorStack.Count == 0)
                    {
                        operatorStack.Push(token);
                    }
                    else if(operatorStack.Peek().Precedence < token.Precedence)
                    {
                        operatorStack.Push(token);
                    }
                    else if(operatorStack.Peek().Precedence >= token.Precedence)
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
    }
}
