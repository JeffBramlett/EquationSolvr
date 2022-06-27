using System;
using System.Collections.Generic;
using EquationSolver.Dto;

namespace EquationSolver
{
	public class VariableTableRow
	{
		private List<Variable> _cells;

		private List<Variable> Cells
        {
            get
            {
				_cells = _cells ?? new List<Variable>();
				return _cells;
            }
            set
            {
				_cells = value;
            }
        }

		public int Count
        {
            get
            {
                return Cells.Count;
            }
        }

		public VariableTableRow()
		{
		}
	}
}

