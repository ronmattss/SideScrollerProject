using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New Hurt State", menuName = "ShadedGames/StateAbilityData/Hurt")]
    public class HurtState : StateData
    {

        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool("IsHurt",false);
        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
           animator.ResetTrigger("IsHurt");
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
           
        }
    }
}
