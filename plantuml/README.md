# EquationSolver PlantUML Diagrams

This directory contains PlantUML diagrams that document the EquationSolver project architecture and functionality.

## Available Diagrams

1. **Class Diagram** (`class_diagram.puml`): Shows the main classes, interfaces, and their relationships in the EquationSolver library.

2. **Sequence Diagram** (`sequence_diagram.puml`): Illustrates the sequence of operations when creating and using an EquationSolver.

3. **Component Diagram** (`component_diagram.puml`): Provides a high-level view of the main components and their interactions.

4. **Use Case Diagram** (`use_case_diagram.puml`): Shows the main use cases for the EquationSolver library.

5. **Activity Diagram** (`activity_diagram.puml`): Illustrates the equation solving process flow.

6. **State Diagram** (`state_diagram.puml`): Shows the lifecycle states of an equation.

7. **Object Diagram** (`object_diagram.puml`): Provides an example of an equation project for solving a quadratic equation.

8. **Package Diagram** (`package_diagram.puml`): Shows the organization of packages in the system.

9. **Entity-Relationship Diagram** (`entity_relationship_diagram.puml`): Focuses on the data model aspects of the system.

## How to View These Diagrams

You can view these PlantUML diagrams using:

1. **Online PlantUML Server**: Upload the .puml files to http://www.plantuml.com/plantuml/

2. **IDE Plugins**: Many IDEs have PlantUML plugins:
   - Visual Studio Code: PlantUML extension
   - JetBrains IDEs: PlantUML integration plugin
   - Eclipse: PlantUML plugin

3. **Command Line**: Use the PlantUML JAR to generate images:
   ```
   java -jar plantuml.jar diagram.puml
   ```

## Diagram Descriptions

### Class Diagram
The class diagram shows the main classes and interfaces in the EquationSolver library, including their properties, methods, and relationships. It provides a structural view of the system.

### Sequence Diagram
The sequence diagram illustrates the interactions between different components when creating an EquationSolver and solving equations. It shows the flow of method calls and the order of operations.

### Component Diagram
The component diagram provides a high-level view of the main components in the EquationSolver library and how they interact with each other. It shows the dependencies between components.

### Use Case Diagram
The use case diagram shows the main functionality provided by the EquationSolver library from a user's perspective. It illustrates what a client application can do with the library.

### Activity Diagram
The activity diagram shows the flow of activities when solving equations. It illustrates the decision points and the sequence of operations.

### State Diagram
The state diagram shows the different states an equation can be in during its lifecycle, from creation to execution.

### Object Diagram
The object diagram provides a concrete example of an equation project for solving a quadratic equation. It shows the objects and their relationships in a specific scenario.

### Package Diagram
The package diagram shows the organization of packages in the system, illustrating how the code is structured into logical groups and the dependencies between these groups.

### Entity-Relationship Diagram
The entity-relationship diagram focuses on the data model aspects of the system, showing the main entities (like EquationProject, Equation, Variable, etc.) and their relationships.