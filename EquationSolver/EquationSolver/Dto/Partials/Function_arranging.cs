using System;
using System.Collections.Generic;
using System.Text;

namespace EquationSolver.Dto
{
    public partial class Argument : IComparable<Argument>, IComparer<Argument>
    {
        public int Compare(Argument x, Argument y)
        {
            return x.CompareTo(y);
        }

        public int CompareTo(Argument other)
        {
            return Ordinal - other.Ordinal;
        }
    }
}
