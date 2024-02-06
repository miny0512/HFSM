using DG.Tweening;
using UnityEngine;

namespace FSM
{
    public enum TestState
    {
        TEST1,
        TEST2,
    }
    public class TestFsm : StateMachine<TestState, Test>
    {
        private void Awake()
        {
            Initialize(owner, new TestStateFactory());
        }

        public override void Initialize(Test owner, IStateFactory<TestState, Test> factory)
        {
            initialized = true;
            this.owner = owner;
            states = factory.MakeStates(this, owner);

            MakeTransitions();

            SetDefaultState();
        }

        public override void MakeTransitions()
        {
            AddTransition(states[TestState.TEST1], states[TestState.TEST2], () => Input.GetKeyDown(KeyCode.A));
            AddTransition(states[TestState.TEST2], states[TestState.TEST1], () => Input.GetKeyDown(KeyCode.B));
        }

        protected override void SetDefaultState()
        {
            ChangeState(states[TestState.TEST1]);
        }
    }
}