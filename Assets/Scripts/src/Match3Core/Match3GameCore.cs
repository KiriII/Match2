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
using Match3Core.Box;

namespace Match3Core
{
    public class Match3GameCore
    {
        private BoardModel _boardModel;
        private BoxModel _boxModel;

        private SwitchCellsContoller _switchCellController;
        private SwitchSlotsController _switchSlotsController;
        private FallingController _fallingController;
        private SlotUnblockController _slotUnblockController;
        private CellsDestroyController _cellsDestroyController;
        private CheckTriplesController _checkTriplesController;
        private TurnController _turnController;
        private SlotManipulateController _slotManipulateController;

        private BoxController _boxController;

        private ScoreHolder _scoreHolder;

        private event Action _updateView;
        private event Action<Slot[,]> _boardScreen;

        public Match3GameCore(Slot[,] slots, Dictionary<Coordinate, string> boxes)  
        {
            _updateView += UpdateView;

            _boardModel = new BoardModel(slots);
            _boxModel = new BoxModel(boxes);

            _scoreHolder = new ScoreHolder();

            _switchCellController = new SwitchCellsContoller(_boardModel);
            _switchSlotsController = new SwitchSlotsController(_boardModel);

            _slotUnblockController = new SlotUnblockController(_boardModel);
            _cellsDestroyController = new CellsDestroyController(_switchCellController, _boardModel, _updateView);
            _fallingController = new FallingSidewayController(_boardModel, _switchCellController, _updateView);
            _checkTriplesController = new CheckTriplesController(_boardModel);
            _turnController = new TurnController(_boardModel, _switchCellController);
            _slotManipulateController = new SlotManipulateController(_switchSlotsController, _boardModel, _updateView);

            _boxController = new BoxController(_boxModel, _boardModel);

            _turnController.EnableCorrectTurnDoneListener(_checkTriplesController.FindTriples);
            _checkTriplesController.EnableTriplesCountListener(_scoreHolder.AddScore);
            _checkTriplesController.EnableTriplesFindedListener(_slotUnblockController.UnblockSlots);
            _slotUnblockController.EnableCellUnblockedListener(_cellsDestroyController.SimpleDestroyCells);
            _boxController.EnableSpecialCellDroppedDownListener(_cellsDestroyController.ForceDestroyCells);
            _cellsDestroyController.EnableCellDestroyedListener(_fallingController.FallingWithDeadCells);
            _slotManipulateController.EnableSloMovedListener(_fallingController.FallingWithDeadCells);
            _fallingController.EnableCellsFellListener(_boxController.FindBoxDropdown);
            _fallingController.EnableCellsFellListener(_checkTriplesController.FindTriples);   

            // Create Cells On Board
            CreateCellsOnBoard.CreateBoard(_boardModel.GetSlots(), _switchCellController);
        }

        public BoardModel GetBoardModel()
        {
            return _boardModel;
        }

        public BoxModel GetBoxModel()
        {
            return _boxModel;
        }

        public void UpdateView()
        {
            _boardScreen?.Invoke(_boardModel.GetBoardCopy());
        }

        public void TurnMade(Turn turn)
        {
            _scoreHolder.ResetRowAfterTurn();
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
    }
}
