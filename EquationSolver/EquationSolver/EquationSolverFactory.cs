using EquationSolver.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EquationSolver
{
    /// <summary>
    /// Factory for Eqution Projects to Equation Solver
    /// </summary>
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
        /// <summary>
        /// Static Singleton instance of EquationSolverFactory
        /// </summary>
        public static EquationSolverFactory Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        #endregion

        #region Publics
        public IEquationSolver CreateEquationSolver(string equationProjectAsJson, VariableProvider varProvider = null)
        {
            EquationProject proj = JsonConvert.DeserializeObject<EquationProject>(equationProjectAsJson);

            return CreateEquationSolver(proj, varProvider);
        }

        public IEquationSolver CreateEquationSolver(EquationProject equationProject, VariableProvider varProvider = null)
        {
            foreach(var eq in equationProject.Equations)
            {
                var equationValids = ValidateEquation(eq);
                if(equationValids != null && equationValids.Count() > 0)
                {
                    var exp = new ArgumentException("Equation(s) are not valid in the project (see exception.Data for details)");
                    exp.Data.Add("ValidationErrors", new List<string>(equationValids));
                    throw exp;
                }
            }

            var tableValids = ValidateTables(equationProject);
            if (tableValids != null && tableValids.Count() > 0)
            {
                var exp = new ArgumentException("Project table(s) have errors (see exception.Data for details)");
                exp.Data.Add("Validation Errors", new List<string>(tableValids));
            }

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
            solver.AddFunctions(equationProject.Functions);
            solver.AddTables(equationProject.Tables);

            return solver;
        }
        
        public static Variable SolveExpression(string expression, VariableProvider varProvider = null)
        {
            VariableProvider prov = varProvider == null ? new VariableProvider() : varProvider;

            ExpressionSolver expressionSolver = new ExpressionSolver();
            expressionSolver.Resolve(expression, prov);

            Variable v = new Variable();
            v.SetName("result").SetValue(expressionSolver.StringResult);

            return v;
        }
        #endregion

        #region Privates
        private IEnumerable<string> ValidateEquation(Equation equation)
        {
            if (!ValidateExpression(equation.UseExpression))
                yield return "UseExpression is invalid";
            if (!ValidateExpression(equation.Expression))
                yield return "Expression is invalid";

            foreach(var eq in equation.MoreEquations)
            {
                foreach(var result in ValidateEquation(eq))
                {
                    yield return result;
                }
            }
        }

        private const string OnlyNameRegExpression = "^[a-zA-Z0-9_]+";

        private IEnumerable<string> ValidateTables(EquationProject project)
        {
            foreach(var table in project.Tables)
            {
                Regex nameRegex = new Regex(OnlyNameRegExpression);
                if (!nameRegex.IsMatch(table.Name))
                    yield return "Table name is invalid (only alpha-numeric characters and no spaces)";
            }
        }

        private bool ValidateExpression(string expression)
        {
            if (string.IsNullOrEmpty(expression.Trim()))
                return false;

            int openCnt = expression.Count(c => c == '(');
            int closedCnt = expression.Count(c => c == ')');

            return openCnt == closedCnt;
        }

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
