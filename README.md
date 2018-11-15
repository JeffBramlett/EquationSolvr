# EquationSolvr
Solve Equations Expressed as strings, branch equations for custom evaluations.  Equations can be mathmatical, boolean/comparison, with logical ANDs and ORs. 

The libray is built with .Net Standard.  It uses a forward only parser for expressions so it is very fast and since it is small it is well suited to mobile applications that get the equations from a service the application applying values to variables in the equation.

Calculation in Expressions follow the common order of operations (PEMDAS) (see: https://study.com/academy/lesson/order-of-operations.html)

Note:  This library supports parentheses and nested parentheses

To use:
1. Create/load an EquationProject
2. Use EquationSolverFactory to build and return the IEquationSolver
3. Solve the equations
4. Use/review Variables by name

## Parts of an Equation
*UseExpression* - a string expression that must evaluate to a true or false.  If this UseExpression evaluates to true then the equation and the inner equations are executed.

*Expression* - a string that produces a value (number, boolean, or text), the value is then assigned to the Variable named in Target.

*Target* - the variable name to hold the value calculated from the Expression.

*MoreEquations* - a collection of Equations that will be executed if this equation executes. (provides a tree of equations)

Example:

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
            }
        }
    };

    IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
    solver.SolveEquations();

    var myCalculatedDecimal = solver.Variables["T1"].DecimalValue
