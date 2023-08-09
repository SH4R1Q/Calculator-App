using AlgebraLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class Claculator : Form
    {
        Button button1 = new Button();
        Button numbers = new Button();
        GroupBox groupBox1 = new GroupBox();
        TextBox textBox1 = new TextBox();
        ExpressionEvaluator expressionEvaluator = new ExpressionEvaluator();
        public Claculator()
        {
            InitializeComponent();
            textBox1.Location = new Point(10,0);
            textBox1.Size = new Size(750,30) ;
            this.Controls.Add(textBox1);
            button1.Location = new Point(330, 40);
            button1.Text = "Evaluate";
            button1.Size = new Size(100,30) ;
            this.Controls.Add(button1);
            groupBox1.Location = new Point(10, 80);
            groupBox1.Size = new Size(750, 400);
            this.Controls.Add((GroupBox)groupBox1);
        }

        private void Claculator_Load(object sender, EventArgs e)
        {
            button1.Click += new EventHandler(Button_Click);
        }
        private void Button_Click(object sender,EventArgs e)
        {
            try
            {
                double result = expressionEvaluator.Evaluate(textBox1.Text);
                textBox1.Text = result.ToString();
            }
            catch (Exception exception)
            { 
                textBox1.Text = exception.Message;
            }
        }
    }
}
