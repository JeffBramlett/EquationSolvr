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
                },

                Triggers = new List<Trigger>()
                {
                    new Trigger()
                    {
                        UseExpression = "subtotal < 500 and customerlevel < 5",
                        VariableTrigger = "subtotal",
                        Target = "subtotal",
                        Expression = "90 * quantity"
                    },
                    new Trigger()
                    {
                        UseExpression = "subtotal < 500 and customerlevel < 10 and customerlevel >= 5",
                        VariableTrigger = "subtotal",
                        Target = "subtotal",
                        Expression = "80 * quantity"
                    },
                    new Trigger()
                    {
                        UseExpression = "subtotal > 500",
                        VariableTrigger = "subtotal",
                        Target = "subtotal",
                        Expression = "70 * quantity"
                    }
                }
            };

            Equation defaultPrice = new Equation()
            {
                UseExpression = "quantity > 0",
                Expression = "price * quantity",
                Target = "subtotal",
                Name = "Default",
            };
            project.Equations.Add(defaultPrice);


            string asjson = Helpers.Serialize(project);

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project, variables);
            solver.SolveEquations();

            double expected1 = 360;

            Assert.AreEqual(expected1, variables["subtotal"].DoubleValue);

            variables["customerlevel"].SetValue(7);
            solver.SolveEquations();
            Assert.AreEqual(320, variables["subtotal"].DoubleValue);

            variables["quantity"].SetValue(8);
            solver.SolveEquations();
            Assert.AreEqual(560, variables["subtotal"].DoubleValue);

        }
    }
}
