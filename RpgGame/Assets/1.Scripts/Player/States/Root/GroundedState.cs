using HFSM;
using UnityEngine;

namespace PlayerState
{
    public class GroundedState : Rootstate<Player.RootState, Player.SubState, Player>
    {
        public GroundedState(HierarchicalStateMachine<Player.RootState, Player.SubState, Player> sm, Player owner) : base(sm, owner) { }

        public override void InitializeTransition()
        {
            // Idle -> ...
            AddTransition(StateMachine.Factory.GetSubState(Player.SubState.Idle), StateMachine.Factory.GetSubState(Player.SubState.Walk), () => owner.ReuseableData.IsMovementPressed == true);
            AddTransition(StateMachine.Factory.GetSubState(Player.SubState.Idle), StateMachine.Factory.GetSubState(Player.SubState.Run), () => owner.ReuseableData.IsMovementPressed == true && owner.ReuseableData.IsRunPressed);

            // Walk -> ...
            AddTransition(StateMachine.Factory.GetSubState(Player.SubState.Walk), StateMachine.Factory.GetSubState(Player.SubState.Idle), () => owner.ReuseableData.IsMovementPressed == false);
            AddTransition(StateMachine.Factory.GetSubState(Player.SubState.Walk), StateMachine.Factory.GetSubState(Player.SubState.Run), () => owner.ReuseableData.IsRunPressed == true);

            // Run-> ...
            AddTransition(StateMachine.Factory.GetSubState(Player.SubState.Run), StateMachine.Factory.GetSubState(Player.SubState.Walk), () => owner.ReuseableData.IsRunPressed == false && owner.ReuseableData.IsMovementPressed);
            AddTransition(StateMachine.Factory.GetSubState(Player.SubState.Run), StateMachine.Factory.GetSubState(Player.SubState.Idle), () => owner.ReuseableData.IsMovementPressed == false);
        }
        public override void Enter()
        {
            owner.Controller.Animator.CrossFade(owner.Data.Hash.ANIM_LOCOMOTION, 0.1f);
            owner.ReuseableData.CurrentGravity = owner.Data.Gravity.Grounded;
            SetSubState();
        }

        public override void Exit()
        {
        }

        public override void UpdateState()
        {
            base.UpdateState();
            owner.Controller.Animator.SetFloat(owner.Data.Hash.Parameter_DirX, owner.ReuseableData.CurrentMovementInput.x, 0.1f, Time.deltaTime);
            owner.Controller.Animator.SetFloat(owner.Data.Hash.Parameter_DirY, owner.ReuseableData.CurrentMovementInput.y, 0.1f, Time.deltaTime);
            owner.Controller.Animator.SetFloat(owner.Data.Hash.Parameter_RunGuage, owner.ReuseableData.IsRunPressed ? 1 : 0, 0.1f, Time.deltaTime);
        }

        //public override void SetDefaultSubState()
        //{
        //    ChangeState(StateMachine.Factory.GetSubState(Player.SubState.Idle));
        //}

        public override void SetSubState()
        {
            if(StateMachine.Owner.ReuseableData.IsMovementPressed == false)
            {
                ChangeState(StateMachine.Factory.GetSubState(Player.SubState.Idle));
            }
            else if(StateMachine.Owner.ReuseableData.IsMovementPressed && !StateMachine.Owner.ReuseableData.IsRunPressed)
            {
                ChangeState(StateMachine.Factory.GetSubState(Player.SubState.Walk));
            }
            else if(StateMachine.Owner.ReuseableData.IsMovementPressed && StateMachine.Owner.ReuseableData.IsRunPressed)
            {
                ChangeState(StateMachine.Factory.GetSubState(Player.SubState.Run));
            }
        }
    }
}
