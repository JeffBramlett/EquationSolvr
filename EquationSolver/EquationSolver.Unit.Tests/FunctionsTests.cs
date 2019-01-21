using System;
using System.Collections.Generic;
using EquationSolver.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationSolver.Unit.Tests
{
    [TestClass]
    public class FunctionsTests
    {
        [TestMethod]
        public void ExpTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Project",
                Audit = new AuditInfo()
                {
                    CreatedBy = "UnitTest",
                    CreatedOn = DateTime.Now,
                    ModifiedBy = "UnitTest",
                    ModifiedOn = DateTime.Now
                },
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "exp(10)",
                        Target = "T1"
                    },
                }
            };

            var expected = Convert.ToDecimal(Math.Exp(10));

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expected, solver.VariableProvider["T1"].DecimalValue);
        }

        [TestMethod]
        public void LogTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Project",
                Audit = new AuditInfo()
                {
                    CreatedBy = "UnitTest",
                    CreatedOn = DateTime.Now,
                    ModifiedBy = "UnitTest",
                    ModifiedOn = DateTime.Now
                },
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "log(10)",
                        Target = "T1"
                    },
                }
            };

            var expected = Convert.ToDecimal(Math.Log(10));

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expected, solver.VariableProvider["T1"].DecimalValue);
        }

        [TestMethod]
        public void Log10est()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Project",
                Audit = new AuditInfo()
                {
                    CreatedBy = "UnitTest",
                    CreatedOn = DateTime.Now,
                    ModifiedBy = "UnitTest",
                    ModifiedOn = DateTime.Now
                },
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "log10(10)",
                        Target = "T1"
                    },
                }
            };

            var expected = Convert.ToDecimal(Math.Log10(10));

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expected, solver.VariableProvider["T1"].DecimalValue);
        }

        [TestMethod]
        public void SqrtTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Project",
                Audit = new AuditInfo()
                {
                    CreatedBy = "UnitTest",
                    CreatedOn = DateTime.Now,
                    ModifiedBy = "UnitTest",
                    ModifiedOn = DateTime.Now
                },
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "sqrt(10)",
                        Target = "T1"
                    },
                }
            };

            var expected = Convert.ToDecimal(Math.Sqrt(10));

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expected, solver.VariableProvider["T1"].DecimalValue);
        }

        [TestMethod]
        public void FloorTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Project",
                Audit = new AuditInfo()
                {
                    CreatedBy = "UnitTest",
                    CreatedOn = DateTime.Now,
                    ModifiedBy = "UnitTest",
                    ModifiedOn = DateTime.Now
                },
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "floor(10.56789)",
                        Target = "T1"
                    },
                }
            };

            var expected = Convert.ToDecimal(Math.Floor(10.56789));

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expected, solver.VariableProvider["T1"].DecimalValue);
        }

        [TestMethod]
        public void CeilTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Project",
                Audit = new AuditInfo()
                {
                    CreatedBy = "UnitTest",
                    CreatedOn = DateTime.Now,
                    ModifiedBy = "UnitTest",
                    ModifiedOn = DateTime.Now
                },
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "ceil(10.56789)",
                        Target = "T1"
                    },
                }
            };

            var expected = Convert.ToDecimal(Math.Ceiling(10.56789));

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expected, solver.VariableProvider["T1"].DecimalValue);
        }

        [TestMethod]
        public void AbsTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Project",
                Audit = new AuditInfo()
                {
                    CreatedBy = "UnitTest",
                    CreatedOn = DateTime.Now,
                    ModifiedBy = "UnitTest",
                    ModifiedOn = DateTime.Now
                },
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "abs(-10.56789)",
                        Target = "T1"
                    },
                }
            };

            var expected = Convert.ToDecimal(Math.Abs(-10.56789));

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expected, solver.VariableProvider["T1"].DecimalValue);
        }

        [TestMethod]
        public void DegTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Project",
                Audit = new AuditInfo()
                {
                    CreatedBy = "UnitTest",
                    CreatedOn = DateTime.Now,
                    ModifiedBy = "UnitTest",
                    ModifiedOn = DateTime.Now
                },
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "deg(1.5)",
                        Target = "T1"
                    },
                }
            };

            decimal expected = 85.9436692696235m;

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expected, solver.VariableProvider["T1"].DecimalValue);
        }

        [TestMethod]
        public void RadTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Project",
                Audit = new AuditInfo()
                {
                    CreatedBy = "UnitTest",
                    CreatedOn = DateTime.Now,
                    ModifiedBy = "UnitTest",
                    ModifiedOn = DateTime.Now
                },
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "rad(90)",
                        Target = "T1"
                    },
                }
            };

            decimal expected = 1.5707963267949m;

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expected, solver.VariableProvider["T1"].DecimalValue);
        }

        [TestMethod]
        public void MinTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Project",
                Audit = new AuditInfo()
                {
                    CreatedBy = "UnitTest",
                    CreatedOn = DateTime.Now,
                    ModifiedBy = "UnitTest",
                    ModifiedOn = DateTime.Now
                },
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "min(0, 10, 7, 8, 3)",
                        Target = "T1"
                    },
                }
            };

            decimal expected = 0;

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expected, solver.VariableProvider["T1"].DecimalValue);
        }

        [TestMethod]
        public void MaxTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Project",
                Audit = new AuditInfo()
                {
                    CreatedBy = "UnitTest",
                    CreatedOn = DateTime.Now,
                    ModifiedBy = "UnitTest",
                    ModifiedOn = DateTime.Now
                },
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "max(0, 10, 7, 8, 9)",
                        Target = "T1"
                    },
                }
            };

            decimal expected = 10;

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(expected, solver.VariableProvider["T1"].DecimalValue);
        }

        [TestMethod]
        public void RoundingTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Project",
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "round(123.456789, 2)",
                        Target = "T1"
                    },
                }
            };

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(123.46, solver.VariableProvider["T1"].DoubleValue);
        }

        [TestMethod]
        public void SumTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Project",
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "(sum(1, 2, 3, 4)*4)/4",
                        Target = "T1"
                    },
                }
            };

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(10, solver.VariableProvider["T1"].DoubleValue);
        }

        [TestMethod]
        public void AvgTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Project",
                Equations = new List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "(avg(1, 2, 3, 4)*4)/4",
                        Target = "T1"
                    },
                }
            };

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.SolveEquations();

            Assert.AreEqual(2.5, solver.VariableProvider["T1"].DoubleValue);
        }
    }
}
