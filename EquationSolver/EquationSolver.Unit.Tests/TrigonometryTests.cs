using System;
using System.Collections.Generic;
using EquationSolver.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationSolver.Unit.Tests
{
    [TestClass]
    public class TrigonometryTests
    {
        [TestMethod]
        public void SinAndCosTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Unit Project",
            };

            Equation sinEquation = new Equation()
            {
                UseExpression = "true",
                Expression = "sin(30)",
                Target = "sin"
            };

            Equation cosEquation = new Equation()
            {
                UseExpression = "true",
                Expression = "cos(30)",
                Target = "cos"
            };

            project.Equations.Add(sinEquation);
            project.Equations.Add(cosEquation);

            var expectedSin = Convert.ToDecimal(Math.Sin(30));
            var expectedCos = Convert.ToDecimal(Math.Cos(30));

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expectedSin, solver.Variables["sin"].DecimalValue);
            Assert.AreEqual(expectedCos, solver.Variables["cos"].DecimalValue);
        }

        [TestMethod]
        public void ASinAndACosTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Unit Project",
            };

            Equation sinEquation = new Equation()
            {
                UseExpression = "true",
                Expression = "asin(.5)",
                Target = "asin"
            };

            Equation cosEquation = new Equation()
            {
                UseExpression = "true",
                Expression = "acos(.5)",
                Target = "acos"
            };

            project.Equations.Add(sinEquation);
            project.Equations.Add(cosEquation);

            var expectedSin = Convert.ToDecimal(Math.Asin(.5));
            var expectedCos = Convert.ToDecimal(Math.Acos(.5));

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expectedSin, solver.Variables["asin"].DecimalValue);
            Assert.AreEqual(expectedCos, solver.Variables["acos"].DecimalValue);
        }

        [TestMethod]
        public void SinHAndHCosTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Unit Project",
            };

            Equation sinEquation = new Equation()
            {
                UseExpression = "true",
                Expression = "sinh(.5)",
                Target = "sinh"
            };

            Equation cosEquation = new Equation()
            {
                UseExpression = "true",
                Expression = "cosh(.5)",
                Target = "cosh"
            };

            project.Equations.Add(sinEquation);
            project.Equations.Add(cosEquation);

            var expectedSin = Convert.ToDecimal(Math.Sinh(.5));
            var expectedCos = Convert.ToDecimal(Math.Cosh(.5));

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expectedSin, solver.Variables["sinh"].DecimalValue);
            Assert.AreEqual(expectedCos, solver.Variables["cosh"].DecimalValue);
        }

        [TestMethod]
        public void TanTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Unit Project",
            };

            Equation tanEquation = new Equation()
            {
                UseExpression = "true",
                Expression = "tan(30)",
                Target = "tan"
            };

            Equation aTanEquation = new Equation()
            {
                UseExpression = "true",
                Expression = "atan(.5)",
                Target = "atan"
            };

            Equation tanhEquation = new Equation()
            {
                UseExpression = "true",
                Expression = "tanh(.5)",
                Target = "tanh"
            };

            project.Equations.Add(tanEquation);
            project.Equations.Add(aTanEquation);
            project.Equations.Add(tanhEquation);

            var expectedTan = Convert.ToDecimal(Math.Tan(30));
            var expectedATan = Convert.ToDecimal(Math.Atan(.5));
            var expectedTanh = Convert.ToDecimal(Math.Tanh(.5));

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expectedTan, solver.Variables["tan"].DecimalValue);
            Assert.AreEqual(expectedATan, solver.Variables["atan"].DecimalValue);
            Assert.AreEqual(expectedTanh, solver.Variables["tanh"].DecimalValue);
        }
    }
}
