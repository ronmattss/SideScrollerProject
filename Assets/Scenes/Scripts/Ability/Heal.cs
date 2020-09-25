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
        public ParticleSystem healthParticles;
        public float currentChargeTime = 0;
        public float castTime = 0;


        // what happens when Charging the ability
        public override void ChargeAbility()
        {

            if (castTime >= castLength && playerStatus.playerIngameStats.CurrentResource >= 20 && playerStatus.playerIngameStats.CurrentHealth !=100)
            {
                Debug.Log("percentage Healing Cast Length" + castLength);
                castTime = 0;
                HealPlayer(20, 30);
            }
            else
            {
                currentChargeTime += Time.deltaTime;
                castTime += Time.deltaTime;
            }
        }

        public override void Initialize(GameObject obj)
        {
            playerStatus = obj.GetComponent<PlayerStatus>();
            healthParticles.transform.position = obj.transform.position;

        }

        public override void CastAbility()
        {
           // HealPlayer(healValue, depletionValue);
            currentChargeTime = castTime = 0;
        }

        private void HealPlayer(int amount, int depletion = 50)
        {

            if (castTime < castLength)
            {
                if (playerStatus.playerIngameStats.CurrentResource >= depletion)
                {
                    playerStatus.DepleteResourceBar(depletion);
                    ParticleSystem particles = Instantiate(healthParticles, playerStatus.transform.position, Quaternion.identity);
                    particles.GetComponent<TrackPlayer>().player = playerStatus.gameObject;
                    healthParticles.Play();
                    if (amount + playerStatus.playerIngameStats.CurrentHealth <= playerStatus.currentStatus.maxHealth)
                    {
                        playerStatus.ChangeHealthBar(amount);
                    }
                    else
                    {
                        int reducedHealing = Mathf.Abs(playerStatus.currentStatus.maxHealth - playerStatus.playerIngameStats.CurrentHealth);

                        playerStatus.ChangeHealthBar(reducedHealing);
                    }
                    Destroy(particles, 2);

                }
            }
            else
            {
                // do some particle effects maybe
            }


        }

        public override void CastOnPressAbility()
        {
          //  throw new System.NotImplementedException();
        }
    }
}
