using Match3Core;

namespace Match3Core.Board
{
    public class SwitchCellsContoller
    {
        private ISwitchCellsModel _switchCellsModel;

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
        }

        public void SwitchWithNewCell(Coordinate CellCoordinate, Cell newCell)
        {
            if (!CanSwitchCell(CellCoordinate)) return;

            _switchCellsModel.SetCell(CellCoordinate, newCell);
        }

        private bool CanSwitchCell(Coordinate switchCell)
        {
            var canHoldSell1 = _switchCellsModel.GetCanHoldCell(switchCell);

            return canHoldSell1;
        }
    }
}