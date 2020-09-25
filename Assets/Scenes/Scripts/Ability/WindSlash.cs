﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    [CreateAssetMenu(menuName = "Abilities/Player/Wind Slash")]
    public class WindSlash : ProjectileAbility,ILaunchProjectile
    {
        GameObject parent;
        PlayerStatus playerStatus;

        public override void Initialize(GameObject obj)
        {
            parent = obj;
            playerStatus = parent.GetComponent<PlayerStatus>();


        }

        public override void CastAbility()
        {
            AttackTriggerManager.instance.attackAnimationController.TriggerAnimationBool(animatorParameter);
           // SpawnProjectile();
        }
        public void SpawnProjectile()
        {

            if (playerStatus.playerIngameStats.CurrentResource >= depletionValue)
            {
                playerStatus.DepleteResourceBar(depletionValue);
                GameObject currentProjectile = Instantiate(projectile, parent.transform.position, parent.transform.rotation);
                WindSlashScript windSlashScript = currentProjectile.GetComponent<WindSlashScript>();
                windSlashScript.InitializeStats(damage, maxTarget, speed, duration, parent.transform.localScale.x);
                windSlashScript.Launch();
            }

        }

        public override void ChargeAbility()
        {
            //  throw new System.NotImplementedException();
        }

        public override void CastOnPressAbility()
        {
            //  throw new System.NotImplementedException();
        }
    }
}
