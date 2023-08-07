﻿using AlgebraLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    [Serializable]
    public class StartingWithBinaryOperatorException : Exception
    {
        public StartingWithBinaryOperatorException() 
            :base(Resources.StartingWithOperator) { }
    }
}