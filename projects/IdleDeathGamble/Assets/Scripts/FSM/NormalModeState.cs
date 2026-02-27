using UnityEngine;

namespace IdleDeathGamble.FSM
{
    public class NormalModeState : IState
    {
        private PachinkoManager manager;

        public NormalModeState(PachinkoManager manager)
        {
            this.manager = manager;
        }

        public void Enter()
        {
            Debug.Log("Entered Normal Mode: 1/239 Base Probability.");
        }

        public void Execute()
        {
            // Awaiting player attacks to trigger Preview Roll
        }

        public void Exit()
        {
            Debug.Log("Exiting Normal Mode.");
        }
    }
}
