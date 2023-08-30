using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Match3Core;
using Match3Core.Board;
using Match3Core.DestroyCells;
using Match3Core.Falling;
using Match3Core.Triples;
using Match3Core.MakeTurn;
using Match3Core.RandomGenerate;
using Match3Core.SlotManipulate;
using Match3Core.gui;

namespace Match3Core
{
    public class Match3GameCore
    {
        private BoardModel _boardModel;

        private SwitchCellsContoller _switchCellController;
        private SwitchSlotsController _switchSlotsController;
        private FallingController _fallingController;
        private SlotUnblockController _slotUnblockController;
        private CellsDestroyController _cellsDestroyController;
        private CheckTriplesController _checkTriplesController;
        private TurnController _turnController;
        private SlotManipulateController _slotManipulateController;

        private event Action _updateView;
        private event Action<Slot[,]> _boardScreen;

        public Match3GameCore(Slot[,] slots)  
        {
            _updateView += UpdateView;

            _boardModel = new BoardModel(slots);

            _switchCellController = new SwitchCellsContoller(_boardModel);
            _switchSlotsController = new SwitchSlotsController(_boardModel);

            _slotUnblockController = new SlotUnblockController(_boardModel);
            _cellsDestroyController = new CellsDestroyController(_switchCellController, _boardModel, _updateView);
            _fallingController = new FallingSidewayController(_boardModel, _switchCellController, _updateView);
            _checkTriplesController = new CheckTriplesController(_boardModel);
            _turnController = new TurnController(_boardModel, _switchCellController);
            _slotManipulateController = new SlotManipulateController(_switchSlotsController, _boardModel, _updateView);

            _turnController.EnableCorrectTurnDoneListener(_checkTriplesController.FindTriples);
            _checkTriplesController.EnableTriplesFindedListener(_slotUnblockController.UnblockSlots);
            _slotUnblockController.EnableCellUnblockedListener(_cellsDestroyController.DestroyCells);
            _cellsDestroyController.EnableCellDestroyedListener(_fallingController.FallingWithDeadCells);
            _slotManipulateController.EnableSloMovedListener(_fallingController.FallingWithDeadCells);
            _fallingController.EnableCellsFellListener(_checkTriplesController.FindTriples);

            // Create Cells On Board
            CreateCellsOnBoard.CreateBoard(_boardModel.GetSlots(), _switchCellController);
        }

        public BoardModel GetBoardModel()
        {
            return _boardModel;
        }

        public void UpdateView()
        {
            _boardScreen?.Invoke(_boardModel.GetBoardCopy());
        }

        public void TurnMade(Turn turn)
        {
            _turnController.MakeTurn(turn);
        }

        public bool DestroyCell(Coordinate coordinate)
        {
            return _cellsDestroyController.DestroyCells(coordinate);
        }

        public bool DestroySlot(Coordinate coordinate)
        {
            return _slotManipulateController.DestroySlot(coordinate);
        }
        public bool CreateSlot(Coordinate coordinate)
        {
            return _slotManipulateController.CreateSlot(coordinate);
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

        public void EnableBoardScreenListener(Action<Slot[,]> methodInLitener)
        {
            _boardScreen += methodInLitener;
        }

        public void DesableBoardScreenListener(Action<Slot[,]> methodInLitener)
        {
            _boardScreen -= methodInLitener;
        }

        public void DEADCELLS(List<Coordinate> tripledCells)
        {
            _cellsDestroyController.DestroyCells(tripledCells);
        }

        public void FindTriples()
        {
            _checkTriplesController.FindTriples();
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
