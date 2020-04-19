using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New Enemy Dead State", menuName = "ShadedGames/StateAbilityData/Enemy/Dead")]
    public class EnemyDead : StateData
    {
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            Destroy(animator.gameObject, 0);
            //  Color alpha = animator.gameObject.GetComponent<SpriteRenderer>().color;
            // alpha.a = 0f;
            //animator.GetComponentInParent<Status>()
            // animator.SetBool("isDead",false);   
            //  animator.gameObject.GetComponent<SpriteRenderer>().color = alpha;


        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
