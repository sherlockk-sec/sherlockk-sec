using UnityEngine;

namespace IdleDeathGamble.FSM
{
    public class PreviewRollState : IState
    {
        private PachinkoManager manager;

        public PreviewRollState(PachinkoManager manager)
        {
            this.manager = manager;
        }

        public void Enter()
        {
            Debug.Log("Entered Preview Roll: Slots are spinning rapidly.");
            
            // Assume we just do an immediate math calculation instead of real time spin for this prototype
            Pachinko.SpinResult result = manager.prngManager.RollSpin();
            
            // Process the result to transition to next state
            manager.ProcessSpinResult(result);
        }

        public void Execute()
        {
        }

        public void Exit()
        {
        }
    }
}
