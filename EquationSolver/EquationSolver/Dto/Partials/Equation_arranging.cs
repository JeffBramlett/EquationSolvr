using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver.Dto
{
    public partial class Equation : IComparable<Equation>, IComparer<Equation>
    {
        public int Compare(Equation x, Equation y)
        {
            return x.CompareTo(y);
        }

        public int CompareTo(Equation other)
        {
            int hasUse = UseExpression.Contains(other.Target)? 1: 0;
            int hasExp = Expression.Contains(other.Target) ? 1 : 0;

            return hasUse + hasExp;
            
        }

        public bool HasTarget(string targetName)
        {
            bool found = false;

            if (Target == targetName)
                return true;

            foreach(var subEquation in MoreEquations)
            {
                if (subEquation.HasTarget(targetName))
                    return true;
            }

            return found;
        }
    }
}
