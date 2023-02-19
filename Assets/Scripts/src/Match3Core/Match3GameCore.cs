using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Match3Core;
using Match3Core.Board;
using Match3Core.Falling;
using Match3Core.DestroyCells;

namespace Match3Core
{
    public class Match3GameCore
    {
        private BoardModel _boardModel;

        private SwitchCellsContoller _switchCellController;
        private FallingController _fallingController;
        private CellsDestroyController _cellsDestroyController;

        public Match3GameCore(Slot[,] slots)  
        {
            _boardModel = new BoardModel(slots);

            _switchCellController = new SwitchCellsContoller(_boardModel);
            _cellsDestroyController = new CellsDestroyController(_switchCellController);
            _fallingController = new FallingController(_boardModel, _switchCellController);

            _cellsDestroyController.EnableDestroyedCellDestroyedListener(_fallingController.FallingWithDeadCells);
        }

        public BoardModel GetBoardModel()
        {
            return _boardModel;
        }

        // ------ ACTIONS ------
        public void EnableCellSwitchedListener(Action methodInLitener)
        {
            _switchCellController.EnableCellSwitchedListener(methodInLitener);
        }

        public void DesableCellSwitchedListener(Action methodInLitener)
        {
            _switchCellController.DesableCellSwitchedListener(methodInLitener);
        }
        /*
        public void Turn((int x1, int y1) switchCell1, (int x2, int y2) switchCell2)
        {
            _switchCellsController.Turn(switchCell1, switchCell2);
        }

        public void FindDestroyedCellsInCollumns(List<(int x, int y)> destroyedCells)
        {
            var rowCount = _match3Model.GetRowsCount();
            _findDestroyedCellsController.FindDestroyedCellsInCollumns(destroyedCells, rowCount);
        }

        // ----- debug -----
        public void PrintCurrnetBoard()
        {
            Debug.Log(_match3Model.Print());
        }

        public List<(int, int)> Check()
        {
            Debug.Log(String.Join(", ", _checkSameOnBoardController.CheckSame()));
            return _checkSameOnBoardController.CheckSame();
        }
        */
    }
}
