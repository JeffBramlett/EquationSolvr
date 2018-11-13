using EquationSolver.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    sealed class EquationSolver :  IEquationSolver
    {
        #region Fields
        VariableProvider _variableProvider;
        List<Equation> _equationList;
        ExpressionSolver _solver;
        #endregion

        #region Properties
        public VariableProvider Variables
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
                _solver = _solver ?? new ExpressionSolver();
                return _solver;
            }
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
                ExecuteEquation(equation);
            }
            return this;
        }

        #endregion

        #region Public Loading
        public void AddEquation(Equation equation)
        {
            Equations.Add(equation);
        }

        public void AddEquations(IEnumerable<Equation> equations)
        {
            Equations.AddRange(equations);
        }
        #endregion

        #region privates
        private void ExecuteEquation(Equation equation)
        {
            Solver.Resolve(equation.UseExpression, Variables);
            if (Solver.BoolResult)
            {
                Solver.Resolve(equation.Expression, Variables);
                Variables.SetVariable(equation.Target, Solver.StringResult);

                foreach(var moreEquation in equation.MoreEquations)
                {
                    ExecuteEquation(moreEquation);
                }
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
