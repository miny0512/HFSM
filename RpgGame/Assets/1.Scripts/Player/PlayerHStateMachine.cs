using Assets._1.Scripts.Player;
using HFSM;
using PlayerState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.UI.GridLayoutGroup;

public class PlayerHStateMachine : HierarchicalStateMachine<Player.RootState, Player.SubState, Player>
{
    public PlayerHStateMachine(Player owner)
    {
        _factory = new PlayerStateFactory(this, owner);
        _factory.InitializeRootState();
        SetOwner(owner);
        InitializeTransition();
    }

    private HierarchicalStateFactory<Player.RootState, Player.SubState, Player> _factory;
    public override HierarchicalStateFactory<Player.RootState, Player.SubState, Player> Factory { get { return _factory; } }

    public override void SetDefaultState()
    {
        ChangeState(Factory.GetRootState(Player.RootState.Grounded));
    }

    protected override void InitializeTransition()
    {
        AddTransition(Factory.GetRootState(Player.RootState.Grounded), Factory.GetRootState(Player.RootState.Jump), ()=> !owner.ReuseableData.IsActiveAbility && owner.ReuseableData.IsJumpPressed);
        AddTransition(Factory.GetRootState(Player.RootState.Jump), Factory.GetRootState(Player.RootState.Grounded), () => owner.Controller.CharacterController.isGrounded);
        // 수정
        AddTransition(Factory.GetRootState(Player.RootState.Grounded), Factory.GetRootState(Player.RootState.Air), ()=> !owner.ReuseableData.IsActiveAbility && owner.Controller.CharacterController.isGrounded == false && owner.ReuseableData.ExpectedVelocityY < 0f);
        AddTransition(Factory.GetRootState(Player.RootState.Air), Factory.GetRootState(Player.RootState.Grounded), () => owner.Controller.GroundChecker.IsGrounded);
        
        AddTransition(Factory.GetRootState(Player.RootState.Grounded), Factory.GetRootState(Player.RootState.Ability), ()=> owner.Controller.AbilityHolder.IsActiveAbility);
        AddTransition(Factory.GetRootState(Player.RootState.Air), Factory.GetRootState(Player.RootState.Ability), ()=> owner.Controller.AbilityHolder.IsActiveAbility);
        AddTransition(Factory.GetRootState(Player.RootState.Jump), Factory.GetRootState(Player.RootState.Ability), ()=> owner.Controller.AbilityHolder.IsActiveAbility);

        AddTransition(Factory.GetRootState(Player.RootState.Ability), Factory.GetRootState(Player.RootState.Grounded), ()=> !owner.Controller.AbilityHolder.IsActiveAbility && owner.Controller.CharacterController.isGrounded);
        AddTransition(Factory.GetRootState(Player.RootState.Ability), Factory.GetRootState(Player.RootState.Air), ()=> !owner.Controller.AbilityHolder.IsActiveAbility && owner.Controller.CharacterController.isGrounded == false);
    }
}
