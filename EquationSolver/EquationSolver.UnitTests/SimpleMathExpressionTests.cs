using System;
using System.Collections.Generic;
using System.Diagnostics;
using EquationSolver.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationSolver.Unit.Tests
{
    [TestClass]
    public class SimpleMathExpressionTests
    {
        [TestMethod]
        public void AddSubtractTests()
        {
            EquationProject additionProject = new EquationProject()
            {
                Title = "Addition Project",
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "1 + 1",
                        Target = "T1"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "4 - 2",
                        Target = "T2"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "(2 + 2) - (1 + 1)",
                        Target = "T3"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "(3 - 2) + (7 - 6)",
                        Target = "T4"
                    }
                }
            };

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(additionProject);
            solver.SolveEquations();

            Assert.AreEqual(2, solver.VariableProvider["T1"].DecimalValue);
            Assert.AreEqual(2, solver.VariableProvider["T2"].DecimalValue);
            Assert.AreEqual(2, solver.VariableProvider["T3"].DecimalValue);
            Assert.AreEqual(2, solver.VariableProvider["T4"].DecimalValue);
        }

        [TestMethod]
        public void WithMultiplyAndDividesTests()
        {
            EquationProject additionProject = new EquationProject()
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

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(additionProject);
            solver.SolveEquations();

            Assert.AreEqual(2, solver.VariableProvider["T1"].DecimalValue);
            Assert.AreEqual(2, solver.VariableProvider["T2"].DecimalValue);
            Assert.AreEqual(2, solver.VariableProvider["T3"].DecimalValue);
            Assert.AreEqual(2, solver.VariableProvider["T4"].DecimalValue);
        }

        [TestMethod]
        public void WithVariablesTests()
        {
            EquationProject additionProject = new EquationProject()
            {
                Title = "Unit Project",
                Variables = new List<Variable>()
                {
                    new Variable(){ Name = "One", StringValue = "1"},
                    new Variable(){ Name = "Two", StringValue = "2"},
                    new Variable(){ Name = "Four", StringValue = "4"}
                },
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "Two * One",
                        Target = "T1"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "Four / Two",
                        Target = "T2"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "(Two * Two) - (Four / Two)",
                        Target = "T3"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "(Two * Four) / (Two * Two)",
                        Target = "T4"
                    }
                }
            };

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(additionProject);
            solver.SolveEquations();

            Assert.AreEqual(2, solver.VariableProvider["T1"].DecimalValue);
            Assert.AreEqual(2, solver.VariableProvider["T2"].DecimalValue);
            Assert.AreEqual(2, solver.VariableProvider["T3"].DecimalValue);
            Assert.AreEqual(2, solver.VariableProvider["T4"].DecimalValue);
        }

        [TestMethod]
        public void MinMaxTest()
        {
            VariableProvider localVariables = new VariableProvider();
            localVariables.SetVariable("a", 1);
            localVariables.SetVariable("b", 3);

            EquationProject minMaxProject = new EquationProject()
            {
                Title = "Min Max Equation",
            };

            var minEquation = new Equation()
            {
                UseExpression = "true",
                Expression = "min(a, b)",
                Target = "TheMin"
            };
            minMaxProject.Equations.Add(minEquation);

            var maxEquation = new Equation()
            {
                UseExpression = "true",
                Expression = "max(a, b)",
                Target = "TheMax"
            };
            minMaxProject.Equations.Add(maxEquation);

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(minMaxProject, localVariables);
            solver.SolveEquations();

            Assert.AreEqual(1, localVariables["TheMin"].DecimalValue);
            Assert.AreEqual(3, localVariables["TheMax"].DecimalValue);
        }

        [TestMethod]
        public void QuadraticEquationTest()
        {
            VariableProvider localVariables = new VariableProvider();
            localVariables.SetVariable("a", 1);
            localVariables.SetVariable("b", 3);
            localVariables.SetVariable("c", -4);

            EquationProject quadraticProject = new EquationProject()
            {
                Title = "Quadratic Equations",
            };

            var negativeXquation = new Equation()
            {
                UseExpression = "true",
                Expression = "((b*-1) - sqrt(b^2 - 4*a*c))/(2*a)",
                Target = "X1"
            };

            var positiveXEquation = new Equation()
            {
                UseExpression = "true",
                Expression = "((b*-1) + sqrt(b^2 - 4*a*c))/(2*a)",
                Target = "X2"
            };

            quadraticProject.Equations.Add(negativeXquation);
            quadraticProject.Equations.Add(positiveXEquation);

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(quadraticProject, localVariables);
            solver.SolveEquations();

            Assert.AreEqual(-4, localVariables["X1"].DecimalValue);
            Assert.AreEqual(1, localVariables["X2"].DecimalValue);
        }

    }
}
