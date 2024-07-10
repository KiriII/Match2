using System;
using UnityEngine;
using Match3Core;

namespace Match3Input
{
    public class ShockerState : InputState
    {
        public ShockerState(InputController controller, InputStateMachine stateMachine) : base(controller, stateMachine)
        {
        }

        public override void MakeTurn(Turn turn)
        {
            if (turn.fallenSlot == null)
            {
                stateMachine.ChangeState(controller.turnState);
            }        
            else if (turn.vector == Vector2.zero)
            {
                if (controller.CreateShockerSlot(turn.coordinate, turn.fallenSlot, turn.fallenSlotObject))
                {
                    Debug.Log($"Create Shocker Slot {turn}");
                    stateMachine.ChangeState(controller.turnState);
                }
            }
        }
    }
}