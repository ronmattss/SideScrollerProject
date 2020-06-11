using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public class PlayerManager : MonoBehaviour
    {
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


    }
}
