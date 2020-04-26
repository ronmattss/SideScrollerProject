using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
     [CreateAssetMenu(fileName = "New Enemy Movement State", menuName = "ShadedGames/StateAbilityData/Enemy/Attack")]
    public class EnemyAttack : StateData
    {
        [SerializeField]
      //  private Transform attackPoint;
    



        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
//            animator.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            animator.gameObject.GetComponent<Status>().RegisterAttack(animator);
        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool("playerInRange",false);
          //  animator.SetBool("isMoving",false);
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
