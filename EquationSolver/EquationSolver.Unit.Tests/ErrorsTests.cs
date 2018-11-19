using System;
using EquationSolver.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationSolver.Unit.Tests
{
    [TestClass]
    public class ErrorsTests
    {
        [TestMethod]
        public void VariableNotFoundTest()
        {
            try
            {
                EquationProject project = new EquationProject()
                {
                    Title = "Unit Project",
                };

                Equation sinEquation = new Equation()
                {
                    UseExpression = "true",
                    Expression = "notfound * 2",  /// missing closing parenthesis
                    Target = "T1"
                };

                project.Equations.Add(sinEquation);

                IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
                solver.VariableNotFoundException += delegate (object sender, EventArgs e)
                {
                    var vnfe = e as VariableNotFoundEventArgs;
                    if(vnfe != null)
                    {
                        Assert.AreEqual("notfound", vnfe.VariableName);
                    }
                    else
                        Assert.Fail();
                };

                solver.SolveEquations();

            }
            catch (Exception vnfe)
            {
                // this is failure
                Assert.Fail();
            }
        }
        [TestMethod]
        public void DivideByZeroTest()
        {
            try
            {
                EquationProject project = new EquationProject()
                {
                    Title = "Unit Project",
                };

                Equation sinEquation = new Equation()
                {
                    UseExpression = "true",
                    Expression = "10/0",  /// missing closing parenthesis
                    Target = "T1"
                };

                project.Equations.Add(sinEquation);

                IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
                solver.VariableNotFoundException += delegate (object sender, EventArgs e)
                {
                    var vnfe = e as VariableNotFoundEventArgs;
                    if (vnfe != null)
                    {
                        Assert.AreEqual("notfound", vnfe.VariableName);
                    }
                    else
                        Assert.Fail();
                };

                solver.ExceptionOccurred += delegate (object sender, EventArgs e)
                {
                    Assert.IsTrue( e is ExceptionEventArgs);
                };

                solver.SolveEquations();

            }
            catch (Exception vnfe)
            {
                // this is failure
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ValidForParenthesisTest()
        {
            try
            {
                EquationProject project = new EquationProject()
                {
                    Title = "Unit Project",
                };

                Equation sinEquation = new Equation()
                {
                    UseExpression = "true",
                    Expression = "asin(.5",  /// missing closing parenthesis
                    Target = "asin"
                };

                project.Equations.Add(sinEquation);

                IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);

                Assert.Fail();
            }
            catch (Exception)
            {
                // this is success
            }
        }

        [TestMethod]
        public void ValidForBlankExpressionTest()
        {
            try
            {
                EquationProject project = new EquationProject()
                {
                    Title = "Unit Project",
                };

                Equation sinEquation = new Equation()
                {
                    UseExpression = "true",
                    Expression = "   ",  /// missing closing parenthesis
                    Target = "asin"
                };

                project.Equations.Add(sinEquation);

                IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);

                Assert.Fail();
            }
            catch (Exception)
            {
                // this is success
            }
        }
    }
}
