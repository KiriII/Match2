namespace Match3Core
{
    public class Slot
    {
        public Coordinate Coordinate { get => _coordinate; }
        public Cell Cell { get => _cell; set => _cell = value; }

        public bool CanHoldCell { get => _canHoldCell; set => _canHoldCell = value; }
        public bool CanPassCell { get => _canPassCell; set => _canPassCell = value; }

        private Cell _cell;
        private bool _canHoldCell;
        private bool _canPassCell;
        private Coordinate _coordinate;

        public Slot(Coordinate coordinate, bool canHoldCell, bool canPassCell = true, Cell cell = null)
        {
            _canHoldCell = canHoldCell;
            _canPassCell = canPassCell;
            _cell = cell;
            _coordinate = coordinate;
        }

        public Slot(Slot slot)
        {
            _canHoldCell = slot.CanHoldCell;
            _canPassCell = slot.CanPassCell;
            _cell = new Cell(slot.Cell);
            _coordinate = slot.Coordinate;
        }
    }
}