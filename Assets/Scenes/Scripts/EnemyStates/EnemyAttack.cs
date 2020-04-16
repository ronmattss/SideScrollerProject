using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
     [CreateAssetMenu(fileName = "New Enemy Movement State", menuName = "ShadedGames/StateAbilityData/Enemy/Attack")]
    public class EnemyAttack : StateData
    {
        [SerializeField]
        private Transform attackPoint;
         float attackRange;
         int attackDamage = 10;
        public LayerMask playerLayer;



        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            attackPoint = animator.gameObject.GetComponent<Transform>().GetChild(0);
            animator.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
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
