using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FSM
{
    public abstract class StateMachine<EState, OwnerType> : MonoBehaviour where EState : Enum where OwnerType : MonoBehaviour
    {
        protected List<Transition> transitions;
        protected OwnerType owner;

        protected Dictionary<EState, IState> states;
        public Dictionary<EState, IState> State => states;

        public OwnerType Owner => owner;

        protected EState eCurrentState;
        protected IState currentState;

        protected bool initialized = false;

        public abstract void Initialize(OwnerType owner, IStateFactory<EState, OwnerType> factory);
        public abstract void MakeTransitions();

        public EState GetKeyState(IState state)
        {
            foreach(var pair in states)
            {
                if (pair.Value == state) return pair.Key;
            }

            throw new InvalidDataException();
        }

        private void Update()
        {
            Debug.Log("FSM Update");
            if (initialized == false) return;
            CheckTransitions();
            currentState?.UpdateState();
        }
        private void FixedUpdate()
        {
            if (initialized == false) return;
            currentState?.FixedUpdateState();
        }

        private void CheckTransitions()
        {
            foreach (var transition in transitions)
            {
                if (transition.From != currentState) continue;
                if (transition.CheckCondition())
                {
                    ChangeState(transition.To);
                }
            }
        }

        public void AddTransition(IState from, IState to, Func<bool> condition)
        {
            if (transitions == null) transitions = new();
            transitions.Add(new Transition(from, to, condition));
        }

        protected void ChangeState(IState newState)
        {
            if(currentState == newState) return;

            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }

        protected abstract void SetDefaultState();
    }
}
