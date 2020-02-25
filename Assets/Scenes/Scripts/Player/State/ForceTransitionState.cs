using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New State", menuName = "ShadedGames/StateAbilityData/ForceTransition")]
    public class ForceTransitionState : StateData
    {
        [Range(0.01f, 1f)]
        public float transitionValue = 0f;
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(AnimatorParams.ForceTransition.ToString(), false);
        }


        // if animation reaches a certain transition value
        // move to next animation
        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= transitionValue)
            {
                animator.SetBool(AnimatorParams.ForceTransition.ToString(), true);
            }
        }


    }
}
