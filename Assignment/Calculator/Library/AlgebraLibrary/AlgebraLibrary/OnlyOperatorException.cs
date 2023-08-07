using AlgebraLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    [Serializable]
    public class OnlyOperatorException : Exception
    {
        public OnlyOperatorException()
            : base(Resources.OperatorOnly) { }
    }
}
