using System;
using UnityEngine;
using Match3Core;

namespace Match3Input
{
    public class CreateSlotState : InputState
    {
        public CreateSlotState(InputController controller, InputStateMachine stateMachine) : base(controller, stateMachine)
        {
        }

        public override void MakeTurn(Turn turn)
        {
            if (turn.fallenSlot == null) throw new Exception("NullReferenceException. Try to create null slot");
            if (turn.vector == Vector2.zero)
            {
                if (controller.CreateSlot(turn.coordinate, turn.fallenSlot, turn.fallenSlotObject))
                {
                    //Debug.Log($"Create Slot {turn}");
                    stateMachine.ChangeState(controller.turnState);
                }
            }
        }
    }
}