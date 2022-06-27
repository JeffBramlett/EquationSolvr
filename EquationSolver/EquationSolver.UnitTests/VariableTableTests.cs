using System;
using System.Globalization;
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
                },
                Settings = new SolverSettings()
                {
                    CalculationMethod = CalculationMethods.Double
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
                        Target = "Test(row,0)"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "cos(angle)",
                        Target = "Test(row,1)"
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
            solver.VariableProvider.SetVariable("row", -1);
            solver.VariableProvider.SetVariable("angle", 30);

            Decimal sin35 = RoundDouble(Math.Sin(35));
            decimal cos35 = RoundDouble(Math.Cos(35));

            solver.SolveEquations();

            Assert.AreEqual(sin35, RoundDecimal(solver.VariableProvider.Tables["Test"].GetVariableAt(0, 0).DecimalValue), "Sin Test1 failed");
            Assert.AreEqual(cos35, RoundDecimal(solver.VariableProvider.Tables["Test"].GetVariableAt(0, 1).DecimalValue), "Cos Test1 failed");

            Assert.AreEqual(RoundDouble(Math.Sin(40)), RoundDecimal(solver.VariableProvider.Tables["Test"].GetVariableAt(1, 0).DecimalValue), "Sin Test2 failed");
            Assert.AreEqual(RoundDouble(Math.Cos(40)), RoundDecimal(solver.VariableProvider.Tables["Test"].GetVariableAt(1, 1).DecimalValue), "Cos Test2 failed");

            Assert.AreEqual(RoundDouble(Math.Sin(45)), RoundDecimal(solver.VariableProvider.Tables["Test"].GetVariableAt(2, 0).DecimalValue), "Sin Test3 failed");
            Assert.AreEqual(RoundDouble(Math.Cos(45)), RoundDecimal(solver.VariableProvider.Tables["Test"].GetVariableAt(2, 1).DecimalValue), "Cos Test3 failed");

            Assert.AreEqual(RoundDouble(Math.Sin(50)), RoundDecimal(solver.VariableProvider.Tables["Test"].GetVariableAt(3, 0).DecimalValue), "Sin Test4 failed");
            Assert.AreEqual(RoundDouble(Math.Cos(50)), RoundDecimal(solver.VariableProvider.Tables["Test"].GetVariableAt(3, 1).DecimalValue), "Cos Test4 failed");

            Assert.AreEqual(RoundDouble(Math.Sin(55)), RoundDecimal(solver.VariableProvider.Tables["Test"].GetVariableAt(4, 0).DecimalValue), "Sin Test5 failed");
            Assert.AreEqual(RoundDouble(Math.Cos(55)), RoundDecimal(solver.VariableProvider.Tables["Test"].GetVariableAt(4, 1).DecimalValue), "Cos Test5 failed");
        }


        private decimal RoundDouble(double dob)
        {
            Decimal dec = Convert.ToDecimal(dob);
            return RoundDecimal(dec);
        }

        private decimal RoundDecimal(decimal dec)
        {
            return Decimal.Round(dec, 14);
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
                        Target = "QuadTable(row,0)"
                    },
                    new Equation()
                    {
                        UseExpression = "true",
                        Expression = "quadraticPlus(a, b, c)",
                        Target = "QuadTable(row,1)"
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
            solver.VariableProvider.SetVariable("row", -1);

            solver.SolveEquations();

            Assert.AreEqual(-4, solver.VariableProvider.GetVariableInTable("QuadTable", 0, 0).DecimalValue);
            Assert.AreEqual(1, solver.VariableProvider.GetVariableInTable("QuadTable", 1, 0).DecimalValue);

            Assert.AreEqual(-2.3507810593582125M, solver.VariableProvider.GetVariableInTable("QuadTable", 0, 1).DecimalValue);
            Assert.AreEqual(0.8507810593582125M, solver.VariableProvider.GetVariableInTable("QuadTable", 1, 1).DecimalValue);

            Assert.AreEqual(-1.7583057392117916666666666667M, solver.VariableProvider.GetVariableInTable("QuadTable", 0, 2).DecimalValue);
            Assert.AreEqual(0.7583057392117916666666666667M, solver.VariableProvider.GetVariableInTable("QuadTable", 1, 2).DecimalValue);

            Assert.AreEqual(-1.44300046816469125M, solver.VariableProvider.GetVariableInTable("QuadTable", 0, 3).DecimalValue);
            Assert.AreEqual(0.69300046816469125M, solver.VariableProvider.GetVariableInTable("QuadTable", 1, 3).DecimalValue);

            Assert.AreEqual(-1.24339811320566M, solver.VariableProvider.GetVariableInTable("QuadTable", 0, 4).DecimalValue);
            Assert.AreEqual(0.64339811320566M, solver.VariableProvider.GetVariableInTable("QuadTable", 1, 4).DecimalValue);

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
                        Expression = "SubTotal * lookup(RateTable, Rate2, Rate1 is 1)",
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

            Assert.AreEqual(500M, solver.VariableProvider["Total"].DecimalValue);
        }


        [TestMethod]
        public void VariableTableBasicTest()
        {
            VariableTable table = new VariableTable("Test", 4);
            for (var i = 0; i < 4; i++)
            {
                table.MakeRow();
            }

            for (var r = 0; r < 4; r++)
            {
                for (var c = 0; c < 4; c++)
                {
                    table[r][c].SetValue(r);
                }
            }

            Assert.AreEqual(4, table.Count);
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
                table.MakeRow($"Row{i}", i, i, i, i);
            }


            decimal avgCol = table.AverageOfColumn(2);
            decimal sumCol = table.SumOfColumn(2);

            decimal avgRow = table.AverageOfRow(3);
            decimal sumRow = table.SumOfRow(3);

            Assert.AreEqual(1.5M, avgCol);
            Assert.AreEqual(6M, sumCol);

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

            table.SetColumnLabel(0, "Test");
            Assert.AreEqual("Test", table.ColumnNames[0]);
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
            Assert.AreEqual("Test", table.RowNames[1]);
        }

    }
}
