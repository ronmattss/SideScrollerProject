using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New Enemy PrepAttack State", menuName = "ShadedGames/StateAbilityData/Enemy/AttackPrep")]
    public class EnemyPrepareRangeAttack : StateData
    {
        //Vector2 targetPosition;
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            //targetPosition = animator.GetComponent<Status>().LockTarget();
        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            //animator.GetComponent<Status>().finalTargetPosition = targetPosition;
            //  animator.GetComponent<Status>().position = targetPosition;
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        // Start is called before the first frame update

    }
}