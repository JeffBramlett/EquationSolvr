using System;
using System.Collections.Generic;
using EquationSolver.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationSolver.Unit.Tests
{
    [TestClass]
    public class TriggerTests
    {
        [TestMethod]
        public void TriggerTestSuccess1()
        {
            VariableProvider variables = new VariableProvider();
            variables.SetVariable("price", 100);
            variables.SetVariable("quantity", 4);
            variables.SetVariable("subtotal", 1);
            variables.SetVariable("customerlevel", 1);
            variables.SetVariable("discount", 1);

            EquationProject project = new EquationProject()
            {
                Title = "Simple Trigger Example",
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
                Activation = "quantity > 0",
                Expression = "price * quantity",
                Target = "subtotal",
            };

            Equation calcEquation = new Equation()
            {
                Activation = "quantity > 0",
                Expression = "price * quantity",
                Target = "subtotal",
                Trigger = "customerlevel,quantity",
                MoreEquations = new List<Equation>()
                {
                    new Equation()
                    {
                        Activation = "subtotal < 500 and customerlevel < 5",
                        Expression = "90",
                        Target = "price"
                    },
                    new Equation()
                    {
                        Activation = "subtotal < 500 and customerlevel < 10 and customerlevel >= 5",
                        Expression = "80",
                        Target = "price"
                    },
                    new Equation()
                    {
                        Activation = "subtotal > 500",
                        Expression = "70",
                        Target = "price"
                    },
                    new Equation()
                    {
                        Activation = "true",
                        Expression = "price * quantity",
                        Target = "subtotal"
                    },
                }

            };

            project.Equations.Add(subTotal);
            project.Equations.Add(calcEquation);

            string asjson = Helpers.Serialize(project);

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project, variables);
            solver.SolveEquations();

            Assert.AreEqual(360, variables["subtotal"].DoubleValue);

            variables.SetVariable("customerlevel",7);
            Assert.AreEqual(320, variables["subtotal"].DoubleValue);

            variables["quantity"].SetValue(8);
            Assert.AreEqual(560, variables["subtotal"].DoubleValue);

        }
    }
}
