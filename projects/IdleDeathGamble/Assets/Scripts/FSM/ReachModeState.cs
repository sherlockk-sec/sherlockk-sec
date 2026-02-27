using UnityEngine;

namespace IdleDeathGamble.FSM
{
    public class ReachModeState : IState
    {
        private PachinkoManager manager;

        public ReachModeState(PachinkoManager manager)
        {
            this.manager = manager;
        }

        public void Enter()
        {
            Debug.Log("Entered Reach Mode: 2 slots match. Buffering cinematic timeline.");
            manager.reachManager.PlayReachCinematic(manager.lastResult, OnCinematicComplete);
        }

        private void OnCinematicComplete(bool success)
        {
            manager.lastResult.IsJackpot = success;
            manager.ProcessSpinResult(manager.lastResult);
        }

        public void Execute()
        {
        }

        public void Exit()
        {
        }
    }
}
