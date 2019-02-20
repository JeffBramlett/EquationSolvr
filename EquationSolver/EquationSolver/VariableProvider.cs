using EquationSolver.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    /// <summary>
    /// Data management  class for variables and tables
    /// </summary>
    public class VariableProvider
    {
        #region Fields
        private Dictionary<string, Variable> _variables;
        private Dictionary<string, VariableTable> _tables;
        #endregion

        #region Events
        /// <summary>
        /// Delegate for notifying that a variable has changed.
        /// </summary>
        /// <param name="variableName"></param>
        public delegate void VarableValueChangedDelegate(string variableName);

        /// <summary>
        /// Event to notify delegates that a variable has changed.
        /// </summary>
        public event VarableValueChangedDelegate VariableValueChanged;
        #endregion

        #region Properties
        private Dictionary<string, Variable> Variables
        {
            get
            {
                _variables = _variables ?? new Dictionary<string, Variable>();
                return _variables;
            }
        }

        /// <summary>
        /// Table dictionary of variables
        /// </summary>
        public Dictionary<string, VariableTable> Tables
        {
            get
            {
                _tables = _tables ?? new Dictionary<string, VariableTable>();
                return _tables;
            }
        }
        #endregion

        #region Ctors and Dtors
        /// <summary>
        /// Default Ctor
        /// </summary>
        public VariableProvider()
        {

        }
        #endregion

        #region Publics for Variables
        /// <summary>
        /// Add a Variable
        /// </summary>
        /// <param name="variable">the variable to add</param> 
        public void AddVariable(Variable variable)
        {
            if (Variables.ContainsKey(variable.Name))
            {
                Variables[variable.Name] = variable;
                RaiseVariableChanged(variable.Name);
            }
            else
            {
                Variables.Add(variable.Name, variable);
            }
        }

        /// <summary>
        /// Set (add/update) variable by name and value
        /// </summary>
        /// <param name="name">the variable name</param>
        /// <param name="value">the variable value</param>
        public void SetVariable(string name, object value)
        {
            if (Variables.ContainsKey(name))
            {
                Variables[name].SetValue(value);
                RaiseVariableChanged(name);
            }
            else
            {
                Variable variable = new Variable();
                variable.SetName(name);
                variable.SetValue(value);
                Variables.Add(name, variable);
            }
        }

        /// <summary>
        /// Remove a Variable by name
        /// </summary>
        /// <param name="name">the name of the variable to remove</param>
        public void RemoveVariable(string name)
        {
            if (Variables.ContainsKey(name))
            {
                Variables.Remove(name);
            }

        }

        #endregion

        #region Public Dump of data methods
        /// <summary>
        /// Dump the Variables to an enumeration
        /// </summary>
        /// <returns>Enumeration of the current Variables</returns>
        public IEnumerable<Variable> DumpVariables()
        {
            var list = Variables.Values.ToList();
            list.Sort();
            return list;
        }

        /// <summary>
        /// Dump a table to variable Enumeration by name
        /// </summary>
        /// <param name="tableName">the name of the table to dump to list</param>
        /// <returns>Enumeration of Variables in the Table</returns>
        public IEnumerable<Variable> DumpTable(string tableName)
        {
            var list = new List<Variable>();

            if (Tables.ContainsKey(tableName))
            {
                list.AddRange(Tables[tableName].EnumerateVariables());
            }

            return list;
        }

        #endregion

        #region Public Table Methods
        /// <summary>
        /// Define a new Table to begin holding values
        /// </summary>
        /// <param name="tableName">the name of the Table</param>
        /// <param name="columns">the number of columns the table contains</param>
        public void StartTable(string tableName, int columns)
        {
            if (!Tables.ContainsKey(tableName))
            {
                VariableTable table = new VariableTable(tableName, columns);
                Tables.Add(tableName, table);
            }
        }

        public bool HasTable(string tableName)
        {
            return Tables.ContainsKey(tableName);
        }

        public void SetColumnLabel(string tableName, int ndx, string label)
        {
            if (Tables.ContainsKey(tableName))
            {
                Tables[tableName].SetColumnLabel(ndx, label);
            }
        }

        public void SetRowLabel(string tableName, int ndx, string label)
        {
            if (Tables.ContainsKey(tableName))
            {
                Tables[tableName].SetRowLabel(ndx, label);
            }
        }



        public void MakeRowInTable(string tableName, string rowName = "")
        {
            if (Tables.ContainsKey(tableName))
            {
                Tables[tableName].MakeRow(rowName);
            }
        }

        public Variable GetVariableInTable(string tableName, int column, int row)
        {
            return Tables[tableName].GetVariableAt(row, column);
        }

        public void SetValueInTable(string tableName, int column, int row, object value)
        {
            if (Tables.ContainsKey(tableName))
            {
                Tables[tableName].SetVariableValueAt(row, column, value);
            }
        }
        #endregion

        #region Private Event Raising
        private void RaiseVariableChanged(string variableName)
        {
            VariableValueChanged?.Invoke(variableName);
        }
        #endregion

        #region Accessor
        public Variable this[string name]
        {
            get
            {
                if (Variables.ContainsKey(name))
                {
                    return Variables[name];
                }
                else
                    return null;
            }
        }
        #endregion
    }
}
