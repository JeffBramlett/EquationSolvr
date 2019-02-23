using System;
using System.Collections.Generic;
using EquationSolver.Dto;

namespace EquationSolver
{
    #region Enums
    /// <summary>
    /// Enumeration of Result Types
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// Default enum value
        /// </summary>
        NONE,
        /// <summary>
        /// ResultType as Boolean
        /// </summary>
        BOOL,
        /// <summary>
        /// ResultType as Number
        /// </summary>
        NUMBER,
        /// <summary>
        /// ResultType as String
        /// </summary>
        STRING
    }

    #endregion
    interface IExpressionSolver
    {
        bool BoolResult { get; }
        List<Function> Functions { get; set; }

        decimal ResultAsDecimal { get; }

        double ResultAsDouble { get; }

        string StringResult { get; }

        ResultType TypeOfResult { get; }


        event EventHandler ExceptionOccurred;
        event EventHandler VariableNotFound;

        void Resolve(string input, VariableProvider variableProvider);
    }
}