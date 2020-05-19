using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New Post Attack Wizard State", menuName = "ShadedGames/StateAbilityData/Enemy/PostAttackWizard")]
    public class PostAttackWizard : StateData
    {
        [SerializeField]
        private bool isRange = false;




        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (Random.Range(0, 100) > 50)
                animator.GetComponent<WizardBoss>().Teleport();
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
