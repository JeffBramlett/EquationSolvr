﻿using EquationSolver.Dto;
using System;
using System.Collections.Generic;

namespace EquationSolver
{
    public interface IEquationSolver: IDisposable
    {
        VariableProvider VariableProvider { get; }
        IEquationSolver SolveEquations();

        event EventHandler VariableNotFoundException;
        event EventHandler ExceptionOccurred;
    }
}