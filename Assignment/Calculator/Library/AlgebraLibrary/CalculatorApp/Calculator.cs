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


namespace CalculatorApp
{
    public partial class Calculator : Form
    {
        TableLayoutPanel _numberSet = new TableLayoutPanel();
        Button _evaluateButton = new Button();
        Button _clearButton = new Button();
        Button _backspaceButton = new Button();
        GroupBox _calculatorControls = new GroupBox();
        FlowLayoutPanel _operatorSet = new FlowLayoutPanel();
        TextBox _calculatorScreen = new TextBox();
        TextBox _memory = new TextBox();
        ExpressionEvaluator _expressionEvaluator = new ExpressionEvaluator();
        Button _prevAnswer = new Button();
        Stack<Double> _memoryStack = new Stack<Double>();
        double _result;
        double _prevResult;

        public Calculator()
        {
            InitializeComponent();

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

            // Execute Button

            _evaluateButton = CreateButton("=","Evaluate Button");
            _evaluateButton.Dock = DockStyle.Fill;
            _evaluateButton.Click += new EventHandler(EvaluateExpression);
            _numberSet.Controls.Add(_evaluateButton, 2, 4);

            // Calculator Control

            _calculatorControls.Location = new Point(10, 60);
            _calculatorControls.Size = new Size(750, 420);
            _calculatorControls.Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
            this.Controls.Add((GroupBox)_calculatorControls);

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
            InitializeNumberSet();

            // Clear Button 

            _clearButton = CreateButton("C", "Clear All");
            _clearButton.Dock = DockStyle.Fill;
            _clearButton.Click += new EventHandler(ButtonClick);
            _numberSet.Controls.Add(_clearButton, 0, 0);

            // Back Space Button

            _backspaceButton = CreateButton("<-","Back Space");
            _backspaceButton.Dock = DockStyle.Fill;
            _backspaceButton.Click += new EventHandler(ButtonClick);
            _numberSet.Controls.Add(_backspaceButton, 2, 0);

            // Operator Set

            _operatorSet.Location = new Point(10,110);
            _operatorSet.Size = new Size(460,300);
            _operatorSet.BackColor = Color.FromArgb(0, 10, 40);
            this._calculatorControls.Controls.Add(_operatorSet);
            _operatorSet.Anchor = AnchorStyles.Bottom | AnchorStyles.Left ;
            InitializeOperatorSet();

            // Previous Answer

            _prevAnswer = CreateButton("Ans", "Previous Answer");
            _prevAnswer.Dock = DockStyle.Fill;
            _prevAnswer.Click += new EventHandler(ButtonClick);
            _numberSet.Controls.Add(_prevAnswer, 1, 0);
        }
        private void EvaluateExpression(object sender, EventArgs e)
        {
            try
            {                
                _result = _expressionEvaluator.Evaluate(_calculatorScreen.Text);
                _calculatorScreen.Text = _result.ToString();
                _prevResult = _result;
            }
            catch (Exception exception)
            {
                _calculatorScreen.Text = exception.Message;
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
                    Button btn = new Button();
                    btn = CreateButton(numberSetArray[arrayIndex++].ToString(),"Number");
                    btn.Dock = DockStyle.Fill;
                    btn.Click += new EventHandler(ButtonClick);
                    _numberSet.Controls.Add(btn, columns, rows);
                    btn = null;
                }
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            switch (clickedButton.Name)
            {
                case "Clear All":
                    _calculatorScreen.Clear();
                    break;

                case "Number":
                    _calculatorScreen.Text += clickedButton.Text;
                    break;

                case "Operator":
                    _calculatorScreen.Text += clickedButton.Text;
                    break;

                case "Back Space":
                    if (_calculatorScreen.Text.Length == 0) { break; }
                    _calculatorScreen.Text = _calculatorScreen.Text.Substring(0, _calculatorScreen.Text.Length - 1);
                    break;

                case "Previous Answer":
                    _calculatorScreen.Text += _prevResult.ToString();
                    break;

                case "Add Memory":
                    if (!double.TryParse(_calculatorScreen.Text, out double number1)) { break; }
                    double add = _memoryStack.Pop();
                    add += Convert.ToDouble(_calculatorScreen.Text);
                    _memoryStack.Push(add);
                    _memory.Text = _memoryStack.Peek().ToString();
                    break;

                case "Subtract Memory":
                    if (!double.TryParse(_calculatorScreen.Text, out double number2)) { break; }
                    double subtract = _memoryStack.Pop();
                    subtract -= Convert.ToDouble(_calculatorScreen.Text);
                    _memoryStack.Push(subtract);
                    _memory.Text = _memoryStack.Peek().ToString();
                    break;

                case "Save Memory":
                    if(!double.TryParse(_calculatorScreen.Text, out double number3)) { break; }
                    _memoryStack.Push(Convert.ToDouble(_calculatorScreen.Text));
                    _memory.Text = _memoryStack.Peek().ToString();
                    break;

                case "Read Memory":
                    if (!double.TryParse(_memory.Text, out double number4)) { break; }
                    _calculatorScreen.Text += _memory.Text;
                    break;

                case "Clear Memory":
                    _memoryStack.Clear();
                    _memory.Text = String.Empty;
                    break;

                case "All Clear":
                    _memoryStack.Clear();
                    _memory.Text = String.Empty;
                    _calculatorScreen.Text = String.Empty;
                    _prevResult = 0;
                    _result = 0;
                    break;
            }
        }
        private Button CreateButton(string text,string name)
        {
            Button newButton = new Button
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
            return newButton;
        }
        private void InitializeOperatorSet()
        {
            List<ConfigureClass> operators = new List<ConfigureClass>();
            string fileName = File.ReadAllText("Properties\\ConfigurationFile.json");
            operators = JsonConvert.DeserializeObject<List<ConfigureClass>>(fileName);
            foreach(ConfigureClass token in operators)
            {
                Button newButton = CreateButton(token.Symbol,"Operator");
                newButton.Size = new Size(70,70);
                newButton.Click += new EventHandler(ButtonClick);
                _operatorSet.Controls.Add(newButton);
                newButton = null;
            }
            Button memoryButtons;
            memoryButtons = CreateButton("M+", "Add Memory");
            memoryButtons.Click += new EventHandler(ButtonClick);
            _operatorSet.Controls.Add(memoryButtons);
            memoryButtons = CreateButton("M-", "Subtract Memory");
            memoryButtons.Click += new EventHandler(ButtonClick);
            _operatorSet.Controls.Add(memoryButtons);
            memoryButtons = CreateButton("MS", "Save Memory");
            memoryButtons.Click += new EventHandler(ButtonClick);
            _operatorSet.Controls.Add(memoryButtons);
            memoryButtons = CreateButton("MR", "Read Memory");
            memoryButtons.Click += new EventHandler(ButtonClick);
            _operatorSet.Controls.Add(memoryButtons);
            memoryButtons = CreateButton("MC", "Clear Memory");
            memoryButtons.Click += new EventHandler(ButtonClick);
            _operatorSet.Controls.Add(memoryButtons);
            memoryButtons = CreateButton("AC", "All Clear");
            memoryButtons.Click += new EventHandler(ButtonClick);
            _operatorSet.Controls.Add(memoryButtons);
            memoryButtons = null;
        }
    }
}
