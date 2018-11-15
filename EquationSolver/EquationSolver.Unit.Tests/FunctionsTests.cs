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
        public void RoundingTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Project",
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "round(123.456789, 2)",
                        Target = "T1"
                    },
                }
            };

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(123.46, solver.Variables["T1"].DoubleValue);
        }
    }
}
