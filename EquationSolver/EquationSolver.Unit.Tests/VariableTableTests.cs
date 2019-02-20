using System;
using EquationSolver.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationSolver.Unit.Tests
{
    [TestClass]
    public class VariableTableTests
    {
        [TestMethod]
        public void VariableTableFactoryTest()
        {
            EquationProject project = new EquationProject()
            {
                Title = "Sin and Cosine table",
                Audit = new AuditInfo()
                {
                    CreatedBy = "UnitTest",
                    CreatedOn = DateTime.Now,
                    ModifiedBy = "UnitTest",
                    ModifiedOn = DateTime.Now
                }
            };

            Equation plotSinAndCos = new Equation()
            {
                UseExpression = "true",
                Expression = "row + 1",
                Iterate = 5,
                Target = "row",
                MoreEquations = new System.Collections.Generic.List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "angle + 5",
                        Target = "angle"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "sin(angle)",
                        Target = "Test(row,1)"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "cos(angle)",
                        Target = "Test(row,2)"
                    }
                }
            };


            Table calcTable = new Table()
            {
                Name = "Test",
                RowHeader = new Row()
                {
                    Columns = new System.Collections.Generic.List<string>()
                    {
                        "Sin",
                        "Cos"
                    }
                },
                Rows = new System.Collections.Generic.List<Row>()
                {
                    new Row(),
                    new Row(),
                    new Row(),
                    new Row(),
                    new Row(),
                }

            };

            project.Equations.Add(plotSinAndCos);
            project.Tables.Add(calcTable);

            string asjson = Helpers.Serialize(project);

            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
            solver.VariableProvider.SetVariable("row", 0);
            solver.VariableProvider.SetVariable("angle", 30);

            solver.SolveEquations();

            Assert.AreEqual(Math.Round(Math.Sin(35), 15), solver.VariableProvider.Tables["Test"].GetVariableAt(1, 1).DoubleValue);
            Assert.AreEqual(Math.Round(Math.Cos(35), 15), solver.VariableProvider.Tables["Test"].GetVariableAt(1, 2).DoubleValue);

            Assert.AreEqual(Math.Round(Math.Sin(40), 15), solver.VariableProvider.Tables["Test"].GetVariableAt(2, 1).DoubleValue);
            Assert.AreEqual(Math.Round(Math.Cos(40), 15), solver.VariableProvider.Tables["Test"].GetVariableAt(2, 2).DoubleValue);

            Assert.AreEqual(Math.Round(Math.Sin(45), 15), solver.VariableProvider.Tables["Test"].GetVariableAt(3, 1).DoubleValue);
            Assert.AreEqual(Math.Round(Math.Cos(45), 15), solver.VariableProvider.Tables["Test"].GetVariableAt(3, 2).DoubleValue);

            Assert.AreEqual(Math.Round(Math.Sin(50), 15), solver.VariableProvider.Tables["Test"].GetVariableAt(4, 1).DoubleValue);
            Assert.AreEqual(Math.Round(Math.Cos(50), 15), solver.VariableProvider.Tables["Test"].GetVariableAt(4, 2).DoubleValue);

            Assert.AreEqual(Math.Round(Math.Sin(55), 15), solver.VariableProvider.Tables["Test"].GetVariableAt(5, 1).DoubleValue);
            //Assert.AreEqual(Math.Round(Math.Cos(55), 15), solver.VariableProvider.Tables["Test"].GetVariableAt(5, 2).DoubleValue);

        }

        [TestMethod]
        public void PlotQuadraticsTest()
        {
            // Define functions
            Function quadraticMinusFunction = new Function()
            {
                Name = "quadraticMinus",
                Description = "Quadratic (-) formula function",
                Expression = "((b * -1) - sqrt(b ^ 2 - 4 * a * c)) / (2 * a)",
                Arguments = new System.Collections.Generic.List<Argument>()
                {
                    new Argument()
                    {
                        Name = "a",
                        Ordinal = 1,
                        Default ="1"
                    },
                    new Argument()
                    {
                        Name = "b",
                        Ordinal = 2,
                        Default = "3"
                    },
                    new Argument()
                    {
                        Name = "c",
                        Ordinal = 3,
                        Default = "-4"
                    }
                }
            };

            Function quadraticPlusFunction = new Function()
            {
                Name = "quadraticPlus",
                Description = "Quadratic (+) formula function",
                Expression = "((b * -1) + sqrt(b ^ 2 - 4 * a * c)) / (2 * a)",
                Arguments = new System.Collections.Generic.List<Argument>()
                {
                    new Argument()
                    {
                        Name = "a",
                        Ordinal = 1,
                        Default ="1"
                    },
                    new Argument()
                    {
                        Name = "c",
                        Ordinal = 3,
                        Default = "-4"
                    },
                    new Argument()
                    {
                        Name = "b",
                        Ordinal = 2,
                        Default = "3"
                    }
                }
            };

            // Define Project
            EquationProject quadraticProject = new EquationProject()
            {
                Title = "Quadratic Plotting to Table",
                Functions = new System.Collections.Generic.List<Function>()
                {
                    quadraticMinusFunction,
                    quadraticPlusFunction
                }
            };

            // Define table for results
            Table quadTable = new Table()
            {
                Name = "QuadTable",
                RowHeader = new Row()
                {
                    Columns = new System.Collections.Generic.List<string>()
                    {
                        "X-",
                        "X+"
                    }
                },
                Rows = new System.Collections.Generic.List<Row>()
                {
                    new Row()
                    {
                        Label = "1"
                    },
                    new Row()
                    {
                        Label = "2"
                    },
                    new Row()
                    {
                        Label = "3"
                    },
                    new Row()
                    {
                        Label = "4"
                    },
                    new Row()
                    {
                        Label = "5"
                    }
                }
            };

            var iterateEquation = new Equation()
            {
                UseExpression = "true",
                Expression = "row + 1",
                Iterate = 5,
                Target = "row",
                MoreEquations = new System.Collections.Generic.List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "a + 1",
                        Target = "a"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "quadraticMinus(a, b, c)",
                        Target = "QuadTable(row,1)"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "quadraticPlus(a, b, c)",
                        Target = "QuadTable(row,2)"
                    }
                }
            };

            // Add to the project
            quadraticProject.Tables.Add(quadTable);
            quadraticProject.Equations.Add(iterateEquation);

            string asjson = Helpers.Serialize(quadraticProject);

            // Get the solver for calculations
            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(quadraticProject);

            solver.VariableProvider.SetVariable("a", 0);
            solver.VariableProvider.SetVariable("b", 3);
            solver.VariableProvider.SetVariable("c", -4);
            solver.VariableProvider.SetVariable("row", 0);

            solver.SolveEquations();

            Assert.AreEqual(-4, solver.VariableProvider.GetVariableInTable("QuadTable", 1, 1).DecimalValue);
            Assert.AreEqual(1, solver.VariableProvider.GetVariableInTable("QuadTable", 2, 1).DecimalValue);

            Assert.AreEqual(-2.3507810593582125M, solver.VariableProvider.GetVariableInTable("QuadTable", 1, 2).DecimalValue);
            Assert.AreEqual(0.8507810593582125M, solver.VariableProvider.GetVariableInTable("QuadTable", 2, 2).DecimalValue);

            Assert.AreEqual(-1.7583057392117916666666666667M, solver.VariableProvider.GetVariableInTable("QuadTable", 1, 3).DecimalValue);
            Assert.AreEqual(0.7583057392117916666666666667M, solver.VariableProvider.GetVariableInTable("QuadTable", 2, 3).DecimalValue);

            Assert.AreEqual(-1.44300046816469125M, solver.VariableProvider.GetVariableInTable("QuadTable", 1, 4).DecimalValue);
            Assert.AreEqual(0.69300046816469125M, solver.VariableProvider.GetVariableInTable("QuadTable", 2, 4).DecimalValue);

            Assert.AreEqual(-1.24339811320566M, solver.VariableProvider.GetVariableInTable("QuadTable", 1, 5).DecimalValue);
            Assert.AreEqual(0.64339811320566M, solver.VariableProvider.GetVariableInTable("QuadTable", 2, 5).DecimalValue);

        }

        [TestMethod]
        public void TableValueAccessTest()
        {
            // Add functions to Project
            EquationProject project = new EquationProject()
            {
                Title = "Table Access as Lookup",
            };

            // Define table for results
            Table rateTable = new Table()
            {
                Name = "RateTable",
                RowHeader = new Row()
                {
                    Columns = new System.Collections.Generic.List<string>()
                    {
                        "Rate1",
                        "Rate2"
                    }
                },
                Rows = new System.Collections.Generic.List<Row>()
                {
                    new Row()
                    {
                        Label = "Qualifier1",
                        Columns = new System.Collections.Generic.List<string>()
                        {
                            ".25",
                            ".35"
                        }
                    },
                    new Row()
                    {
                        Label = "Qualifier2",
                        Columns = new System.Collections.Generic.List<string>()
                        {
                            ".15",
                            ".25"
                        }
                    },
                    new Row()
                    {
                        Label = "Qualifier3",
                        Columns = new System.Collections.Generic.List<string>()
                        {
                            ".05",
                            ".15"
                        }
                    },
                    new Row()
                    {
                        Label = "Qualifier4",
                        Columns = new System.Collections.Generic.List<string>()
                        {
                            ".01",
                            ".02"
                        }
                    },
                    new Row()
                    {
                        Label = "Qualifier5",
                        Columns = new System.Collections.Generic.List<string>()
                        {
                            ".005",
                            ".015"
                        }
                    }
                }
            };

            // Define Equations
            var quoteEquation = new Equation()
            {
                UseExpression = "true",
                Expression = "Quantity * 100",
                Target = "SubTotal",
                MoreEquations = new System.Collections.Generic.List<Equation>()
                {
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "SubTotal * RateTable(2, 2)",
                        Target = "Tax",

                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "SubTotal + Tax",
                        Target = "Total",
                    }
                }
            };

            // Add to the project
            project.Tables.Add(rateTable);
            project.Equations.Add(quoteEquation);

            string asjson = Helpers.Serialize(project);

            // Get the solver for calculations
            IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);

            solver.VariableProvider.SetVariable("Quantity", 5);

            solver.SolveEquations();

            Assert.AreEqual(625M, solver.VariableProvider["Total"].DecimalValue);
        }


        [TestMethod]
        public void VariableTableBasicTest()
        {
            VariableTable table = new VariableTable("Test", 4);
            for (var i = 0; i < 4; i++)
            {
                table.MakeRow();
            }

            for (var r = 1; r <= 4; r++)
            {
                for (var c = 1; c <= 4; c++)
                {
                    table[r][c].SetValue(r);
                }
            }

            Assert.AreEqual(5, table.Count);
        }

        [TestMethod]
        public void VariableTableInvalidColumnIndexOnGetTest()
        {
            VariableTable table = new VariableTable("Test", 4);
            for (var i = 0; i < 4; i++)
            {
                table.MakeRow();
            }
            try
            {
                table.GetVariableAt(3, 100);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException ex)
            {
                // Expected to happen
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void VariableTableInvalidColumnIndexOnSetTest()
        {
            VariableTable table = new VariableTable("Test", 4);
            for (var i = 0; i < 4; i++)
            {
                table.MakeRow();
            }
            try
            {
                table[3].SetValue("not happening", 100);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException ex)
            {
                // Expected to happen
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void VariableTableInvalidRowIndexTest()
        {
            VariableTable table = new VariableTable("Test", 4);
            for (var i = 0; i < 4; i++)
            {
                table.MakeRow();
            }
            try
            {
                var notHappening = table[100];
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Expected to happen
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void VariableTableSumAndAverageTest()
        {
            VariableTable table = new VariableTable("Test", 4);
            for (var i = 0; i < 4; i++)
            {
                table.MakeRow();
            }

            for (var r = 1; r <= 4; r++)
            {
                for (var c = 1; c <= 4; c++)
                {
                    table[r][c].SetValue(r);
                }
            }

            decimal avgCol = table.AverageOfColumn(2);
            decimal sumCol = table.SumOfColumn(2);

            decimal avgRow = table.AverageOfRow(3);
            decimal sumRow = table.SumOfRow(3);

            Assert.AreEqual(2.5M, avgCol);
            Assert.AreEqual(10M, sumCol);

            Assert.AreEqual(3M, avgRow);
            Assert.AreEqual(12M, sumRow);
        }

        [TestMethod]
        public void ColumnLabelTest()
        {
            VariableTable table = new VariableTable("Test", 4);
            for (var i = 0; i < 4; i++)
            {
                table.MakeRow();
            }

            table.SetColumnLabel(1, "Test");
            Assert.AreEqual("Test", table[0][1].StringValue);
        }

        [TestMethod]
        public void RowLabelTest()
        {
            VariableTable table = new VariableTable("Test", 4);
            for (var i = 0; i < 4; i++)
            {
                table.MakeRow();
            }

            table.SetRowLabel(1, "Test");
            Assert.AreEqual("Test", table[1][0].StringValue);
        }

    }
}
