using UnityEngine;

namespace Match3Core
{
    public class Cell
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

        public override bool Equals(object obj)
        {
            Cell cell = obj as Cell;
            if (cell == null 
                || Color == CellsColor.Empty || cell.Color == CellsColor.Empty 
                || Color == CellsColor.Special || cell.Color == CellsColor.Special)
            {
                return false;
            }
            return Color == cell.Color;
        }

        public override string ToString()
        {
            return $"{Color}";
        }
    }
}