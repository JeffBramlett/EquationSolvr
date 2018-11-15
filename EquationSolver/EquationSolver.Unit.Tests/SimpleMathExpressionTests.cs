﻿using System;
using System.Collections.Generic;
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

            Assert.AreEqual(2, solver.Variables["T1"].DecimalValue);
            Assert.AreEqual(2, solver.Variables["T2"].DecimalValue);
            Assert.AreEqual(2, solver.Variables["T3"].DecimalValue);
            Assert.AreEqual(2, solver.Variables["T4"].DecimalValue);
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

            Assert.AreEqual(2, solver.Variables["T1"].DecimalValue);
            Assert.AreEqual(2, solver.Variables["T2"].DecimalValue);
            Assert.AreEqual(2, solver.Variables["T3"].DecimalValue);
            Assert.AreEqual(2, solver.Variables["T4"].DecimalValue);
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

            Assert.AreEqual(2, solver.Variables["T1"].DecimalValue);
            Assert.AreEqual(2, solver.Variables["T2"].DecimalValue);
            Assert.AreEqual(2, solver.Variables["T3"].DecimalValue);
            Assert.AreEqual(2, solver.Variables["T4"].DecimalValue);
        }

    }
}