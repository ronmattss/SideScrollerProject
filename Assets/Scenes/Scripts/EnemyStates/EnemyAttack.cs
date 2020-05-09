using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New Enemy Movement State", menuName = "ShadedGames/StateAbilityData/Enemy/Attack")]
    public class EnemyAttack : StateData
    {
        [SerializeField]
        private bool isRange = false;




        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            //            animator.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            isRange = animator.gameObject.GetComponent<Status>().isRange;
            if (!isRange)
                animator.gameObject.GetComponent<Status>().RegisterAttack(animator);
            else
                animator.gameObject.GetComponent<Status>().RangeAttack();
        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool("playerInRange", false);
            if (isRange)
                animator.GetComponent<Status>().laser.gameObject.SetActive(false);
            //  animator.SetBool("isMoving",false);
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
