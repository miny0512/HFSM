using HFSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayerState
{
    public class WalkState : Substate<Player.RootState, Player.SubState, Player>
    {
        public WalkState(HierarchicalStateMachine<Player.RootState, Player.SubState, Player> sm, Player owner) : base(sm, owner)
        {
        }

        public override void Enter()
        {
            owner.ReuseableData.CurrentMoveSpeed = owner.Data.Movement.MoveSpeed;
        }

        public override void Exit()
        {
        }

        public override void UpdateState()
        {
            owner.ReuseableData.CurrentMovementVector =  owner.Controller.ModelTransform.forward * owner.ReuseableData.CurrentMovementInput.y + owner.Controller.ModelTransform.right * owner.ReuseableData.CurrentMovementInput.x;
            owner.ReuseableData.CurrentMovementVector = owner.ReuseableData.CurrentMoveSpeed * owner.ReuseableData.CurrentMovementVector;
 

            owner.ReuseableData.ExpectedVelocityX = owner.ReuseableData.CurrentMovementVector.x;
            owner.ReuseableData.ExpectedVelocityZ = owner.ReuseableData.CurrentMovementVector.z;
        }

        public override void FixedUpdateState()
        {
        }
    }
}
