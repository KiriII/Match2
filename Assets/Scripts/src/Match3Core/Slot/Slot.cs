using System;

namespace Match3Core
{
    public class Slot
    {
        public Coordinate Coordinate { get; }
        public Cell Cell { get; set; }

        public bool IsActive { get; set; }
        public bool CanHoldCell { get; set; }
        public bool CanPassCell { get; set; }
        public bool IsBlocked { get; set; }

        private Cell _cell;
        private Coordinate _coordinate;

        public Slot(Coordinate coordinate, Cell cell = null, bool canHoldCell = true, bool canPassCell = true, bool isBlocked = false, bool isActive = true)
        {
            IsActive = isActive;
            CanHoldCell = canHoldCell;
            CanPassCell = canPassCell;
            IsBlocked = isBlocked;
            Cell = cell;
            Coordinate = coordinate;
        }

        public Slot(Coordinate coordinate, bool canPassCell, bool isActive)
        {
            if (isActive) throw new Exception($"Try to create active Slot not correct in {coordinate}");
            Coordinate = coordinate;
            CanPassCell = canPassCell;
            IsActive = isActive;
            CanHoldCell = false;
            IsBlocked = false;
            Cell = null;
        }

        public Slot(Slot slot)
        {
            IsActive = slot.IsActive;
            CanHoldCell = slot.CanHoldCell;
            CanPassCell = slot.CanPassCell;
            IsBlocked = slot.IsBlocked;
            Cell = new Cell(slot.Cell);
            Coordinate = slot.Coordinate;
        }
    }
}