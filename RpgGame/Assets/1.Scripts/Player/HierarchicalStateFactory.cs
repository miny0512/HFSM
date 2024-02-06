using HFSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._1.Scripts.Player
{
    public abstract class HierarchicalStateFactory<RootState, SubState, OwnerType>
        where RootState : Enum
        where SubState : Enum
        where OwnerType : MonoBehaviour
    {
        public HierarchicalStateFactory(HierarchicalStateMachine<RootState, SubState, OwnerType> sm, OwnerType owner)
        {
            InitializeStateFactory(sm, owner);
         }

        protected Dictionary<RootState, Rootstate<RootState, SubState, OwnerType>> rootStates;
        protected Dictionary<SubState, Substate<RootState, SubState, OwnerType>> subStates;

        private void InitializeStateFactory(HierarchicalStateMachine<RootState, SubState, OwnerType> sm, OwnerType owner)
        {
            InitializerSubStateFactory(sm, owner);
            InitializerRootStateFactory(sm, owner);
        }

        // 트랜지션, 기본 상태 설정
        public void InitializeRootState()
        {
            foreach (var i in rootStates.Values)
            {
                i.InitializeTransition();
                // i.SetDefaultSubState();
            }
        }
        public abstract void InitializerRootStateFactory(HierarchicalStateMachine<RootState, SubState, OwnerType> sm, OwnerType owner);
        public abstract void InitializerSubStateFactory(HierarchicalStateMachine<RootState, SubState, OwnerType> sm, OwnerType owner);


        public Rootstate<RootState, SubState, OwnerType> GetRootState(RootState rootState) => rootStates[rootState];
        public Substate<RootState, SubState, OwnerType> GetSubState(SubState subState) => subStates[subState];
    }
}
