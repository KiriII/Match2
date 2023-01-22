namespace Match3Core
{
    public class Cell
    {
        public CellsColor color { get; set; }

        public Cell(CellsColor cellsColor)
        {
            this.color = cellsColor;
        }

        public Cell(Cell cell)
        {
            color = cell.color;
        }

        public override bool Equals(object obj)
        {
            Cell cell = obj as Cell;
            if (cell == null)
            {
                return false;
            }
            return color == cell.color;
        }
    }
}