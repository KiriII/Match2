using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Match3Core.Board;
using Match3Core.RandomGenerate;
using Match3Core.gui;
using Match3Core.Box;

namespace Match3Core
{
    public class Match3GameCore
    {
        private BoardModel _boardModel;
        private BoxModel _boxModel;
        private FallenOffSlotsModel _fallenOffSlotsModel;

        private SwitchCellsContoller _switchCellController;
        private SwitchSlotsController _switchSlotsController;
        private FallingController _fallingController;
        private SlotUnblockController _slotUnblockController;
        private CellsDestroyController _cellsDestroyController;
        private CheckTriplesController _checkTriplesController;
        private TurnController _turnController;
        private SlotManipulateController _slotManipulateController;
        private AbilityController _abilityController;

        private BoxController _boxController;

        private WinContoller _winContoller;

        private ScoreHolder _scoreHolder;

        private event Action _updateView;
        private event Action<Slot[,]> _boardScreen;
        private event Action _createNextLevel;

        public Match3GameCore(Slot[,] slots,
            Condition condition,
            Action CurrentLevelComplete,
            Action CreateNextLevel)  
        {
            _updateView += UpdateView;
            _createNextLevel = CreateNextLevel;

            _boardModel = new BoardModel(slots);
            _boxModel = new BoxModel(_boardModel.GetRows());
            _fallenOffSlotsModel = new FallenOffSlotsModel();

            _scoreHolder = new ScoreHolder();

            _switchCellController = new SwitchCellsContoller(_boardModel, _updateView);
            _switchSlotsController = new SwitchSlotsController(_boardModel, _updateView);

            _slotUnblockController = new SlotUnblockController(_boardModel);
            _cellsDestroyController = new CellsDestroyController(_switchCellController, _boardModel, _updateView);
            _fallingController = new FallingSidewayController(_boardModel, _switchCellController, _updateView);
            _checkTriplesController = new CheckTriplesController(_boardModel);
            _turnController = new TurnController(_boardModel, _switchCellController);
            _slotManipulateController = new SlotManipulateController(_switchSlotsController, _boardModel, _fallenOffSlotsModel, _updateView);
            _abilityController = new AbilityController(_boardModel, _slotManipulateController, _cellsDestroyController);

            _boxController = new BoxController(_boxModel, _boardModel);

            _winContoller = new WinContoller(condition, _boardModel, _boxModel);

            _winContoller.EnableLevelWonListener(CurrentLevelComplete);
            _winContoller.EnableLevelWonListener(LevelComplete);

            _turnController.EnableCorrectTurnDoneListener(_checkTriplesController.FindTriples);
            _checkTriplesController.EnableTriplesCountListener(_scoreHolder.AddScore);
            _checkTriplesController.EnableTriplesFindedListener(_slotUnblockController.UnblockSlots);
            _slotUnblockController.EnableCellUnblockedListener(_cellsDestroyController.SimpleDestroyCells);
            _slotUnblockController.EnableCountUnblockedListener(_winContoller.SlotUnblocked);
            _boxController.EnableSpecialCellDroppedDownListener(_cellsDestroyController.ForceDestroyCells);
            _boxController.EnableSpecialCellDroppedDownListener(_winContoller.SpecialCellDestroyed);
            _cellsDestroyController.EnableCellsFindedListener(_winContoller.CellsDestroyed);
            _cellsDestroyController.EnableCellDestroyedListener(_fallingController.FallingWithDeadCells);
            _slotManipulateController.EnableSloMovedListener(_fallingController.FallingWithDeadCells);
            _slotManipulateController.EnableSloMovedListener(_winContoller.ShapeChanged);
            _slotManipulateController.EnableSloMovedSimpleListener(_boxController.FindBoxDropdown);
            _fallingController.EnableCellsFellListener(_boxController.FindBoxDropdown);
            _fallingController.EnableCellsFellListener(_checkTriplesController.FindTriples);
            _switchSlotsController.EnableSlotSwitchedListener(_checkTriplesController.FindTriples);

            // Create Cells On Board
            CreateCellsOnBoard.CreateBoard(_boardModel.GetSlots(), _switchCellController);
        }

        public void LevelComplete()
        {
            _createNextLevel?.Invoke();
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

        public bool CreateSlot(Coordinate coordinate, Slot slot)
        {
            return _slotManipulateController.CreateSlot(coordinate, slot);
        }

        public bool CreateShockerSlot(Coordinate coordinate, Slot slot)
        {
            return _abilityController.CreateRowShocker(coordinate, slot);
        }

        public bool CreateBombSlot(Coordinate coordinate, Slot slot)
        {
            return _abilityController.CreateBomb(coordinate, slot);
        }

        #region GETTERS
        public int GetScore()
        {
            return _scoreHolder.GetScore();
        }

        public Slot[,] GetSlots()
        {
            return _boardModel.GetSlots();
        }

        public int GetRows()
        {
            return _boardModel.GetRows();
        }

        public int GetCollumns()
        {
            return _boardModel.GetCollumns();
        }

        public Slot GetFallenSlot()
        {
            return _fallenOffSlotsModel.GetFallenSlot();
        }
        #endregion

        #region ACTIONS
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
        #endregion
    }
}
