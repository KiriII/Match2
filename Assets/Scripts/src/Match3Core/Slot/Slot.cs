namespace Match3Core
{
    public class Slot
    {
        public Cell Cell { get => _cell; set => _cell = value; }

        public bool CanHoldCell { get => _canHoldCell; }

        private Cell _cell;
        private bool _canHoldCell;

        public Slot(bool canHoldSell, Cell cell = null)
        {
            _canHoldCell = canHoldSell;
            _cell = cell;
        }

    }
}