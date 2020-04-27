﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SideScrollerProject
{// Heal Ability, heals the player

    [CreateAssetMenu(menuName = "Abilities/Player/Heal")]
    public class Heal : Ability
    {
        public int healValue = 10;
        public PlayerStatus playerStatus;
        public ParticleSystem healthParticles;
        public override void Initialize(GameObject obj)
        {
            playerStatus = obj.GetComponent<PlayerStatus>();
            healthParticles.transform.position = obj.transform.position;

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
                ParticleSystem particles = Instantiate(healthParticles, playerStatus.transform.position, Quaternion.identity);
                particles.GetComponent<TrackPlayer>().player = playerStatus.gameObject;
                healthParticles.Play();
                if (healValue + playerStatus.currentHealth <= playerStatus.maxHealth)
                {
                    playerStatus.ChangeHealthBar(healValue);
                }
                else
                {
                    int reducedHealing = Mathf.Abs(playerStatus.maxHealth - playerStatus.currentHealth);

                    playerStatus.ChangeHealthBar(reducedHealing);
                }
                Destroy(particles, 2);

            }


        }


    }
}