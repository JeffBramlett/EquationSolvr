using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver.Dto
{
    /// <summary>
    /// Data class for an Equation
    /// </summary>
    public partial class Equation : IComparable<Equation>, IComparer<Equation>
    {
        /// <summary>
        /// Compare two Equations and return a number for sorting
        /// </summary>
        /// <param name="x">the first equation to compare with the second</param>
        /// <param name="y">the second Equation</param>
        /// <returns>a number for sorting</returns>
        public int Compare(Equation x, Equation y)
        {
            return x.CompareTo(y);
        }

        /// <summary>
        /// Compare this Equation with the other
        /// </summary>
        /// <param name="other">the other Equation</param>
        /// <returns>a number for sorting</returns>
        public int CompareTo(Equation other)
        {
            int hasUse = UseExpression.Contains(other.Target) ? 1 : 0;
            int hasExp = Expression.Contains(other.Target) ? 1 : 0;

            return hasUse + hasExp;

        }

        /// <summary>
        /// Does this Equations have a variable target for it's value
        /// </summary>
        /// <param name="targetName">the target name</param>
        /// <returns>true for having a target, false otherwise</returns>
        public bool HasTarget(string targetName)
        {
            bool found = false;

            if (Target == targetName)
                return true;

            foreach (var subEquation in MoreEquations)
            {
                if (subEquation.HasTarget(targetName))
                    return true;
            }

            return found;
        }

        /// <summary>
        /// Find the equation by name or return false
        /// </summary>
        /// <param name="equationName">the equation name to find</param>
        /// <param name="found">the found equation or null if not found</param>
        /// <returns>true if found, false otherwise</returns>
        public bool TryFindByName(string equationName, out Equation found)
        {
            found = null;

            if (Name == equationName)
            {
                found = this;
            }
            else
            {
                foreach (var eq in MoreEquations)
                {
                    if (eq.TryFindByName(equationName, out found))
                    {
                        break;
                    }
                }
            }

            return found != null;
        }
    }
}
