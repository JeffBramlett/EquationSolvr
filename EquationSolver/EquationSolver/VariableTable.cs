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
        
 
        public int Count
        {
            get
            {
                return Rows.Count;
            }
        }
       
        private List<Variable[]> Rows
        {
            get
            {
                if(_rows == null)
                {
                    _rows = new List<Variable[]>();

                    var varArray = new Variable[_columns + 1];
                    for (var i = 1; i <= _columns; i++)
                    {
                        varArray[i] = new Variable();
                        varArray[i].SetValue("column" + i);
                    }
                    Rows.Add(varArray);

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

        public Variable GetVariableAt(int row, int column)
        {
            return Rows[row][column];
        }

        public void SetVariableValueAt(int row, int column, object value)
        {
            Rows[row][column].SetValue(value);
        }

        public void SetColumnLabel(int column, string label)
        {
            Rows[0][column].SetValue(label);
        }

        public void SetRowLabel(int row, string label)
        {
            Rows[row][0].SetValue(label);
        }

        public int MakeRow(string rowLabel = "")
        {
            string label = string.IsNullOrEmpty(rowLabel) ? "Row" + Rows.Count : rowLabel;

            var varArray = new Variable[_columns + 1];

            varArray[0] = new Variable();
            varArray[0].SetValue(label);

            for(var i = 1; i <= _columns; i++)
            {
                varArray[i] = new Variable();
            }
            Rows.Add(varArray);
            return Rows.Count - 1;
        }

        public decimal SumOfColumn(int column)
        {
            decimal sum = 0;

            for (var i = 1; i <= _columns; i++)
            {
                sum += Rows[i][column].DecimalValue;
            }

            return sum;
        }

        public decimal SumOfRow(int row)
        {
            decimal sum = 0;

            var rowArray = Rows[row];
            for(var i = 1; i <= _columns; i++)
            {
                sum += rowArray[i].DecimalValue;
            }

            return sum;
        }

        public decimal AverageOfColumn(int column)
        {
            decimal sum = 0;

            for (var i = 1; i < Rows.Count; i++)
            {
                sum += Rows[i][column].DecimalValue;
            }

            return sum / (Rows.Count - 1);
        }

        public decimal AverageOfRow(int row)
        {
            decimal sum = 0;

            var rowArray = Rows[row];
            for (var i = 1; i <= _columns; i++)
            {
                sum += rowArray[i].DecimalValue;
            }

            return sum / _columns;
        }
    }
}
