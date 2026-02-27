using UnityEngine;

namespace IdleDeathGamble.FSM
{
    public class JackpotState : IState
    {
        private PachinkoManager manager;

        public JackpotState(PachinkoManager manager)
        {
            this.manager = manager;
        }

        public void Enter()
        {
            Debug.Log("Entered Jackpot State! 3 slots match. Music starts playing.");
            manager.jackpotManager.StartJackpot(manager.lastResult);
            // Once jackpot finishes, PachinkoManager will manually transition back to NormalMode 
            // via PostJackpotModifiers.
        }

        public void Execute()
        {
        }

        public void Exit()
        {
            manager.postJackpotModifiers.OnSpinComplete();
        }
    }
}
