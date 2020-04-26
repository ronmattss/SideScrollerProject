using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProject
{// Heal Ability, heals the player

    [CreateAssetMenu(menuName = "Abilities/Player/Heal")]
    public class Heal : Ability
    {
        public int healValue = 10;
        public PlayerStatus playerStatus;
        public override void Initialize(GameObject obj)
        {
            playerStatus = obj.GetComponent<PlayerStatus>();

        }

        public override void TriggerAbility()
        {
            HealPlayer();
        }

        private void HealPlayer()
        {
            if (playerStatus.currentResource >= depletionValue)
            {
                playerStatus.DepleteResourceBar(depletionValue);

                if (healValue + playerStatus.currentHealth <= playerStatus.maxHealth)
                {
                    playerStatus.ChangeHealthBar(healValue);
                }
                else
                {
                    int reducedHealing = Mathf.Abs(playerStatus.maxHealth - playerStatus.currentHealth);

                    playerStatus.ChangeHealthBar(reducedHealing);
                }

            }

        }


    }
}
