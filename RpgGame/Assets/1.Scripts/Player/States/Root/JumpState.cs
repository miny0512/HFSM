using HFSM;
using Unity.VisualScripting;
using UnityEngine;

namespace PlayerState
{
    public class JumpState : Rootstate<Player.RootState, Player.SubState, Player>
    {
        public JumpState(HierarchicalStateMachine<Player.RootState, Player.SubState, Player> sm, Player owner) : base(sm, owner) 
        {
         
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
            Debug.Log("JumpEnter");
            SetSubState();
     //       owner.ReuseableData.FreezePlayerRoation = true;

            owner.Controller.Animator.CrossFade(owner.Data.Hash.ANIM_JUMP, 0.05f);
            owner.Controller.Animator.SetFloat(owner.Data.Hash.Parameter_DirX, 0);
            owner.Controller.Animator.SetFloat(owner.Data.Hash.Parameter_DirY, 0);
            owner.Controller.Animator.SetFloat(owner.Data.Hash.Parameter_RunGuage, 0);

            //if(owner.ReuseableData.IsMovementPressed && !owner.ReuseableData.IsRunPressed)
            //{
            //    owner.ReuseableData.CurrentMovementVector = owner.Controller.ModelTransform.forward * owner.ReuseableData.CurrentMovementInput.y;
            //    owner.ReuseableData.CurrentMovementVector = owner.Data.Jump.MinMoveSpeed * owner.ReuseableData.CurrentMovementVector;
            //    owner.ReuseableData.ExpectedVelocityX = owner.ReuseableData.CurrentMovementVector.x;
            //    owner.ReuseableData.ExpectedVelocityZ = owner.ReuseableData.CurrentMovementVector.z;
            //}
            //else if(owner.ReuseableData.IsMovementPressed && owner.ReuseableData.IsRunPressed)
            //{
            //    owner.ReuseableData.CurrentMovementVector = owner.Controller.ModelTransform.forward * owner.ReuseableData.CurrentMovementInput.y;
            //    owner.ReuseableData.CurrentMovementVector = owner.Data.Jump.MaxMoveSpeed * owner.ReuseableData.CurrentMovementVector;
            //    owner.ReuseableData.ExpectedVelocityX = owner.ReuseableData.CurrentMovementVector.x;
            //    owner.ReuseableData.ExpectedVelocityZ = owner.ReuseableData.CurrentMovementVector.z;
            //}

            float velocity = Mathf.Sqrt(owner.Data.Jump.JumpHeight * -2f * owner.Data.Gravity.Air);
            owner.Controller.HandleJump(velocity);
            owner.ReuseableData.CurrentGravity = owner.Data.Gravity.Air;
            owner.Controller.GroundChecker.DelayOnOff();
        }

        public override void Exit()
        {
          //  owner.ReuseableData.FreezePlayerRoation = false;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if(owner.ReuseableData.ExpectedVelocityY < 0f)
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
