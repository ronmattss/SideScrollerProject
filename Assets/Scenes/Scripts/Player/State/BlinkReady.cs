using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "BlinkReady", menuName = "ShadedGames/StateAbilityData/BlinkReady")]
    public class BlinkReady : StateData
    {
        public float whenToCast = 0;
        public string abilityName = "ability";
        public string animationParameter = "isBlinking";

        AbilityManager abilityCaster;
        AlphaStrike strike;
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            AudioManager.instance.Play("OmniCharge");
            animator.SetBool(animationParameter, false);
            AbilityManager[] abilities = animator.GetComponents<AbilityManager>();
            foreach (AbilityManager a in abilities)
            {
                if (a.nameOfAbility.Equals(abilityName) && a.useAnimation)
                {
                    abilityCaster = a;
                    if (abilityName == "BlinkStrike")
                    {
                        strike = (AlphaStrike)abilityCaster.GetAbility();
                        //  if(strike != null)
                        Debug.Log(strike.damage);
                        strike.OpenPopUp();
                        // animator.GetComponent<PlayerStatus>().playerRenderer.material = animator.GetComponent<PlayerStatus>().emissionMaterial;
                    }
                    return;
                }
            }

            // Open Pop up for 3 frames? 


        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool("isSlashing", true);
            strike.SetSplash();
            strike.BlinkPlayer();

        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {   // if popUp is deactivated
            strike.LockOnEnemies();
            strike.FreezeEnemyPositions(0);

        }
    }
}
