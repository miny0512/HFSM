using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public interface IStateFactory<EState, OwnerType> where EState : Enum where OwnerType : MonoBehaviour
    {
        Dictionary<EState, IState> MakeStates(StateMachine<EState, OwnerType> sm, OwnerType owner);
    }
}
