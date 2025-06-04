using System;
using EquationSolver.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationSolver.Unit.Tests
{
    [TestClass]
    public class UserDefinedFunctionTests
    {
        [TestMethod]
        public void SimpleUserDefinedFunctionTest()
        {
            // Define functions
            Function quadraticMinusFunction = new Function()
            {
                Name = "quadraticMinus",
                Description = "Quadratic (-) formula function",
                Expression = "((b * -1) - sqrt(b ^ 2 - 4 * a * c)) / (2 * a)",
                Arguments = new System.Collections.Generic.List<Argument>()
                {
                    new Argument()
                    {
                        Name = "a",
                        Ordinal = 1,
                        Default ="1"
                    },
                    new Argument()
                    {
                        Name = "b",
                        Ordinal = 2,
                        Default = "3"
                    },
                    new Argument()
                    {
                        Name = "c",
                        Ordinal = 3,
                        Default = "-4"
                    }
                }
            };

            Function quadraticPlusFunction = new Function()
            {
                Name = "quadraticPlus",
                Description = "Quadratic (+) formula function",
                Expression = "((b * -1) + sqrt(b ^ 2 - 4 * a * c)) / (2 * a)",
                Arguments = new System.Collections.Generic.List<Argument>()
                {
                    new Argument()
                    {
                        Name = "a",
                        Ordinal = 1,
                        Default ="1"
                    },
                    new Argument()
                    {
                        Name = "c",
                        Ordinal = 3,
                        Default = "-4"
                    },
                    new Argument()
                    {
                        Name = "b",
                        Ordinal = 2,
                        Default = "3"
                    }
                }
            };

            // Add functions to Project
            EquationProject quadraticProject = new EquationProject()
            {
                Title = "Quadratic Equations",
                Functions = new System.Collections.Generic.List<Function>()
                {
                    quadraticMinusFunction,
                    quadraticPlusFunction
                }
            };


            var negativeXquation = new Equation()
            {
                Activation = "true",
                Expression = "quadraticMinus(1, 3, -4)",
                Target = "X1"
            };

            var positiveXEquation = new Equation()
            {
                Activation = "true",
                Expression = "quadraticPlus(1, 3, -4)",
                Target = "X2"
            };

            quadraticProject.Equations.Add(negativeXquation);
            quadraticProject.Equations.Add(positiveXEquation);

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(quadraticProject);
            solver.SolveEquations();

            Assert.AreEqual(-4, solver.VariableProvider["X1"].DecimalValue);
            Assert.AreEqual(1, solver.VariableProvider["X2"].DecimalValue);
        }
    }
}
