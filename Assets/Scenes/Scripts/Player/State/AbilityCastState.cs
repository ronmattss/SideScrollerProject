﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(fileName = "New Ability Cast State", menuName = "ShadedGames/StateAbilityData/AbilityState")]
    public class AbilityCastState : StateData
    {
        // Start is called before the first frame update

        public float whenToCast = 0;
        public string abilityName = "Wind Slash";
        public string animationParameter = "isCasting";
        int cast = 0;
        AbilityManager abilityCaster;
        ILaunchProjectile launchProjectile;
        //TODO: FIX ANIMATIONS
        public override void OnEnter(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            AbilityManager[] abilities = animator.GetComponents<AbilityManager>();
          
           
            foreach (AbilityManager a in abilities)
            {
                Debug.Log("WTFIS THE ABILITY: " + a.nameOfAbility + " Does it use animation: " + a.useAnimation);
                abilityCaster = a;
                 animator.SetBool(animationParameter,false);
                Debug.Log("Does it find the ability? " + a.nameOfAbility);
                if (a.nameOfAbility.Equals(abilityName) && a.useAnimation)
                {
                    if (abilityName == "WindSlash" || abilityName == "Wind Slash")
                    {   launchProjectile = (a.GetAbility() as WindSlash);
                        animator.GetComponent<PlayerStatus>().playerRenderer.material = animator.GetComponent<PlayerStatus>().emissionMaterial;
                    }
                    return;
                }
            }
           // if(stateInfo.normalizedTime >= 0.99)
        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            
            cast = 0;
            if (abilityName == "WindSlash" || abilityName == "Wind Slash")
            {
                animator.GetComponent<PlayerStatus>().playerRenderer.material = animator.GetComponent<PlayerStatus>().baseMaterial;
            }
            
            animator.ResetTrigger(animationParameter);
            animator.SetBool(animationParameter, false);
            animator.SetBool("IsInCombo", false);
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= whenToCast && cast == 0)
            {
                launchProjectile.SpawnProjectile();
                cast++;
            }
        }
    }
}
