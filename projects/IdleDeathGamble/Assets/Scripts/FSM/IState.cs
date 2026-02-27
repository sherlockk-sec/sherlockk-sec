using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdleDeathGamble.FSM
{
    public interface IState
    {
        void Enter();
        void Execute();
        void Exit();
    }
}
