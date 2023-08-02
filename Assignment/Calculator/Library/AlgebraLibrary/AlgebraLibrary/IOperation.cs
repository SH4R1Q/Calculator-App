using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AlgebraLibrary
{
    public interface IOperation
    {
       int OperandCount { get; set; }
       double Evaluate(double[] expression);
    }
}
