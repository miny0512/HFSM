using HFSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayerState
{
    public class IdleState : Substate<Player.RootState, Player.SubState, Player>
    {
        public IdleState(HierarchicalStateMachine<Player.RootState, Player.SubState, Player> sm, Player owner) : base(sm, owner)
        {
        }

        public override void Enter()
        {
            owner.ReuseableData.CurrentMoveSpeed = 0;
            owner.ReuseableData.ExpectedVelocityX= 0;
            owner.ReuseableData.ExpectedVelocityZ= 0;
        }

        public override void Exit()
        {
        }

        public override void UpdateState()
        {
        }

        public override void FixedUpdateState()
        {
        }

    }
}
