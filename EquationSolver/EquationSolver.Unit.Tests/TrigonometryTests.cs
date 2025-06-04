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
                Activation = "true",
                Expression = "sin(30)",
                Target = "sin"
            };

            Equation cosEquation = new Equation()
            {
                Activation = "true",
                Expression = "cos(30)",
                Target = "cos"
            };

            project.Equations.Add(sinEquation);
            project.Equations.Add(cosEquation);

            var expectedSin = Convert.ToDecimal(Math.Sin(30));
            var expectedCos = Convert.ToDecimal(Math.Cos(30));

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expectedSin, solver.VariableProvider["sin"].DecimalValue);
            Assert.AreEqual(expectedCos, solver.VariableProvider["cos"].DecimalValue);
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
                Activation = "true",
                Expression = "asin(.5)",
                Target = "asin"
            };

            Equation cosEquation = new Equation()
            {
                Activation = "true",
                Expression = "acos(.5)",
                Target = "acos"
            };

            project.Equations.Add(sinEquation);
            project.Equations.Add(cosEquation);

            var expectedSin = Convert.ToDecimal(Math.Asin(.5));
            var expectedCos = Convert.ToDecimal(Math.Acos(.5));

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expectedSin, solver.VariableProvider["asin"].DecimalValue);
            Assert.AreEqual(expectedCos, solver.VariableProvider["acos"].DecimalValue);
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
                Activation = "true",
                Expression = "sinh(.5)",
                Target = "sinh"
            };

            Equation cosEquation = new Equation()
            {
                Activation = "true",
                Expression = "cosh(.5)",
                Target = "cosh"
            };

            project.Equations.Add(sinEquation);
            project.Equations.Add(cosEquation);

            var expectedSin = Convert.ToDecimal(Math.Sinh(.5));
            var expectedCos = Convert.ToDecimal(Math.Cosh(.5));

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expectedSin, solver.VariableProvider["sinh"].DecimalValue);
            Assert.AreEqual(expectedCos, solver.VariableProvider["cosh"].DecimalValue);
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
                Activation = "true",
                Expression = "tan(30)",
                Target = "tan"
            };

            Equation aTanEquation = new Equation()
            {
                Activation = "true",
                Expression = "atan(.5)",
                Target = "atan"
            };

            Equation tanhEquation = new Equation()
            {
                Activation = "true",
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

            Assert.AreEqual(expectedTan, solver.VariableProvider["tan"].DecimalValue);
            Assert.AreEqual(expectedATan, solver.VariableProvider["atan"].DecimalValue);
            Assert.AreEqual(expectedTanh, solver.VariableProvider["tanh"].DecimalValue);
        }
    }
}
