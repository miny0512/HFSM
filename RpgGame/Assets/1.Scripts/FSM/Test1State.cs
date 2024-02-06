using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FSM
{
    public class Test1State : TestStateBase
    {
        public Test1State(FSM.StateMachine<TestState, Test> sm, Test owner) : base(sm, owner) { }

        public override void Enter()
        {
        }


        public override void Exit()
        {
        }


        public override void FixedUpdateState()
        {
        }


        public override void UpdateState()
        {
            Debug.Log("Test1 Update");
        }
    }
    public class Test2State : TestStateBase
    {
        public Test2State(FSM.StateMachine<TestState, Test> sm, Test owner) : base(sm, owner) { }

        public override void Enter()
        {
        }


        public override void Exit()
        {
        }


        public override void FixedUpdateState()
        {
        }


        public override void UpdateState()
        {
            Debug.Log("Test2 Update");
        }
    }
}