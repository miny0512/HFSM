using HFSM;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class AbilityState : Rootstate<Player.RootState, Player.SubState, Player>
{
    public AbilityState(HierarchicalStateMachine<Player.RootState, Player.SubState, Player> sm, Player owner) : base(sm, owner)
    {
    }

    public override void Enter()
    {
        Debug.Log("Ability State ENter");
    }

    public override void Exit()
    {
    }

    public override void UpdateState()
    {
        Debug.Log("AbilityState");
    }

    public override void FixedUpdateState()
    {
    }

    public override void SetSubState()
    {
    }

    public override void InitializeTransition()
    {
    }
}
