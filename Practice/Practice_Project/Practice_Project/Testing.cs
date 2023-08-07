using AlgebraLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Practice_Project
{
    internal class Testing
    {
        public static void Main()
        {
            /*
            PostFixConverter expression = new PostFixConverter();
            List<Token> postFix = new List<Token>();
            string exp = "fact(5-2)";
            postFix = expression.Converter(exp);
            foreach (Token token in postFix)
            {
                Console.WriteLine(token.Symbol+" "+token.Type+" "+token.Precedence);
            }
            */
            List<Token> tokens = new List<Token>();
            //string path = @"D:\Shariq\Practice\Practice_Project\Practice_Project\jsconfig1.json";
            string jsonString = File.ReadAllText("D:\\Shariq\\Practice\\Practice_Project\\Practice_Project\\jsconfig1.json");
            tokens  = JsonConvert.DeserializeObject<List<Token>>(jsonString);
            foreach (Token token in tokens) 
            {
                Console.WriteLine(token.Symbol + " \tis a " + token.Type + " and has a precedence of " + token.Precedence + "\n");
            }
            Console.ReadLine();
        }
    }

}
