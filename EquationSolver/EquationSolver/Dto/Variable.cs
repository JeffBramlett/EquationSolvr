using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver.Dto
{
    public partial class Variable: IComparable<Variable>, IComparer<Variable>
    {
        #region private Fields
        private VariableTypes _variableType = VariableTypes.NONE;
        #endregion

        #region Properties

        public VariableTypes VariableType
        {
            get { return _variableType; }
            private set { _variableType = value; }
        }


        public bool BoolValue { get; private set; }

        public short ShortValue
        {
            get { return Convert.ToInt16(DecimalValue); }
        }

        public int IntValue
        {
            get { return Convert.ToInt32(DecimalValue); }
        }

        public long LongValue
        {
            get { return Convert.ToInt64(DecimalValue); }
        }

        public decimal DecimalValue { get; private set; }

        public double DoubleValue { get; private set; }
        #endregion

        #region Ctors and Dtors
        public Variable()
        {
        }
        #endregion

        #region Publics
        public Variable SetName(string variableName)
        {
            Name = variableName;
            return this;
        }

        public void SetValue(object value)
        {
            StringValue = value.ToString();

            if (value is bool)
            {
                VariableType = VariableTypes.BOOL;
                BoolValue = StringValue.ToLower() == "true";
                DecimalValue = BoolValue ? 1 : 0;
                DoubleValue = BoolValue ? 1 : 0;
            }
            else if (value is short || value is int || value is long)
            {
                VariableType = VariableTypes.NUMBER;
                DecimalValue = Convert.ToDecimal(value);
                DoubleValue = Convert.ToDouble(value);
                BoolValue = DecimalValue >= 0;
            }
            else if (value is decimal)
            {
                VariableType = VariableTypes.NUMBER;
                DecimalValue = Convert.ToDecimal(value);
                DoubleValue = Convert.ToDouble(value);
                BoolValue = DecimalValue >= 0;
            }
            else if (value is double)
            {
                VariableType = VariableTypes.NUMBER;
                DecimalValue = Convert.ToDecimal(value);
                DoubleValue = Convert.ToDouble(value);
                BoolValue = DecimalValue >= 0;
            }

            ReadString();
        }


        public int Compare(Variable x, Variable y)
        {
            return x.Name.CompareTo(y.Name);
        }

        public int CompareTo(Variable other)
        {
            return Name.CompareTo(other.Name);
        }
        #endregion

        #region Privates
        private bool ReadString()
        {
            if(StringValue.ToLower() == "true" || StringValue.ToLower() == "false")
            {
                BoolValue = StringValue.ToLower() == "true";
                return true;
            }
            else if (short.TryParse(StringValue, out short s))
            {
                VariableType = VariableTypes.NUMBER;
                DecimalValue = Convert.ToDecimal(s);
                DoubleValue = Convert.ToDouble(s);
                BoolValue = s > 0;
                return true;
            }
            else if (int.TryParse(StringValue, out int i))
            {
                VariableType = VariableTypes.NUMBER;
                DecimalValue = Convert.ToDecimal(i);
                DoubleValue = Convert.ToDouble(i);
                BoolValue = i > 0;
                return true;
            }
            else if (long.TryParse(StringValue, out long l))
            {
                VariableType = VariableTypes.NUMBER;
                DecimalValue = Convert.ToDecimal(l);
                DoubleValue = Convert.ToDouble(l);
                BoolValue = l > 0;
                return true;
            }
            else if (decimal.TryParse(StringValue, out decimal dc))
            {
                VariableType = VariableTypes.NUMBER;
                DecimalValue = dc;
                DoubleValue = Convert.ToDouble(dc);
                BoolValue = dc > 0;
                return true;
            }
            else if (double.TryParse(StringValue, out double db))
            {
                VariableType = VariableTypes.NUMBER;
                DecimalValue = Convert.ToDecimal(db);
                DoubleValue = db;
                BoolValue = db > 0;
                return true;
            }
            else
            {
                VariableType = VariableTypes.TEXT;
                DecimalValue = 0;
                DoubleValue = 0;
                BoolValue = true;
                return true;
            }
        }
        #endregion
    }
}
