using System;
using System.Collections.Generic;
using System.Text;

namespace EquationSolver
{
    public interface ILogEquationSolver
    {
        void LogDebug(string message, params object[] formatArgs);
    }
}
