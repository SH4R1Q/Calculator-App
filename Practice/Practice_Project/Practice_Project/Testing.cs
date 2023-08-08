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
            List<ConfigJson> tokens = new List<ConfigJson>();
            string path = Path.GetFullPath("jsconfig1.json");
            string jsonString = File.ReadAllText(path);
            tokens = JsonConvert.DeserializeObject<List<ConfigJson>>(jsonString);
            Dictionary<Token, IOperation> operatorInfo = new Dictionary<Token, IOperation>();
            
            Addition add = new Addition();
            string json = JsonConvert.SerializeObject(new ConfigJson("+", TokenType.BinaryOperator, 3, add.ToString()));
            ConfigJson djson = JsonConvert.DeserializeObject<ConfigJson>(json);
            Console.WriteLine(json);
            Type className1 = Type.GetType(djson.ClassName);
            object obj1 = Activator.CreateInstance(className1);
            Console.WriteLine(obj1);
            
            foreach(var token in tokens)
            {
                Type classType = Type.GetType(token.ClassName);
                object obj = Activator.CreateInstance(classType);
                operatorInfo.Add(new Token(token.Symbol, token.Type, token.Precedence), (IOperation)obj);
            }
            foreach(var token in operatorInfo) 
            {
                Console.WriteLine(token.Key+" : "+token.Value);
            }
            Console.ReadLine();
        }
    }

}
