using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New Ability Cast State", menuName = "ShadedGames/StateAbilityData/AbilityState")]
    public class AbilityCastState : StateData
    {
        // Start is called before the first frame update

        public float whenToCast = 0;
        public string abilityName = "ability";
        public string animationParameter = "isCasting";
        int cast = 0;
        AbilityManager abilityCaster;
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            AbilityManager[] abilities = animator.GetComponents<AbilityManager>();
            foreach (AbilityManager a in abilities)
            {
                if (a.nameOfAbility.Equals(abilityName) && a.useAnimation)
                {
                    abilityCaster = a;
                    return;
                }
            }
        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            cast = 0;
            animator.ResetTrigger(animationParameter);
            animator.SetBool(animationParameter, false);
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= whenToCast && cast == 0)
            {
                abilityCaster.AnimationTriggerAbility();
                cast++;
            }
        }
    }
}
