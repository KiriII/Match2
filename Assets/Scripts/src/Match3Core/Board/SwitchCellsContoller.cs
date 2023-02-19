using Match3Core;
using System;

namespace Match3Core.Board
{
    public class SwitchCellsContoller
    {
        private ISwitchCellsModel _switchCellsModel;

        private event Action _CellSwitched;

        public SwitchCellsContoller(ISwitchCellsModel switchCellsModel)
        {
            _switchCellsModel = switchCellsModel;
        }

        public void SwitchCells(Coordinate switchCoordinate1, Coordinate switchCoordinate2)
        { 
            if (!CanSwitchCell(switchCoordinate1) || !CanSwitchCell(switchCoordinate2))
            {
                return;
            }
            var cell1 = new Cell(_switchCellsModel.GetCell(switchCoordinate1));
            var cell2 = new Cell(_switchCellsModel.GetCell(switchCoordinate2));

            _switchCellsModel.SetCell(switchCoordinate2, cell1);
            _switchCellsModel.SetCell(switchCoordinate1, cell2);

            OnCellSwitched();
        }

        public void SwitchWithNewCell(Coordinate CellCoordinate, Cell newCell)
        {
            if (!CanSwitchCell(CellCoordinate)) return;

            _switchCellsModel.SetCell(CellCoordinate, newCell);

            OnCellSwitched();
        }

        private bool CanSwitchCell(Coordinate switchCell)
        {
            var canHoldSell1 = _switchCellsModel.GetCanHoldCell(switchCell);

            return canHoldSell1;
        }

        private void OnCellSwitched()
        {
            _CellSwitched?.Invoke();
        }

        public void EnableCellSwitchedListener(Action methodInLitener)
        {
            _CellSwitched += methodInLitener;
        }

        public void DesableCellSwitchedListener(Action methodInLitener)
        {
            _CellSwitched -= methodInLitener;
        }
    }
}