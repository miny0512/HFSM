using HFSM;
using UnityEngine;

namespace PlayerState
{
    public class AirState : Rootstate<Player.RootState, Player.SubState, Player>
    {
        public AirState(HierarchicalStateMachine<Player.RootState, Player.SubState, Player> sm, Player owner) : base(sm, owner) { 
        }

        public override void InitializeTransition()
        {
            AddTransition(StateMachine.Factory.GetSubState(Player.SubState.Idle), StateMachine.Factory.GetSubState(Player.SubState.Walk), () => owner.ReuseableData.IsMovementPressed == true);
            AddTransition(StateMachine.Factory.GetSubState(Player.SubState.Idle), StateMachine.Factory.GetSubState(Player.SubState.Run), () => owner.ReuseableData.IsMovementPressed == true && owner.ReuseableData.IsRunPressed);

            AddTransition(StateMachine.Factory.GetSubState(Player.SubState.Walk), StateMachine.Factory.GetSubState(Player.SubState.Idle), () => owner.ReuseableData.IsMovementPressed == false);
            AddTransition(StateMachine.Factory.GetSubState(Player.SubState.Walk), StateMachine.Factory.GetSubState(Player.SubState.Run), () => owner.ReuseableData.IsRunPressed == true);

            AddTransition(StateMachine.Factory.GetSubState(Player.SubState.Run), StateMachine.Factory.GetSubState(Player.SubState.Walk), () => owner.ReuseableData.IsRunPressed == false && owner.ReuseableData.IsMovementPressed);
            AddTransition(StateMachine.Factory.GetSubState(Player.SubState.Run), StateMachine.Factory.GetSubState(Player.SubState.Idle), () => owner.ReuseableData.IsMovementPressed == false);
        }

        public override void Enter()
        {
            SetSubState();
            owner.Controller.Animator.CrossFade(owner.Data.Hash.ANIM_AIR, 0.1f);
            owner.ReuseableData.IsAir = true;
            owner.ReuseableData.CurrentGravity = owner.Data.Gravity.Air;
            //
        }

        public override void Exit()
        {
            owner.ReuseableData.IsAir = false;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (owner.ReuseableData.ExpectedVelocityY < 0f)
            {
                owner.ReuseableData.CurrentGravity = owner.Data.Gravity.Air * owner.Data.Fall.FallMultiplier;
            }
        }


        public override void SetSubState()
        {
            if (StateMachine.Owner.ReuseableData.IsMovementPressed == false)
            {
                ChangeState(StateMachine.Factory.GetSubState(Player.SubState.Idle));
            }
            else if (StateMachine.Owner.ReuseableData.IsMovementPressed && !StateMachine.Owner.ReuseableData.IsRunPressed)
            {
                ChangeState(StateMachine.Factory.GetSubState(Player.SubState.Walk));
            }
            else if (StateMachine.Owner.ReuseableData.IsMovementPressed && StateMachine.Owner.ReuseableData.IsRunPressed)
            {
                ChangeState(StateMachine.Factory.GetSubState(Player.SubState.Run));
            }
        }
    }
}
