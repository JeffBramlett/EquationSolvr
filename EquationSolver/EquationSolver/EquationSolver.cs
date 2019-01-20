using EquationSolver.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EquationSolver
{
    sealed class EquationSolver :  IEquationSolver
    {
        #region constants
        private const string TableDefineRegExp = @"^table\(([a-zA-Z0-9]+),([0-9 ]+)\)";
        private const string TableAccessRegExp = @"^([a-zA-Z0-9]+)\(([a-zA-Z0-9 ]+),([0-9 ]+)\)";
        private const string TableSetValueRegExp = @"^([a-zA-Z0-9]+)\(([0-9 ]+),([0-9 ]+), ([a-zA-Z0-9 ]+)\)";
        #endregion

        #region Fields
        VariableProvider _variableProvider;
        List<Equation> _equationList;
        ExpressionSolver _solver;
        #endregion

        #region Properties
        public VariableProvider VariableProvider
        {
            get
            {
                _variableProvider = _variableProvider ?? new VariableProvider();
                return _variableProvider;
            }
        }

        private ExpressionSolver Solver
        {
            get
            {
                if(_solver == null)
                {
                    _solver = new ExpressionSolver();
                    _solver.ExceptionOccurred += Solver_ExceptionOccurred;
                    _solver.VariableNotFound += Solver_VariableNotFound;
                }
                _solver = _solver ?? new ExpressionSolver();
                return _solver;
            }
        }

        private void Solver_VariableNotFound(string variableName)
        {
            VariableNotFoundException?.Invoke(this, new VariableNotFoundEventArgs(variableName));
        }

        private void Solver_ExceptionOccurred(Exception e)
        {
            ExceptionOccurred?.Invoke(this, new ExceptionEventArgs(e));
        }

        private List<Equation> Equations
        {
            get
            {
                _equationList = _equationList ?? new List<Equation>();
                return _equationList;
            }
        }
        #endregion

        #region Events

        public event EventHandler VariableNotFoundException;
        public event EventHandler ExceptionOccurred;
        #endregion

        #region Ctors and Dtors
        public EquationSolver(VariableProvider provider = null)
        {
            _variableProvider = provider;
        }
        #endregion

        #region Public
        #endregion

        #region Public Variable methods
        #endregion

        #region Public Execution methods
        public IEquationSolver SolveEquations()
        {          
            foreach(var equation in Equations)
            {
                for (var i = 0; i < equation.Iterate; i++)
                {
                    ExecuteEquation(equation);
                }
            }
            return this;
        }

        #endregion

        #region Public Loading
        public void AddEquation(Equation equation)
        {
            Equations.Add(equation);
        }
        public void AddFunctions(IEnumerable<Function> functions)
        {
            Solver.Functions.AddRange(functions);
        }
        public void AddEquations(IEnumerable<Equation> equations)
        {
            Equations.AddRange(equations);
        }

        public void AddTables(IEnumerable<Table> tables)
        {
            foreach(var table in tables)
            {
                int columnCnt = table.RowHeader.Columns.Count;

                VariableProvider.StartTable(table.Name, columnCnt);

                int ndx = 1;
                foreach(var col in table.RowHeader.Columns)
                {
                    VariableProvider.SetColumnLabel(table.Name, ndx, col);
                    ndx++;
                }

                int rowNdx = 1;
                foreach(var row in table.Rows)
                {
                    VariableProvider.MakeRowInTable(table.Name, row.Label);

                    for(var c = 0; c < columnCnt; c++)
                    {
                        if (c < row.Columns.Count)
                        {
                            VariableProvider.SetValueInTable(table.Name, c + 1, rowNdx, row.Columns[c]);
                        }
                    }

                    rowNdx++;
                }
            }
        }
        #endregion

        #region privates


        private void ExecuteEquation(Equation equation)
        {
            Solver.Resolve(equation.UseExpression, VariableProvider);
            if (Solver.BoolResult)
            {
                Solver.Resolve(equation.Expression, VariableProvider);

                if (!HandleTargetForTable(equation.Target, Solver.StringResult))
                {
                    VariableProvider.SetVariable(equation.Target, Solver.StringResult);
                }

                foreach(var moreEquation in equation.MoreEquations)
                {
                    ExecuteEquation(moreEquation);
                }
            }
        }

        private bool HandleTargetForTable(string input, string resultForTarget)
        {
            Regex tableRegex = new Regex(TableAccessRegExp);
            var groups = tableRegex.Match(input).Groups;

            if (tableRegex.IsMatch(input.Trim()))
            {
                
                var tableName = groups[1].Value.Trim();
                var grp1 = groups[2].Value.Trim();
                var grp2 = groups[3].Value.Trim();

                int cols = int.Parse(grp2);

                if(tableName.ToLower() == "table")
                {
                    VariableProvider.StartTable(grp1, cols);
                }
                else if(VariableProvider.HasTable(tableName))
                {
                    int row;
                    if(!int.TryParse(grp1, out row))
                    {
                        var varValue = VariableProvider[grp1].DecimalValue;
                        row = Convert.ToInt32(varValue);
                    }

                    VariableProvider.SetValueInTable(tableName, cols, row, resultForTarget);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EquationSolver() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
