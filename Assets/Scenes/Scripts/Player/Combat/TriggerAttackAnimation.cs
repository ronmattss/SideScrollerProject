using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScrollerProject
{
    public interface IAttackAnimationTrigger
    {
        void TriggerAnimationBool(string animationParameter); // Offensive abilities and attacks should Trigger one common trigger and specific trigger
        void TriggerAnimationInt(string animationParameter);
    }

    public class TriggerAttackAnimation : IAttackAnimationTrigger
    {
        //Call this function to all offensive skills
        // so that it only needs one function to run animation parameters
        Animator animator;

        public TriggerAttackAnimation(Animator animator)
        {
            this.animator = animator;
        }
        public void TriggerAnimationBool(string animationParameter)
        {
            // offensive Abilities will trigger the IsInCombo Parameter
            animator.SetBool(animationParameter, true);
            animator.SetBool("IsInCombo", true);
        }


        public void TriggerAnimationInt(string animationParameter)
        {
            
        }

        /// Consider The Current Melee Attack System
        // Attack Logic is in the State Behaviour
        // Button Z activates animation trigger for basic attack
        // Offensive Abilities should also trigger an attack boolean 
        // abstract the attack animation triggers
        // Attack Manager? if this then create an input Manager
        // Enum of Attack Light, Heavy, Ability



        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}