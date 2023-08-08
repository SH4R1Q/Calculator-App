using AlgebraLibrary.Properties;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace AlgebraLibrary
{
    public class PostfixConverter
    {
        private void ProcessNonNumericCharacters(Token token, Stack<Token>stack, List<Token> postFixTokens)
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
        private void ProcessNonNumericCharacters(string expression, ref string operatorName,List<Token> tokens,int index)
        {
            // Alphabets(unary operators)
            if ((int)expression[index] >= 65 && (int)expression[index] <= 90 || (int)expression[index] >= 97 && (int)expression[index] <= 122)
            {
                ProcessAlphabets(expression,ref operatorName,tokens, index);
            }
            else
            {
                ProcessOperators(expression, tokens, index);
            }
        }
        private void ProcessAlphabets(string expression, ref string operatorName, List<Token> tokens, int index)
        {
            operatorName += expression[index];
            if (index < expression.Length - 1 && (expression[index + 1] != '(' && expression[index+1] != ')'))
            {
                return;
            }
            tokens.Add(new Token(operatorName, TokenType.UnaryOperator, 5));
            operatorName = string.Empty;
        }
        private void ProcessOperators(string expression, List<Token> tokens, int index)
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
                    throw new ExpressionException(Resources.EmptyParenthesis);
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
        private void OperatorCheck(List<Token> tokenExpression)
        {
            if (tokenExpression[0].Type == TokenType.BinaryOperator && !(tokenExpression[0].Symbol.Equals("-") || tokenExpression[0].Symbol.Equals("+")))
            {
                throw new ExpressionException(Resources.StartingWithOperator);
            }
            else if (tokenExpression.Last().Type == TokenType.BinaryOperator)
            {
                throw new ExpressionException(Resources.OperatorOnly);
            }
        }
        private void OperatorCheck(string stringExpression)
        {
            Regex syntaxCheckExpression = new Regex(@"^.*(?=\*\/|\/\*|\-\/|\+\/|\+\*|\-\*|\/{2,}|\*{2,}|\=|\+\-\-|\-\+\+|\-{3,}|\+{3,}).*$");
            Regex noOperandCheckExpression = new Regex(@"^[-+/*]{1,}$");
            Regex extraOperatorCheckExpression = new Regex(@"^\+\+\-|^\-\-\+.*.*$");
            Regex onlyAlphabetsCheckExpression = new Regex(@"^.+.+[a-zA-Z]{1,}$");
            if(stringExpression.Length == 0)
            {
                throw new ExpressionException(Resources.EmptyExpression);
            }
            else if (onlyAlphabetsCheckExpression.IsMatch(stringExpression))
            {
                throw new ExpressionException(Resources.ParenthesisMismatch);
            }
            else if (stringExpression.Contains("()"))
            {
                throw new ExpressionException(Resources.EmptyParenthesis);
            }
            else if (syntaxCheckExpression.IsMatch(stringExpression) || extraOperatorCheckExpression.IsMatch(stringExpression))
            {
                throw new ExpressionException(Resources.WrongSyntax);
            }
            else if (noOperandCheckExpression.IsMatch(stringExpression))
            {
                throw new ExpressionException(Resources.WrongSyntax);
            }
            else if (stringExpression[0] == '.' && !int.TryParse(stringExpression[1].ToString(),out int number))
            {
                throw new ExpressionException(Resources.MisplacedDecimal);
            }
        }
        private void ParenthesisMatcher(string expression)
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
                        throw new ExpressionException(Resources.ParenthesisMismatch);

                    }
                    parenthesisStack.Pop();
                }
            }
            if(parenthesisStack.Count > 0)
            {
                throw new ExpressionException(Resources.ParenthesisMismatch);
            }
        }
        private void ProcessNumbers(string expression, ref string operatorName, List<Token> tokens, int index)
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
                Regex rightDecimalCheckExpression = new Regex(@"^[0-9]*\.{1}[0-9]+$");
                Regex leftDecimalCheckExpression = new Regex(@"^[0-9]+\.{1}[0-9]*$");
                if (rightDecimalCheckExpression.IsMatch(operatorName) || leftDecimalCheckExpression.IsMatch(operatorName))
                {
                    tokens.Add(new Token(operatorName, TokenType.Number, 1));
                    operatorName = string.Empty;
                    return;
                }
                else
                {
                    throw new ExpressionException(Resources.MisplacedDecimal);
                }
            }
            tokens.Add(new Token(operatorName, TokenType.Number, 1));
            operatorName = string.Empty;
        }
        public List<Token> ConvertToTokens(string inFixExpression)
        {
            ParenthesisMatcher(inFixExpression);
            List<Token> tokenizedExpression = new List<Token>();
            string operatorName = string.Empty;
            OperatorCheck(inFixExpression);
            for (int arrayIndex = 0; arrayIndex < inFixExpression.Length; arrayIndex++)
            {
                
                if ((int)inFixExpression[arrayIndex] >= 48 && (int)inFixExpression[arrayIndex] <= 57 || inFixExpression[arrayIndex] == '.')
                {
                    ProcessNumbers(inFixExpression, ref operatorName, tokenizedExpression, arrayIndex);
                }
                else
                {
                    ProcessNonNumericCharacters(inFixExpression, ref operatorName, tokenizedExpression, arrayIndex);
                } 
            }
            return tokenizedExpression;

        }
        public List<Token> ConvertToPostFix(string inFixExpression)
        {
            List<Token> postFixTokens = new List<Token>();
            List<Token> inFixTokens = ConvertToTokens(inFixExpression);
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
                    ProcessNonNumericCharacters(token, operatorStack, postFixTokens);
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
            return ConvertToPostFix(Expression.ToLower());
        }
    }
}
