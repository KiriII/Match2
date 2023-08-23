using UnityEngine;

namespace Match3Core
{
    public class Cell
    {
        public CellsColor color { get; set; }

        public Cell(CellsColor cellsColor = CellsColor.Empty)
        {
            this.color = cellsColor;
        }

        public Cell(Cell cell)
        {
            if (cell is not null)
            {
                color = cell.color;
            }
        }

        public override bool Equals(object obj)
        {
            Cell cell = obj as Cell;
            if (cell == null 
                || color == CellsColor.Empty || cell.color == CellsColor.Empty 
                || color == CellsColor.Special || cell.color == CellsColor.Special)
            {
                return false;
            }
            return color == cell.color;
        }
    }
}