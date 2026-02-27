using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdleDeathGamble.FSM
{
    public class StateMachine : MonoBehaviour
    {
        private IState currentState;

        public void ChangeState(IState newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = newState;
            
            if (currentState != null)
            {
                currentState.Enter();
            }
        }

        private void Update()
        {
            if (currentState != null)
            {
                currentState.Execute();
            }
        }
    }
}
