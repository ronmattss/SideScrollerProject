﻿using System.Collections;
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
        private bool hit = true;
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
                        strike.SpawnHit();
                        // animator.GetComponent<PlayerStatus>().playerRenderer.material = animator.GetComponent<PlayerStatus>().emissionMaterial;
                    }
                    return;
                }
            }
        }

        public override void OnExit(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            strike.FreezeEnemyPositions(1);
            strike.StrikeEnemies();
            animator.SetBool(animationParameter, false);
            animator.SetBool("isSlashing", false);
            animator.gameObject.transform.GetChild(6).gameObject.SetActive(false);
        }

        public override void UpdateAbility(BaseState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= whenToCast)
            {

                AudioManager.instance.Play("OmniHitsSFX");
                hit = false;
            }
            if (strike.splash.activeSelf)
                strike.FreezeEnemyPositions();


        }
    }

}