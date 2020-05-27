using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "BlinkSlash", menuName = "ShadedGames/StateAbilityData/BlinkSlash")]
    public class BlinkSlash : StateData
    {

        public float whenToCast = 0;
        public string abilityName = "ability";
        public string animationParameter = "isBlinking";
        AbilityManager abilityCaster;
        AlphaStrike strike;
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            AbilityManager[] abilities = animator.GetComponents<AbilityManager>();
            foreach (AbilityManager a in abilities)
            {
                if (a.nameOfAbility.Equals(abilityName) && a.useAnimation)
                {
                    abilityCaster = a;
                    if (abilityName == "BlinkStrike")
                    {
                        strike = (AlphaStrike)abilityCaster.GetAbility();

                        // animator.GetComponent<PlayerStatus>().playerRenderer.material = animator.GetComponent<PlayerStatus>().emissionMaterial;
                    }
                    return;
                }
            }
        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            strike.StrikeEnemies();
            animator.SetBool(animationParameter, false);
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            strike.FreezeEnemyPositions();

        }
    }

}