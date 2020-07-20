using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public class PlayerManager : MonoBehaviour
    {
        public AbilityManager[] playerAbilityManager;

        public static PlayerManager instance { get; private set; }
        public GameObject player;
        void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(this.gameObject);
                return;
            }
            player = GameObject.Find("Player");
            playerAbilityManager = GetPlayerAbilities();

        }

        public void EnableDisableSkill(int abilityIndex) // should also be changeable via Ability name
        {
            AbilityManager[] ability = GetPlayerAbilities();
            for (int i = 0; i < ability.Length; i++)
            {
                if (abilityIndex == i)
                {
                    if (ability[i].enabled)
                    {
                        ability[i].abilityImage.gameObject.SetActive(false);
                        ability[i].enabled = false;
                    }
                    else
                    {
                        ability[i].abilityImage.gameObject.SetActive(true);
                        ability[i].enabled = true;
                    }
                }

            }
        }

        public PlayerStatus GetPlayerStatus()
        {
            return player.GetComponent<PlayerStatus>();
        }

        public Rigidbody2D GetPlayerRigidbody2D()
        {
            return player.GetComponent<Rigidbody2D>();
        }

        public Actions GetPlayerAction()
        {
            return player.GetComponent<Actions>();
        }
        public AbilityManager GetPlayerAbility(string abilityName)
        {
            foreach (AbilityManager ability in player.GetComponents<AbilityManager>())
            {
                if (ability.nameOfAbility == abilityName)
                    return ability;

            }
            return null;

        }
        public AbilityManager[] GetPlayerAbilities()
        {
            return player.GetComponents<AbilityManager>();
        }

    }
}
