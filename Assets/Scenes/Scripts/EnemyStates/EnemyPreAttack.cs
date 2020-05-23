using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New Enemy PreAttack State", menuName = "ShadedGames/StateAbilityData/Enemy/PreAttackBehavior")]
    public class EnemyPreAttack : StateData
    {
        // Start is called before the first frame update
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool("isAttacking", true);
            animator.gameObject.layer = 14;
        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            //animator.SetBool("isAttacking", false);
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
