using AlgebraLibrary.Properties;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json;


namespace AlgebraLibrary
{
    public class PostfixConverter
    {
        private static List<ConfigureClass> _validTokens =  new List<ConfigureClass>();
        public static List<ConfigureClass> ValidTokens { get { return _validTokens; } }
        public PostfixConverter()
        {
            string filePath = "Properties\\ConfigurationFile.json";
            string fileName = File.ReadAllText(filePath);
            _validTokens = JsonConvert.DeserializeObject<List<ConfigureClass>>(fileName);
        }
        private void TokenCreator(string operatorName, List<Token> tokens)
        {
            bool flag = false;
            foreach(ConfigureClass token in _validTokens)
            {
                if (token.Symbol.Equals(operatorName))
                {
                    flag = true;
                    tokens.Add(new Token(token.Symbol, token.Type, token.Precedence));
                    break;
                }
            }
            if (!flag)
            {
                    throw new ExpressionException(Resources.WrongSyntax);
            }
        }
        private void TokenCreator(string operatorName, List<Token> tokens,TokenType type)
        {
            bool flag = false;
            foreach (ConfigureClass token in _validTokens)
            {
                if (token.Symbol.Equals(operatorName) && token.Type == type)
                {
                    flag = true;
                    tokens.Add(new Token(token.Symbol, token.Type, token.Precedence));
                    break;
                }
            }
            if (!flag)
            {
                throw new ExpressionException(Resources.WrongSyntax);
            }       
        }
        private void ProcessNonNumericCharacters(Token token, Stack<Token>stack, List<Token> postFixTokens)
        {
            if (token.Type == TokenType.OpeningParenthesis)
            {
                stack.Push(token);
            }
            else if (token.Type == TokenType.ClosingParenthesis)
            {
                while (stack.Count > 0 && !(stack.Peek().Type == TokenType.OpeningParenthesis))
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
            TokenCreator(operatorName, tokens);
            //tokens.Add(new Token(operatorName, TokenType.UnaryOperator, 5));
            operatorName = string.Empty;
        }
        private void ProcessOperators(string expression, List<Token> tokens, int index)
        {
            // Parenthesis 

            if (expression[index] == '(')
            {
                TokenCreator(expression[index].ToString(), tokens);
            }
            else if (expression[index] == ')')
            {
                if(tokens.Last().Type == TokenType.BinaryOperator || tokens.Last().Type == TokenType.UnaryOperator)
                {
                    throw new ExpressionException(Resources.EmptyParenthesis);
                }
                else
                {
                    TokenCreator(expression[index].ToString(), tokens);
                    //tokens.Add(new Token(expression[index].ToString(), TokenType.ClosingParenthesis, 1));
                }
            }
            else
            {
                // Operators(binary operators)
                if((expression[index] == '+' || expression[index] == '-') && (tokens.Count == 0 || tokens.Last().Type == TokenType.BinaryOperator || tokens.Last().Type == TokenType.OpeningParenthesis))
                {
                    TokenCreator(expression[index].ToString(), tokens, TokenType.UnaryOperator);
                }
                else if (expression[index] == '+' || expression[index] == '-')
                {
                    TokenCreator(expression[index].ToString(), tokens, TokenType.BinaryOperator);
                }
                else
                {
                    TokenCreator(expression[index].ToString(), tokens);
                }
            }
        }
        private void OperatorCheck(List<Token> tokenExpression)
        {
            if (tokenExpression[0].Type == TokenType.BinaryOperator)
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
            Regex syntaxCheckExpression = new Regex(@"^.*(?=\*\/|\%{2,}|\/\*|\-\/|\+\/|\+\*|\-\*|\/{2,}|\*{2,}|\=|\+\-|\-\+|\-{2,}|\+{2,}|\^{2,}|\^\-|\^\+|\-\^|\+\^|\^\*|\^\*|\^\/|\/\^).*$");
            Regex noOperandCheckExpression = new Regex(@"^[-+/*%]{1,}$");
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
            else if ((int)stringExpression[0] == 46 && !int.TryParse(stringExpression[1].ToString(),out int number))
            {
                throw new ExpressionException(Resources.MisplacedDecimal);
            }
        }
        private void ParenthesisMatcher(List<Token> expression)
        {
            Stack<Token> parenthesisStack = new Stack<Token>();
            foreach(Token token in expression)
            {
                if (token.Type == TokenType.OpeningParenthesis)
                {
                    parenthesisStack.Push(token);
                }
                if (token.Type == TokenType.ClosingParenthesis)
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
            else if (index < expression.Length - 1 && ((int)expression[index + 1] == 46))
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
            ParenthesisMatcher(inFixTokens);
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
