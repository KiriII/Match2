using UnityEngine;
using Match3Core.MakeTurn;

namespace Match3Input
{
    public class DestroySlotState : InputState
    {
        public DestroySlotState(InputController controller, InputStateMachine stateMachine) : base(controller, stateMachine)
        {
        }

        public override void MakeTurn(Turn turn)
        {
            if (turn.fallenSlot != null)
            {
                ChangeToCreateSlotState(turn);
            }
            else if (turn.vector == Vector2.zero)
            {
                if (controller.DestroySlot(turn.coordinate, turn.position))
                {
                    stateMachine.ChangeState(controller.turnState);
                }
            }
        }
    }
}