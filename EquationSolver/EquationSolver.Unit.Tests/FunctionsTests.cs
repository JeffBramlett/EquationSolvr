using System;
using System.Collections.Generic;
using EquationSolver.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationSolver.Unit.Tests
{
    [TestClass]
    public class FunctionsTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Mulitply Divide Project",
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "2 * 1",
                        Target = "T1"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "4 / 2",
                        Target = "T2"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "(2 * 2) - (4 / 2)",
                        Target = "T3"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "(2 * 4) / (2 * 2)",
                        Target = "T4"
                    }
                }
            };

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(2, solver.Variables["T1"].DecimalValue);
            Assert.AreEqual(2, solver.Variables["T2"].DecimalValue);
            Assert.AreEqual(2, solver.Variables["T3"].DecimalValue);
            Assert.AreEqual(2, solver.Variables["T4"].DecimalValue);
        }
    }
}
