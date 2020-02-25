using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New State", menuName = "ShadedGames/StateAbilityData/Attack")]
    public class AttackState : StateData
    {
        // Start is called before the first frame update
        private int attackCounter = 0;




        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            attackCounter = animator.GetInteger(AnimatorParams.AttackCounter.ToString());
            animator.SetBool(AnimatorParams.Attacking.ToString(), false);

        }
        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

            // if (animator.GetInteger(AnimatorParams.AttackCounter.ToString()) > 1)
         //   {
               // animator.SetBool(AnimatorParams.Attacking.ToString(), true);
            //    animator.SetBool(AnimatorParams.Attacking.ToString(), false);
             //   animator.SetInteger(AnimatorParams.AttackCounter.ToString(), 0);
                //  animator.GetComponentInParent<InputManager>().attackCounter = 0;
            //    InputManager.instance.attackCounter = 0;
           // }
            //  animator.SetInteger(AnimatorParams.AttackCounter.ToString(), 2);



        }


    }
}

