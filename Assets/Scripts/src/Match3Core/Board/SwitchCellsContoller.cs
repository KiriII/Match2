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

        public void SwitchCells((int x, int y) switchCell1, (int x, int y) switchCell2)
        {
            var cell1 = new Cell(_switchCellsModel.GetCell(switchCell1.x, switchCell1.y));
            var cell2 = new Cell(_switchCellsModel.GetCell(switchCell2.x, switchCell2.y));

            _switchCellsModel.SetCell(cell1, switchCell2.x, switchCell2.y);
            _switchCellsModel.SetCell(cell2, switchCell1.x, switchCell1.y);
        }
    }
}