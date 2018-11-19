using System;
using System.Collections.Generic;
using System.Text;

namespace EquationSolver
{
    public class VariableNotFoundEventArgs: EventArgs
    {
        public string VariableName { get; set; }

        public VariableNotFoundEventArgs(string variableName)
        {
            VariableName = variableName;
        }
    }
}
