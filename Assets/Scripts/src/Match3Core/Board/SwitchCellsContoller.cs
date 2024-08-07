using Match3Core;
using System;

namespace Match3Core.Board
{
    public class SwitchCellsContoller
    {
        private IBoardSwitchCellsModel _switchCellsModel;

        private event Action _CellSwitched;
        private event Action _updateView;

        public SwitchCellsContoller(IBoardSwitchCellsModel switchCellsModel, Action updateView)
        {
            _switchCellsModel = switchCellsModel;
            _updateView = updateView;
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

            //OnViewUpdate();
            OnCellSwitched();
        }

        public void SwitchWithNewCell(Coordinate CellCoordinate, Cell newCell)
        {
            if (!CanSwitchCell(CellCoordinate)) return;

            _switchCellsModel.SetCell(CellCoordinate, newCell);

            //OnViewUpdate();
            OnCellSwitched();
        }

        private bool CanSwitchCell(Coordinate switchCell)
        {
            var canHoldSell = _switchCellsModel.GetCanHoldCell(switchCell);
            var isBlocked = _switchCellsModel.GetBlocked(switchCell);

            return canHoldSell && !isBlocked;
        }

        private void OnViewUpdate()
        {
            _updateView?.Invoke();
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