namespace FSM
{
    public abstract class TestStateBase : IState
    {
        public TestStateBase(FSM.StateMachine<TestState, Test> sm, Test owner) 
        {
            stateMachine = sm;
           this. owner = owner;
        }

        protected FSM.StateMachine<TestState, Test> stateMachine;
        protected Test owner;
        public abstract void Enter();
        public abstract void Exit();
        public abstract void FixedUpdateState();
        public abstract void UpdateState();
    }
}
