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
        [SerializeField] private Image abilityImage;
        private float coolDownDuration;
        private float nextReadyTime;
        private float coolDownTimeLeft;
        private PlayerStatus playerStatus;

        void Start()
        {
            playerStatus = GetComponent<PlayerStatus>();
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

        private void Initialize(Ability selectedAbility)
        {
            ability = selectedAbility;
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
