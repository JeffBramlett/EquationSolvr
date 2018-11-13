using EquationSolver.Dto;
using System;
using System.Collections.Generic;

namespace EquationSolver
{
    public interface IEquationSolver: IDisposable
    {
        VariableProvider Variables { get; }
        IEquationSolver SolveEquations();
    }
}