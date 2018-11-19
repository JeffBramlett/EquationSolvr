using System;
using System.Collections.Generic;
using System.Text;

namespace EquationSolver
{
    public class ExceptionEventArgs: EventArgs
    {
        public Exception Exception { get; set; }

        public ExceptionEventArgs(Exception ex)
        {
            Exception = ex;
        }
    }
}
