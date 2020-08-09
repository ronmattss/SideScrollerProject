using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SideScrollerProjectFSM;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New Enemy MeleeAttackAnimation State", menuName = "ShadedGames/StateAbilityData/Enemy/MeleeAttackAnimationState")]
    public class MeleeAttackAnimationState : StateData
    {
        public StateUser currentState;
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            currentState = animator.gameObject.GetComponent<StateUser>();
        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            currentState.currentState.Exit();
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }


    }
}