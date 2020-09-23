using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Handles Abilities
namespace SideScrollerProject
{
    public class AbilityManager : MonoBehaviour
    {
        public string nameOfAbility;
        public KeyCode abilityButton;
        public Image darkMask;
        public TMP_Text coolDownText;
        public Animator animator;
        public bool useAnimation;
        [SerializeField] private Ability ability;
        public Image abilityImage;
        private float coolDownDuration;
        private float nextReadyTime;
        private float coolDownTimeLeft;
        private PlayerStatus playerStatus;
        public float chargeTimer = 0;

        void Start()
        {
            playerStatus = GetComponent<PlayerStatus>();
            if (ability == null) return;
            Initialize(ability);
            useAnimation = ability.hasAnimation;
            nameOfAbility = ability.aName;
            animator = GetComponent<Animator>();
        }
        public Ability GetAbility()
        {
            return ability;
        }
        void Update()
        {
            //NOTE: Ability Manager is just for connecting buttons Ui to the skilss
            // logic should be done on the skill itself
            bool coolDownComplete = (Time.time > nextReadyTime);
            if (coolDownComplete)
            {
                AbilityReady(); // UI Display
                                //                Debug.Log("Ability Ready");
                if (Input.GetKeyDown(abilityButton))
                {
                    //   ButtonTriggered();
                }
                //Charge Casting  Logic
                if (Input.GetKey(abilityButton))
                { //  Debug.Log("HEALLIINGGGGG");
                    //Hold cast logic
                    // post hold logic
                    if (ability.holdToCast)
                        ability.ChargeAbility();
                }
                // 
                if (Input.GetKeyUp(abilityButton))
                {
                    if (ability.quickCast || ability.castAfterCharge)
                        ButtonTriggered();
                }
            }
            else
            {
                Debug.Log("Cooldown");
                CoolDown();
            }
        }

        public void Initialize(Ability selectedAbility)
        {
            ability = selectedAbility;
            ability.thisAbilityManager = this;
            Debug.Log("New Ability Acquired: " + ability.aName);
            useAnimation = selectedAbility.hasAnimation;
            if (useAnimation)
                animator = GetComponent<Animator>();
            abilityImage.sprite = ability.aSprite;
            darkMask.sprite = ability.aSprite;
            coolDownDuration = ability.aBaseCooldown;
            ability.Initialize(this.gameObject);
            AbilityReady();
        }

        private void AbilityReady()
        {
            coolDownText.enabled = false;
            darkMask.enabled = false;
        }

        void CoolDown()
        {
            coolDownTimeLeft -= Time.deltaTime;
            float roundedCd = Mathf.Round(coolDownTimeLeft);
            coolDownText.text = roundedCd.ToString();
            darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
        }

        // Triggers Ability
        void ButtonTriggered()
        {

            //abilitySource.Play();
            //abilitySource.clip = ability.aSound;
            if (playerStatus.playerIngameStats.CurrentResource >= ability.depletionValue)
            {
                nextReadyTime = coolDownDuration + Time.time;
                coolDownTimeLeft = coolDownDuration;
                darkMask.enabled = true;
                coolDownText.enabled = true;
                if (useAnimation)
                {
                    AttackTriggerManager.instance.attackAnimationController.TriggerAnimationBool(ability.animatorParameter);
                    // animator.SetBool(ability.animatorParameter, true);
                    //Play animation then send the animationTriggerAbility to the State Data
                }
                else
                {
                    ability.CastAbility();
                }
            }


        }
        public void AnimationTriggerAbility()
        {
            ability.CastAbility();
        }
    }
}
