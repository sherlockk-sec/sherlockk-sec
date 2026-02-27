using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IdleDeathGamble.Cinematics;

namespace IdleDeathGamble.FSM
{
    public class PachinkoManager : MonoBehaviour
    {
        public StateMachine stateMachine { get; private set; }

        public NormalModeState normalState { get; private set; }
        public PreviewRollState previewState { get; private set; }
        public ReachModeState reachState { get; private set; }
        public JackpotState jackpotState { get; private set; }

        [Header("Managers")]
        public Pachinko.PRNGManager prngManager;
        public ReachCinematicManager reachManager;
        public Pachinko.JackpotManager jackpotManager;
        public Pachinko.PostJackpotModifiers postJackpotModifiers;

        public Pachinko.SpinResult lastResult;

        private void Awake()
        {
            stateMachine = gameObject.AddComponent<StateMachine>();
            
            normalState = new NormalModeState(this);
            previewState = new PreviewRollState(this);
            reachState = new ReachModeState(this);
            jackpotState = new JackpotState(this);
        }

        private void Start()
        {
            stateMachine.ChangeState(normalState);
        }

        public void TriggerAttackRoll()
        {
            if (stateMachine.currentState == normalState)
            {
                stateMachine.ChangeState(previewState);
            }
        }
        
        public void ProcessSpinResult(Pachinko.SpinResult result)
        {
            lastResult = result;
            if (result.IsReach && !result.IsJackpot)
            {
                stateMachine.ChangeState(reachState);
            }
            else if (result.IsJackpot)
            {
                stateMachine.ChangeState(jackpotState);
            }
            else
            {
                postJackpotModifiers.OnSpinComplete();
                stateMachine.ChangeState(normalState);
            }
        }
    }
}
