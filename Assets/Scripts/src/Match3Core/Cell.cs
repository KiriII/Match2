using System;
using UnityEngine;

namespace Match3Core
{
    public class Cell : IFormattable
    {
        public CellsColor Color { get; set; }
        public string Id { get; }

        public Cell(CellsColor cellsColor = CellsColor.Empty, string id = "")
        {
            Color = cellsColor;
            Id = id;
        }

        public Cell(Cell cell)
        {
            if (cell is not null)
            {
                Color = cell.Color;
                Id = cell.Id;
            }
        }

        public static bool Equals(Cell c1, Cell c2)
        {
            if (c1 == null && c2 == null
                || c1.Color == CellsColor.Empty || c2.Color == CellsColor.Empty
                || c1.Color == CellsColor.Special || c2.Color == CellsColor.Special)
            {
                return false;
            }
            return c1.Color == c2.Color;
        }

        public bool EqualsTo(object obj)
        {
            Cell cell = obj as Cell;
            return Equals(this, cell);
        }

        public override string ToString()
        {
            return $"{Color}";
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }
    }
}