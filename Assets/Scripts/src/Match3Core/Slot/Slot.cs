namespace Match3Core
{
    public class Slot
    {
        public Cell Cell { get => _cell; set => _cell = value; }

        public bool CanHoldCell { get => _canHoldCell; set => _canHoldCell = value; }
        public bool CanPassCell { get => _canPassCell; set => _canPassCell = value; }

        private Cell _cell;
        private bool _canHoldCell;
        private bool _canPassCell;

        public Slot(bool canHoldSell, bool canPassCell = true, Cell cell = null)
        {
            _canHoldCell = canHoldSell;
            _canPassCell = canPassCell;
            _cell = cell;
        }

    }
}