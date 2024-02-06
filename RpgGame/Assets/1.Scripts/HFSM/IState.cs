using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HFSM
{
    public abstract class Rootstate<ERootState, ESubState, OwnerType> : IState
        where ERootState : Enum
        where ESubState : Enum
        where OwnerType : MonoBehaviour
    {
        public Rootstate(HierarchicalStateMachine<ERootState, ESubState, OwnerType> stateMachine, OwnerType owner)
        {
            StateMachine= stateMachine;
            this.owner = owner;
        }

        protected   OwnerType owner;

        public HierarchicalStateMachine<ERootState, ESubState, OwnerType> StateMachine { get; protected set; }
        public  Substate<ERootState, ESubState, OwnerType> CurrentSubstate { get; protected set; }


        protected List<Transition> transitions = new List<Transition>();

        public void AddTransition(Substate<ERootState, ESubState, OwnerType> from, Substate<ERootState, ESubState, OwnerType> to, Func<bool> condition)
        {
            var transition = new Transition(from, to, condition);
            transitions.Add(transition);
        }

        protected void ChangeState(Substate<ERootState, ESubState, OwnerType> newSubState)
        {
            if(CurrentSubstate == newSubState) return;
            CurrentSubstate?.Exit();
            CurrentSubstate = newSubState;
            CurrentSubstate?.Enter();
        }

        // 서브스테이트 트랜지션 체크
        public void CheckTransition()
        {
            foreach (var transition in transitions)
            {
                if (transition.From == CurrentSubstate && transition.CheckCondition())
                {
                    ChangeState(transition.To as Substate<ERootState, ESubState, OwnerType>);
                    return;
                }
            }
        }

        public abstract void SetSubState();
        //public abstract void SetDefaultSubState();
        public abstract void Enter();
        public abstract void Exit();
        public abstract void InitializeTransition();

        public virtual void UpdateState()
        {
            CheckTransition();
            CurrentSubstate?.UpdateState();
        }

        public virtual void FixedUpdateState()
        {
            CurrentSubstate?.FixedUpdateState();
        }
    }

    public abstract class Substate<ERootState, ESubState, OwnerType> : IState
        where ERootState : Enum
        where ESubState : Enum
        where OwnerType : MonoBehaviour
    {
        public Substate(HierarchicalStateMachine<ERootState, ESubState, OwnerType> stateMachine, OwnerType owner)
        {
            StateMachine = stateMachine;
            this.owner = owner;
        }

        public HierarchicalStateMachine<ERootState, ESubState, OwnerType> StateMachine { get; protected set; }
        protected OwnerType owner;

        public abstract void Enter();
        public abstract void Exit();
        public abstract void FixedUpdateState();
        public abstract void UpdateState();
    }
}
