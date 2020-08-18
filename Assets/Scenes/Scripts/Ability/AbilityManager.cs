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
            bool coolDownComplete = (Time.time > nextReadyTime);
            if (coolDownComplete)
            {
                AbilityReady(); // UI Display
                                //                Debug.Log("Ability Ready");
                if (Input.GetKeyDown(abilityButton))
                {
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

        void ButtonTriggered()
        {

            //abilitySource.Play();
            //abilitySource.clip = ability.aSound;
            if (playerStatus.currentResource >= ability.depletionValue)
            {
                nextReadyTime = coolDownDuration + Time.time;
                coolDownTimeLeft = coolDownDuration;
                darkMask.enabled = true;
                coolDownText.enabled = true;
                if (useAnimation)
                {
                    if (animator == null)
                        animator = this.gameObject.GetComponent<Animator>();
                    animator.SetBool(ability.animatorParameter, true);
                    //Play animation then send the animationTriggerAbility to the State Data
                }
                else
                {
                    ability.TriggerAbility();
                }
            }


        }
        public void AnimationTriggerAbility()
        {
            ability.TriggerAbility();
        }
    }
}
