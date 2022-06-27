using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver.Unit.Tests
{
    [TestClass]
    public class MultiStepTests
    {
        [TestMethod]
        public void SolveSimple2Step()
        {
            string eq1 = "2*x+3=5*x+8";
            string eq2 = "2*(3x+4) = 2*x + 12";

            var solver = EquationSolverFactory.SolveForX(eq1);
        }
    }
}
