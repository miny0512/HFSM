using System.Collections.Generic;

namespace FSM
{
    public class TestStateFactory : IStateFactory<TestState, Test>
    {
        public Dictionary<TestState, IState> MakeStates(StateMachine<TestState, Test> sm, Test owner)
        {
            Dictionary<TestState, IState> dic = new();
            dic.Add(TestState.TEST1, new Test1State(sm, owner));
            dic.Add(TestState.TEST2, new Test2State(sm, owner));

            return dic;
        }
    }
}
