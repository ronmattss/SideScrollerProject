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
            Debug.Log("Anim:State:");
        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            Debug.Log("Exit");
            currentState.currentState.Exit();
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            Debug.Log("Anim:State UA:");
           // if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
           //     currentState.currentState.Exit();
            //Debug.Log("Anim:State UA: This Should Exit rn");
        }


    }
}