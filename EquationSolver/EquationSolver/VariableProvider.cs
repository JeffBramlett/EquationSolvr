using EquationSolver.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    public class VariableProvider
    {
        #region Fields
        private Dictionary<string, Variable> _variables;
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
        #endregion

        #region Ctors and Dtors
        public VariableProvider()
        {

        }
        #endregion

        #region Publics
 
        public void AddVariable(Variable variable)
        {
            if (Variables.ContainsKey(variable.Name))
            {
                Variables[variable.Name] = variable;
            }
            else
            {
                Variables.Add(variable.Name, variable);
            }
        }

        public void SetVariable(string name, object value)
        {
            if(Variables.ContainsKey(name))
            {
                Variables[name].SetValue(value);
            }
            else
            {
                Variable variable = new Variable();
                variable.SetName(name);
                variable.SetValue(value);
                Variables.Add(name, variable);
            }
        }

        public void RemoveVariable(string name)
        {
            if (Variables.ContainsKey(name))
            {
                Variables.Remove(name);
            }

        }

        public IEnumerable<Variable> DumpVariables()
        {
            var list = Variables.Values.ToList();
            list.Sort();
            return list;
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
