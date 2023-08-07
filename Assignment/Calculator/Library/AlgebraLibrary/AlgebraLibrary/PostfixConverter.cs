using AlgebraLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AlgebraLibrary
{
    public class PostfixConverter
    {
        public void IsNotNumber(Token token, Stack<Token>stack, List<Token> postFixTokens)
        {
            if (token.Symbol.Equals("("))
            {
                stack.Push(token);
            }
            else if (token.Symbol.Equals(")"))
            {
                while (stack.Count > 0 && !(stack.Peek().Symbol.Equals("(")))
                {
                    postFixTokens.Add(stack.Pop());
                }
                stack.Pop();
            }
            // Token is Operator
            else if (stack.Count == 0)
            {
                stack.Push(token);
            }
            else if (stack.Peek().Precedence < token.Precedence)
            {
                stack.Push(token);
            }
            else if (stack.Peek().Precedence >= token.Precedence)
            {
                while (stack.Count > 0 && stack.Peek().Precedence >= token.Precedence)
                {
                    postFixTokens.Add(stack.Pop());
                }
                stack.Push(token);
            }
        }
        public void IsNotNumber(ref string expression, ref string operatorName,List<Token> tokens,int index)
        {
            // Alphabets(unary operators)
            if ((int)expression[index] >= 65 && (int)expression[index] <= 90 || (int)expression[index] >= 97 && (int)expression[index] <= 122)
            {
                IsAlphabet(ref expression,ref operatorName,tokens, index);
            }
            else
            {
                IsNotAlphabet(ref expression, ref operatorName, tokens, index);
            }
        }
        public void IsAlphabet(ref string expression, ref string operatorName, List<Token> tokens, int index)
        {
            operatorName += expression[index];
            if (index < expression.Length - 1 && (expression[index + 1] != '(' && expression[index+1] != ')'))
            {
                return;
            }
            tokens.Add(new Token(operatorName, TokenType.UnaryOperator, 5));
            operatorName = string.Empty;
        }
        public void IsNotAlphabet(ref string expression, ref string operatorName, List<Token> tokens, int index)
        {
            // Parenthesis 

            if (expression[index] == '(')
            {
                tokens.Add(new Token(expression[index].ToString(), TokenType.Parenthesis, 1));
            }
            else if (expression[index] == ')')
            {
                if(tokens.Last().Type == TokenType.BinaryOperator || tokens.Last().Type == TokenType.UnaryOperator)
                {
                    throw new EmptyParenthesisException();
                }
                else
                {
                    tokens.Add(new Token(expression[index].ToString(), TokenType.Parenthesis, 1));
                }
            }
            else
            {
                // Operators(binary operators)
                if((expression[index] == '+' || expression[index] == '-') && (tokens.Count == 0 || tokens.Last().Type == TokenType.BinaryOperator || tokens.Last().Symbol == "("))
                {
                    tokens.Add(new Token(expression[index].ToString(), TokenType.UnaryOperator, 4));
                }
                else if (expression[index] == '+' || expression[index] == '-')
                {
                    tokens.Add(new Token(expression[index].ToString(), TokenType.BinaryOperator, 2));
                }
                else if (expression[index] == '*' || expression[index] == '/')
                {
                    tokens.Add(new Token(expression[index].ToString(), TokenType.BinaryOperator, 3));
                }
            }
        }
        public void OperatorCheck(List<Token> tokenExpression)
        {
            if (tokenExpression[0].Type == TokenType.BinaryOperator && !(tokenExpression[0].Symbol.Equals("-") || tokenExpression[0].Symbol.Equals("+")))
            {
                throw new StartingWithBinaryOperatorException();
            }
            else if (tokenExpression.Last().Type == TokenType.BinaryOperator)
            {
                throw new ExtraOperatorException();
            }
        }
        public void OperatorCheck(ref string stringExpression)
        {
            Regex reg1 = new Regex(@"^.*(?=\*\/|\/\*|\-\/|\+\/|\+\*|\-\*|\/{2,}|\*{2,}|\=|\+\-\-|\-\+\+|\-{3,}|\+{3,}).*$");
            Regex reg2 = new Regex(@"^[-+/*]{1,}$");
            Regex reg3 = new Regex(@"^\+\+\-|^\-\-\+.*.*$");
            if(stringExpression.Length == 0)
            {
                throw new EmptyExpressionException();
            }
            else if (stringExpression.Contains("()"))
            {
                throw new EmptyParenthesisException();
            }
            else if (reg1.IsMatch(stringExpression) || reg3.IsMatch(stringExpression))
            {
                throw new ExtraOperatorException();
            }
            else if (reg2.IsMatch(stringExpression))
            {
                throw new OnlyOperatorException();
            }
            else if (stringExpression[0] == '.' && !int.TryParse(stringExpression[1].ToString(),out int number))
            {
                throw new MisPlacedDecimalException();
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
        public void IsNumber(ref string expression, ref string operatorName, List<Token> tokens, int index)
        {
            operatorName += expression[index];
            if (index < expression.Length - 1 && ((int)expression[index + 1] >= 48 && (int)expression[index + 1] <= 57))
            {
                return;
            }
            //Decimal Handeling
            else if (index < expression.Length - 1 && (expression[index + 1] == '.'))
            {
                return;
            }
            // Decimal Handeling
            else if (operatorName.Contains('.'))
            {
                Regex reg1 = new Regex(@"^[0-9]*\.{1}[0-9]+$");
                Regex reg2 = new Regex(@"^[0-9]+\.{1}[0-9]*$");
                if (reg1.IsMatch(operatorName) || reg2.IsMatch(operatorName))
                {
                    tokens.Add(new Token(operatorName, TokenType.Number, 1));
                    operatorName = string.Empty;
                    return;
                }
                else
                {
                    throw new MisPlacedDecimalException();
                }
            }
            tokens.Add(new Token(operatorName, TokenType.Number, 1));
            operatorName = string.Empty;
        }
        public List<Token> Tokenizer(string inFixExpression)
        {
            ParenthesisMatcher(inFixExpression);
            List<Token> tokenizedExpression = new List<Token>();
            string operatorName = string.Empty;
            OperatorCheck(ref inFixExpression);
            for (int arrayIndex = 0; arrayIndex < inFixExpression.Length; arrayIndex++)
            {
                
                if ((int)inFixExpression[arrayIndex] >= 48 && (int)inFixExpression[arrayIndex] <= 57 || inFixExpression[arrayIndex] == '.')
                {
                    IsNumber(ref inFixExpression, ref operatorName, tokenizedExpression, arrayIndex);
                }
                else
                {
                    IsNotNumber(ref inFixExpression, ref operatorName, tokenizedExpression, arrayIndex);
                } 
            }
            return tokenizedExpression;

        }
        public List<Token> ToPostFix(string inFixExpression)
        {
            List<Token> postFixTokens = new List<Token>();
            List<Token> inFixTokens = Tokenizer(inFixExpression);
            Stack<Token> operatorStack = new Stack<Token>();
            OperatorCheck(inFixTokens);
            foreach (Token token in inFixTokens)
            {
                // Token is a Number
                if (token.Type == TokenType.Number)
                {
                    postFixTokens.Add(token);
                }
                else
                {
                    IsNotNumber(token, operatorStack, postFixTokens);
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
            return ToPostFix(Expression.ToLower());
        }
    }
}
