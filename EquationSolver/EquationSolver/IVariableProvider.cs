using System.Collections.Generic;
using EquationSolver.Dto;

namespace EquationSolver
{
    public interface IVariableProvider
    {
        Variable this[string name] { get; }

        Dictionary<string, VariableTable> Tables { get; }

        event VariableProvider.VarableValueChangedDelegate VariableValueChanged;

        void AddVariable(Variable variable);
        IEnumerable<Variable> DumpTable(string tableName);
        IEnumerable<Variable> DumpVariables();
        Variable GetVariableInTable(string tableName, int column, int row);
        bool HasTable(string tableName);
        void MakeRowInTable(string tableName, string rowName = "");
        void RemoveVariable(string name);
        void SetColumnLabel(string tableName, int ndx, string label);
        void SetRowLabel(string tableName, int ndx, string label);
        void SetValueInTable(string tableName, int column, int row, object value);
        void SetVariable(string name, object value);
        void StartTable(string tableName, int columns);
        void RaiseVariableChanged(string variableName);
    }
}