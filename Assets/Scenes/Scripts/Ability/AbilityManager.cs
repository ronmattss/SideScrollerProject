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
        public KeyCode abilityButton;
        public Image darkMask;
        public TMP_Text coolDownText;
        [SerializeField] private Ability ability;
        [SerializeField] private Image abilityImage;
        private float coolDownDuration;
        private float nextReadyTime;
        private float coolDownTimeLeft;

        void Start()
        {
            Initialize(ability);
        }
        void Update()
        {
            bool coolDownComplete = (Time.time > nextReadyTime);
            if (coolDownComplete)
            {
                 AbilityReady(); // UI Display
                Debug.Log("Ability Ready");
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
            nextReadyTime = coolDownDuration + Time.time;
            coolDownTimeLeft = coolDownDuration;
            darkMask.enabled = true;
            coolDownText.enabled = true;

            //abilitySource.Play();
            //abilitySource.clip = ability.aSound;
            ability.TriggerAbility();

        }
    }
}
