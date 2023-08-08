using AlgebraLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Project
{
    public class ConfigJson
    {
        private string _symbol;
        private TokenType _type;
        private int _precedence;
        private String _className;

        public ConfigJson(string symbol, TokenType type, int precedence, String className)
        {
            _symbol = symbol;
            _type = type;
            _precedence = precedence;
            _className = className;
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
        public String ClassName
        {
            get => _className;
            set => _className = value;
        }
    }
}
