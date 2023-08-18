using AlgebraLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using CalculatorApp;

namespace CalculatorApp
{
    public partial class Calculator : Form
    {
        TableLayoutPanel _numberSet = new TableLayoutPanel();
        CalculatorButton _evaluateButton = new CalculatorButton();
        CalculatorButton _clearButton = new CalculatorButton();
        CalculatorButton _backspaceButton = new CalculatorButton();
        CalculatorButton _selectLanguage  = new CalculatorButton();
        GroupBox _calculatorControls = new GroupBox();
        FlowLayoutPanel _operatorSet = new FlowLayoutPanel();
        TextBox _calculatorScreen = new TextBox();
        TextBox _expression = new TextBox();
        TextBox _memory = new TextBox();
        ExpressionEvaluator _expressionEvaluator = new ExpressionEvaluator();
        CalculatorButton _prevAnswer = new CalculatorButton();
        Stack<Double> _memoryStack = new Stack<Double>();
        double _result;
        double _prevResult;
        static List<CalculatorButton> _buttonInfo = new List<CalculatorButton>();

        public Calculator()
        {
            InitializeComponent();
            InitializeLanguage();
            // Calculator Screen

            _calculatorScreen.Location = new Point(10, 10);
            _calculatorScreen.Size = new Size(750, 50);
            _calculatorScreen.Multiline = true;
            _calculatorScreen.Font = new Font(_calculatorScreen.Font.Name, 20);
            _calculatorScreen.BackColor = Color.FromArgb(30, 30, 80);
            _calculatorScreen.ForeColor = Color.White;
            _calculatorScreen.Cursor = Cursors.Arrow;
            _calculatorScreen.BorderStyle = BorderStyle.FixedSingle;
            _calculatorScreen.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom; 
            this.Controls.Add(_calculatorScreen);

            // Expression Screen

            _expression.Location = new Point(10, 80);
            _expression.Size = new Size(730, 50);
            _expression.Font = new Font(_expression.Font.Name, 10);
            _expression.BackColor = Color.FromArgb(30, 30, 50);
            _expression.ForeColor = Color.White;
            _expression.Cursor = Cursors.Arrow;
            _expression.BorderStyle = BorderStyle.FixedSingle;
            _calculatorControls.Controls.Add( _expression);

            // Calculator Control

            _calculatorControls.Location = new Point(10, 60);
            _calculatorControls.Size = new Size(750, 420);
            _calculatorControls.Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
            this.Controls.Add((GroupBox)_calculatorControls);

            // Select Language




            //  Memory

            _memory.Location = new Point(10, 20);
            _memory.Size = new Size(730, 50);
            _memory.Multiline = true;
            _memory.Font = new Font(_memory.Font.Name, 20);
            _memory.BackColor = Color.FromArgb(30, 30, 80);
            _memory.ForeColor = Color.White;
            _memory.BorderStyle = BorderStyle.FixedSingle;
            _memory.Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
            _memory.Cursor = Cursors.Arrow;
            _memory.ReadOnly = true;
            _calculatorControls.Controls.Add(_memory);

            // Number Set

            _numberSet.RowCount = 5;
            _numberSet.ColumnCount = 3;
            for(int rows = 1; rows <= _numberSet.RowCount; rows++)
            {
                _numberSet.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            }
            for (int columns = 1; columns <= _numberSet.ColumnCount; columns++)
            {
                _numberSet.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            }
            _numberSet.Location = new Point(480, 110);
            _numberSet.Size = new Size(260,300);
            this._calculatorControls.Controls.Add(_numberSet);
            _numberSet.Anchor = AnchorStyles.Bottom | AnchorStyles.Right  ;


            // Operator Set

            _operatorSet.Location = new Point(10,110);
            _operatorSet.Size = new Size(460,300);
            _operatorSet.BackColor = Color.FromArgb(0, 10, 40);
            this._calculatorControls.Controls.Add(_operatorSet);
            _operatorSet.Anchor = AnchorStyles.Bottom | AnchorStyles.Left ;

            InitializeCalculatorControls();

        }
        public delegate void EventDelegate(object sender, EventArgs e);
        private void InitializeLanguage()
        {
            string fileName = File.ReadAllText("Properties\\ConfigureUIHindi.json");
            _buttonInfo = JsonConvert.DeserializeObject<List<CalculatorButton>>(fileName);
        }
        private void EvaluateExpression(object sender, EventArgs e)
        {
            try
            {                
                _result = _expressionEvaluator.Evaluate(_calculatorScreen.Name);
                _calculatorScreen.Text = _result.ToString();
                _calculatorScreen.Name = _result.ToString();
                _expression.Text = _calculatorScreen.Name;
                _prevResult = _result;
            }
            catch (Exception exception)
            {
                _calculatorScreen.Text = exception.Message;
                _calculatorScreen.Name = exception.Message;
                _expression.Text = String.Empty;
                _result = 0;
            }
        }
        private void InitializeNumberSet()
        {
            char[] numberSetArray = {'7','4','1','.','8','5','2','0','9','6','3'};
            int arrayIndex = 0;
            for(int columns = 0; columns < _numberSet.ColumnCount; columns++)
            {
                for(int rows = 0; rows < _numberSet.RowCount; rows++)
                {
                    if(rows == 0 || (columns == _numberSet.ColumnCount-1 && rows == _numberSet.RowCount-1)) { continue; }
                    CalculatorButton btn = new CalculatorButton();
                    btn = CreateButton(numberSetArray[arrayIndex].ToString(), numberSetArray[arrayIndex].ToString(),ButtonType.Number,ButtonClick);
                    btn.Dock = DockStyle.Fill;
                    _numberSet.Controls.Add(btn, columns, rows);
                    arrayIndex++;
                    btn = null;
                }
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            CalculatorButton clickedButton = (CalculatorButton)sender;
            switch (clickedButton.Type)
            {
                case ButtonType.Clear:
                    _calculatorScreen.Clear();
                    _calculatorScreen.Name = string.Empty;
                    _expression.Text = _calculatorScreen.Name;
                    break;

                case ButtonType.Number:
                case ButtonType.Operator:
                    _calculatorScreen.Text += clickedButton.Text;
                    _calculatorScreen.Name += clickedButton.Name;
                    _expression.Text = _calculatorScreen.Name;
                    break;

                case ButtonType.BackSpace:
                    if (_calculatorScreen.Name.Length == 0) { break; }
                    _calculatorScreen.Text = _calculatorScreen.Text.Substring(0, _calculatorScreen.Text.Length - 1);
                    _calculatorScreen.Name = _calculatorScreen.Name.Substring(0, _calculatorScreen.Name.Length - 1);
                    _expression.Text = _calculatorScreen.Name;
                    break;

                case ButtonType.PreiousAnswer:
                    _calculatorScreen.Text += _prevResult.ToString();
                    _calculatorScreen.Name += _prevResult.ToString();
                    _expression.Text = _calculatorScreen.Name;
                    break;

                case ButtonType.AddMemory:
                    if (_memoryStack.Count == 0 || !double.TryParse(_calculatorScreen.Name, out double number1)) { break; }
                    double add = _memoryStack.Pop();
                    add += Convert.ToDouble(_calculatorScreen.Name);
                    _memoryStack.Push(add);
                    _memory.Text = _memoryStack.Peek().ToString();
                    _memory.Name = _memoryStack.Peek().ToString();
                    break;

                case ButtonType.SubtractMemory:
                    if (_memoryStack.Count == 0 || !double.TryParse(_calculatorScreen.Name, out double number2)) { break; }
                    double subtract = _memoryStack.Pop();
                    subtract -= Convert.ToDouble(_calculatorScreen.Name);
                    _memoryStack.Push(subtract);
                    _memory.Text = _memoryStack.Peek().ToString();
                    _memory.Name = _memoryStack.Peek().ToString();
                    break;

                case ButtonType.SaveMemory:
                    if(!double.TryParse(_calculatorScreen.Name, out double number3)) { break; }
                    _memoryStack.Push(Convert.ToDouble(_calculatorScreen.Name));
                    _memory.Text = _memoryStack.Peek().ToString();
                    _memory.Name = _memoryStack.Peek().ToString();
                    break;

                case ButtonType.ReadMemory:
                    if (!double.TryParse(_memory.Name, out double number4)) { break; }
                    _calculatorScreen.Text += _memory.Text;
                    _calculatorScreen.Name += _memory.Name;
                    _expression.Text += _calculatorScreen.Name;
                    break;

                case ButtonType.ClearMemory:
                    _memoryStack.Clear();
                    _memory.Text = String.Empty;
                    _memory.Name = String.Empty;
                    break;

                case ButtonType.AllClear:
                    _memoryStack.Clear();
                    _memory.Text = String.Empty;
                    _memory.Name = String.Empty;
                    _calculatorScreen.Text = String.Empty;
                    _calculatorScreen.Name = String.Empty;
                    _expression.Text = _calculatorScreen.Name;
                    _prevResult = 0;
                    _result = 0;
                    break;
            }
        }
        private CalculatorButton CreateButton(string text,string name,ButtonType type ,EventDelegate eventName)
        {
            CalculatorButton newButton = new CalculatorButton
            {
                Size = new Size(70,70),
                BackColor = Color.DimGray,
                ForeColor = Color.White
            };
            newButton.Font = new Font(newButton.Font.Name, newButton.Font.Size + 3, FontStyle.Bold);
            newButton.FlatStyle = FlatStyle.Flat;
            newButton.FlatAppearance.BorderSize = 0;
            newButton.Text = text;
            newButton.Name = name;
            newButton.Type = type;
            newButton.Click += new EventHandler(eventName);
            return newButton;
        }
        private void InitializeOperatorSet(CalculatorButton newButton)
        {
            List<ConfigureClass> operators = PostfixConverter.ValidTokens;
            foreach(ConfigureClass token in operators)
            {
                if (token.Symbol.Equals(newButton.Meaning))
                {
                    _operatorSet.Controls.Add(CreateButton(newButton.TextShown, token.Symbol, ButtonType.Operator, ButtonClick));
                }
            }
        }
        private void InitializeCalculatorControls()
        {
            InitializeNumberSet();
            foreach(CalculatorButton button in _buttonInfo)
            {
                switch (button.Type)
                {
                    case ButtonType.Evaluate:
                        _evaluateButton = CreateButton(button.TextShown, "=", ButtonType.Evaluate, EvaluateExpression);
                        _evaluateButton.Dock = DockStyle.Fill;
                        _numberSet.Controls.Add(_evaluateButton, 2, 4);
                        break;
                    case ButtonType.Clear:
                        _clearButton = CreateButton(button.TextShown, "C", ButtonType.Clear, ButtonClick);
                        _clearButton.Dock = DockStyle.Fill;
                        _numberSet.Controls.Add(_clearButton, 0, 0);
                        break;
                    case ButtonType.BackSpace:
                        _backspaceButton = CreateButton(button.TextShown, "<-", ButtonType.BackSpace, ButtonClick);
                        _backspaceButton.Dock = DockStyle.Fill;
                        _numberSet.Controls.Add(_backspaceButton, 2, 0);
                        break;
                    case ButtonType.PreiousAnswer:
                        _prevAnswer = CreateButton(button.TextShown, "Ans", ButtonType.PreiousAnswer, ButtonClick);
                        _prevAnswer.Dock = DockStyle.Fill;
                        _numberSet.Controls.Add(_prevAnswer, 1, 0);
                        break;
                    case ButtonType.AddMemory:
                        _operatorSet.Controls.Add(CreateButton(button.TextShown, "M+", ButtonType.AddMemory, ButtonClick));
                        break;
                    case ButtonType.SubtractMemory:
                        _operatorSet.Controls.Add(CreateButton(button.TextShown, "M-", ButtonType.SubtractMemory, ButtonClick)); 
                        break;
                    case ButtonType.SaveMemory:
                        _operatorSet.Controls.Add(CreateButton(button.TextShown, "MS", ButtonType.SaveMemory, ButtonClick)); 
                        break;
                    case ButtonType.ReadMemory:
                        _operatorSet.Controls.Add(CreateButton(button.TextShown, "MR", ButtonType.ReadMemory, ButtonClick)); 
                        break;
                    case ButtonType.ClearMemory:
                        _operatorSet.Controls.Add(CreateButton(button.TextShown, "MC", ButtonType.ClearMemory, ButtonClick)); 
                        break;
                    case ButtonType.AllClear:
                        _operatorSet.Controls.Add(CreateButton(button.TextShown, "AC", ButtonType.AllClear, ButtonClick)); 
                        break;
                    case ButtonType.Operator:
                        InitializeOperatorSet(button);
                        break;
                }
            }

        }
    }
}
