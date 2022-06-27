using System;
using EquationSolver.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationSolver.Unit.Tests
{
    [TestClass]
    public class StaticExpressionTests
    {
        [TestMethod]
        public void SimpleExpressionTest()
        {
            var value = EquationSolverFactory.SolveExpression("100*2");

            Assert.AreEqual(200, value.IntValue);
        }

        [TestMethod]
        public void ComparisonExpressionTest()
        {
            var value = EquationSolverFactory.SolveExpression("(100*2) > 1");

            Assert.IsTrue(value.BoolValue);
        }

        [TestMethod]
        public void FunctionExpressionTest()
        {
            var value = EquationSolverFactory.SolveExpression("fac(6)");

            Assert.AreEqual(720, value.IntValue);
        }

        [TestMethod]
        public void NestedExpressionTest()
        {
            var value = EquationSolverFactory.SolveExpression("(100*2) /(2 * 4)");

            Assert.AreEqual(25, value.IntValue);
        }

        [TestMethod]
        public void VariableTest()
        {
            VariableProvider variables = new VariableProvider();
            Variable variable1 = new Variable();
            Variable variable2 = new Variable();

            variable1.SetName("V1").SetValue(50);
            variable2.SetName("V2").SetValue(2);

            variables.AddVariable(variable1);
            variables.AddVariable(variable2);

            var value = EquationSolverFactory.SolveExpression("V1 * V2", variables);

            Assert.AreEqual(100, value.IntValue);
        }
    }
}
