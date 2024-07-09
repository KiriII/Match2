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
        public CreateSlotState(InputController controller, InputStateMachine stateMachine) : base(controller, stateMachine)
        {
        }

        public override void MakeTurn(Turn turn)
        {
            Debug.Log("CREATE SLOT");
            if (turn.fallenSlot == null) throw new Exception("NullReferenceException. Try to create null slot");
            if (turn.vector == Vector2.zero)
            {
                if (controller.CreateSlot(turn.coordinate, turn.fallenSlot, turn.fallenSlotObject))
                {
                    Debug.Log($"Create Slot {turn}");
                    stateMachine.ChangeState(controller.turnState);
                }
            }
        }
    }
}