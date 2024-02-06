using Assets._1.Scripts.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HFSM
{
    public abstract class StateMachine<EState, OwnerType>  where EState :Enum
        where OwnerType : MonoBehaviour
    {
        public StateMachine(OwnerType owner)
        {
            SetOwner(owner);
            InitializeStates();
        }

        protected IState currentState;
        protected EState eCurrentState;
        public IState CurrentState => currentState;

        protected OwnerType owner;
        public OwnerType Owner => owner;

        protected Dictionary<EState, IState> states;
        public Dictionary<EState, IState> States => states;

        public void UpdateState()
        {
            currentState?.UpdateState();
        }

        public void FixedUpdateState()
        {
            currentState?.FixedUpdateState();
        }

        public virtual void ChangeState(EState state)
        {
            if (eCurrentState.Equals(state)) return;
            currentState?.Exit();
            currentState = states[state];
            eCurrentState = state;
            currentState?.Enter();
        }

        public abstract void InitializeStates();

        public void SetOwner(OwnerType owner) { this.owner = owner; }
    }

    public abstract class HierarchicalStateMachine<ERootState, ESubState, OwnerType>
        where ERootState : Enum
        where ESubState : Enum
        where OwnerType : MonoBehaviour
    {
        public abstract void SetDefaultState();

        private bool isStopped = false;

        public void StopMachine() => isStopped = true;
        public void PlayerMachine() => isStopped = false;

        protected List<Transition> transitions = new List<Transition>();
        protected Rootstate<ERootState, ESubState, OwnerType> currentState;

        public Rootstate<ERootState, ESubState, OwnerType> CurrentState => currentState;

        public abstract HierarchicalStateFactory<ERootState, ESubState, OwnerType> Factory { get; }

        protected OwnerType owner;
        public OwnerType Owner => owner;

        public void AddTransition(Rootstate<ERootState, ESubState, OwnerType> from, Rootstate<ERootState, ESubState, OwnerType> to, Func<bool> condition)
        {
            transitions.Add(new Transition(from, to, condition));
        }

        public virtual void UpdateState()
        {
            if(isStopped) return;
            CheckTransition();

            currentState?.UpdateState();
        }

        public virtual void FixedUpdateState()
        {
            if(isStopped) return;

            currentState?.FixedUpdateState();
        }

        public virtual void CheckTransition()
        {
            foreach(var transition in transitions)
            {
                if (transition.From == currentState && transition.CheckCondition())
                {
                    ChangeState(transition.To as Rootstate<ERootState, ESubState, OwnerType>);
                    return;
                }
            }
        }

        protected void ChangeState(Rootstate<ERootState, ESubState, OwnerType> newState)
        {
            if (isStopped) return;

            if(currentState == newState) return;

            currentState?.Exit();
            currentState?.CurrentSubstate?.Exit();
            currentState = newState;
            currentState?.Enter();
            currentState?.CurrentSubstate?.Enter();
        }

        protected abstract void InitializeTransition();

        public void SetOwner(OwnerType owner) { this.owner = owner; }
    }
}
