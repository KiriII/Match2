using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;
using Match3Core.MakeTurn;

namespace Match3Input
{
    public class InputController
    {
        public InputStateMachine inputSM;
        public TurnState turnState;
        public DestroyCellState destroyCellState;
        public DestroySlotState destroySlotState;
        public CreateSlotState createSlotState;

        private Match3GameCore _gameCore;

        private event Action<Vector3> _slotDestroyed;
        private event Action<GameObject> _slotCreated;

        public InputController(Match3GameCore gameCore)
        {
            _gameCore = gameCore;

            inputSM = new InputStateMachine();

            turnState = new TurnState(this, inputSM);
            destroyCellState = new DestroyCellState(this, inputSM);
            destroySlotState = new DestroySlotState(this, inputSM);
            createSlotState = new CreateSlotState(this, inputSM);

            inputSM.Initialize(turnState);
        }

        public void AbilityButtonPressed(int buttonId)
        {
            turnState.AbilityButtonPressed(buttonId);
        }

        public void TurnMade(Turn turn)
        {
            Debug.Log(turn);
            inputSM.currentState.MakeTurn(turn);
        }

        public void MakeSwapTurn(Turn turn)
        {
            _gameCore.TurnMade(turn);
        }

        public bool DestroyCell(Coordinate coordinate)
        {
            return _gameCore.DestroyCell(coordinate);
        }

        public bool DestroySlot(Coordinate coordinate, Vector3 position)
        {
            if (_gameCore.DestroySlot(coordinate))
            {
                _slotDestroyed?.Invoke(position);
                return true;
            }
            return false;   
        }

        public bool CreateSlot(Coordinate coordinate, Slot fallenSlot, GameObject fallenSlotObject)
        {
            if (_gameCore.CreateSlot(coordinate, fallenSlot))
            {
                _slotCreated?.Invoke(fallenSlotObject);
                return true;
            }
            return false;
        }

        public void EnableSlotDestroyedListener(Action<Vector3> methodInLitener)
        {
            _slotDestroyed += methodInLitener;
        }

        public void DesableSlotDestroyedListener(Action<Vector3> methodInLitener)
        {
            _slotDestroyed -= methodInLitener;
        }
        public void EnableSlotCreatedListener(Action<GameObject> methodInLitener)
        {
            _slotCreated += methodInLitener;
        }

        public void DesableSlotCreatedListener(Action<GameObject> methodInLitener)
        {
            _slotCreated -= methodInLitener;
        }
    }
}
