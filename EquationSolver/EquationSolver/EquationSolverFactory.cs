using EquationSolver.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    public class EquationSolverFactory
    {
        #region fields
        private static Lazy<EquationSolverFactory> _instance = new Lazy<EquationSolverFactory>();
        #endregion

        #region Ctors and Dtors
        public EquationSolverFactory()
        {

        }
        #endregion

        #region Singleton
        public static EquationSolverFactory Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        #endregion

        #region Publics
        public IEquationSolver CreateEquationSolver(EquationProject equationProject, VariableProvider varProvider = null)
        {
            if(varProvider == null)
            {
                varProvider = new VariableProvider();
            }

            foreach(Variable vr in equationProject.Variables)
            {
                varProvider.SetVariable(vr.Name, vr.StringValue);
            }

            EquationSolver solver = new EquationSolver(varProvider);

            equationProject.Equations.Sort();

            solver.AddEquations(equationProject.Equations);

            return solver;
        }
        #endregion

        #region Privates
        private List<Equation> SortEquations(List<Equation> listToSort)
        {
            List<Equation> sortedList = new List<Equation>();
            foreach(var eq in listToSort)
            {
                if (sortedList.Count == 0)
                    sortedList.Add(eq);
                else
                {
                    var ndx = GetIndexToSortedList(eq, sortedList);

                    if (ndx >= sortedList.Count)
                        sortedList.Add(eq);
                    else
                        sortedList.Insert(ndx, eq);
                }
                
            }
            return sortedList;
        }

        private int GetIndexToSortedList(Equation eq, List<Equation> sortedList)
        {
            int ndx = 0;
            foreach (var seq in sortedList)
            {
                if (seq.HasTarget(eq.Target))
                {
                    ndx = sortedList.IndexOf(seq) + 1;
                }
            }
            return ndx;
        }
        #endregion
    }
}
