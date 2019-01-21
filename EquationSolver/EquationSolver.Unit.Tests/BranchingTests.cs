using System;
using System.Collections.Generic;
using EquationSolver.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationSolver.Unit.Tests
{
    [TestClass]
    public class BranchingTests
    {
        [TestMethod]
        public void SimpleBranchTest()
        {
            VariableProvider variables = new VariableProvider();
            variables.SetVariable("price", 100);
            variables.SetVariable("quantity", 4);
            variables.SetVariable("subtotal", 1);
            variables.SetVariable("customerlevel", 1);
            variables.SetVariable("discount", 1);


            EquationProject project = new EquationProject()
            {
                Title = "Simple Branching Example",
                Audit = new AuditInfo()
                {
                    CreatedBy = "UnitTest",
                    CreatedOn = DateTime.Now,
                    ModifiedBy = "UnitTest",
                    ModifiedOn = DateTime.Now
                }
            };

            Equation subTotal = new Equation()
            {
                UseExpression = "quantity > 0",
                Expression = "price * quantity",
                Target = "subtotal",
                MoreEquations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "subtotal < 500 and customerlevel < 5",
                        Expression = "90",
                        Target = "price"
                    },
                    new Equation()
                    {
                        UseExpression = "subtotal < 500 and customerlevel < 10 and customerlevel >= 5",
                        Expression = "80",
                        Target = "price"
                    },
                    new Equation()
                    {
                        UseExpression = "subtotal > 500",
                        Expression = "70",
                        Target = "price"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "price * quantity",
                        Target = "subtotal"
                    },
               }
            };

            project.Equations.Add(subTotal);

            string asjson = Helpers.Serialize(project);

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project, variables);
            solver.SolveEquations();

            Assert.AreEqual(360, variables["subtotal"].DoubleValue);

            variables["customerlevel"].SetValue(7);
            solver.SolveEquations();
            Assert.AreEqual(320, variables["subtotal"].DoubleValue);

            variables["quantity"].SetValue(8);
            solver.SolveEquations();
            Assert.AreEqual(560, variables["subtotal"].DoubleValue);

        }
    }
}
