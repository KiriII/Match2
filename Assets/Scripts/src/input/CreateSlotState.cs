using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;
using Match3Core.MakeTurn;

namespace Match3Input
{
    public class CreateSlotState : InputState
    {
        private Match3GameCore _gameCore;
        private InputController _inputController;

        private event Action<GameObject> _slotCreated;

        public CreateSlotState(InputController inputController, Match3GameCore gameCore, Action<GameObject> slotCreated)
        {
            state = States.CreateSlot;
            _inputController = inputController;
            _gameCore = gameCore;
            _slotCreated = slotCreated;
        }

        public override void MakeTurn(Turn turn)
        {
            if (turn.fallenSlot == null) throw new Exception("NullReferenceException. Try to create null slot");
            if (turn.vector == Vector2.zero)
            {
                if (_gameCore.CreateSlot(turn.coordinate, turn.fallenSlot))
                {
                    _slotCreated?.Invoke(turn.fallenSlotObject);
                    Debug.Log($"Create Slot {turn}");
                    _inputController.ChangeState(0);
                }
            }
        }
    }
}