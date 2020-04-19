using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProject
{

    [CreateAssetMenu(fileName = "New Enemy Hurt State", menuName = "ShadedGames/StateAbilityData/Enemy/Hurt")]
    public class EnemyHurt : StateData
    {
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.gameObject.GetComponent<Status>().Knockback();
            if (animator.gameObject.GetComponent<Status>().currentHealth <= 0)
            {
                animator.SetBool("isDead", true);
                animator.SetBool("isHurt", true);
                animator.SetBool("playerOnSight", false);
                animator.SetBool("isMoving", false);
                animator.SetBool("isIdle", false);
                animator.SetBool("playerInRange", false);
               
                
            }
            else
            {
                animator.SetBool("isHurt", false);
            }


        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {


        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}