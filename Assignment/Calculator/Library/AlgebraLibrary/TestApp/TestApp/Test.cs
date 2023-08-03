using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgebraLibrary;

namespace TestApp
{
    internal class Test
    {
        static void Main(string[] args)
        {
            double[] exp = {16, 4};
            Addition add = new Addition();
            add.OperandCount = 5;
            Subtraction subtract = new Subtraction();
            Multiplication multiply = new Multiplication();
            Division divide = new Division();
            // Reciprocal reciprocal = new Reciprocal();
            try
            {
                Console.WriteLine(add.Evaluate(exp));
                Console.WriteLine(subtract.Evaluate(exp));
                Console.WriteLine(multiply.Evaluate(exp));
                Console.WriteLine(divide.Evaluate(exp));
                // Console.WriteLine(reciprocal.Evaluate(exp);

            } catch (Exception ex) { 

                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }
    }
}
