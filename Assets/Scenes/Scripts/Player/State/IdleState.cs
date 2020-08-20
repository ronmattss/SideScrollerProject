using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New State", menuName = "ShadedGames/StateAbilityData/Idle")]
    public class IdleState : StateData
    {
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            PlayerManager.instance.GetPlayerAction().canRun = true;
            animator.SetInteger(AnimatorParams.AttackCounter.ToString(), 0);
            animator.GetComponentInParent<InputManager>().attackCounter = 0;
            animator.SetBool(AnimatorParams.Attacking.ToString(), false);
            PlayerManager.instance.GetPlayerAction().attackCounter = 0;

        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
