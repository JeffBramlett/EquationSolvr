# EquationSolvr
Solve equations expressed as strings, branch equations for custom evaluations. Equations can be mathematical, boolean/comparison, with logical ANDs and ORs. 

## Documentation and Resources

- Documentation: [Conceptual Equation Solver](http://www.bluejaysvc.com/OpenSource/ConceptualEquationSolver)
- NuGet Package: [EquationSolver](https://www.nuget.org/packages/EquationSolver/)

## Overview

The library is built with .NET Standard. It uses a forward-only parser for expressions so it is very fast, and since it is small, it is well suited to mobile applications that get equations from a service and apply values to variables in the equation.

## Calculation Rules

Calculations in expressions follow the common order of operations (PEMDAS) (see: [Order of Operations](https://study.com/academy/lesson/order-of-operations.html))

### Example

The Quadratic formula: 
```csharp
((b*-1) - sqrt(b^2 - 4*a*c))/(2*a)
```

**Note:** This library supports parentheses and nested parentheses

## Getting Started

To use the library:
1. Create/load an `EquationProject`
2. Use `EquationSolverFactory` to build and return the `IEquationSolver`
3. Solve the equations
4. Use/review variables by name

## Parts of an Equation

* **Activation** - A string expression that must evaluate to a true or false. If this Activation expression evaluates to true, then the equation and the inner equations are executed.

* **Expression** - A string that produces a value (number, boolean, or text). The value is then assigned to the variable named in Target.

* **Target** - The variable name to hold the value calculated from the Expression.

* **MoreEquations** - A collection of Equations that will be executed if this equation executes (provides a tree of equations).

## Usage Example

```csharp
EquationProject project = new EquationProject()
{
    Title = "Multiply Divide Project",
    Equations = new List<Equation>()
    {
        new Equation()
        {
            Activation = "true",
            Expression = "2 * 1",
            Target = "T1"
        }
    }
};

IEquationSolver solver = EquationSolverFactory.Instance.CreateEquationSolver(project);
solver.SolveEquations();

var myCalculatedDecimal = solver.Variables["T1"].DecimalValue;
```
