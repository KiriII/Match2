using Match3Core;

namespace Match3.Board
{
    public class SwitchCellsContoller
    {
        private ISwitchCellsModel _switchCellsModel;

        public SwitchCellsContoller(ISwitchCellsModel switchCellsModel)
        {
            _switchCellsModel = switchCellsModel;
        }

        public void SwitchCells(Coordinate switchCell1, Coordinate switchCell2)
        {
            if (!CanSwitchCell(switchCell1) || !CanSwitchCell(switchCell2))
            {
                return;
            }
            var cell1 = new Cell(_switchCellsModel.GetCell(switchCell1));
            var cell2 = new Cell(_switchCellsModel.GetCell(switchCell2));

            _switchCellsModel.SetCell(cell1, switchCell2);
            _switchCellsModel.SetCell(cell2, switchCell1);
        }

        private bool CanSwitchCell(Coordinate switchCell)
        {
            var canHoldSell1 = _switchCellsModel.GetCanHoldCell(switchCell);

            return canHoldSell1;
        }
    }
}