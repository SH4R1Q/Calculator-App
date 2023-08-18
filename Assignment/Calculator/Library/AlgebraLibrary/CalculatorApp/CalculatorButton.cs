using AlgebraLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorApp
{
    public class CalculatorButton : Button
    {

        private string _textShown;
        private string _meaning;
        private ButtonType _type;

        public CalculatorButton(string symbol, ButtonType type)
        {
            _textShown = symbol;
            _type = type;
        }
        public CalculatorButton()
        {
            _textShown = String.Empty;
        }
        public string TextShown
        {
            get => _textShown;
            set => _textShown = value;
        }
        public string Meaning
        {
            get => _meaning;
            set => _meaning = value;
        }
        public ButtonType Type
        {
            get => _type;
            set => _type = value;
        }
        public override bool Equals(object obj)
        {
            if (obj is CalculatorButton typeToken)
            {
                return TextShown == typeToken.TextShown && Type == typeToken.Type;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return (TextShown, Type).GetHashCode();
        }
    }
    public enum ButtonType
    {
        Operator,
        Number,
        Evaluate,
        Clear,
        BackSpace,
        PreiousAnswer,
        AddMemory,
        SubtractMemory,
        SaveMemory,
        ReadMemory,
        ClearMemory,
        AllClear
    }
}

