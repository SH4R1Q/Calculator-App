using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public class Token
    {
        private string _symbol;
        private TokenType _type;
        private int _precedence;

        public Token(string symbol, TokenType type, int precedence)
        {
            _symbol = symbol;
            _type = type;
            _precedence = precedence;
        }
        public string Symbol 
        { 
            get => _symbol;
            set => _symbol = value; 
        }
        public TokenType Type
        {
            get => _type;
            set => _type = value;
        }
        public int Precedence
        {
            get => _precedence; 
            set => _precedence = value;
        }
        public override bool Equals(object obj)
        {
            if(obj is Token typeToken)
            {
                return Symbol==typeToken.Symbol && Type==typeToken.Type && Precedence==typeToken.Precedence;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return (Symbol, Type, Precedence).GetHashCode();
        }
    }
    public enum TokenType
    {
        UnaryOperator,
        BinaryOperator,
        Number,
        Parenthesis
    }
}
