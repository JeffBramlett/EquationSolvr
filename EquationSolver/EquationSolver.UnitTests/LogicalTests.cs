using System;
using System.Collections.Generic;
using EquationSolver.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationSolver.Unit.Tests
{
    [TestClass]
    public class LogicalTests
    {
        [TestMethod]
        public void SimpleComparisonTest()
        {
            EquationProject project = new EquationProject()
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
                        Expression = "true",
                        Target = "T1"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "false",
                        Target = "T2"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "Two > One",
                        Target = "T3"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "Two < One",
                        Target = "T4"
                    }
                }
            };

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.IsTrue(solver.VariableProvider["T1"].BoolValue);
            Assert.IsFalse(solver.VariableProvider["T2"].BoolValue);
            Assert.IsTrue(solver.VariableProvider["T3"].BoolValue);
            Assert.IsFalse(solver.VariableProvider["T4"].BoolValue);
        }
        [TestMethod]
        public void StringComparisonTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Unit Project",
                Variables = new List<Variable>()
                {
                    new Variable(){ Name = "One", StringValue = "Test1"},
                    new Variable(){ Name = "Two", StringValue = "Test2"},
                },
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "One = Two",
                        Target = "T1"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "One != Two",
                        Target = "T2"
                    }
                }
            };

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.IsFalse(solver.VariableProvider["T1"].BoolValue);
            Assert.IsFalse(solver.VariableProvider["T2"].BoolValue);
        }


        [TestMethod]
        public void CombinedComparisonAndLogicalTest()
        {
            EquationProject project = new EquationProject()
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
                        UseExpression = "1=One",
                        Expression = "Two > One and One < Four",
                        Target = "T1"
                    },
                    new Equation()
                    {
                        UseExpression = "Two>1",
                        Expression = "Four / Two >= Two AND One > 2",
                        Target = "T2"
                    },
                    new Equation()
                    {
                        UseExpression = "Two > One",
                        Expression = "(Two * Two) = 4 oR (Four / Two) = 2",
                        Target = "T3"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "(Two * Two) != 4 oR (Four / Two) != 2",
                        Target = "T4"
                    }
                }
            };

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.IsTrue(solver.VariableProvider["T1"].BoolValue);
            Assert.IsFalse(solver.VariableProvider["T2"].BoolValue);
            Assert.IsTrue(solver.VariableProvider["T3"].BoolValue);
            Assert.IsFalse(solver.VariableProvider["T4"].BoolValue);
        }

    }
}
