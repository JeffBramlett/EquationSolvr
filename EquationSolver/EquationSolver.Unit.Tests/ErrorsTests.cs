using System;
using EquationSolver.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationSolver.Unit.Tests
{
    [TestClass]
    public class ErrorsTests
    {
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                // this is success
            }
        }
    }
}
