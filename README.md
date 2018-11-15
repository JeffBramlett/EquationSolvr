# EquationSolvr
Solve Equations Expressed as strings, branch equations for custom evaluations.  Equations can be mathmatical, boolean/comparison, with logical ANDs and ORs.

To use:

1. Create/load and EquationProject
2. Use EquationSolverFactory to build and return the IEquationSolver
3. Use/review Variables


Example:
'''
EquationProject project = new EquationProject()
{
    Title = "Mulitply Divide Project",
    Equations = new List<Equation>()
    {
        new Equation()
        {
            UseExpression = "true",
            Expression = "2 * 1",
            Target = "T1"
        },
        new Equation()
        {
            UseExpression = "true",
            Expression = "4 / 2",
            Target = "T2"
        },
        new Equation()
        {
            UseExpression = "true",
            Expression = "(2 * 2) - (4 / 2)",
            Target = "T3"
        },
        new Equation()
        {
            UseExpression = "true",
            Expression = "(2 * 4) / (2 * 2)",
            Target = "T4"
        }
    }
};

IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
solver.SolveEquations();

var myCalculatedDecimal = solver.Variables["T1"].DecimalValue
'''