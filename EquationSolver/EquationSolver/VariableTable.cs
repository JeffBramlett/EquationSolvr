using EquationSolver.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquationSolver
{
    public class VariableTable
    {
        private int _columns;
        private List<Variable[]> _rows;

        private List<string> _rowNames;
        private List<string> _columnNames;

        public int Count
        {
            get
            {
                return Rows.Count;
            }
        }

        public List<string> ColumnNames
        {
            get
            {
                _columnNames = _columnNames ?? new List<string>();
                return _columnNames;
            }
        }

        public List<string> RowNames
        {
            get
            {
                _rowNames = _rowNames ?? new List<string>();
                return _rowNames;
            }
        }
       
        private List<Variable[]> Rows
        {
            get
            {
                if(_rows == null)
                {
                    _rows = new List<Variable[]>();
                }

                return _rows;
            }
        }

        public string Name { get; set; }

        public VariableTable(string name, int columns)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentOutOfRangeException(nameof(name));
            if (columns <= 0)
                throw new ArgumentOutOfRangeException(nameof(columns));

            _columns = columns;
        }

        public Variable[] this[int row]
        {
            get { return Rows[row]; }
        }

        /// <summary>
        /// Enumerate the variables of this table
        /// </summary>
        /// <returns>the list of variables</returns>
        public IEnumerable<Variable> EnumerateVariables()
        {
            List<Variable> list = new List<Variable>();
            foreach(Variable[] row in Rows)
            {
                list.AddRange(row);
            }

            return list;
        }

        public Variable this[int row, int column]
        {
            get
            {
                return Rows[row][column];
            }
            set
            {
                Rows[row][column].SetValue(value);
            }
        }

        public Variable this[int row, string column]
        {
            get
            {
                int ndx = ColumnNames.IndexOf(column);
                return Rows[row][ndx];
            }
            set
            {
                int ndx = ColumnNames.IndexOf(column);
                Rows[row][ndx].SetValue(value);
            }
        }

        public Variable[] RowAt(int index)
        {
            return Rows[index];
        }

        public Variable GetVariableAt(int row, int column)
        {
            return Rows[row][column];
        }

        public Variable GetVariableAt(int row, string column)
        {
            var rowVars = Rows[row];
            var colNdx = ColumnNames.IndexOf(column);
            return rowVars[colNdx];
        }

        public void SetVariableValueAt(int row, int column, object value)
        {
            Rows[row][column].SetValue(value);
        }

        public void SetColumnLabel(int column, string label)
        {
            if (ColumnNames.Contains(label))
                return;

            if(column < ColumnNames.Count)
            {
                ColumnNames[column] = label;
            }
            else
                ColumnNames.Add(label);
        }

        public void SetRowLabel(int row, string label)
        {
            if(row < RowNames.Count)
            {
                RowNames[row] = label;
            }
            else
                RowNames.Add(label);
        }

        public int MakeRow(string rowLabel = "", params object[] columnValues)
        {
            string label = string.IsNullOrEmpty(rowLabel) ? "Row" + Rows.Count : rowLabel;

            var varArray = new Variable[_columns];

            for(var i = 0; i < _columns; i++)
            {
                varArray[i] = new Variable();
                if(columnValues.Length > 0 && i < columnValues.Length)
                {
                    varArray[i].SetValue(columnValues[i]);
                }
            }

            Rows.Add(varArray);

            SetRowLabel(Rows.Count - 1, label);
            
            return Rows.Count - 1;
        }

        public decimal SumOfColumn(int column)
        {
            decimal sum = 0;

            for (var i = 0; i < _columns; i++)
            {
                sum += Rows[i][column].DecimalValue;
            }

            return sum;
        }

        public decimal SumOfRow(int row)
        {
            decimal sum = 0;

            var rowArray = Rows[row];
            for(var i = 0; i < _columns; i++)
            {
                sum += rowArray[i].DecimalValue;
            }

            return sum;
        }

        public decimal AverageOfColumn(int column)
        {
            decimal sum = 0;

            for (var i = 0; i < Rows.Count; i++)
            {
                sum += Rows[i][column].DecimalValue;
            }

            return sum / (Rows.Count);
        }

        public decimal AverageOfRow(int row)
        {
            decimal sum = 0;

            var rowArray = Rows[row];
            for (var i = 0; i < _columns; i++)
            {
                sum += rowArray[i].DecimalValue;
            }

            return sum / _columns;
        }
    }
}
