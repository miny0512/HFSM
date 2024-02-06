using Assets._1.Scripts.Player;
using HFSM;
using PlayerState;

public class PlayerStateFactory : HierarchicalStateFactory<Player.RootState, Player.SubState, Player>
{
    public PlayerStateFactory(HierarchicalStateMachine<Player.RootState, Player.SubState, Player> sm, Player owner) : base(sm, owner) { }

    public override void InitializerRootStateFactory(HierarchicalStateMachine<Player.RootState, Player.SubState, Player> sm, Player owner)
    { 
        rootStates = new ();
        rootStates.Add(Player.RootState.Air, new AirState(sm, owner));
        rootStates.Add(Player.RootState.Grounded, new GroundedState(sm, owner));
        rootStates.Add(Player.RootState.Jump, new JumpState(sm, owner));
        rootStates.Add(Player.RootState.Ability, new AbilityState(sm, owner));
    }

    public override void InitializerSubStateFactory(HierarchicalStateMachine<Player.RootState, Player.SubState, Player> sm, Player owner)
    {
        subStates = new();
        subStates.Add(Player.SubState.Idle, new IdleState(sm, owner));
        subStates.Add(Player.SubState.Walk, new WalkState(sm, owner));
        subStates.Add(Player.SubState.Run, new RunState(sm, owner));
    }
}
