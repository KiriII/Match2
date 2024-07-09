using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Input
{
    public class InputStateMachine
    {
        public InputState currentState { get; set; }

        public void Initialize(InputState StartingState)
        {
            currentState = StartingState;
            Debug.Log(currentState);
        }

        public void ChangeState(InputState newState)
        {
            currentState = newState;
            Debug.Log(currentState);
        }
    }
}