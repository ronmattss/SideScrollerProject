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
        public GameObject cameraTarget;
        public GameObject fallTarget;
        public float playerSpriteDirection { get { return player.transform.localScale.x; } }
        public Transform PlayerTransform => player.transform;
        public PlayerStatus PlayerStatus => player.GetComponent<PlayerStatus>();
        public Movement PlayerMovement => player.GetComponent<Movement>();

        public Rigidbody2D PlayerRigidbody2D => player.GetComponent<Rigidbody2D>();
        public MaskController thirdEye => player.GetComponent<MaskController>();
        public bool thirdEyeState => thirdEye.thirdEyeOn;
        public Animator PlayerAnimator => player.GetComponent<Animator>();
        public AbilityManager[] PlayerAbilities => player.GetComponents<AbilityManager>();
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

            playerAbilityManager = PlayerAbilities;

        }

        public void EnableDisableSkill(int abilityIndex) // should also be changeable via Ability name
        {
            AbilityManager[] ability = PlayerAbilities;
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


        public Actions PlayerAction => player.GetComponent<Actions>();
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
